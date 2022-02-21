$(function () {
    $('input[name="paymentMethod"]').off('click').on('click', function () {
        if ($(this).val() == 'NL') {
            $('.boxContent').hide();
            $('#nganluongContent').show();
        }
        else if ($(this).val() == 'ATM_ONLINE') {
            $('.boxContent').hide();
            $('#bankContent').show();
        }
        else {
            $('.boxContent').hide();
        }
    });
    function Pay() {
        var order = new Object();
        order.fullname = $('#cart-fullname').val();
        order.phone = $('#cart-phone').val();
        order.email = $('#cart-email').val();
        order.address = $('#cart-town').val() + ' ' + ($('#town').val() == '-1' ? '' : $('#town').val()) + ' ' + ($('#district').val() == '-1' ? '' : $('#district').val()) + ' ' + ($('#province').val() == '-1' ? '' : $('#province').val());
        order.note = $("#order_note").val();
        var PaymentMethod = $('input[name="paymentMethod"]:checked').val();
        var BankCode = $('input[groupname="bankcode"]:checked').prop('id');
        disable('#btnbill');
        $.ajax({
            url: '/thanh-toan',
            type: 'POST',
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ order, PaymentMethod, BankCode }),
            success: function (response) {
                enable('#btnbill');

                if (response.status) {
                    if (PaymentMethod == 'CASH')
                    {
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
                alert(JSON.stringify(data));
            }
        });
    }
    $('#btnbill').click(Pay)
})