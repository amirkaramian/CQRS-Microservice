var DealsDatatableViewModel = function () {
    let self = this;

	var dataTable = {};

	//var formatter = new Intl.NumberFormat('en-US', { style: 'currency', currency: 'USD', currencySign: 'accounting' });

	function init() {

		console.log(window.currencyCode);

		dataTable = $('#datatable').DataTable({
			responsive: true,
			searchDelay: 500,
			dom: "<'row'<'col-sm-12'tr>>\n\t\t\t<'row'<'col-sm-12 col-md-5'i><'col-sm-12 col-md-7 dataTables_pager'lp>>",
			language: {
				emptyTable: 'No Deals have been setup yet for ' + window.currencyCode + '.'
			},
			processing: true,
			serverSide: true,
			ajax: {
				url: '/api/v3/business/paymentinvite/deals?currencyCode=' + window.currencyCode,
				type: 'POST',
				data: {
					// parameters for custom backend script demo
					columnsDef: [
						'title',
						'status',
						'createUtc',
						'actions'
					],
				},
				error: function (err) {
					console.log(err);
				}
			},
			columns: [
				{ data: 'title' },
				{ data: 'status' },
				{ data: 'createUtc' },
				{ data: 'actions', responsivePriority: -1 }
			],
			columnDefs: [

				{
					targets: 0,
					orderable: false,
					render: function (data, type, full) {
						return '<div class="d-flex align-items-center">'
							//+ '< div class="symbol symbol-50 symbol-light-success" flex - shrink - 0"="">'
							//+ '< div class="symbol-label font-size-h5" > A</div >'
       //                     + '</div >'
							+ '<div class="ml-3">'
							+ '<span class="text-dark-75 font-weight-bold line-height-sm d-block pb-2">' + data + '</span>'
							+ '<p href="#" class="text-muted text-hover-primary">' + full.description + '</p>'
							+ '</div>'
                            + '</div >'
					}
				},
				{
					width: '75px',
					title: 'Status',
					targets: 1,
					className: 'text-center',
					render: function (data, type, full, meta) {
						var status = {
							'Active': { 'title': 'Active', 'class': ' label-success' },
							'Inactive': { 'title': 'Inactive', 'class': ' label-warning' },
							'Expired': { 'title': 'Expired', 'class': ' label-light-danger' }
						};
						if (typeof status[data] === 'undefined') {
							return data;
						}
						return '<span class="label label-lg font-weight-bold' + status[data].class + ' label-inline">' + status[data].title + '</span>';
					},
				},
				{
					targets: 2,
					title: 'Created&nbsp;On',
					render: function (data, type, full) {
						return moment(data).format('hh:mm A')
					}
				},
				{
					targets: -1,
					width: '70px',
					title: 'Actions',
					orderable: false,
					render: function (data, type, full, meta) {


						//return '<div class="dropdown dropdown-inline">'
						//	 + '< a href = "javascript:;" class="btn btn-sm btn-clean btn-icon" data - toggle="dropdown" > <i class="la la-cog"></i></a > <div class="dropdown-menu dropdown-menu-sm dropdown-menu-right">									<ul class="nav nav-hoverable flex-column">							    		<li class="nav-item"><a class="nav-link" href="#"><i class="nav-icon la la-edit"></i><span class="nav-text">Edit Details</span></a></li>							    		<li class="nav-item"><a class="nav-link" href="#"><i class="nav-icon la la-leaf"></i><span class="nav-text">Update Status</span></a></li>							    		<li class="nav-item"><a class="nav-link" href="#"><i class="nav-icon la la-print"></i><span class="nav-text">Print</span></a></li>									</ul>							  	</div>							</div > <a href="javascript:;" class="btn btn-sm btn-clean btn-icon" title="Edit details">								<i class="la la-edit"></i>							</a>							<a href="javascript:;" class="btn btn-sm btn-clean btn-icon" title="Delete">								<i class="la la-trash"></i>							</a> ';



						return '<div class="dropdown dropdown-inline">'
							+ '<a href="javascript:;" class="btn btn-sm btn-clean btn-icon" data-toggle="dropdown">'
							+ '<i class="fas fa-cog"></i>'
							+ '</a>'
							+ '<div class="dropdown-menu dropdown-menu-sm dropdown-menu-right">'
							+ '<ul class="nav nav-hoverable flex-column">'
							+ '<li class="nav-item"><a class="nav-link" href="#"><i class="nav-icon la la-edit"></i><span class="nav-text">Edit Details</span></a></li>'
							//+ ((full.depositMethodId == 2 || full.depositMethodId == 3) && full.status == 'UnderReview' ? '<li class="nav-item"><a data-id="' + full.id + '" class="nav-link view-proof" href="javascript:;"><i class="nav-icon la la-file-image"></i><span class="nav-text">View Proof</span></a></li>' : '')
							//+ '<li class="nav-item"><a data-id="' + full.id + '" class="nav-link add-payment" href="#"><i class="nav-icon la la-money"></i><span class="nav-text">Add Payment</span></a></li>'
							+ '</ul>'
							+ '</div>'
							+ '</div>'
							+ '<a href="/Admin/Clients/ClientAccess?clientId=' + full.id + '" target="_blank" class="btn btn-sm btn-clean btn-icon" title="Access Backoffice">'
							+ '<i class="fas fa-user"></i>'
							+ '</a>';
					},
				}
			],
		});

	}
	init();
}