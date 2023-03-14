var HeaderViewModel = function () {
    var self = this;

    self.model = {
        currencyCode: ko.observable().publishOn('currencyCode')
    }

    self.currencies = ko.observableArray([]);

    function init() {
        $.ajax({
            url: '/api/v3/accountsetting',
            type: 'get',
            success: response => {
                self.model.currencyCode(response.accountSetting.currencyCode);

                $.ajax({
                    url: '/api/v3/currencies',
                    type: 'get',
                    success: data => {
                        //console.log(data);
                        //self.currencies(data);
                        ko.utils.arrayForEach(data, function (x) {
                            if (x.code == response.accountSetting.currencyCode) {
                                x.isActive = ko.observable(true);
                            } else {
                                x.isActive = ko.observable(false);
                            }
                            self.currencies.push(x);
                        });
                    },
                    error: err => { console.log(err) }
                });
            },
            error: err => { console.log(err) }
        });
    }
    init();
}