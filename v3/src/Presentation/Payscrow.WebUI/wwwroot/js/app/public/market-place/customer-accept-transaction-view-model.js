var AuthenticateCustomerViewModel = function () {
    var self = this;
}

var PaymentDetailsStep = function (transactionId) {
    var self = this;

    self.hasPaymentId = ko.observable(false);

    self.paymentMethodList = ko.observableArray([]);
    self.currencyCode = ko.observable().subscribeTo('currencyCode');
    self.brokerAccountId = ko.observable().subscribeTo('brokerAccountId');
    self.paymentId = ko.observable().subscribeTo('paymentId');

    self.getPaymentLink = function (paymentMethod) {
        $.blockUI();

        $.ajax({
            url: '/api/v3/payments/payment-link',
            type: 'post',
            contentType: 'application/json',
            data: ko.toJSON({
                paymentId: self.paymentId(),
                paymentMethodId: paymentMethod.id
            }),
            success: response => {
                console.log(response);

                window.location.href = response.link;
            },
            error: xhr => { console.log(xhr); },
            complete: _ => { $.unblockUI(); }
        });
    }

    self.init = function () {
        if (self.paymentMethodList().length == 0) {
            $.blockUI();

            $.ajax({
                url: '/api/v3/Payments/payment-methods?currencyCode=' + self.currencyCode(),
                /* url: '/api/v3/Payments/payment-methods?currencyCode=' + self.currencyCode() + '&accountId=' + self.brokerAccountId(),*/
                type: 'get',
                success: data => {
                    self.paymentMethodList(data.paymentMethods);
                    console.log(ko.toJS(self.paymentMethodList));
                },
                complete: _ => { $.unblockUI(); }
            });
        }

        if (!self.hasPaymentId()) {
            $.ajax({
                url: '/api/v3/marketplace/transactions/' + transactionId + '/paymentid',
                type: 'get',
                success: response => {
                    console.log(response);
                    if (response.success) {
                        self.paymentId(response.paymentId);
                        self.hasPaymentId(true);
                    }
                },
                error: xhr => { console.log(xhr) }
            });
        }
    }
}

//var CustomerDetailsStep = function () {
//    var self = this;

//    self.name = ko.observable().extend({ required: true });
//    self.emailAddress = ko.observable().extend({ required: true });
//    self.phoneNumber = ko.observable().extend({ required: true });

//    self.isCustomerVerified = ko.observable(false).publishOn('isCustomerVerified');
//}

//var CustomerVerificationStep = function () {
//    var self = this;

//    self.isCodeSent = ko.observable(false);

//    self.verificationCode = ko.observable();
//    //self.transactionLinkCode = ko.observable();
//    //self.confirmTransactionLinkCodeCopied = ko.observable(false);

//    self.init = function () {
//        if (!self.isCodeSent()) {
//        }
//    }
//}

var TransactionDetailsStep = function (transactionId) {
    var self = this;

    self.model = {
        transaction: {
            id: ko.observable(),
            number: ko.observable(),
            brokerAccountId: ko.observable().publishOn('brokerAccountId'),
            brokerName: ko.observable(),
            merchantName: ko.observable(),
            merchantEmailAddress: ko.observable(),
            customerAccountId: ko.observable().publishOn('customerAccountId'),
            customerName: ko.observable().publishOn('customerName'),
            customerEmailAddress: ko.observable().publishOn('customerEmailAddress'),
            customerCharge: ko.observable(),
            grandTotalPayable: ko.observable(),
            currencyCode: ko.observable().publishOn('currencyCode'),
            status: {
                displayName: ko.observable()
            },
        },
        items: ko.observableArray()
    };

    self.total = ko.pureComputed(function () {
        return parseFloat(self.model.transaction.grandTotalPayable()) - parseFloat(self.model.transaction.customerCharge());
    }, self);

    function init() {
        $.blockUI();

        $.ajax({
            url: '/api/v3/marketplace/transactions/pending/' + transactionId,
            type: 'get',
            success: data => {
                ko.mapping.fromJS(data, {}, self.model);
                console.log(ko.toJS(self.model));
            },
            error: xhr => { console.log(xhr); },
            complete: _ => { $.unblockUI(); }
        });
    }
    init();
}

var CustomerAcceptTransactionViewModel = function (transactionId) {
    var self = this;

    var stepModel1 = new TransactionDetailsStep(transactionId);
    //var stepModel2 = new CustomerDetailsStep();
    //var stepModel3
    var stepModel2 = new PaymentDetailsStep(transactionId);

    self.stepModels = ko.observableArray([
        new Step(1, 'Transaction Details', 'transaction_details_tmpl', stepModel1),
        //new Step(2, 'Payer Details', 'customer_details_tmpl', stepModel2),
        new Step(2, 'Payment Initiation', 'payment_tmpl', stepModel2)
    ]);

    self.proceedToPay = function () {
        self.goNext();
    }

    self.getState = function (index) {
        if (self.currentIndex() == index) {
            return 'current';
        }

        if (self.stepModels()[index].model().isDone()) {
            return 'done';
        }
        return '';
    }

    self.currentStep = ko.observable(self.stepModels()[0]);

    self.currentIndex = ko.dependentObservable(function () {
        return self.stepModels.indexOf(self.currentStep());
    });

    self.getTemplate = function (data) {
        return self.currentStep().template();
    };

    self.canGoNext = ko.dependentObservable(function () {
        return self.currentIndex() < self.stepModels().length - 1;
    });

    self.goNext = function () {
        self.validateStep = ko.validatedObservable(self.stepModels()[self.currentIndex()]);

        if (!self.validateStep.isValid()) {
            self.validateStep.errors.showAllMessages();

            if (self.stepModels()[self.currentIndex()].model().isPrestine) {
                self.stepModels()[self.currentIndex()].model().isPrestine(false);
            }
            return false;
        }

        if ($.isFunction(self.stepModels()[self.currentIndex() + 1].model().init)) {
            self.stepModels()[self.currentIndex() + 1].model().init();
        }

        if (self.canGoNext()) {
            self.currentStep(self.stepModels()[self.currentIndex() + 1]);
        }
    };

    self.canGoPrevious = ko.dependentObservable(function () {
        return self.currentIndex() > 0;
    });

    self.goPrevious = function () {
        if (self.canGoPrevious()) {
            self.currentStep(self.stepModels()[self.currentIndex() - 1]);
        }
    };
}