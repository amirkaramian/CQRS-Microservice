var TransactionDetailStep = function (dealId) {
    var self = this;

    self.currencyCode = ko.observable().publishOn('currencyCode');

    self.items = ko.observableArray([]).extend({
        atLeastOne: {
            message: "Please add at least one item."
        }
    }).publishOn('transactionItems');

    self.subTotal = ko.computed(_ => {
        var total = 0;

        ko.utils.arrayForEach(self.items(), x => {
            total += x.total();
        });

        return total;
    });

    self.buyerCharges = ko.observable(0);
    self.grandTotal = ko.pureComputed(_ => {
        return self.subTotal() + self.buyerCharges();
    });



    var initialize = function () {

        $.blockUI();

        $.ajax({
            url: '/api/v3/paymentinvitetransactions/create-transaction-data?dealId=' + dealId,
            type: 'get',
            success: function (response) {

                self.currencyCode(response.currencyCode);
                self.buyerCharges(response.buyerTransactionCharge);

                console.log(response);

                ko.utils.arrayForEach(response.items, function (x) {

                    if (x.quantity > 0) {

                        self.items.push(
                        function(){
                            var i = this;

                            i.description = x.description;
                            i.name = x.name;
                            i.quantity = ko.observable(x.quantity);
                            i.amount = ko.observable(x.amount);
                            i.total = ko.computed(_ => { return i.quantity() * i.amount(); });

                            return {
                                description: i.description,
                                name: i.name,
                                quantity: i.quantity,
                                amount: i.amount,
                                total: i.total
                            };
                        }());

                    }                    
                });
            },
            error: xhr => { console.log(xhr); },
            complete: _ => { $.unblockUI(); }
        });
    }
    initialize();
    
}

var BuyerInfoStep = function (dealId) {
    var self = this;

    self.command = {
        dealId: dealId,
        buyerEmail: ko.observable().extend({ required: true, email: true }),
        buyerPhone: ko.observable().extend({ required: true })
    };
}

var PaymentMethodsStep = function () {
    var self = this;

    self.currencyCode = ko.observable().subscribeTo('currencyCode');
    self.methods = ko.observableArray([]);
    self.paymentId = ko.observable().subscribeTo('paymentId');

    self.initialize = function () {
        $.blockUI();

        $.ajax({
            url: '/api/v3/Payments/payment-methods?currencyCode=' + self.currencyCode(),
            type: 'get',
            success: function (response) {
                self.methods(response.paymentMethods);
            },
            error: xhr => { console.log(xhr); },
            complete: _ => { $.unblockUI(); }
        });
    }

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
}


var CreateTransactionViewModel = function (dealId) {
    var self = this;


    self.loading = ko.observable().subscribeTo("isLoading");
    self.hasError = ko.observable(false);
    self.errors = ko.observableArray([]);

    var stepModel1 = new TransactionDetailStep(dealId);
    var stepModel2 = new BuyerInfoStep(dealId);
    var stepModel3 = new PaymentMethodsStep();


    self.stepModels = ko.observableArray([
        new Step(1, 'Transaction Details', 'transaction_items_tmpl', stepModel1),
        new Step(2, 'Personal Info', 'buyer_info_tmpl', stepModel2),
        new Step(3, 'Payment Methods', 'payment_methods_tmpl', stepModel3)
    ]);

    self.transactionId = ko.observable().publishOn('transactionId');
    self.paymentId = ko.observable().publishOn('paymentId');

    self.createTransaction = function () {

        var validation = ko.validatedObservable(stepModel2.command);

        if (!validation.isValid()) {
            validation.errors.showAllMessages();
            return;
        }

        $.blockUI();

        $.ajax({
            url: '/api/v3/PaymentInvitetransactions/create-transaction',
            type: 'post',
            contentType: 'application/json',
            data: ko.toJSON(stepModel2.command),
            success: result => {
                if (result.success) {

                    self.transactionId(result.transactionId);
                    self.paymentId(result.paymentId);

                    console.log(result);

                    self.goNext();
                }                
            },
            error: xhr => { console.log(xhr); },
            complete: _ => { $.unblockUI(); }
        });
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


    self.isModelDone = function (index) {
        return self.stepModels()[index].model().isDone();
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

        if (self.canGoNext()) {
            if (!self.validateStep.isValid()) {
                self.validateStep.errors.showAllMessages();

                if (self.stepModels()[self.currentIndex()].model().isPrestine) {
                    self.stepModels()[self.currentIndex()].model().isPrestine(false);
                }
                return false;
            }

            if ($.isFunction(self.stepModels()[self.currentIndex() + 1].model().initialize)) {

                self.stepModels()[self.currentIndex() + 1].model().initialize();
            }

            self.stepModels()[self.currentIndex()].model().isDone(true);

            self.currentStep(self.stepModels()[self.currentIndex() + 1]);
        }
    };

    self.canGoPrevious = ko.dependentObservable(function () {
        return self.currentIndex() > 0 && self.currentIndex() < 2;
    });

    self.goPrevious = function () {
        if (self.canGoPrevious()) {
            self.currentStep(self.stepModels()[self.currentIndex() - 1]);
        }
    };
}