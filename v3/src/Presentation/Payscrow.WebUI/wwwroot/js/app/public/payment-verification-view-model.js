var PaymentVerificationViewModel = function (paymentMethodId, paymentId, externalTransactionId, status) {
    var self = this;


    function init() {       

        if (status != 'successful') {
            alert('payment unsuccessful!');
        } else {

            $.blockUI();

            $.ajax({
                url: '/api/v3/payments/verify-payment',
                type: 'post',
                contentType: 'application/json',
                data: ko.toJSON({
                    paymentMethodId: paymentMethodId,
                    paymentId: paymentId,
                    externalTransactionId: externalTransactionId,
                    status: status
                }),
                success: response => {
                    console.log(response);

                    if (response.isVerified) {
                        alert("Your payment is Now Complete");
                    }
                },
                error: xhr => { console.log(xhr); },
                complete: _ => { $.unblockUI(); }
            });
        }

    }
    init();
}