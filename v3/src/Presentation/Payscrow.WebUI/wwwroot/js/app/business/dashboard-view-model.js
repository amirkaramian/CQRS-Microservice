var DashboardViewModel = function () {
    var self = this;

    self.currencyCode = ko.observable(window.currencyCode);

    self.formatter = new Intl.NumberFormat('en-US', { style: 'currency', currency: window.currencyCode, currencySign: 'accounting' });

    self.model = {
        amountInEscrow: ko.observable(),
        numberOfTransactions: ko.observable(),
        amountInWallet: ko.observable(),
        escrowTransactions: ko.observableArray([])
    };

    self.getTransactionDetailUrl = function (transaction) {
        switch (transaction.serviceType.id()) {
            case 1:
                return '/business/marketplace/transactions/details?transid=' + transaction.transactionGuid();
            default:
                return '';
        }
    }

    function init() {
        $.blockUI();

        $.ajax({
            url: '/api/v3/business/dashboard/' + window.currencyCode,
            type: 'get',
            success: data => {
                console.log(data);
                ko.mapping.fromJS(data, {}, self.model);

                KTMenu.createInstances();
            },
            error: xhr => { console.log(xhr); },
            complete: _ => { $.unblockUI(); }
        });
    }
    init();

    self.applyEscrowCode = function (transaction) {
        var model = new ApplyEscrowCodeViewModel(transaction.transactionGuid());
        showModal({
            viewModel: model,
            context: self
        }).then(function (result) {
            if (result.success) {
                init();
            }
        });
    }

    self.canRaiseDispute = function (transaction) {
        let result = true;

        if (transaction.inDispute()) {
            result = false;
        }

        return result;
    }

    self.raiseDispute = function (transaction) {
        var model = new RaiseDisputeViewModel(getServiceDisputeUrl(transaction.serviceType.id(), transaction.transactionGuid()));

        showModal({
            viewModel: model,
            context: self
        }).then(result => {
            if (result.success) {
                init();
            }
        });
    }

    function getServiceDisputeUrl(serviceTypeId, tranactionId) {
        switch (serviceTypeId) {
            case 1:
                return '/api/v3/business/marketplace/transactions/' + tranactionId + '/dispute';
            default:
        }
    }
}