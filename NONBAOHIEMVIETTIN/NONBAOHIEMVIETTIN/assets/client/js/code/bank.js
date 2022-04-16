$(function () {

    function Pay() {
        var order = new Object();
        order.fullname = $('#cart-fullname').val();
        order.phone = $('#cart-phone').val();
        order.email = $('#cart-email').val();
        let address = $('#cart-town').val() + ' ' + ($('#town').val() == '-1' ? '' : $('#town').val()) + ' ' + ($('#district').val() == '-1' ? '' : $('#district').val()) + ' ' + ($('#province').val() == '-1' ? '' : $('#province').val());
        order.address = address.trim();

        order.note = $("#order_note").val();

        var PaymentMethod = $('input[name="paymentMethod"]:checked').val();

        if (order.fullname == '') {
            iziToast.warning({
                timeout: 1500,
                title: 'Cảnh báo',
                message: 'Họ tên không được rỗng.',
                position: 'topRight'
            });
        }
        else
            if (order.phone == '') {
                iziToast.warning({
                    timeout: 1500,
                    title: 'Cảnh báo',
                    message: 'Số điện thoại không được rỗng.',
                    position: 'topRight'
                });
            }
            else
                if (order.email == '') {
                    iziToast.warning({
                        timeout: 1500,
                        title: 'Cảnh báo',
                        message: 'Email không được rỗng.',
                        position: 'topRight'
                    });
                }
                else
                    if (order.address == '') {
                        iziToast.warning({
                            timeout: 1500,
                            title: 'Cảnh báo',
                            message: 'Địa chỉ không được rỗng.',
                            position: 'topRight'
                        });
                    }
                    else {
                        disable('#btnbill');
                        $.ajax({
                            url: '/thanh-toan',
                            type: 'POST',
                            dataType: 'json',
                            contentType: "application/json; charset=utf-8",
                            data: JSON.stringify({ order, PaymentMethod }),
                                    success: function (response) {
                                        enable('#btnbill');

                                        if (response.status) {
                                            if (PaymentMethod == 'CASH') {
                                                $('.img-cart').show();
                                                $('#shipping-group').hide();
                                                $('.shopping_cart_area').hide();
                                                $('.shopping_cart a span').text('0 sản phẩm- 0đ');
                                                $('#cart').hide();
                                                swal({
                                                    title: "Thông báo",
                                                    text: "Đặt hàng thành công.Vui lòng kiểm email đơn đặt hàng.",
                                                    icon: "success",
                                                    button: "Ok",
                                                });
                                            }
                                            else
                                                location.href = response.urlCheckout;
                                        }
                                        else
                                            swal({
                                                title: "Thông báo",
                                                text: "Lỗi hệ thống! Vui lòng thử lại sau.",
                                                icon: "error",
                                                button: "Ok",
                                            });
                                    },
                            error: function (data) {
                                console.log(JSON.stringify(data));
                            }
                        });
                    }
    }
    $('#btnbill').click(Pay)
})