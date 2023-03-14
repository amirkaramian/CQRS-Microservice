var DealTransactionDetailViewModel = function (transactionId) {
    var self = this;


    self.transaction = {
        dealTitle: ko.observable(),
        dealDescription: ko.observable(),
        number: ko.observable(),
        status: ko.observable(),
        inEscrow: ko.observable(false),
        sellerChargeAmount: ko.observable(),
        totalAmount: ko.observable(),
        paymentStatus: ko.observable(),
        currencyCode: ko.observable(),
        currencySymbol: ko.observable(),
        createUtc: ko.observable(),
        deliveryAddress: {
            street: ko.observable(),
            city: ko.observable(),
            state: ko.observable(),
            country: ko.observable(),
            zipCode: ko.observable()
        }
    };

    self.transactionItems = ko.observableArray([]);
    self.transactionStatusLogs = ko.observableArray([]);


    function init() {
        $.blockUI();

        $.ajax({
            url: '/api/v3/business/paymentinvite/transactions/seller/detail?transactionId=' + transactionId,
            type: 'get',
            success: response => {
                console.log(response);
                ko.mapping.fromJS(response.transaction, {}, self.transaction);
                self.transactionItems(response.transactionItems);
                self.transactionStatusLogs(response.transactionStatusLogs);
            },
            error: xhr => { console.log(xhr); },
            complete: _ => { $.unblockUI(); }
        });
    }
    init();
}