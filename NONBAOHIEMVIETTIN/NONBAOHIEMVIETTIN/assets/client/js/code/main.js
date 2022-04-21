/*Kiểm tra số điện thoại hợp lệ*/
function validatePhone($Phone) {
    let filter = /^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$/im;
    return filter.test($Phone);
}
/*Hàm upload ảnh lên server*/
function upLoad(dom) {
    let fileUpload = $(dom).get(0);
    let files = fileUpload.files;

    // Create  a FormData object
    let fileData = new FormData();

    // if there are multiple files , loop through each files
    for (let i = 0; i < files.length; i++) {
        fileData.append(files[i].name, files[i]);
    }

    // Adding more keys/values here if need
    fileData.append('Test', "Test Object values");

    $.ajax({
        url: '/tai-anh', //URL to upload files 
        type: "POST", //as we will be posting files and other method POST is used
        processData: false, //remember to set processData and ContentType to false, otherwise you may get an error
        contentType: false,
        data: fileData,
        success: function (result) {
        },
        error: function (err) {
            iziToast.error({
                timeout: 1500,
                title: 'Lỗi',
                message: 'Lỗi tải ảnh.',
                position: 'topRight'
            });
        }
    });

}
/*Hàm disable button khi chờ server xử lý */
function disable(dom) {
    $(dom).attr('disabled', 'true');
    $(dom).attr('data-text', $(dom).text());
    $(dom).html(`<span class="spinner-grow spinner-grow-sm" ></span> <span>Loading...</span>`);
}
/*Hàm disable button khi chờ server xử lý */
function enable(dom) {
    $(dom + ' span').hide();
    $(dom).removeAttr('disabled');
    $(dom).html($(dom).data('text'));
}
//Hàm xử lý format tiền tệ sang vnd
function formatCurrency(nStr, decSeperate, groupSeperate) {
    nStr += '';
    x = nStr.split(decSeperate);
    x1 = x[0];
    x2 = x.length > 1 ? '.' + x[1] : '';
    var rgx = /(\d+)(\d{3})/;
    while (rgx.test(x1)) {
        x1 = x1.replace(rgx, '$1' + groupSeperate + '$2');
    }
    return x1 + x2;
}
/*Kiểm tra email hợp lệ*/
function validateEmail($email) {
    var emailReg = /^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/;
    return emailReg.test($email);
}
$(function () {
    $('#btnchangepasswordlink').click(function (e) {
        e.preventDefault();
        $("#myModal7").modal({ backdrop: "static" });

    })
    /*Log out tài khoản khỏi hệ thống*/
    $('#btnlogout').click(function (e) {
        e.preventDefault();
        $.ajax({
            url: '/dang-xuat',
            data: JSON.stringify({}),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            type: "POST",
            success: function (data) {

                if (data == 1) {

                    location.href = '/dang-nhap';
                    FB.logout(function (response) {
                    });
                }
            },
            error: function (data) {
                iziToast.error({
                    timeout: 1500,
                    title: 'Lỗi',
                    message: 'Lỗi chưa xác định.',
                    position: 'topRight'
                });
            }
        })
    })
    //Đổi mật khẩu cho tài khoản
    $('#btnsubmitchangepassword').click(function () {
        var passold = $('#passoldchange').val();
        var passnew = $('#passnewchange').val();

        var prepass = $('#prepasschange').val();
        if (passold == "")
            iziToast.warning({
                timeout: 1500,
                title: 'Cảnh báo',
                message: 'Mật khẩu cũ không được rỗng.',
                position: 'topRight'
            });
        else
            if (passnew == "")
                iziToast.warning({
                    timeout: 1500,
                    title: 'Cảnh báo',
                    message: 'Mật khẩu mới không được rỗng.',
                    position: 'topRight'
                });
            else
                if (passnew.trim().length < 5)
                    iziToast.warning({
                        timeout: 1500,
                        title: 'Cảnh báo',
                        message: 'Mật khẩu mới ít nhất 5 kí tự.',
                        position: 'topRight'
                    });
                else
                    if (prepass == "")
                        iziToast.warning({
                            timeout: 1500,
                            title: 'Cảnh báo',
                            message: 'Nhập lại mật khẩu không được rỗng.',
                            position: 'topRight'
                        });
                    else
                        if (prepass.trim().length < 5)
                            iziToast.warning({
                                timeout: 1500,
                                title: 'Cảnh báo',
                                message: 'Nhập lại mật khẩu ít nhất 5 kí tự.',
                                position: 'topRight'
                            });
                        else {
                            disable('#btnsubmitchangepassword');
                            $.ajax({
                                url: "/doi-mat-khau",
                                data: JSON.stringify({ passold, passnew, prepass }),
                                        contentType: "application/json; charset=utf-8",
                                        dataType: "json",
                                type: "POST",
                                success: function (data) {
                                    if (data == "-1")
                                        iziToast.error({
                                            timeout: 1500,
                                            title: 'Lỗi',
                                            message: 'Mật khẩu cũ không chính xác.',
                                            position: 'topRight'
                                        });
                                    else if (data == "1") {
                                        iziToast.success({
                                            timeout: 1500,
                                            title: 'Thành công',
                                            message: 'Đổi mật khẩu thành công!Vui lòng đăng nhập lại.',
                                            position: 'topRight'
                                        });
                                        $('#btnclosechangepassword').click();
                                        location.href = "/dang-nhap";
                                        $('#btnlogout').click();
                                    }
                                    else
                                        if (data == "0")
                                            iziToast.error({
                                                timeout: 1500,
                                                title: 'Lỗi',
                                                message: 'Nhập lại mật khẩu không giống nhau.',
                                                position: 'topRight'
                                            });
                                        else
                                            iziToast.error({
                                                timeout: 1500,
                                                title: 'Lỗi',
                                                message: 'Có lỗi xảy ra.',
                                                position: 'topRight'
                                            });
                                    enable('#btnsubmitchangepassword');

                                },
                                error: function (data) {
                                    iziToast.error({
                                        timeout: 1500,
                                        title: 'Lỗi',
                                        message: 'Lỗi chưa xác định.',
                                        position: 'topRight'
                                    });
                                }
                            })
                        }
    })
    $('#passoldchange').keypress(function (e) {
        if (e.which == 13)
            $('#btnsubmitchangepassword').click()
    })
    $('#passnewchange').keypress(function (e) {
        if (e.which == 13)
            $('#btnsubmitchangepassword').click()
    })
    $('#prepasschange').keypress(function (e) {
        if (e.which == 13)
            $('#btnsubmitchangepassword').click()
    })
    //Các options khi sắp xếp
    $('.nice-select li').off('click').click(function () {
        var val = $(this).data('value');
        if (val == '1') {
            $("#list-item .col-lg-4.col-md-4.col-sm-6").sort(desc_sortName).appendTo('#list-item');
            $("#list .product_list_item.mb-35").sort(desc_sortName_1).appendTo('#list');

        }
        else
            if (val == '0') {
                $("#list-item .col-lg-4.col-md-4.col-sm-6").sort(asc_sortName).appendTo('#list-item');
                $("#list .product_list_item.mb-35").sort(asc_sortName_1).appendTo('#list');
            }
            else
                if (val == '2') {
                    $("#list-item .col-lg-4.col-md-4.col-sm-6").sort(asc_sortPrice).appendTo('#list-item');
                    $("#list .product_list_item.mb-35").sort(asc_sortPrice_1).appendTo('#list');

                }
                else if (val == '3') {
                    $("#list-item .col-lg-4.col-md-4.col-sm-6").sort(desc_sortPrice).appendTo('#list-item');
                    $("#list .product_list_item.mb-35").sort(desc_sortPrice_1).appendTo('#list');

                }

    })
    // Sắp xếp theo thứ tự tăng dần theo giá
    function asc_sortPrice(a, b) {
        var e = $(a).find('.product_price');
        var f = $(b).find('.product_price');
        return (e.text()) > (f.text()) ? 1 : -1;
    }
    // Sắp xếp theo thứ tự tăng dần theo giá
    function asc_sortPrice_1(a, b) {
        var e = $(a).find('.old-price');
        var f = $(b).find('.old-price');
        return (e.text()) > (f.text()) ? 1 : -1;
    }
    // Sắp xếp theo thứ tự giảm dần theo giá
    function desc_sortPrice(a, b) {
        var e = $(a).find('.product_price');
        var f = $(b).find('.product_price');
        return (e.text()) < (f.text()) ? 1 : -1;
    }

    // Sắp xếp theo thứ tự giảm dần theo giá
    function desc_sortPrice_1(a, b) {
        var e = $(a).find('.old-price');
        var f = $(b).find('.old-price');
        return (e.text()) < (f.text()) ? 1 : -1;
    }
    // Sắp xếp theo thứ tự giảm dần theo tên
    function desc_sortName(a, b) {
        var e = $(a).find('.product_title > a');
        var f = $(b).find('.product_title > a');

        return (e.text()) < (f.text()) ? 1 : -1;
    }
    // Sắp xếp theo thứ tự giảm dần theo tên
    function desc_sortName_1(a, b) {
        var e = $(a).find('.list_title > h3 > a');
        var f = $(b).find('.list_title > h3 > a');

        return (e.text()) < (f.text()) ? 1 : -1;
    }
    // Sắp xếp theo thứ tự tăng dần theo tên dần theo tên
    function asc_sortName(a, b) {
        var e = $(a).find('.product_title > a');
        var f = $(b).find('.product_title > a');
        return (e.text()) > (f.text()) ? 1 : -1;
    }
    // Sắp xếp theo thứ tự tăng dần theo tên dần theo tên
    function asc_sortName_1(a, b) {
        var e = $(a).find('.list_title >h3 > a');
        var f = $(b).find('.list_title >h3 > a');
        return (e.text()) > (f.text()) ? 1 : -1;
    }
    //Hàm xử lý thêm yêu thích
    function addWish(ProductId, Quantity) {
        if (Quantity == 0) {
            iziToast.warning({
                timeout: 1500,
                title: 'Cảnh báo',
                message: 'Số lượng phải lớn hơn 0.',
                position: 'topRight'
            });
            return;
        }
        $.ajax({
            url: "/them-vao-yeu-thich",
            data: JSON.stringify({ ProductId, Quantity }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    type: "POST",
                    success: function (data) {
                console.log(data)
                if (data.status == "1") {
                    iziToast.success({
                    timeout: 1500,
                        title: 'Thành công',
                        message: 'Đã thêm vào yêu thích.',
                        position: 'topRight'
                    });
                    $('#wish-' + data.id + ' .quantity').text('SL:' + data.quantity);
                    $('#lst-wish .block_content p').text(`${data.sumQuantity} sản phẩm.`);
                }
                else if (data.status == "0") {
                    var html = `<div class="cart_item" id="wish-${data.id}">
                <div class="cart_img">
                    <a href="/chi-tiet/${data.alias}"><img src="/${data.image}" alt=""></a>
                </div>
                <div class="cart_info">
                    <a href="/chi-tiet/${data.alias}">${data.name}</a>
                    <span class="cart_price">${data.price}</span>
                    <span class="quantity">SL: ${data.quantity}</span>
                </div>
                <div class="cart_remove">
                    <a title="Xoá sản phẩm" data-id="${data.id}" href="" class ="deletewish"><i class ="fa fa-times-circle"></i></a>
                </div>
            </div>`;
                    //đoạn scripts thêm vào để ajax nhận js xoá yêu thích
                    var deletewish = `<script> function deletewish(ProductId,check) {
            $.ajax({
                url: "/xoa-yeu-thich",
                data: JSON.stringify({ ProductId}),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        type: "POST",
                        success: function (data) {
                            if (data.status == "1") {
                                $('#wish-' +ProductId).hide(500);
                                $('#lst-wish .block_content p').text(data.sumQuantity+ ' sản phẩm.');
                                if(check)
                                  iziToast.success({
                                        timeout: 1500,
                                        title: 'Thành công',
                                        message: 'Xoá sản phẩm thành công.',
                                        position: 'topRight'
                                    });
                                if(data.sumQuantity==0)
                        {
                                    $('#wish').hide();
                                    $('.img-wish').show();
                        }
                        }
                        else
                                if(check)
                                 iziToast.error({
                                    timeout: 1500,
                                    title: 'Lỗi',
                                     message: 'Lỗi hệ thống khi xoá sản phẩm. Vui lòng thao tác lại sau.',
                                    position: 'topRight'
                                });
                        },
                            error: function (data) {
                             iziToast.error({
                                timeout: 1500,
                                title: 'Lỗi',
                                message: 'Lỗi chưa xác định.',
                                position: 'topRight'
                            });
                        }

                        });
                            };
                              $('.deletewish').off('click').click(function (e) {
        e.preventDefault();
             var ProductId = $(this).data('id');

                 swal({
                                    title: "Bạn chắc chắn xoá sản phẩm này?",
                                    icon: "warning",
                                    buttons: true,
                                    dangerMode: true,
                                })
                        .then((willDelete) => {
                            if (willDelete) {
                                deletewish(ProductId, true)
                                }
                                });
                            })
                            </script>`
                    iziToast.success({
                        timeout: 1500,
                        title: 'Thành công',
                        message: 'Đã thêm vào yêu thích.',
                        position: 'topRight'
                    });
                    $('#lst-wish').prepend(html);
                    $('#lst-wish').append(deletewish);
                    $('#lst-wish .block_content p').text(`${data.sumQuantity} sản phẩm.`);
                    $('.img-wish').hide();
                }
                else if (data.status == '-1') {
                    swal("Thông báo", "Mời bạn đăng nhập!", "warning")
                    .then((value) => {
                        location.href = "/dang-nhap";
                    });

                }
                else
                    if (data.status == '-3') {
                        swal("Thông báo", data.message, "info")
                    }
                    },
            error: function (data) {
                iziToast.error({
                    timeout: 1500,
                    title: 'Lỗi',
                    message: 'Lỗi chưa xác định.',
                    position: 'topRight'
                });

            }
        });
    }
    //Thêm vào yêu thích
    $('.addwish').off('click').click(function (e) {
        e.preventDefault();
        var ProductId = $(this).data('id');
        var Quantity = $(this).data('value');
        addWish(ProductId, Quantity);
    })
    //Thêm vào yêu thích có số lượng
    $('#btnaddwish').click(function (e) {
        e.preventDefault();
        var ProductId = $(this).data('id');
        var Quantity = $('#txtquantity-detail').val();
        addWish(ProductId, Quantity);
    })

    //Xoá 1 yêu thích
    $('.deletewish').off('click').click(function (e) {
        e.preventDefault();
        var ProductId = $(this).data('id');
        swal({
            title: "Bạn chắc chắn xoá sản phẩm này?",
            icon: "warning",
            buttons: true,
            dangerMode: true,
        })
        .then((willDelete) => {
            if (willDelete) {
                deletewish(ProductId, true)
            }
        });
    })
    //Hàm xử lý xoá yêu thích
    function deletewish(ProductId, check) {
        $.ajax({
            url: "/xoa-yeu-thich",
            data: JSON.stringify({ ProductId }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    type: "POST",
                    success: function (data) {
                if (data.status == "1") {
                    $('#wish-' +ProductId).hide(500);
                    $('#lst-wish .block_content p').text(`${data.sumQuantity} sản phẩm.`);
                    if (check)
                        iziToast.success({
                            timeout: 1500,
                            title: 'Thành công',
                            message: 'Xoá sản phẩm thành công.',
                            position: 'topRight'
                        });
                    if (data.sumQuantity == 0) {
                        $('#wish').hide();
                        $('.img-wish').show();
                    }
                }
                else
                    if (check)
                        iziToast.error({
                            timeout: 1500,
                            title: 'Lỗi',
                            message: 'Lỗi hệ thống khi xoá sản phẩm. Vui lòng thao tác lại sau.',
                            position: 'topRight'
                        });
                    },
            error: function (data) {
                iziToast.error({
                    timeout: 1500,
                    title: 'Lỗi',
                    message: 'Lỗi chưa xác định.',
                    position: 'topRight'
                });
            }

        });
    }
    //Hàm xử lý thêm giỏ hàng
    function addCart(ProductId, Quantity) {
        if (Quantity == 0) {
            iziToast.warning({
                timeout: 1500,
                title: 'Cảnh báo',
                message: 'Số lượng phải lớn hơn 0.',
                position: 'topRight'
            });
            return;
        }
        $.ajax({
            url: "/them-vao-gio-hang",
            data: JSON.stringify({ ProductId, Quantity }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    type: "POST",
                    success: function (data) {
                console.log(data)
                if (data.status == "1") {
                    iziToast.success({
                    timeout: 1500,
                    title: 'Thành công',
                    message: 'Đã thêm vào giỏ hàng.',
                    position: 'topRight'
            });
                    $('.cart-' +data.id + ' .quantity').text('Số lượng:' + data.quantity);
                    $('#lst-cart .prices').text(data.sumMoney);
                    if (location.pathname == '/yeu-thich')
                        deletewish(ProductId, false);
                    $('.shopping_cart a span').text(`${data.sumQuantity} sản phẩm-${data.sumMoney}`);
                }
                else if (data.status == "0") {
                    var html = `<div class="cart_item cart-${data.id}">
                                                            <div class="cart_img">
                                                                <a href="/chi-tiet/${data.alias}"><img src="/${data.image}" alt=""></a>
                                                            </div>
                                                            <div class="cart_info">
                                                                <a href="/chi-tiet/${data.alias}">${data.name}</a>
                                                                <span class ="cart_price">${data.price}</span>
                                                                <span class ="quantity">Số lượng: ${data.quantity}</span>
                                                            </div>
                                                        <div class="cart_remove">
                                                            <a title="Xoá sản phẩm ${data.name}" class ="deletecart" data-id='${data.id}' href=""><i class ="fa fa-times-circle"></i></a>
                                                        </div></div>`;
                    var script = `<script>
                                $('.deletecart').off('click').click(function (e) {
        e.preventDefault();
        e.stopPropagation();
            var ProductId = $(this).data('id');
         swal({
                            title: "Bạn chắc chắn xoá sản phẩm này?",
                            icon: "warning",
                            buttons: true,
                            dangerMode: true,
                        })
                .then((willDelete) => {
                    if (willDelete) {
                        deletecart(ProductId, true)
                        }
                        });
                            })
    function deletecart(ProductId, check) {
        $.ajax({
                                url: "/xoa-gio-hang",
                                data: JSON.stringify({ ProductId}),
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                type: "POST",
                                success: function (data) {
                        if (data.status == "1") {
                            $('.cart-' +ProductId).hide(500);
                            $('.shopping_cart a span').text(data.sumQuantity +' sản phẩm-'+data.sumMoney);
                            $('#lst-cart .prices').text(data.sumMoney);
                            if(check)
                              iziToast.success({
                        timeout: 1500,
                        title: 'Thành công',
                        message: 'Xoá sản phẩm thành công.',
                        position: 'topRight'
                    });
                            if (data.sumQuantity == 0) {
                                $('.img-cart').show();
                                $('#shipping-group').hide();
                                $('.shopping_cart_area').hide();
                            }
                            }
                            else
                            if (check)
                                 iziToast.error({
                                    timeout: 1500,
                                    title: 'Lỗi',
                                    message: 'Lỗi hệ thống khi xoá sản phẩm. Vui lòng thao tác lại sau.',
                                    position: 'topRight'
                                });
                            },
                            error: function (data) {
                                     iziToast.error({
                                        timeout: 1500,
                                        title: 'Lỗi',
                                        message: 'Lỗi chưa xác đinh.',
                                        position: 'topRight'
                                    });
                            }

                            });
                            }
                                </script>`;
                    iziToast.success({
                        timeout: 1500,
                        title: 'Thành công',
                        message: 'Đã thêm vào giỏ hàng.',
                        position: 'topRight'
                    });
                    $('#cart').prepend(html);
                    $('#lst-cart').append(script);
                    $('#lst-cart .prices').text(data.sumMoney);
                    $('.img-cart').hide();
                    $('#shipping-group').show();
                    $('.shopping_cart a span').text(`${data.sumQuantity} sản phẩm-${data.sumMoney}`);

                    if (location.pathname == '/yeu-thich')
                        deletewish(ProductId, false);

                }
                else if (data.status == '-1') {
                    swal("Thông báo", "Mời bạn đăng nhập!", "warning")
                    .then((value) => {
                        location.href = "/dang-nhap";
                    });
                }
                else {
                    swal("Thông báo", data.message, "info")
                }
                    },
            error: function (data) {
                iziToast.error({
                    timeout: 1500,
                    title: 'Lỗi',
                    message: 'Lỗi chưa xác đinh.',
                    position: 'topRight'
                });
            }
        });
    }
    //Thêm giỏ hàng
    $('.addcart').off('click').click(function (e) {
        e.preventDefault();
        var ProductId = $(this).data('id');
        var Quantity = $(this).data('value');
        addCart(ProductId, Quantity);
    })
    //Event thêm giỏ hàng
    $('.btnaddcart').off('click').click(function () {
        var ProductId = $(this).data('id');
        var Quantity = $('#txtquantity-' + ProductId).val();
        if (typeof (Quantity) == 'undefined') {
            var classes = $(this).parent().attr('class');
            if (classes == 'form form-production')
                Quantity = $('#txtquantity-production-' + ProductId).val();
            else
                if (classes == 'form form-category')
                    Quantity = $('#txtquantity-category-' + ProductId).val();
                else
                    if (classes == 'form form-group-product')
                        Quantity = $('#txtquantity-group-' + ProductId).val();
                    else
                        Quantity = $('#txtquantity-detail').val();
        }

        addCart(ProductId, Quantity);
    })
    //Event xoá giỏ hàng
    $('.deletecart').off('click').click(function (e) {
        e.preventDefault();
        e.stopPropagation();
        var ProductId = $(this).data('id');
        swal({
            title: "Bạn chắc chắn xoá sản phẩm này?",
            icon: "warning",
            buttons: true,
            dangerMode: true,
        })
      .then((willDelete) => {
          if (willDelete) {
              deletecart(ProductId, true)
          }
      });
    })
    //Hàm xử lý xoá giỏ hàng
    function deletecart(ProductId, check) {
        $.ajax({
            url: "/xoa-gio-hang",
            data: JSON.stringify({ ProductId }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    type: "POST",
                    success: function (data) {
                if (data.status == "1") {
                    $('.cart-' +ProductId).hide(500);
                    $('.shopping_cart a span').text(`${data.sumQuantity} sản phẩm-${data.sumMoney}`);
                    $('#lst-cart .prices,.cart_amount.sum_money').text(data.sumMoney);
                    $('.cart_amount.sum_quantity').text(data.sumQuantity)
                    if (check)
                        iziToast.success({
                    timeout: 1500,
                            title: 'Thành công',
                            message: 'Xoá sản phẩm thành công.',
                            position: 'topRight'
                        });
                    if (data.sumQuantity == 0) {
                        $('.img-cart').show();
                        $('#shipping-group').hide();
                        $('.shopping_cart_area').hide();
                    }
                }
                else
                    if (check)
                        iziToast.info({
                            timeout: 1500,
                            title: 'Lỗi',
                            message: 'Lỗi hệ thống khi xoá sản phẩm. Vui lòng thao tác lại sau.',
                            position: 'topRight'
                        });
                    },
            error: function (data) {
                iziToast.info({
                    timeout: 1500,
                    title: 'Lỗi',
                    message: 'Lỗi chưa xác định.',
                    position: 'topRight'
                });
            }

        });
    }
    //Bỏ hành vi mặc định (a href)
    $('#btncheckout,#cart .cart_info a,#cart .cart_img a').click(function (e) {
        e.stopPropagation();
    })
    //Xử lý tăng số lượng trong giỏ hàng
    $('.txtquantity-lstcart').change(function () {
        var ProductId = $(this).data('id');
        var Quantity = $(this).val();
        var dom = $(this);
        $.ajax({
            url: "/cap-nhat-so-luong-gio-hang",
            data: JSON.stringify({ ProductId, Quantity }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    type: "POST",
                    success: function (data) {
                if (data.status == "1") {
                    if (Quantity <= 0)
                        $('.cart-' +ProductId).hide(500);
                    dom.data('val', dom.val());
                    $('.shopping_cart a span').text(`${data.sumQuantity} sản phẩm-${data.sumMoney}`);
                    $('#total-' +ProductId).text(data.total);
                    $('#lst-cart .prices,.cart_amount.sum_money').text(data.sumMoney);
                    $('.cart-' +ProductId + ' .cart_info .quantity').text('Số lượng:' +Quantity);
                    $('.cart_amount.sum_quantity').text(data.sumQuantity)
                    if (data.sumQuantity == 0) {
                        $('.img-cart').show();
                        $('#shipping-group').hide();
                        $('.shopping_cart_area').hide();
                    }
                }
                else
                    if (data.status == -3) {
                        dom.val(dom.data('val'));
                        iziToast.warning({
                            timeout: 1500,
                            title: 'Cảnh báo',
                            message: data.message,
                            position: 'topRight'
                        });
                    }
                    else
                        iziToast.error({
                            timeout: 1500,
                            title: 'Lỗi',
                            message: 'Lỗi hệ thống khi xoá sản phẩm. Vui lòng thao tác lại sau.',
                            position: 'topRight'
                        });
                    },
            error: function (data) {
                iziToast.error({
                    timeout: 1500,
                    title: 'Lỗi',
                    message: 'Lỗi chưa xác định.',
                    position: 'topRight'
                });
            }

        });
    })
    //Hàm tìm kiếm sản phẩm
    function search() {
        var keyword = $('#txtkeyword').val();
        if (keyword == '')
            iziToast.warning({
                timeout: 1500,
                title: 'Cảnh báo',
                message: 'Bạn chưa nhập từ khoá.',
                position: 'topRight'
            });
        else
            location.href = '/tim-kiem?tukhoa=' + keyword;
    }
    //Event tìm kiếm sản phẩm
    $('#btnsearch').click(search)
    $('#txtkeyword').keypress(function (e) {
        if (e.which == 13)
            search();
    })

    //hàm gợi ý khi tìm kiếm
    function suggestSearch(term) {
        $('.search-list').html(`<div class='loader1'>
                                                <span class='fas fa-spinner turn _icon'>
                                                </span>
                                            </div>`);
        $('.search-list').css('minHeight', '150px');

        $.ajax({
            url: "/danh-sach-goi-y",
            data: JSON.stringify({ term}),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    type: "POST",
                    success: function (res) {
                        let data = res.data;
                        console.log(data);
                        let html;
                        if (data == null || data.length == 0)                
                    html = `<div class='search-list_title'><i class='fa fa-search mr-2'></i><span>Không có kết quả cho '<span class='search_list_keyword'>${term}</span>'</span></div>`;            
            else {
                    let li='';

                    data.forEach(ele => {
                        li += `<li><img src='../../${ele.image}' alt=''><a href='/chi-tiet/${ele.alias}'>${ele.name}</a></li>`;

                    });
                            html = `<div class='search-list_title'>
                                                <i class ='fa fa-search mr-2'></i>
                                                <span>Kết quả cho '<span class='search_list_keyword'>${term}</span>'</span>
                                            </div>
                                            <div class='search-list_body'>

                                                <ul class='search_list_category'>

                                                    <li>
                                                        <ul class='search_list_item'>
                                                            ${li}
                                                        </ul>
                                                    </li>
                                                </ul>
                                            </div>`;
                        }
                        $('.search-list').html(html);
                        $('.search-list').css('minHeight', 'unset');

                    },
            error: function (data) {
                iziToast.error({
                    timeout: 1500,
                    title: 'Lỗi',
                    message: 'Lỗi chưa xác định.',
                    position: 'topRight'
                });
            }

        });
    }
    $('#txtkeyword').keyup(function () {
        let val = $(this).val();
        if (val == null || val == '')
            $('.search-list').css('display', 'none');
        else {
            suggestSearch(val);
            $('.search-list').css('display', 'unset');
        }
    })
    $(document).click(function (e) {
        let container = $(".search-list");
        let container2 = $(".search_bar");

        if ((!container.is(e.target) && container.has(e.target).length === 0) && (!container2.is(e.target) && container2.has(e.target).length === 0)) {
            $('.search-list').css('display', 'none');
        }
    });
    $('#txtkeyword')
        .focus(function () {
            let val = $(this).val();
            if (val == null || val == '')
                $('.search-list').css('display', 'none');
            else {
                suggestSearch(val);
                $('.search-list').css('display', 'unset');
            }

        });
 
    //Đánh giá sản phẩm khi hover vào các sao sẽ tự sáng lên
    $('#rate li').each(function (index, element) {
        $(element).find('a').mouseenter(function (e) {
            $(this).addClass('ratting');
            for (var i = 0; i < 5; i++) {
                if (i <= index)
                    $($('#rate li')[i]).find('a').addClass('ratting');
                else
                    if (!$($('#rate li')[i]).find('a').hasClass('active'))
                        $($('#rate li')[i]).find('a').removeClass('ratting');
            }

            return false;
        }).mouseleave(function (e) {
            for (var i = 0; i < 5; i++) {
                if (!$($('#rate li')[i]).find('a').hasClass('active'))
                    $($('#rate li')[i]).find('a').removeClass('ratting');
            }
        })
    })
    //Đánh giá sản phẩm khi click các sao ngoài trước tự sáng ( ví dụ click 4 sao các sao 1 2 3 tự sáng)
    $('#rate li').each(function (index, element) {
        $(element).find('a').mousedown(function (e) {
            e.preventDefault();
            for (var i = 0; i < 5; i++) {
                if (i <= index)
                    $($('#rate li')[i]).find('a').addClass('active');
                else
                    $($('#rate li')[i]).find('a').removeClass('active');

            }
            return false;
        })
    })
    //Xử lý đánh giá của khách hàng
    $('#btnrate').click(function () {
        var comment = $("#comment").val();
        if (comment == '') {
            iziToast.warning({
                timeout: 1500,
                title: 'Cảnh báo',
                message: 'Bạn chưa viết đánh giá.',
                position: 'topRight'
            });
            return;
        }
        var sum = 0;
        $('#rate li').each(function (index, elemet) {
            if ($(this).find('a').hasClass('active'))
                sum++;
        })

        var ratting = new Object();
        ratting.id_product = $(this).data('id');
        ratting.comment = comment;
        ratting.star = sum;
        disable('#btnrate');
        $.ajax({
            url: "/danh-gia-san-pham",
            data: JSON.stringify({ ratting }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    type: "POST",
                    success: function (data) {
                if (data.status == 1) {
                    $('#comment').val('');
                    $('#sumrate').text(`Tổng đánh giá (${data.countRate})`);
                    $('#countrate li').each(function (index, element) {
                        if (index < data.avgStar)
                            $(element).find('a').addClass('ratting')
            else
                            $(element).find('a').removeClass('ratting')
            })
                    $('#rate li').each(function (index, element) {
                        if (index != 0)
                            $(element).find('a').removeClass('ratting active');
            })

                    var img = '';
                    if (data.issocial == 0)
                        img = `/${data.image}`;
                    else
                        img = data.image;
                    var li = '';
                    for (var i = 0; i < 5; i++)
                        if (i < sum)
                            li += `<li><a class="ratting"><i class="fa fa-star"></i></a></li>`;
                        else
                            li += `<li><a><i class="fa fa-star"></i></a></li>`;
                    var html = `<div class="lst-ratting mb-2">
                                        <div class="product_ratting mb-10">
                                            <div>
                                                    <img style="border-radius:50%;width: 24px;margin-right: 2px;" src="${img}" alt="">
                                                <strong>${data.fullname}</strong>
                                            </div>
                                            <div style="display: flex;align-items: baseline;margin-top: 4px;">
                                                <ul style="margin-right:10px">
                                                    ${li}
                                                </ul>
                                                <p style="margin-bottom:unset">${data.createDate}</p>

                                            </div>
                                        </div>
                                            <p>${comment}</p>
                                    </div>`;
                    $('#sheet').append(html);
                    iziToast.success({
                        timeout: 1500,
                        title: 'Thành công',
                        message: data.message,
                        position: 'topRight'
                    });

                } else
                    iziToast.error({
                        timeout: 1500,
                        title: 'Lỗi',
                        message: data.message,
                        position: 'topRight'
                    });
                        enable('#btnrate');
                    },
            error: function (data) {
                iziToast.error({
                    timeout: 1500,
                    title: 'Lỗi',
                    message: 'Lỗi chưa xác định.',
                    position: 'topRight'
                });

            }

        });
    })
    //Gửi phản hồi của khách hàng
    $('#btnsendcontact').click(function () {
        var send = new Object();
        send.name = $('#name').val();
        send.email = $('#email').val();
        send.subject = $('#subject').val();
        send.phone = $('#phone').val();
        send.message = CKEDITOR.instances['message'].getData();
        if (send.name == '')
            iziToast.warning({
                timeout: 1500,
                title: 'Cảnh báo',
                message: 'Tên không được rỗng.',
                position: 'topRight'
            });
        else
            if (send.email == '')
                iziToast.warning({
                    timeout: 1500,
                    title: 'Cảnh báo',
                    message: 'Email không được rỗng.',
                    position: 'topRight'
                });
            else
                if (send.subject == '')
                    iziToast.warning({
                        timeout: 1500,
                        title: 'Cảnh báo',
                        message: 'Tiêu đề không được rỗng.',
                        position: 'topRight'
                    });
                else
                    if (send.phone == '')
                        iziToast.warning({
                            timeout: 1500,
                            title: 'Cảnh báo',
                            message: 'Số điện thoại không được rỗng.',
                            position: 'topRight'
                        });
                    else
                        if (send.message == '')
                            iziToast.warning({
                                timeout: 1500,
                                title: 'Cảnh báo',
                                message: 'Nội dung không được rỗng.',
                                position: 'topRight'
                            });
                        else {
                            disable('#btnsendcontact');
                            $.ajax({
                                url: "/phan-hoi",
                                data: JSON.stringify({ send }),
                                        contentType: "application/json; charset=utf-8",
                                        dataType: "json",
                                        type: "POST",
                                        success: function (data) {
                                            enable('#btnsendcontact');

                                            if (data.status == 1) {
                                                iziToast.success({
                                        timeout: 1500,
                                        title: 'Thành công',
                                        message: data.message,
                                        position: 'topRight'
                                });

                                        $('#name').val('');
                                        $('#email').val('');
                                        $('#subject').val('');
                                                $('#phone').val('');
                                                CKEDITOR.instances['message'].setData('');
                                            }
                                            else
                                                iziToast.error({
                                                    timeout: 1500,
                                                    title: 'Lỗi',
                                                    message: data.message,
                                                    position: 'topRight'
                                                });

                                        },
                                error: function (data) {
                                    iziToast.error({
                                        timeout: 1500,
                                        title: 'Lỗi',
                                        message: 'Lỗi chưa xác định.',
                                        position: 'topRight'
                                    });

                                }

                            });
                        }
    })
    //Xử lý khách hàng đăng kí nhận tin
    $('#btnsub').click(function () {
        var sub = new Object();
        sub.email = $('#emailsub').val();
        if (sub.email == '')
            iziToast.warning({
                timeout: 1500,
                title: 'Cảnh báo',
                message: 'Bạn chưa điền email để đăng kí.',
                position: 'topRight'
            });
        else
            if (!validateEmail(sub.email))
                iziToast.warning({
                    timeout: 1500,
                    title: 'Cảnh báo',
                    message: 'Không đúng định dạng email',
                    position: 'topRight'
                });
            else {
                disable('#btnsub');
                $.ajax({
                    url: "/dang-ki-nhan-tin",
                    data: JSON.stringify({ sub }),
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            type: "POST",
                            success: function (data) {
                                enable('#btnsub');
                                if (data.status == 1) {
                                    iziToast.success({
                            timeout: 1500,
                            title: 'Thành công',
                            message: data.message,
                            position: 'topRight'
                    });
                            $('#emailsub').val('');
                    }
                    else
                                    iziToast.error({
                        timeout: 1500,
                        title: 'Lỗi',
                        message: data.message,
                        position: 'topRight'
                    });
                    },
                        error: function (data) {
                            iziToast.error({
                                timeout: 1500,
                                title: 'Lỗi',
                                message: 'Lỗi chưa xác định.',
                                position: 'topRight'
                            });

                        }

                });
            }
    })
    $('#btninfoacc').click(function () {
        location.href = '/thong-tin-tai-khoan';
    })
    //Hàm update tài khoản
    function updateAccount() {
        var acc = new Object();
        acc.email = $('#emailupdate').val();
        acc.phone = $('#phoneupdate').val();
        acc.address = $('#addressupdate').val();
        var img = '';
        var check = true;
        try {
            img = $('#imageupdate').val().split("\\").pop();
        } catch (e) {
            check = false;
        }
        acc.image = (img == '') ? $('#imgupdate').data('val') : img;
        acc.fullname = $('#fullnameupdate').val();
        if (acc.fullname == "")
            iziToast.warning({
                timeout: 1500,
                title: 'Cảnh báo',
                message: 'Họ tên không được rỗng.',
                position: 'topRight'
            });
        else
            if (acc.email == "")
                iziToast.warning({
                    timeout: 1500,
                    title: 'Cảnh báo',
                    message: 'Email không được rỗng.',
                    position: 'topRight'
                });
            else
                if (!validateEmail(acc.email))
                    iziToast.warning({
                        timeout: 1500,
                        title: 'Cảnh báo',
                        message: 'Email không đúng định dạng.',
                        position: 'topRight'
                    });
                else
                    if (acc.phone == "")
                        iziToast.warning({
                            timeout: 1500,
                            title: 'Cảnh báo',
                            message: 'Số điện thoại không được rỗng.',
                            position: 'topRight'
                        });
                    else
                        if (!validatePhone(acc.phone))
                            iziToast.warning({
                                timeout: 1500,
                                title: 'Cảnh báo',
                                message: 'Số điện thoại không đúng định dạng.',
                                position: 'topRight'
                            });
                        else
                            if (acc.address == "")
                                iziToast.warning({
                                    timeout: 1500,
                                    title: 'Cảnh báo',
                                    message: 'Địa chỉ không được rỗng.',
                                    position: 'topRight'
                                });
                            else {
                                disable('#btnsaveaccinfo');
                                if (check)
                                    upLoad('#imageupdate');
                                $.ajax({
                                    url: "/cap-nhat",
                                    data: JSON.stringify({ acc }),
                                            contentType: "application/json; charset=utf-8",
                                            dataType: "json",
                                            type: "POST",
                                            success: function (data) {
                                                if (data.status == 1) {
                                                    iziToast.success({
                                            timeout: 1500,
                                            title: 'Thành công',
                                            message: data.message,
                                            position: 'topRight'
                                    });
                                            $('#accountname').text(data.fullname);
                                            $('#accounthome img').attr('src', '/' +data.image);
                                            $('#imgupdate').attr('src', '/' +data.image);


                                    }
                                    else
                                                    iziToast.error({
                                                        timeout: 1500,
                                                        title: 'Lỗi',
                                                        message: data.message,
                                                        position: 'topRight'
                                                    });
                                                enable('#btnsaveaccinfo');

                                            },
                                    error: function (data) {
                                        iziToast.error({
                                            timeout: 1500,
                                            title: 'Lỗi',
                                            message: 'Lỗi chưa xác định.',
                                            position: 'topRight'
                                        });
                                    }
                                })
                            }

    }
    $('#btnsaveaccinfo').click(updateAccount);
    //Xử lý khi bấm nút xem trong đơn hàng 
    $('.view').off('click').click(function () {

        $(this).toggleClass('active');
        var id = $(this).data('id');
        console.log(id);
        $('.view').each(function (index, element) {
            if (id != $(element).data('id') && $(element).hasClass('active')) {
                $('#detail_' + $(element).data('id')).removeClass('show');
                $(element).removeClass('active');
            }
        })
    })
    //hàm chia sẻ sản phẩm qua facebook
    $('.facebook').off('click').click(function (e) {
        e.preventDefault();
        window.open('https://www.facebook.com/sharer/sharer.php?u=' + location.host + '/chi-tiet/' + $(this).data('alias'), '_blank');
    })
    //Hàm xoá 1 đơn hàng
    function delete_order(id) {
        $.ajax({
            url: "/Cart/delete_order/" + id,
            data: JSON.stringify(id),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            type: "POST",
            success: function (data) {
                if (data.status == 1) {
                    iziToast.success({
                        timeout: 1500,
                        title: 'Thành công',
                        message: data.message,
                        position: 'topRight'
                    });
                    $(`#order-${id}`).hide(200);
                    $(`#detail_${id}`).hide();
                }
                else
                    iziToast.error({
                        timeout: 1500,
                        title: 'Lỗi',
                        message: data.message,
                        position: 'topRight'
                    });
            },
            error: function (data) {
                iziToast.error({
                    timeout: 1500,
                    title: 'Lỗi',
                    message: 'Lỗi chưa xác định.',
                    position: 'topRight'
                });
            }
        })
    }
    //Event gọi hàm xoá 1 đơn hàng
    $('.delete_order').off('click').click(function (e) {
        e.preventDefault();
        var id = Number($(this).data('id'));
        swal({
            title: "Bạn chắc chắn huỷ đơn hàng này?",
            icon: "warning",
            buttons: true,
            dangerMode: true,
        })
        .then((willDelete) => {
            if (willDelete) {
                delete_order(id);

            }
        });


    });
})