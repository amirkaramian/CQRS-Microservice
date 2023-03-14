var AddOrEditItemViewModel = function (item) {
    var self = this;

    self.template = 'add_edit_item_tmpl';

    self.model = {
        name: ko.observable().extend({ required: true }),
        description: ko.observable().extend({ required: true }),
        amount: ko.observable().extend({ required: true, number: true }),
        availableQuantity: ko.observable().extend({ required: true, number: true }),
        isTangible: ko.observable(true)
    };

    if (item) {
        self.model.name(item.name);
        self.model.description(item.description);
        self.model.amount(item.amount);
        self.model.availableQuantity(item.availableQuantity);
    }

    self.validationResult = ko.validatedObservable(self.model);

    self.addToList = function () {
        if (!self.validationResult.isValid()) {
            self.validationResult.errors.showAllMessages();
            return false;
        }

        self.modal.close(ko.toJS(self.model));
    }
}

var DealDetailStep = function () {
    var self = this;

    self.loading = ko.observable().subscribeTo("isLoading");

    self.currencies = ko.observableArray([]);

    self.chargeConfigList = ko.observableArray([
        { value: 100, text: 'I will pay' },
        { value: 75, text: 'I will pay 75%' },
        { value: 25, text: 'I will pay 25%' },
        { value: 0, text: 'Buyer pays' }
    ]);

    self.sellerEmail = ko.observable().extend({ required: true, email: true }).publishOn('sellerEmail');
    self.sellerPhone = ko.observable().extend({ required: true, digit: true }).publishOn('sellerPhone');
    self.currencyCode = ko.observable().extend({ required: true }).publishOn('currencyCode');
    self.sellerChargePercentage = ko.observable().extend({ required: true }).publishOn('sellerChargePercentage');
    self.buyerUrl = ko.observable().publishOn('buyerUrl');
    self.title = ko.observable().extend({ required: true }).publishOn('title');
    self.description = ko.observable().publishOn('description');

    self.isAuthenticated = ko.observable(false).publishOn('isAuthenticated');

    self.init = function () {
        self.loading(true);

        $.blockUI();
        $.ajax({
            url: '/api/v3/PaymentInviteDeals/create-deal-data',
            type: 'get',
            success: function (response) {
                console.log(response);

                self.isAuthenticated(response.isAuthenticated);
                self.currencies(response.currencies);
                self.buyerUrl(response.buyerPageUrl);

                if (response.email) {
                    self.sellerEmail(response.email);
                }

                if (response.phone) {
                    self.sellerPhone(response.phone);
                }
            },
            error: function (xhr) {
                console.log(xhr);
            },
            complete: function () {
                self.loading(false);
                $.unblockUI();
            }
        });
    }
    self.init();
}

var DealItemsStep = function () {
    var self = this;

    self.currencyCode = ko.observable().subscribeTo('currencyCode');

    self.items = ko.observableArray([]).extend({
        atLeastOne: {
            message: "Please add at least one item."
        }
    }).publishOn('dealItems');

    self.addItem = function () {
        var model = new AddOrEditItemViewModel();

        showModal({
            viewModel: model,
            context: self
        }).then(x => {
            self.items.push(x);
        });
    }

    self.editItem = function (item) {
        var model = new AddOrEditItemViewModel(item);

        showModal({
            viewModel: model,
            context: self
        }).then(x => {
        });
    }

    self.deleteItem = function (item) {
        self.items.remove(item);
    }
}

var SummaryStep = function () {
    var self = this;

    self.command = {
        sellerEmail: ko.observable().subscribeTo('sellerEmail'),
        sellerLocalPhoneNumber: ko.observable().subscribeTo('sellerPhone'),
        currencyCode: ko.observable().subscribeTo('currencyCode'),
        sellerChargePercentage: ko.observable().subscribeTo('sellerChargePercentage'),
        items: ko.observableArray().subscribeTo('dealItems'),
        buyerUrl: ko.observable().subscribeTo('buyerUrl'),
        title: ko.observable().subscribeTo('title'),
        description: ko.observable().subscribeTo('description')
    };
}

var VerificationAndCompletionStep = function () {
    var self = this;

    self.isAuthenticated = ko.observable().subscribeTo('isAuthenticated');
    self.isVerified = ko.observable(false);

    self.command = {
        dealId: ko.observable().subscribeTo('dealId'),
        code: ko.observable().extend({ required: true })
    };

    self.validationResult = ko.validatedObservable(self.command);

    self.verifyCode = function () {
        if (!self.validationResult.isValid()) {
            self.validationResult.errors.showAllMessages();
            return false;
        }

        $.blockUI();

        $.ajax({
            url: '/api/v3/PaymentInviteDeals/verify-deal',
            type: 'post',
            contentType: 'application/json',
            data: ko.toJSON(self.command),
            success: function (result) {
                console.log(result);

                if (result.errors.length == 0) {
                    swal({
                        title: 'Deal Creation Successful?',
                        text: 'You have successfully created and verified your sales deal.',
                        type: 'success'
                    }).then(function () {
                        window.location.href = '/';
                        //swal(
                        //    'Deleted!',
                        //    'Your imaginary file has been deleted.',
                        //    'success'
                        //)
                    });
                }
            },
            error: function (xhr) {
                console.log(xhr);
            },
            complete: function () {
                $.unblockUI();
            }
        });
    }
}

var CreateDealViewModel = function () {
    var self = this;

    self.loading = ko.observable().subscribeTo("isLoading");
    self.hasError = ko.observable(false);
    self.errors = ko.observableArray([]);

    var stepModel1 = new DealDetailStep();
    var stepModel2 = new DealItemsStep();
    var stepModel3 = new SummaryStep();
    var stepModel4 = new VerificationAndCompletionStep();

    self.stepModels = ko.observableArray([
        new Step(1, 'Deal Details', 'deal_details_tmpl', stepModel1),
        new Step(2, 'Deal Items', 'deal_items_tmpl', stepModel2),
        new Step(3, 'Summary', 'deal_summary_tmpl', stepModel3),
        new Step(4, 'Completion', 'verification_tmpl', stepModel4),
        //new Step(5, 'Package Review And Summary', 'package_summary_tmpl', stepModel5)
    ]);

    self.createdDealId = ko.observable().publishOn('dealId');

    self.createDeal = function () {
        $.blockUI();

        //console.log(ko.toJSON(stepModel3.command));

        $.ajax({
            url: '/api/v3/PaymentInviteDeals/create-deal',
            type: 'post',
            contentType: 'application/json',
            data: ko.toJSON(stepModel3.command),
            success: function (result) {
                if (result.success) {
                    self.createdDealId(result.dealId);

                    if (result.isVerified) {
                        alert('Deal Created Successfully!');
                    } else {
                        self.goNext();
                    }
                }
            },
            error: function (xhr) {
                console.log(xhr);
            },
            complete: function () {
                $.unblockUI();
            }
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
        return self.currentIndex() > 0;
    });

    self.goPrevious = function () {
        if (self.canGoPrevious()) {
            self.currentStep(self.stepModels()[self.currentIndex() - 1]);
        }
    };
}