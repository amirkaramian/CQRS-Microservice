var ApplyEscrowCodeViewModel = function (transactionGuid) {
    var self = this;

    self.template = 'apply_escrow_code_tmpl';

    self.model = {
        transactionNumber: ko.observable(),
        amount: ko.observable(),
        currencyCode: ko.observable()
    };

    self.code = {
        first: ko.observable().extend({ required: true, digit: true }),
        second: ko.observable().extend({ required: true, digit: true }),
        third: ko.observable().extend({ required: true, digit: true }),
        fourth: ko.observable().extend({ required: true, digit: true }),
        fifth: ko.observable().extend({ required: true, digit: true }),
        sixth: ko.observable().extend({ required: true, digit: true })
    };

    self.command = {
        transactionGuid: transactionGuid,
        code: ko.computed(function () {
            return self.code.first() + self.code.second() + self.code.third() + self.code.fourth() + self.code.fifth() + self.code.sixth();
        })
    };

    self.submit = function () {
        self.executeCommand('/api/v3/business/escrow/escrowtransactions/' + transactionGuid + '/applycode', self.command)
            .then(function (result) {
                if (result.success) {
                    Swal.fire({
                        text: "Escrow code applied successfully",
                        icon: "success",
                        buttonsStyling: false,
                        confirmButtonText: "Okay",
                        customClass: {
                            confirmButton: "btn btn-primary"
                        }
                    }).then(function () {
                        self.modal.close(result);
                    });
                }
            }).catch(e => { console.log(e); });
    }

    self.init = function () {
        self.executeQuery('/api/v3/business/escrow/escrowtransactions/' + transactionGuid)
            .then(result => {
                ko.mapping.fromJS(result.transaction, {}, self.model);
            }).catch(e => { console.log(e); });
    }
}