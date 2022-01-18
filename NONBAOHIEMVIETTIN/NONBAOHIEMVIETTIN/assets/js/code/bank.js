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
        order.address = $('#cart-town').val() + '' + $('#town').val() == '-1' ? '' : $('#town').val() + ' ' + $('#district').val() == '-1' ? '' : $('#district').val() + ' ' + $('#province').val() == '-1' ? '' : $('#province').val();
        order.note = $("#order_note").val();
        var PaymentMethod = $('input[name="paymentMethod"]:checked').val();
        var BankCode = $('input[groupname="bankcode"]:checked').prop('id');
        $.ajax({
            url: '/Cart/Pay',
            type: 'POST',
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ order, PaymentMethod, BankCode }),
            success: function (response) {
                //if (response.status) {
                //    if (response.urlCheckout != undefined && response.urlCheckout != '') {
                //        location.href = response.urlCheckout;
                //    }
                //    else {
                //        console.log('create order ok');
                //    }

                //}
                //else {
                //    $('#divMessage').show();
                //    $('#divMessage').text(response.message);
                //}
                if (response.status) {
                    if (PaymentMethod == 'CASH')
                    {
                        $('.img-cart').show();
                        $('#shipping-group').hide();
                        $('.shopping_cart_area').hide();
                        $('.shopping_cart a span').text('0 sản phẩm- 0đ');
                        $('#cart').hide();
                        showToast('Đặt hàng thành công.');
                    }
                    else
                    location.href = response.urlCheckout;
                }
                else
                    showToast('Hệ thống bảo trì.Vui lòng đặt hàng bằng phương thức tiền mặt');
            },
            error: function (data) {
                alert(JSON.stringify(data));
            }
        });
    }
    $('#btnbill').click(Pay)
})