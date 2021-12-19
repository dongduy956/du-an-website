
$(function () {   
    function disable(dom) {
        $(dom + ' span').show();
        $(dom).attr('disabled', 'true');
    }
    function enable(dom) {
        $(dom + ' span').hide();
        $(dom).removeAttr('disabled');
    }
    function login() {
        var usernamelogin = $('#usernamelogin').val();
        var passwordlogin = $('#passwordlogin').val();
        if (usernamelogin == "") {
            showToast('Tài khoản không được để trống.');
        }
        else
            if (passwordlogin == "") {
                showToast('Mật khẩu không được để trống.');
            }
            else {
                disable('#btnlogin');
                $.ajax({
                    url: "/Accounts/Login",
                    data: JSON.stringify({ "usernamelogin": usernamelogin, "passwordlogin": passwordlogin }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    type: "POST",
                    success: function (data) {

                        if (data == "-1") {
                            showToast('Tài khoản hoặc mật khẩu không chính xác.');
                        }
                        else if (data == "0") {
                            showToast('Tài khoản đã bị khoá.Liên hệ admin để giải quyết.');
                        }
                        else {
                            location.href = '/';
                        }
                        enable('#btnlogin');

                    },
                    error: function (data) {

                        alert(JSON.stringify(data));
                    }
                })
            }

    }
    $('#btnlogin').click(login)
    $('#passwordlogin').keypress(function (e) {
        if (e.which == 13)
            login()
    })
    $('#usernamelogin').keypress(function (e) {
        if (e.which == 13)
            login()
    })
    function validateEmail($email) {
        var emailReg = /^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/;
        return emailReg.test($email);
    }
    function validatePhone($Phone) {
        var filter = /^((\+[1-9]{1,4}[ \-]*)|(\([0-9]{2,3}\)[ \-]*)|([0-9]{2,4})[ \-]*)*?[0-9]{3,4}?[ \-]*[0-9]{3,4}?$/;
        return filter.test($Phone);
    }
    function upLoad() {
        var fileUpload = $("#imageregister").get(0);
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
            url: '/Accounts/UploadImg', //URL to upload files 
            type: "POST", //as we will be posting files and other method POST is used
            processData: false, //remember to set processData and ContentType to false, otherwise you may get an error
            contentType: false,
            data: fileData,
            success: function (result) {
            },
            error: function (err) {
                alert(err.statusText);
            }
        });

    }
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
            showToast('Tên đăng nhập không được rỗng.');
        else
            if (acc.password == "")
                showToast('Mật khẩu không được rỗng.');

            else
                if (acc.email == "")
                    showToast('Email không được rỗng.');
                else
                    if (!validateEmail(acc.email))
                        showToast('Email không đúng định dạng.');
                    else
                        if (acc.fullname == "")
                            showToast('Họ tên không được rỗng.');
                        else
                            if (acc.phone == "")
                                showToast('Số điện thoại không được rỗng.');
                            else
                                if (!validatePhone(acc.phone))
                                    showToast('Số điện thoại không đúng định dạng.');
                                else
                                    if (acc.address == "")
                                        showToast('Địa chỉ không được rỗng.');

                                    else
                                        if (acc.password != prepassword)
                                            showToast('Mật khẩu không giống nhau.');
                                        else {
                                            disable('#btnregister');
                                            upLoad();
                                            $.ajax({
                                                url: "/Accounts/Register",
                                                data: JSON.stringify({ acc}),
                                                        contentType: "application/json; charset=utf-8",
                                                dataType: "json",
                                                type: "POST",
                                                success: function (data) {
                                                    console.log(data)
                                                    if (data == "-1")
                                                        showToast('Đăng ký không thành công.');
                                                    else
                                                        if (data == "0")
                                                            showToast('Tên tài khoản này đã được sử dụng.');
                                                        else
                                                            if (data == "2")
                                                                showToast('Email này đã được sử dụng.');
                                                            else
                                                                $("#myModal3").modal({ backdrop: "static" });
                                                    enable('#btnregister');

                                                },
                                                error: function (data) {

                                                    alert(JSON.stringify(data));
                                                }
                                            })
                                        }

    }
    $('#btnlogout').click(function (e) {
        e.preventDefault();
        $.ajax({
            url: '/Accounts/Logout',
            data: JSON.stringify({}),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            type: "POST",
            success: function (data) {
                console.log(data);
                if (data == 1)
                    location.href = '/';
            },
            error: function (data) {
                alert('Lỗi')
            }
        })
    })
    $('#btnregister').click(register);
    $('#btnclosexregister').click(function () {
        enable('#btnregister')
        clearRegister();
    })
    $('#btncloseregister').click(function () {
        enable('#btnregister')
        clearRegister();
    })
    function submit(operation) {
        console.log(operation);
        var code = "";

        var url = "/Accounts/";
        if (operation == "register") {
            code = $('#keyword').val();
            url += "CodeRegister";
        }
        else {
            code = $('#keywordforget').val();
            url += "CodeForget";
        }
        //
        if (code == "")
            showToast('Bạn chưa nhập mã xác nhập.');
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
                                showToast('Mã xác nhận không chính xác.');
                            }
                            else {
                                if (operation == "register") {
                                    showToast('Đăng ký thành công.');
                                    $('#btncloseregister').click();
                                }
                                else {
                                    showToast('Mời bạn check email lấy mã xác nhận.');
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

                    alert(JSON.stringify(data));
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
    function sendAgain() {
        disable('#btnsendagainregister');
        disable('#btnsendagainforgetpass');
        $.ajax({
            url: "/Accounts/SendAgain",
            data: JSON.stringify({}),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            type: "POST",
            success: function (data) {
                console.log(data)
                if (data == "0") {
                    showToast('Lỗi hệ thống không thể gửi mail.');
                }
                else {
                    showToast('Mời bạn check email để lấy mã xác nhận mới.Xin cảm ơn.');
                }
                enable('#btnsendagainregister');
                enable('#btnsendagainforgetpass');

            },
            error: function (data) {

                alert(JSON.stringify(data));
            }
        })
    }
    $('#btnsendagainregister').click(sendAgain);
    $('#btnfogetpass').click(function (e) {
        e.preventDefault();
        $("#myModal4").modal({ backdrop: "static" });
    });
    function confirmEmail() {
        var email = $('#keywordemail').val();
        if (email == "")
            showToast('Bạn chưa nhập email!');
        else
            if (!validateEmail(email))
                showToast('Email không đúng định dạng!');
            else {
                disable('#btnsubmitforget');
                $.ajax({
                    url: "/Accounts/ForgetPass",
                    data: JSON.stringify({ email}),
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            type: "POST",
                            success: function (data) {
                                console.log(data)
                                if (data == "-1")
                                    showToast('Lỗi hệ thống không thể gửi mail.');
                                else if (data == "1") {
                                    showToast('Mời bạn check email để lấy mã xác nhận mới.Xin cảm ơn.');
                                    $('#btncloseforget').click();
                                    $("#myModal5").modal({ backdrop: "static" });
                                }
                                else
                                    showToast('Không tồn tại email này.Bạn hãy kiểm tra lại email.');
                                enable('#btnsubmitforget');

                            },
                    error: function (data) {

                        alert(JSON.stringify(data));
                    }
                })
            }
    }
    $('#btnsubmitforget').click(function () {
        try {
            confirmEmail();
        } catch (e) {
            showToast('Có lỗi!!');
        }
    })
    $('#keywordemail').keypress(function (e) {
        if (e.which == 13)
            $('#btnsubmitforget').click()
    })
    $('#btnsubmitforgetpass').click(function () {
        try {
            submit("forget");
        } catch (e) {
            showToast('Có lỗi!!');
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
    $('#btnsubmitchangepass').click(function () {
        var passNew = $('#passnew').val();
        var prepass = $('#prepass').val();
        if (passNew == "")
            showToast('Mật khẩu mới không được rỗng!!');
        else
            if (prepass != passNew)
                showToast('Mật khẩu không giống nhau!!');

            else {
                disable('#btnsubmitchangepass');
                $.ajax({
                    url: "/Accounts/ChangePasswordEmail",
                    data: JSON.stringify({ passNew}),
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            type: "POST",
                            success: function (data) {
                                if (data == "-1")
                                    showToast('Lỗi hệ thống vui lòng thử lại sau!!');
                                else {
                                    showToast('Đổi mật khẩu thành công.');
                                    $('#btnclosechangepass').click();
                                }
                                enable('#btnsubmitchangepass');

                            },
                    error: function (data) {

                        alert(JSON.stringify(data));
                    }
                })
            }
    })
    $('#btnsendagainforgetpass').click(sendAgain);
    $('#btnchangepasswordlink').click(function (e) {
        e.preventDefault();
        $("#myModal7").modal({ backdrop: "static" });

    })
    $('#btnsubmitchangepassword').click(function () {
        var passold = $('#passoldchange').val();
        var passnew = $('#passnewchange').val();

        var prepass = $('#prepasschange').val();
        if (passold == "")
            showToast('Mật khẩu cũ không được rỗng!!');
        else
            if (passnew == "")
                showToast('Mật khẩu mới không được rỗng!!');
            else
                if (prepass == "")
                    showToast('Nhập lại mật khẩu không được rỗng!!');
                else {
                    disable('#btnsubmitchangepassword');
                    $.ajax({
                        url: "/Accounts/ChangePassword",
                        data: JSON.stringify({ passold, passnew, prepass}),
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                type: "POST",
                                success: function (data) {
                                    if (data == "-1")
                                        showToast('Mật khẩu cũ không chính xác!!!');
                                    else if (data == "1") {
                                        showToast('Đổi mật khẩu thành công!Vui lòng đăng nhập lại!!');
                                        $('#btnclosechangepassword').click();

                                        location.href = "/Accounts/Login";
                                        $('#btnlogout').click();
                                    }
                                    else
                                        if (data == "0")
                                            showToast('Nhập lại mật khẩu không giống nhau!!!');
                                        else
                                            showToast('Có lỗi xảy ra!!!!');

                                    enable('#btnsubmitchangepassword');

                                },
                        error: function (data) {

                            alert(JSON.stringify(data));
                        }
                    })
                }
    })
    $('#passoldchange').click(function () {
        $('#btnsubmitchangepassword').click()
    })
    $('#passnewchange').click(function () {
        $('#btnsubmitchangepassword').click()
    })
    $('#prepasschange').click(function () {
        $('#btnsubmitchangepassword').click()
    })
    $('.nice-select li').off('click').click(function () {
        var val = $(this).data('value');
        if (val == '1')
            $("#list-item .col-lg-4.col-md-4.col-sm-6").sort(desc_sortName).appendTo('#list-item');
        else
            if (val == '0')
                $("#list-item .col-lg-4.col-md-4.col-sm-6").sort(asc_sortName).appendTo('#list-item');
            else
                if (val == '2')
                    $("#list-item .col-lg-4.col-md-4.col-sm-6").sort(asc_sortPrice).appendTo('#list-item');
                else if (val == '3')
                    $("#list-item .col-lg-4.col-md-4.col-sm-6").sort(desc_sortPrice).appendTo('#list-item');

    })
    // Sắp xếp theo thứ tự giảm dần theo giá
    function asc_sortPrice(a, b) {
        var e = $(a).find('.product_price');
        var f = $(b).find('.product_price');
        return (e.text()) > (f.text()) ? 1 : -1;
    }
    // Sắp xếp theo thứ tự giảm dần theo giá
    function desc_sortPrice(a, b) {
        var e = $(a).find('.product_price');
        var f = $(b).find('.product_price');
        return (e.text()) < (f.text()) ? 1 : -1;
    }
    // Sắp xếp theo thứ tự giảm dần theo tên
    function desc_sortName(a, b) {
        var e = $(a).find('.product_title > a');
        var f = $(b).find('.product_title > a');

        return (e.text()) < (f.text()) ? 1 : -1;
    }
    // Sắp xếp theo thứ tự tăng dần theo tên dần theo tên
    function asc_sortName(a, b) {
        var e = $(a).find('.product_title > a');
        var f = $(b).find('.product_title > a');
        return (e.text()) > (f.text()) ? 1 : -1;
    }
    $('.addwish').off('click').click(function (e) {
        e.preventDefault();
        var ProductId = $(this).data('id');
        var Quantity = $(this).data('value');
        addWish(ProductId, Quantity);
    })
    $('#btnaddwish').click(function (e) {
        e.preventDefault();
        var ProductId = $(this).data('id');
        var Quantity = $('#txtquantity-detail').val();
        addWish(ProductId, Quantity);
    })
    function addWish(ProductId, Quantity) {
        console.log(ProductId)
        $.ajax({
            url: "/Wish/AddItem",
            data: JSON.stringify({ ProductId, Quantity }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    type: "POST",
                    success: function (data) {
                        console.log(data)
                        if (data.status == "1") {
                                showToast('Đã thêm vào yêu thích');
                                $('#wish-' +data.id + ' .quantity').text('SL:' +data.quantity);
                            $('#lst-wish .block_content p').text(`${data.sumQuantity} sản phẩm.`);
                        }
                        else if (data.status == "0") {
                            var html = `<div class="cart_item" id="wish-${data.id}">
                <div class="cart_img">
                    <a href="/chi-tiet/${data.alias}.html"><img src="../../assets/img/Nón bảo hiểm/${data.image}" alt=""></a>
                </div>
                <div class="cart_info">
                    <a href="/chi-tiet/${data.alias}.html">${data.name}</a>
                    <span class="cart_price">${data.price}</span>
                    <span class="quantity">SL: ${data.quantity}</span>
                </div>
                <div class="cart_remove">
                    <a title="Xoá sản phẩm" data-id="${data.id}" href="" class ="deletewish"><i class ="fa fa-times-circle"></i></a>
                </div>
            </div>`;
                            var deletewish = `<script> function deletewish(ProductId,check) {
            $.ajax({
                url: "/Wish/DeleteItem",
                data: JSON.stringify({ ProductId}),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        type: "POST",
                        success: function (data) {
                            if (data.status == "1") {
                                $('#wish-' +ProductId).hide(500);
                                $('#lst-wish .block_content p').text(data.sumQuantity+ ' sản phẩm.');
                                if(check)
                                showToast('Xoá sản phẩm thành công.');
                                if(data.sumQuantity==0)
                        {
                                    $('#wish').hide();
                                    $('.img-wish').show();
                        }
                        }
                        else
                                if(check)
                                showToast('Lỗi hệ thống khi xoá sản phẩm. Vui lòng thao tác lại sau.');

                        },
                            error: function (data) {

                    alert(JSON.stringify(data));
                        }

                        });
                            };
                              $('.deletewish').off('click').click(function (e) {
        e.preventDefault();
        if (confirm("Bạn chắc chắc xoá sản phẩm này?")) {
            var ProductId = $(this).data('id');
            deletewish(ProductId, true)
                            }
                            })
                            </script>`
                            showToast('Đã thêm vào yêu thích.');
                            $('#lst-wish').prepend(html);
                            $('#lst-wish').append(deletewish);
                            $('#lst-wish .block_content p').text(`${data.sumQuantity} sản phẩm.`);
                            $('.img-wish').hide();
                        }
                        else {
                            alert('Mời bạn đăng nhập');
                            location.href = "/dang-nhap.html";
                        }
                    },
            error: function (data) {

                alert(JSON.stringify(data));
            }
        });
    }
    $('.deletewish').off('click').click(function (e) {
        e.preventDefault();
        if (confirm("Bạn chắc chắc xoá sản phẩm này?")) {
            var ProductId = $(this).data('id');
            deletewish(ProductId, true)
        }
    })
    function deletewish(ProductId, check) {
        $.ajax({
            url: "/Wish/DeleteItem",
            data: JSON.stringify({ ProductId}),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    type: "POST",
                    success: function (data) {
                        if (data.status == "1") {
                            $('#wish-' +ProductId).hide(500);
                            $('#lst-wish .block_content p').text(`${data.sumQuantity} sản phẩm.`);
                            if(check)
                            showToast('Xoá sản phẩm thành công.');
                            if (data.sumQuantity == 0) {
                                $('#wish').hide();
                                $('.img-wish').show();
                            }
                        }
                        else
                            if (check)
                                showToast('Lỗi hệ thống khi xoá sản phẩm. Vui lòng thao tác lại sau.');

                    },
            error: function (data) {

                alert(JSON.stringify(data));
            }

        });
    }
    $('.addcart').off('click').click(function (e) {
        e.preventDefault();
        var ProductId = $(this).data('id');
        var Quantity = $(this).data('value');
        addCart(ProductId, Quantity);
    })
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

    function addCart(ProductId, Quantity) {
        $.ajax({
            url: "/Cart/AddItem",
            data: JSON.stringify({ ProductId, Quantity }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    type: "POST",
                    success: function (data) {
                        console.log(data)
                        if (data.status == "1") {
                                showToast('Đã thêm vào giỏ hàng.');
                                $('#cart-' +data.id + ' .quantity').text('Số lượng:' +data.quantity);
                                $('#lst-cart .prices').text(data.sumMoney);

                                if (location.pathname == '/yeu-thich.html')
                                    deletewish(ProductId, false);


                            $('.shopping_cart a span').text(`${data.sumQuantity} sản phẩm-${data.sumMoney}`);

                        }
                        else if (data.status == "0") {
                            var html = `<div class="cart_item cart-${data.id}">
                                                            <div class="cart_img">
                                                                <a href="/chi-tiet/${data.alias}.html"><img src="../../assets/img/Nón bảo hiểm/${data.image}" alt=""></a>
                                                            </div>
                                                            <div class="cart_info">
                                                                <a href="/chi-tiet/${data.alias}.html">${data.name}</a>
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
        if (confirm("Bạn chắc chắc xoá sản phẩm này?")) {
            var ProductId = $(this).data('id');
            deletecart(ProductId, true)
                            }
                            })
    function deletecart(ProductId, check) {
        $.ajax({
                                url: "/Cart/DeleteItem",
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
                            showToast('Xoá sản phẩm thành công.');
                            if (data.sumQuantity == 0) {
                                $('.img-cart').show();
                                $('#shipping-group').hide();
                                $('.shopping_cart_area').hide();


                            }
                            }
                            else
                            if (check)
                                showToast('Lỗi hệ thống khi xoá sản phẩm. Vui lòng thao tác lại sau.');

                            },
                            error: function (data) {

                alert(JSON.stringify(data));
                            }

                            });
                            }
                                </script>`;
                            showToast('Đã thêm vào giỏ hàng.');
                            $('#cart').prepend(html);
                            $('#lst-cart').append(script);
                            $('#lst-cart .prices').text(data.sumMoney);
                            $('.img-cart').hide();
                            $('#shipping-group').show();
                            $('.shopping_cart a span').text(`${data.sumQuantity} sản phẩm-${data.sumMoney}`);

                            if (location.pathname == '/yeu-thich.html')
                                deletewish(ProductId, false);

                        }
                        else {
                            alert('Mời bạn đăng nhập');
                            location.href = "/dang-nhap.html";
                        }
                    },
            error: function (data) {

                alert(JSON.stringify(data));
            }
        });
    }

    $('.deletecart').off('click').click(function (e) {
        e.preventDefault();
        e.stopPropagation();
        if (confirm("Bạn chắc chắc xoá sản phẩm này?")) {
            var ProductId = $(this).data('id');
            deletecart(ProductId, true)
        }
    })
    function deletecart(ProductId, check) {
        $.ajax({
            url: "/Cart/DeleteItem",
            data: JSON.stringify({ ProductId}),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    type: "POST",
                    success: function (data) {
                        if (data.status == "1") {
                            $('.cart-' +ProductId).hide(500);
                            $('.shopping_cart a span').text(`${data.sumQuantity} sản phẩm-${data.sumMoney}`);
                            $('#lst-cart .prices,.cart_amount.sum_money').text(data.sumMoney);
                            $('.cart_amount.sum_quantity').text(data.sumQuantity)
                            if(check)
                            showToast('Xoá sản phẩm thành công.');
                            if (data.sumQuantity == 0) {
                                $('.img-cart').show();
                                $('#shipping-group').hide();
                                $('.shopping_cart_area').hide();


                            }
                        }
                        else
                            if (check)
                                showToast('Lỗi hệ thống khi xoá sản phẩm. Vui lòng thao tác lại sau.');

                    },
            error: function (data) {

                alert(JSON.stringify(data));
            }

        });
    }
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
    $('#btncheckout,#cart .cart_info a,#cart .cart_img a').click(function (e) {
        e.stopPropagation();
    })
    $('.txtquantity-lstcart').change(function () {
        var ProductId = $(this).data('id');
        var Quantity = $(this).val();
        $.ajax({
            url: "/Cart/UpdateItem",
            data: JSON.stringify({ ProductId, Quantity}),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    type: "POST",
                    success: function (data) {
                        if (data.status == "1") {
                            if (Quantity <= 0)
                                $('.cart-' + ProductId).hide(500);
                           
                                $('.shopping_cart a span').text(`${data.sumQuantity} sản phẩm-${data.sumMoney}`);
                                $('#total-' + ProductId).text(data.total);
                                $('#lst-cart .prices,.cart_amount.sum_money').text(data.sumMoney);
                                $('.cart-' + ProductId + ' .cart_info .quantity').text('Số lượng:' + Quantity);
                                $('.cart_amount.sum_quantity').text(data.sumQuantity)
                            if (data.sumQuantity == 0) {
                                $('.img-cart').show();
                                $('#shipping-group').hide();
                                $('.shopping_cart_area').hide();
                            }
                        }
                        else
                                showToast('Lỗi hệ thống khi xoá sản phẩm. Vui lòng thao tác lại sau.');

                    },
            error: function (data) {

                alert(JSON.stringify(data));
            }

        });
    })
    function search()
    {
        var keyword = $('#txtkeyword').val();

        if (keyword == '')
            showToast('Mời bạn nhập vào từ khoá.');
        else {
            location.href = '/tim-kiem.html?tu-khoa=' + keyword;
        }
    }
    $('#btnsearch').click(search)
    $('#txtkeyword').keypress(function (e) {
        if (e.which == 13)
            search();
    })
})