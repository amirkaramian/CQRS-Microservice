var SideNavViewModel = function () {
    let self = this;

    self.model = {
        pictureFileName: ko.observable().syncWith('pictureFileName'),
        firstName: ko.observable().syncWith('firstName'),
        lastName: ko.observable().syncWith('lastName'),
        middleName: ko.observable().syncWith('middleName'),
        //gender: ko.observable().extend({ required: true }),
        //occupation: ko.observable().extend({ required: { message: 'occupation is required' } }),
        phoneNumber: ko.observable().syncWith('phoneNumber'),
        email: ko.observable().syncWith('email'),
    }


    function init() {
        $.blockUI();

        $.ajax({
            url: '/api/v3/business/escrow/user',
            type: 'get',
            success: response => {
                ko.mapping.fromJS(response, {}, self.model);
            },
            error: xhr => { console.log(xhr) },
            complete: _ => { $.unblockUI() }
        });
    }
    init();
}