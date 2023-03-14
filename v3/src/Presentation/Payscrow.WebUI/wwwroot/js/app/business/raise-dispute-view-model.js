var RaiseDisputeViewModel = function (serviceUrl) {
    var self = this;

    self.template = 'raise_dispute_tmpl';

    self.command = {
        complaint: ko.observable().extend({ required: true })
    };

    self.submit = function () {
        self.executeCommand(serviceUrl, self.command).then(function (result) {
            if (result.success) {
                Swal.fire({
                    text: "You have successfully raised a dispute for this transaction",
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
}