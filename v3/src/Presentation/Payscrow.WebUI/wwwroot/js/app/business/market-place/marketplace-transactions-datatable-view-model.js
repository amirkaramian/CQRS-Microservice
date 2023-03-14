var MarketPlaceTransactionsDatatableViewModel = function () {
    var self = this;

    var dataTable = {};

    var formatter = new Intl.NumberFormat('en-US', { style: 'currency', currency: window.currencyCode, currencySign: 'accounting' });

    function init() {
        console.log(window.currencyCode);

        dataTable = $('#datatable').DataTable({
            responsive: true,
            searchDelay: 500,
            //dom: "<'row'<'col-sm-12'tr>>\n\t\t\t<'row'<'col-sm-12 col-md-5'i><'col-sm-12 col-md-7 dataTables_pager'lp>>",
            language: {
                emptyTable: 'No Transaction have been initiated yet for ' + window.currencyCode + '.'
            },
            processing: true,
            serverSide: true,
            ajax: {
                url: '/api/v3/business/marketplace/transactions?currencyCode=' + window.currencyCode,
                type: 'POST',
                data: {
                    // parameters for custom backend script demo
                    columnsDef: [
                        'number',
                        'brokerName',
                        'merchantName',
                        'customerName',
                        'grandTotalPayable',
                        'statusName',
                        'createUtc',
                        'actions'
                    ],
                },

                error: function (err) {
                    console.log(err);
                }
            },
            initComplete: function (settings, json) {
                KTMenu.createInstances();
            },
            columns: [
                { data: 'number' },
                { data: 'brokerName' },
                { data: 'merchantName' },
                { data: 'customerName' },
                { data: 'grandTotalPayable' },
                { data: 'statusName' },
                { data: 'createUtc' },
                { data: 'actions', responsivePriority: -1 }
            ],
            columnDefs: [
                {
                    targets: 0,
                    className: 'text-uppercase'
                },
                {
                    targets: 2,
                    //className: 'd-flex align-items-center',
                    orderable: false,
                    render: function (data, type, full) {
                        return '<div class="d-flex flex-column">'
                            + '<a href="#" class="text-gray-800 text-hover-primary mb-1">' + data + '</a>'
                            + '<span>' + full.merchantEmailAddress + '</span>'
                            + '</div>'
                    }
                },
                {
                    targets: 3,
                    //className: 'd-flex align-items-center',
                    orderable: false,
                    render: function (data, type, full) {
                        return '<div class="d-flex flex-column">'
                            + '<a href="#" class="text-gray-800 text-hover-primary mb-1">' + data + '</a>'
                            + '<span>' + full.customerEmailAddress + '</span>'
                            + '</div>'
                    }
                },
                {
                    targets: 4,
                    orderable: false,
                    className: 'text-right highlight',
                    render: function (data) {
                        return formatter.format(data);
                    }
                },

                {
                    width: '75px',
                    title: 'Status',
                    targets: 5,
                    className: 'text-center',
                    render: function (data, type, full, meta) {
                        var status = {
                            1: { 'class': ' badge-light-warning px-4 py-3' },
                            2: { 'class': ' badge-light-primary px-4 py-3' },
                            3: { 'class': ' badge-light-success px-4 py-3' },
                            4: { 'class': ' badge-success' },
                            5: { 'class': ' badge-light-danger px-4 py-3' },
                            6: { 'class': ' badge-light-danger px-4 py-3' }
                        };
                        if (typeof status[full.statusId] === 'undefined') {
                            return data;
                        }
                        return '<span class="badge ' + status[full.statusId].class + ' fw-bolder" style="margin: 3px!important;">' + data + '</span>'
                            + (full.inEscrow ? '<span class="badge badge-info" style="margin: 3px!important;">In Escrow</span>' : '')
                            + (full.inDispute ? '<span class="badge badge-danger" style="margin: 3px!important;">Disputed</span>' : '');
                    },
                },
                {
                    targets: 6,
                    title: 'Initiated&nbsp;On',
                    render: function (data, type, full) {
                        return moment(data).format('DD/MM/YY')
                    }
                },
                {
                    targets: -1,
                    //width: '70px',
                    title: 'Actions',
                    orderable: false,
                    render: function (data, type, full, meta) {
                        console.log(full);

                        return `<a href="#" class="btn btn-light btn-active-light-primary btn-sm" data-kt-menu-trigger="click" data-kt-menu-placement="bottom-end" data-kt-menu-flip="top-end">
                                Actions
                                <span class="svg-icon svg-icon-5 m-0">
                                    <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="24px" height="24px" viewBox="0 0 24 24" version="1.1">
                                        <g stroke="none" stroke-width="1" fill="none" fill-rule="evenodd">
                                            <polygon points="0 0 24 0 24 24 0 24"></polygon>
                                            <path d="M6.70710678,15.7071068 C6.31658249,16.0976311 5.68341751,16.0976311 5.29289322,15.7071068 C4.90236893,15.3165825 4.90236893,14.6834175 5.29289322,14.2928932 L11.2928932,8.29289322 C11.6714722,7.91431428 12.2810586,7.90106866 12.6757246,8.26284586 L18.6757246,13.7628459 C19.0828436,14.1360383 19.1103465,14.7686056 18.7371541,15.1757246 C18.3639617,15.5828436 17.7313944,15.6103465 17.3242754,15.2371541 L12.0300757,10.3841378 L6.70710678,15.7071068 Z" fill="#000000" fill-rule="nonzero" transform="translate(12.000003, 11.999999) rotate(-180.000000) translate(-12.000003, -11.999999)"></path>
                                        </g>
                                    </svg>
                                </span>
                            </a>

                            <div class="menu menu-sub menu-sub-dropdown menu-column menu-rounded menu-gray-600 menu-state-bg-light-primary fw-bold fs-7 w-125px py-4" data-kt-menu="true">

                            <div class="menu-item px-3">
                                    <a href="/business/marketplace/transactions/details?transid=` + full.id + `" class="menu-link px-3" data-kt-docs-table-filter="delete_row">
                                        Details
                                    </a>
                                </div>`
                            +
                            (full.merchantAccountId == window.accountId && full.inEscrow && !full.inDispute ?
                                `<div class="menu-item px-3">
                                    <a href="javascript:;" data-id="` + full.id + `" class="menu-link px-3 apply-code" >
                                        Apply&nbsp;Code
                                    </a>
                                </div>`: ``)
                            + `</div>`;
                    },
                }
            ],
        });
    }
    init();

    dataTable.on('draw', function () {
        KTMenu.createInstances();
    });

    $(document).on('click', '.apply-code', function (e) {
        e.preventDefault();

        var transactionId = $(this).data('id');

        var model = new ApplyEscrowCodeViewModel(transactionId);

        showModal({
            viewModel: model,
            context: self
        }).then(function (result) {
            if (result.success) {
                dataTable.table().draw();
            }
        });
    });
}