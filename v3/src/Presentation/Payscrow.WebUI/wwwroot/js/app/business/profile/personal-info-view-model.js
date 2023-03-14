var PersonalInfoViewModel = function () {
    let self = this;

    self.photoUploadEnabled = ko.observable(false);

    self.enablePhotoUploadEnabled = function () {
        self.photoUploadEnabled(!self.photoUploadEnabled());
    }

    self.profilePhotoUploaded = function (response, result) {
        self.model.pictureFileName(result.fileSystemName);
        self.photoUploadEnabled(false);
    }

    self.removePhoto = function () {
        self.model.pictureFileName(null);
    }


    self.model = {
        pictureFileName: ko.observable().syncWith('pictureFileName'),
        firstName: ko.observable().extend({ required: true }).subscribeTo('firstName'),
        lastName: ko.observable().extend({ required: true }).subscribeTo('lastName'),
        middleName: ko.observable().subscribeTo('middleName'),
        //gender: ko.observable().extend({ required: true }),
        //occupation: ko.observable().extend({ required: { message: 'occupation is required' } }),
        phoneNumber: ko.observable().extend({ required: true, digit: true }).subscribeTo('phoneNumber'),
        email: ko.observable().subscribeTo('email'),
        //street: ko.observable().extend({ required: { message: 'required' } }),
        //city: ko.observable().extend({ required: { message: 'required' } }),
        //state: ko.observable().extend({ required: { message: 'required' } }),
        //zipCode: ko.observable(),
        //country: ko.observable().extend({ required: { message: 'required' } })
    };

    self.validationResult = ko.validatedObservable(self.model);

    self.submit = function () {
        if (!self.validationResult.isValid()) {
            self.validationResult.errors.showAllMessages();
            return false;
        }

        $.blockUI();

        $.ajax({
            url: '/api/v3/business/escrow/user/update',
            type: 'post',
            contentType: 'application/json',
            data: ko.toJSON(self.model),
            success: result => {
                if (result.success) {
                    Swal.fire(
                        'Success',
                        'Your profile data has been updated.',
                        'success'
                    );
                }
            },
            error: xhr => { console.log(xhr); },
            complete: _ => { $.unblockUI(); }
        });
    }

    //function init() {
    //    $.blockUI();

    //    $.ajax({
    //        url: '/api/v3/business/escrow/user',
    //        type: 'get',
    //        success: response => {
    //            ko.mapping.fromJS(response, {}, self.model);
    //        },
    //        error: xhr => { console.log(xhr) },
    //        complete: _ => { $.unblockUI() }
    //    });
    //}
    //init();
}