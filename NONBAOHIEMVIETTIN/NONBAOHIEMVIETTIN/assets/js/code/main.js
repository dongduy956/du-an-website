$(function () {

    $('#btnSearch').click(function () {
        search()
    })
    $('#keyWord').keypress(function (e) {
        if (e.which == 13)
            search()
    })
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
    }
    )
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
    $('.addcart').off('click').click(function (e) {
        var ProductId = $(this).data('id');
        addCart(ProductId);
    })
    function addCart(ProductId) {

        console.log(ProductId)
        var Quantity = 1;
        $.ajax({
            url: "/Cart/AddItem",
            data: JSON.stringify({ ProductId, Quantity }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    type: "POST",
                    success: function (data) {
                        console.log(data)
                        if (data.status == "1") {
                            $('#cart-icon span').text(data.count);
                            alert('Đã thêm vào giỏ hàng');
            }
            else if (data.status == "0") {
                alert('Thêm thất bại');
            }
            else {
                alert('Mời bạn đăng nhập');
                location.href = "/Account/Login";
            }
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
    function search() {
        var keyWord = $('#keyWord').val();
        $.ajax({
            url: "/Home/Search",
            data: JSON.stringify({ "keyWord": keyWord }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            type: "POST",
            success: function (data) {
                var script = `<script>  function addCart(ProductId) {

                    console.log(ProductId)
                    var Quantity = 1;
                    $.ajax({
                        url: "/Cart/AddItem",
                        data: JSON.stringify({ ProductId, Quantity }),
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                type: "POST",
                                success: function (data) {
                                    console.log(data)
                                    if (data.status == "1") {
                                        $('#cart-icon span').text(data.count);
                                        alert('Đã thêm vào giỏ hàng');
                                    }
                                    else if (data.status == "0") {
                                        alert('Thêm thất bại');
                                    }
                                    else {
                                        alert('Mời bạn đăng nhập');
                                        location.href = "/Account/Login";
                                    }
                                },
                        error: function (data) {

                            alert(JSON.stringify(data));
                        }
                    });
              }
                </script>`;
                $('.lst-products').html("");
                var html = '';
                for (var i = 0; i < data.length; i++) {
                    html += `<div class="item-product col-lg-3 mt-1 mb-1 col-md-4 col-md-6 col-sm-12"><div class="card">
                <img class="card-img-top text-center" src="../../img/book/${data[i].Image}" alt="Card image" style="width:100%;height:300px;">
                <div class="card-body card-first">
                    <h4 class ="card-title">${data[i].Productname}</h4>
                    <p class ="card-text">${data[i].SKU}</p>
                    <p class ="card-text text-primary">${formatCurrency(data[i].Price, '.', ',')} đ</p>
                </div>
                <button data-id="${data[i].Id}" onclick='addCart(${data[i].Id})' class ="addcart btn btn-primary">Thêm giỏ hàng</button>
                <div class="card-body card-second">
                   <h4 class="card-title">Loại sách:${data[i].Name}</h4>
                    <p class="card-text">${data[i].Description}</p>
                    <p class="card-text text-primary">${data[i].Stock}</p>
                </div>
            </div>
        </div>`

                }
                if (keyWord == "")
                    $('.title-product').text('Products');
                else
                    $('.title-product').text(`Tìm thấy ${data.length} sách`);

                $('.lst-products').html(html);
                $('.main').append(script);

                console.log(data);
            },
            error: function (data) {
                alert(JSON.stringify(data));
            }
        })
    }
    $('.btn-delete-card').off('click').click(function (e) {
        if (confirm("Bạn chắc chắc xoá sản phẩm này?")) {
            var ProductId = $(this).data('id');
            $.ajax({
                url: "/Cart/DeleteItem",
                data: JSON.stringify({ ProductId}),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        type: "POST",
                        success: function (data) {
                            console.log(data)
                            if (data.status == "1") {
                                $('#cart-icon span').text(data.count == "0" ? "": data.count);
                                if (data.count == "0") {
                                    $('.img-cart').show();
                                    $('#cart').hide();
                                }
                                $('#' + ProductId).hide(500);
                                $('#summoney').text("Tổng tiền:" + data.summoney);
                            }
                            else
                                alert('Xoá thất bại');
                        },
                error: function (data) {

                    alert(JSON.stringify(data));
                }

            });
        }
    })
    $('.btn-update-card').off('click').click(function (e) {
        var ProductId = $(this).data('id');
        var Quantity = $('#quantity-' + ProductId).val();
        console.log(Quantity);
        $.ajax({
            url: "/Cart/UpdateItem",
            data: JSON.stringify({ ProductId, Quantity}),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    type: "POST",
                    success: function (data) {
                        console.log(data)
                        if (data.status == "1") {
                            alert('Cập nhật thành công');

                            $('#cart-icon span').text(data.count == "0" ? "": data.count);
                            if (data.count == "0") {
                                $('.img-cart').show();
                                $('#cart').hide();
                            }
                            if (Quantity == "0")
                                $('#' + ProductId).hide(500);
                            $('#subtotal-' + ProductId).text(data.subtotal);
                            $('#summoney').text("Tổng tiền:" + data.summoney);

                        }
                        else
                            alert('Cập nhật thất bại');
                    },
            error: function (data) {

                alert(JSON.stringify(data));
            }

        });
    })
    $('#btnpay').click(function (e) {

        $.ajax({
            url: "/Cart/Pay",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            type: "POST",
            success: function (data) {
                console.log(data)
                if (data == "1") {
                    alert('Thanh toán thành công');
                    $('.img-cart').show();
                    $('#cart').hide();
                    $('#cart-icon span').text("");

                }
                else
                    alert('Thành toán thất bại');
            },
            error: function (data) {

                alert(JSON.stringify(data));
            }

        });
    })
})