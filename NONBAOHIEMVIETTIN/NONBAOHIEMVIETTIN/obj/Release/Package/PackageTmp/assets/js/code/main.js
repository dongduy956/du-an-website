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
$(function () {
    /*Hàm xử lý login từ tài khoản đăng kí*/
    function login() {
        var usernamelogin = $('#usernamelogin').val();
        var passwordlogin = $('#passwordlogin').val();
        if (usernamelogin == "") {
            $.notify('Tài khoản không được để trống.', 'warn')

        }
        else
            if (passwordlogin == "") {
                $.notify('Mật khẩu không được để trống.', 'warn')
            }
            else {
                disable('#btnlogin');
                $.ajax({
                    url: "/dang-nhap",
                    data: JSON.stringify({ "usernamelogin": usernamelogin, "passwordlogin": passwordlogin }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    type: "POST",
                    success: function (data) {

                        if (data == "-1") {
                            $.notify('Tài khoản hoặc mật khẩu không chính xác.', 'error');
                        }
                        else if (data == "0") {
                            $.notify('Tài khoản đã bị khoá.Liên hệ admin để giải quyết.', 'error');
                        }
                        else {
                            location.href = '/';
                        }
                        enable('#btnlogin');

                    },
                    error: function (data) {
                        $.notify('Lỗi chưa xác định', 'error');
                    }
                })
            }

    }
    /*Event click cho button login */
    $('#btnlogin').click(login)
    /*Event enter cho các input text login*/
    $('#passwordlogin').keypress(function (e) {
        if (e.which == 13)
            login()
    })
    $('#usernamelogin').keypress(function (e) {
        if (e.which == 13)
            login()
    })
    /*Kiểm tra email hợp lệ*/
    function validateEmail($email) {
        var emailReg = /^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/;
        return emailReg.test($email);
    }
    /*Kiểm tra số điện thoại hợp lệ*/
    function validatePhone($Phone) {
        var filter = /^((\+[1-9]{1,4}[ \-]*)|(\([0-9]{2,3}\)[ \-]*)|([0-9]{2,4})[ \-]*)*?[0-9]{3,4}?[ \-]*[0-9]{3,4}?$/;
        return filter.test($Phone);
    }
    /*Hàm upload ảnh lên server*/
    function upLoad(dom) {
        var fileUpload = $(dom).get(0);
        var files = fileUpload.files;

        // Create  a FormData object
        var fileData = new FormData();

        // if there are multiple files , loop through each files
        for (var i = 0; i < files.length; i++) {
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
                $.notify(err.statusText, 'error');
            }
        });

    }
    /*Hàm xoá các text trong input khi đăng kí thành công*/
    function clearRegister() {
        $('#usernameregister').val('');
        $('#passwordregister').val('');
        $('#prepasswordregister').val('');
        $('#emailregister').val('');
        $('#phoneregister').val('');
        $('#addressregister').val('');
        $('#imageregister').val('');
        $('#fullnameregister').val('');
        $('.custom-file-label.selected').text('Chọn file');
    }
    /*Hàm đăng kí tài khoản trên website*/
    function register() {
        var acc = new Object();
        acc.username = $('#usernameregister').val();
        acc.password = $('#passwordregister').val();
        var prepassword = $('#prepasswordregister').val();
        acc.email = $('#emailregister').val();
        acc.phone = $('#phoneregister').val();
        acc.address = $('#addressregister').val();
        acc.image = $('#imageregister').val().split("\\").pop();
        acc.fullname = $('#fullnameregister').val();
        if (acc.username == "")
            $.notify('Tên đăng nhập không được rỗng.', 'warn');
        else
            if (acc.password == "")
                $.notify('Mật khẩu không được rỗng.', 'warn');
            else
                if (acc.email == "")
                    $.notify('Email không được rỗng.', 'warn');
                else
                    if (!validateEmail(acc.email))
                        $.notify('Email không đúng định dạng.', 'warn');
                    else
                        if (acc.fullname == "")
                            $.notify('Họ tên không được rỗng.', 'warn');
                        else
                            if (acc.phone == "")
                                $.notify('Số điện thoại không được rỗng.', 'warn');
                            else
                                if (!validatePhone(acc.phone))
                                    $.notify('Số điện thoại không đúng định dạng.', 'warn');
                                else
                                    if (acc.address == "")
                                        $.notify('Địa chỉ không được rỗng.', 'warn');
                                    else
                                        if (acc.password != prepassword)
                                            $.notify('Mật khẩu không giống nhau.', 'warn');
                                        else {
                                            disable('#btnregister');
                                            upLoad("#imageregister");
                                            $.ajax({
                                                url: "/dang-ki",
                                                data: JSON.stringify({ acc }),
                                                        contentType: "application/json; charset=utf-8",
                                                        dataType: "json",
                                                type: "POST",
                                                success: function (data) {
                                                    if (data == "-1")
                                                        $.notify('Đăng ký không thành công.', 'error');
                                                    else
                                                        if (data == "0")
                                                            $.notify('Tên tài khoản này đã được sử dụng.', 'error');

                                                        else
                                                            if (data == "2")
                                                                $.notify('Email này đã được sử dụng.', 'error');
                                                            else
                                                                $("#myModal3").modal({ backdrop: "static" });
                                                    enable('#btnregister');

                                                },
                                                error: function (data) {
                                                    $.notify('Lỗi chưa xác định.', 'error');
                                                }
                                            })
                                        }

    }
    $('#btnregister').click(register);
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

                if (data == 1)
                    location.href = '/dang-nhap';
            },
            error: function (data) {
                $.notify('Lỗi chưa xác định', 'error');
            }
        })
    })
    /*Close modal khi đăng kí xác nhận gmail*/
    $('#btnclosexregister').click(function () {
        enable('#btnregister')
        clearRegister();
    })
    $('#btncloseregister').click(function () {
        enable('#btnregister')
        clearRegister();
    })
    /*Hàm xác nhận mã từ gmail khi đăng kí*/
    function submit(operation) {
        var code = "";
        var url = "";
        if (operation == "register") {
            code = $('#keyword').val();
            url += "ma-dang-ki";
        }
        else {
            code = $('#keywordforget').val();
            url += "ma-quen-mat-khau";
        }
        if (code == "")
            $.notify('Bạn chưa nhập mã xác nhập.', 'warn');
        else {
            if (operation == 'register')
                disable('#btnsubmitregister');
            else {
                disable('#btnsubmitforgetpass');
            }
            $.ajax({
                url: url,
                data: JSON.stringify({ code }),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        type: "POST",
                        success: function (data) {
                            console.log(data)
                            if (data == "0") {
                                $.notify('Mã xác nhận không chính xác.', 'error');
                            }
                            else {
                                if (operation == "register") {
                                    $.notify('Đăng kí thành công', 'success');
                                    $('#btncloseregister').click();
                                }
                                else {
                                    $.notify('Thành công.Mời bạn đổi mật khẩu mới.', 'success');
                                    $('#btncloseforgetpass').click();
                                    $("#myModal6").modal({ backdrop: "static" });
                                }
                            }
                            if (operation == 'register')
                                enable('#btnsubmitregister');
                            else {
                                enable('#btnsubmitforgetpass');
                            }
                        },
                error: function (data) {
                    $.notify('Lỗi chưa xác định.', 'error');

                }
            })
        }
    }
    $('#btnsubmitregister').click(function () {
        submit("register");

    });
    $('#keyword').keypress(function (e) {
        if (e.which == 13)
            submit("register");
    })
    /*Hàm gửi lại mã xác nhận*/
    function sendAgain() {
        disable('#btnsendagainregister');
        disable('#btnsendagainforgetpass');
        $.ajax({
            url: "/gui-lai-ma",
            data: JSON.stringify({}),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            type: "POST",
            success: function (data) {
                console.log(data)
                if (data == "0")
                    $.notify('Lỗi hệ thống không thể gửi mail.', 'error');
                else
                    $.notify('Mời bạn check email để lấy mã xác nhận mới.Xin cảm ơn.', 'info');
                enable('#btnsendagainregister');
                enable('#btnsendagainforgetpass');

            },
            error: function (data) {
                $.notify('Lỗi chưa xác định', 'error');
            }
        })
    }
    $('#btnsendagainregister').click(sendAgain);
    //Bật modal cho quên mật khẩu
    $('#btnfogetpass').click(function (e) {
        e.preventDefault();
        $("#myModal4").modal({ backdrop: "static" });
    });
    //Nhập email lấy lại mật khẩu
    function confirmEmail() {
        var email = $('#keywordemail').val();
        if (email == "")
            $.notify('Bạn chưa nhập email!', 'warn');
        else
            if (!validateEmail(email))
                $.notify('Email không đúng định dạng!', 'warn');
            else {
                disable('#btnsubmitforget');
                $.ajax({
                    url: "/quen-mat-khau",
                    data: JSON.stringify({ email }),
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            type: "POST",
                            success: function (data) {
                        console.log(data)
                        if (data == "-1")
                            $.notify('Lỗi hệ thống không thể gửi mail.', 'error');
                        else if (data == "1") {
                            $.notify('Mời bạn check email để lấy mã xác nhận mới.Xin cảm ơn.', 'info');
                            $('#btncloseforget').click();
                            $("#myModal5").modal({ backdrop: "static" });
                        }
                        else
                            $.notify('Không tồn tại email này.Bạn hãy kiểm tra lại email.', 'error');
                                enable('#btnsubmitforget');

                            },
                    error: function (data) {
                        $.notify('Lỗi chưa xác định.', 'error');

                    }
                })
            }
    }
    //Xác nhận đúng email để lấy mật khẩu
    $('#btnsubmitforget').click(function () {
        try {
            confirmEmail();
        } catch (e) {
            $.notify('Có lỗi!!', 'error');
        }
    })
    $('#keywordemail').keypress(function (e) {
        if (e.which == 13)
            $('#btnsubmitforget').click()
    })
    //Xác nhận mã từ gmail quên mật khẩu
    $('#btnsubmitforgetpass').click(function () {
        try {
            submit("forget");
        } catch (e) {
            $.notify('Có lỗi!!', 'error');
        }
    })
    $('#keywordforget').keypress(function (e) {
        if (e.which == 13)
            $('#btnsubmitforgetpass').click();
    })
    $('#passnew').keypress(function (e) {
        if (e.which == 13)
            $('#btnsubmitchangepass').click();
    })
    $('#prepass').keypress(function (e) {
        if (e.which == 13)
            $('#btnsubmitchangepass').click();
    })
    //Đổi mật khẩu từ email quên mật khẩu
    $('#btnsubmitchangepass').click(function () {
        var passNew = $('#passnew').val();
        var prepass = $('#prepass').val();
        if (passNew == "")
            $.notify('Mật khẩu mới không được rỗng.', 'warn');
        else
            if (prepass != passNew)
                $.notify('Mật khẩu không giống nhau.', 'warn');
            else {
                disable('#btnsubmitchangepass');
                $.ajax({
                    url: "/doi-mat-khau-email",
                    data: JSON.stringify({ passNew }),
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            type: "POST",
                            success: function (data) {
                                if (data == "-1")
                                    $.notify('Lỗi hệ thống vui lòng thử lại sau.', 'error');
                    else {
                                    $.notify('Đổi mật khẩu thành công.', 'success');
                                    $('#btnclosechangepass').click();
                                }
                                enable('#btnsubmitchangepass');

                            },
                    error: function (data) {
                        $.notify('Lỗi chưa xác định.', 'error');
                    }
                })
            }
    })
    $('#btnsendagainforgetpass').click(sendAgain);
    $('#btnchangepasswordlink').click(function (e) {
        e.preventDefault();
        $("#myModal7").modal({ backdrop: "static" });

    })
    //Đổi mật khẩu cho tài khoản
    $('#btnsubmitchangepassword').click(function () {
        var passold = $('#passoldchange').val();
        var passnew = $('#passnewchange').val();

        var prepass = $('#prepasschange').val();
        if (passold == "")
            $.notify('Mật khẩu cũ không được rỗng.', 'warn');
        else
            if (passnew == "")
                $.notify('Mật khẩu mới không được rỗng.', 'warn');
            else
                if (prepass == "")
                    $.notify('Nhập lại mật khẩu không được rỗng.', 'warn');
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
                                        $.notify('Mật khẩu cũ không chính xác.', 'error');
                        else if (data == "1") {
                            $.notify('Đổi mật khẩu thành công!Vui lòng đăng nhập lại.', 'success');
                            $('#btnclosechangepassword').click();
                            location.href = "/dang-nhap";
                            $('#btnlogout').click();
                        }
                        else
                            if (data == "0")
                                $.notify('Nhập lại mật khẩu không giống nhau.', 'error');
                            else
                                $.notify('Có lỗi xảy ra.', 'error');
                                    enable('#btnsubmitchangepassword');

                                },
                        error: function (data) {
                            $.notify('Lỗi chưa xác định.', 'error');
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
            $.notify('Số lượng phải lớn hơn 0', "warn");
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
                    $.notify('Đã thêm vào yêu thích.', 'success');
                    $('#wish-' +data.id + ' .quantity').text('SL:' +data.quantity);
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
                                $.notify('Xoá sản phẩm thành công.', 'success');
                                if(data.sumQuantity==0)
                        {
                                    $('#wish').hide();
                                    $('.img-wish').show();
                        }
                        }
                        else
                                if(check)
                                $.notify('Lỗi hệ thống khi xoá sản phẩm. Vui lòng thao tác lại sau.', 'error');
                        },
                            error: function (data) {

                        $.notify('Lỗi chưa xác định', 'error');
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
                $.notify('Đã thêm vào yêu thích.', 'success');
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
                $.notify('Lỗi chưa xác định.', 'error');

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
                        $.notify('Xoá sản phẩm thành công.', 'success');
                    if (data.sumQuantity == 0) {
                        $('#wish').hide();
                        $('.img-wish').show();
            }
            }
            else
                    if (check)
                        $.notify('Lỗi hệ thống khi xoá sản phẩm. Vui lòng thao tác lại sau.', 'error');
            },
                error: function (data) {
                    $.notify('Lỗi chưa xác định', 'error');
                }

        });
    }
    //Hàm xử lý thêm giỏ hàng
    function addCart(ProductId, Quantity) {
        if (Quantity == 0) {
            $.notify('Số lượng phải lớn hơn 0', "warn");
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
                    $.notify('Đã thêm vào giỏ hàng.', 'success');
                    $('.cart-' +data.id + ' .quantity').text('Số lượng:' +data.quantity);
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
                    $.notify('Xoá sản phẩm thành công.', 'success');
                            if (data.sumQuantity == 0) {
                                $('.img-cart').show();
                                $('#shipping-group').hide();
                                $('.shopping_cart_area').hide();
                            }
                            }
                            else
                            if (check)
                    $.notify('Lỗi hệ thống khi xoá sản phẩm. Vui lòng thao tác lại sau.', 'error');
                            },
                            error: function (data) {

                alert(JSON.stringify(data));
                            }

                            });
                            }
                                </script>`;
                $.notify('Đã thêm vào giỏ hàng.', 'success');
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
                $.notify('Lỗi chưa xác định.', 'error');
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
                        $.notify('Xoá sản phẩm thành công.', 'success');
                    if (data.sumQuantity == 0) {
                        $('.img-cart').show();
                        $('#shipping-group').hide();
                        $('.shopping_cart_area').hide();
            }
            }
            else
                    if (check)
                        $.notify('Lỗi hệ thống khi xoá sản phẩm. Vui lòng thao tác lại sau.', 'error');
            },
                error: function (data) {
                    $.notify('Lỗi chưa xác định.', 'error');
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
                    if (data.status == -3)
                    {
                        dom.val(dom.data('val'));
                        $.notify(data.message, 'warn');                   
                    }
                    else
                        $.notify('Lỗi hệ thống khi xoá sản phẩm. Vui lòng thao tác lại sau.', 'error');
                    },
            error: function (data) {
                $.notify('Lỗi chưa xác định.', 'error');
            }

        });
    })
    //Hàm tìm kiếm sản phẩm
    function search() {
        var keyword = $('#txtkeyword').val();
        if (keyword == '')
            $.notify('Bạn chưa nhập từ khoá.', 'warn');
        else
            location.href = '/tim-kiem?tukhoa=' + keyword;
    }
    //Event tìm kiếm sản phẩm
    $('#btnsearch').click(search)
    $('#txtkeyword').keypress(function (e) {
        if (e.which == 13)
            search();
    })
    //Gợi ý tìm kiếm khi nhập vào ô tìm kiếm
    $("#txtkeyword").autocomplete({
        minLength: 0,
        source: function (request, response) {
            $.ajax({

                dataType: "jsonp",
                data: {
                    term: request.term
                },
                success: function (data) {
                    response(JSON.stringify(data.data));
                }
            });

            $.ajax({
                url: "/danh-sach-goi-y",
                data: JSON.stringify({ term: request.term }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                type: "POST",
                success: function (data) {
                    response(data.data);

                },
                error: function (data) {
                    $.notify('Lỗi chưa xác định.', 'error');
                }

            });
        },
        focus: function (event, ui) {
            $("#txtkeyword").val(ui.item.name);
            return false;
        },
        select: function (event, ui) {
            $("#txtkeyword").val(ui.item.name);
            return false;
        }
    })
    .autocomplete("instance")._renderItem = function (ul, item) {
        return $("<li>")
         .append(`<div> <img style='width:20px;' src='/${item.image}' />  ${item.name} </div>`)
        .appendTo(ul);
    };
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
            $.notify('Bạn chưa viết đánh giá.', 'warn');
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
                    $.notify(data.message, 'success');

                } else
                    $.notify(data.message, 'error');
                        enable('#btnrate');
                    },
            error: function (data) {
                $.notify('Lỗi chưa xác định.', 'error');

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
            $.notify('Tên không được rỗng.', 'warn');
        else
            if (send.email == '')
                $.notify('Email không được rỗng.', 'warn');
            else
                if (send.subject == '')
                    $.notify('Tiêu đề không được rỗng.', 'warn');
                else
                    if (send.phone == '')
                        $.notify('Số điện thoại không được rỗng.', 'warn');
                    else
                        if (send.message == '')
                            $.notify('Nội dung không được rỗng.', 'warn');
                        else
                        {
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
                                                $.notify(data.message, 'success');

                                        $('#name').val('');
                                        $('#email').val('');
                                        $('#subject').val('');
                                        $('#phone').val('');
                                        CKEDITOR.instances['message'].setData('');
                                }
                                else
                                                $.notify(data.message, 'error');

                                },
                                    error: function (data) {
                                        $.notify('Lỗi chưa xác định.', 'error');

                                }

                                });
                        }
                                })
    //Xử lý khách hàng đăng kí nhận tin
    $('#btnsub').click(function () {
        var sub = new Object();
        sub.email = $('#emailsub').val();
        if (sub.email == '')
            $.notify('Bạn chưa điền email để đăng kí.', 'warn');
        else
            if (!validateEmail(sub.email))
                $.notify('Không đúng định dạng email.', 'warn');
            else
                {
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
                            $.notify(data.message, 'success');
                            $('#emailsub').val('');
                    }
                    else
                            $.notify(data.message, 'error');
                    },
                        error: function (data) {
                            $.notify('Lỗi chưa xác định.', 'error');

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
        acc.image = (img == '') ? $('#imgupdate').data('val'): img;
        acc.fullname = $('#fullnameupdate').val();
        if (acc.fullname == "")
            $.notify('Họ tên không được rỗng.', 'warn');
        else
            if (acc.email == "")
                $.notify('Email không được rỗng.', 'warn');
            else
                if (!validateEmail(acc.email))
                    $.notify('Email không đúng định dạng.', 'warn');
                else
                    if (acc.phone == "")
                        $.notify('Số điện thoại không được rỗng.', 'warn');
                    else
                        if (!validatePhone(acc.phone))
                            $.notify('Số điện thoại không đúng định dạng.', 'warn');
                        else
                            if (acc.address == "")
                                $.notify('Địa chỉ không được rỗng.', 'warn');
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
                                            $.notify(data.message, 'success');
                                            $('#accountname').text(data.fullname);
                                            $('#accounthome img').attr('src', '/' +data.image);
                                            $('#imgupdate').attr('src', '/' +data.image);


                                    }
                                    else
                                            $.notify(data.message, 'error');
                                        enable('#btnsaveaccinfo');

                                    },
                                        error: function (data) {
                                            $.notify('Lỗi chưa xác định.', 'error');
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
                    $.notify(data.message, 'success');
                    $(`#order-${id}`).hide(200);
                    $(`#detail_${id}`).hide();
                }
                else
                    $.notify(data.message, 'error');
            },
            error: function (data) {
                $.notify('Lỗi chưa xác định.', 'error');
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