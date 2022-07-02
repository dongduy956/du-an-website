//hàm xử lý nhớ mật khẩu
function checkRember()
{
    console.log('remember');
    let check = JSON.parse(window.localStorage.getItem('user'));
    if(check!=null)
    {
        $('#usernamelogin').val(check.username);
        $('#passwordlogin').val(check.password);
        $('#remember').attr('checked', true);

    }
    else
    {
        $('#usernamelogin').val('');
        $('#passwordlogin').val('');
        $('#remember').attr('checked', false);
    }
}
///*Hàm xử lý login từ tài khoản đăng kí*/
function loginWeb() {
    let usernamelogin = $('#usernamelogin').val();
    let passwordlogin = $('#passwordlogin').val();
    let recaptcha = $("#g-recaptcha-response-1").val();
    disable('#btnlogin');
    $.ajax({
        url: "/dang-nhap",
        data: JSON.stringify({ "usernamelogin": usernamelogin, "passwordlogin": passwordlogin, recaptcha }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        type: "POST",
        success: function (data) {
            if (data == "-1") {
                iziToast.error({
                    timeout: 1500,
                    title: 'Lỗi',
                    message: 'Tài khoản hoặc mật khẩu không chính xác.',
                    position: 'topRight'
                });
                grecaptcha.reset();

            }
            else if (data == "0") {
                grecaptcha.reset();
                iziToast.error({
                    timeout: 1500,
                    title: 'Lỗi',
                    message: 'Tài khoản đã bị khoá.Liên hệ admin để giải quyết.',
                    position: 'topRight'
                });
            }
            else
                if (data == "-2")
                    iziToast.error({
                        timeout: 1500,
                        title: 'Lỗi',
                        message: 'Bạn chưa xác nhận không phải là người máy.',
                        position: 'topRight'
                    });
                else {
                    if ($('#remember').is(":checked")) 
                        window.localStorage.setItem('user',
                            JSON.stringify({
                                "username": usernamelogin,
                                "password": passwordlogin                                
                            }));
                     else                    
                        window.localStorage.removeItem('user');                    
                    location.href = '/';
                }
            enable('#btnlogin');

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
    let acc = new Object();
    acc.username = $('#usernameregister').val();
    acc.password = $('#passwordregister').val();
    let prepassword = $('#prepasswordregister').val();
    acc.email = $('#emailregister').val();
    acc.phone = $('#phoneregister').val();
    acc.address = $('#addressregister').val();
    acc.image = $('#imageregister').val().split("\\").pop();
    acc.fullname = $('#fullnameregister').val();
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
                iziToast.error({
                    timeout:1500,
                    title: 'Lỗi',
                    message: 'Đăng ký không thành công.',
                    position:'topRight'
                });
            else
                if (data == "0")
                    iziToast.error({
                        timeout:1500,
                        title: 'Lỗi',
                        message: 'Tên tài khoản này đã được sử dụng.',
                        position:'topRight'
                    });

                else
                    if (data == "2")
                        iziToast.error({
                            timeout:1500,
                            title: 'Lỗi',
                            message: 'Email này đã được sử dụng.',
                            position:'topRight'
                        });
                    else
                        $("#myModal3").modal({ backdrop: "static" });
            enable('#btnregister');

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
    let code = "";
    let url = "";
    if (operation == "register") {
        code = $('#keyword').val();
        url += "ma-dang-ki";
    }
    else {
        code = $('#keywordforget').val();
        url += "ma-quen-mat-khau";
    }
    if (code == "")
        iziToast.warning({
            timeout:1500,
            title: 'Cảnh báo',
            message: 'Bạn chưa nhập mã xác nhập.',
            position:'topRight'
        });
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
                    iziToast.error({
                        timeout: 1500,
                        title: 'Lỗi',
                        message: 'Mã xác nhận không chính xác.',
                        position: 'topRight'
                    });
                }
                else {
                    if (operation == "register") {
                        iziToast.success({
                            timeout: 1500,
                            title: 'Thành công',
                            message: 'Đăng kí thành công.',
                            position: 'topRight'
                        });
                        $('#btncloseregister').click();
                    }
                    else {
                        iziToast.success({
                            timeout: 1500,
                            title: 'Thành công',
                            message: 'Mời bạn đổi mật khẩu mới.',
                            position: 'topRight'
                        });
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
                iziToast.error({
                    timeout: 1500,
                    title: 'Lỗi',
                    message: 'Lỗi hệ thống không thể gửi mail.',
                    position: 'topRight'
                });
            else
                iziToast.info({
                    timeout: 1500,
                    title: 'Thông tin',
                    message: 'Mời bạn check email để lấy mã xác nhận mới.Xin cảm ơn.',
                    position: 'topRight'
                });
            enable('#btnsendagainregister');
            enable('#btnsendagainforgetpass');

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
$('#btnsendagainregister').click(sendAgain);
//Bật modal cho quên mật khẩu
$('#btnfogetpass').click(function (e) {
    e.preventDefault();
    $("#myModal4").modal({ backdrop: "static" });
});
//Nhập email lấy lại mật khẩu
function confirmEmail() {
    const email = $('#keywordemail').val();

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
                iziToast.error({
                    timeout: 1500,
                    title: 'Lỗi',
                    message: 'Lỗi hệ thống không thể gửi mail.',
                    position: 'topRight'
                });
            else if (data == "1") {
                iziToast.info({
                    timeout: 1500,
                    title: 'Thông tin',
                    message: 'Mời bạn check email để lấy mã xác nhận mới.Xin cảm ơn.',
                    position: 'topRight'
                });
                        $('#btncloseforget').click();
                        $("#myModal5").modal({ backdrop: "static" });
                    }
            else
                iziToast.error({
                    timeout: 1500,
                    title: 'Lỗi',
                    message: 'Không tồn tại email này.Bạn hãy kiểm tra lại email.',
                    position: 'topRight'
                });
                    enable('#btnsubmitforget');

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
$(window).on('load', function (event) {
    //kiểm tra nhớ mật khẩu
    checkRember();
});
$(function () {
    
    //custom validate phone
    jQuery.validator.addMethod('phone', function (value, element) {
        return validatePhone(element.value)
    });
    //validate và xử lý đăng nhập
    $("form[name='login']").validate({
        rules: {
            usernamelogin: {
                required: true,
                minlength: 5
            },
            passwordlogin: {
                required: true,
                minlength: 5
            }
        },
        messages: {
            usernamelogin: {
                required: "Tên đăng nhập không được để trống.",
                minlength: "Tên đăng nhập phải dài ít nhất 5 kí tự."
            },
            passwordlogin: {
                required: "Mật khẩu không được để trống.",
                minlength: "Mật khẩu phải dài ít nhất 5 kí tự."
            }
        },
        submitHandler: function (form) {
            loginWeb();
        }
    });
    //validate và xử lý đăng kí
    $("form[name='register']").validate({
        rules: {
            usernameregister: {
                required: true,
                minlength: 5
            },
            passwordregister: {
                required: true,
                minlength: 5
            },
            prepasswordregister: {
                required: true,
                minlength: 5,
                equalTo: "#passwordregister"
            },
            emailregister: {
                required: true,
                email: true
            },
            fullnameregister: {
                required: true
            },
            addressregister: {
                required: true
            },
            phoneregister: {
                required: true,
                phone: true
            }
        },
        messages: {
            usernameregister: {
                required: "Tên đăng nhập không được để trống.",
                minlength: "Tên đăng nhập phải dài ít nhất 5 kí tự."
            },
            passwordregister: {
                required: "Mật khẩu không được để trống.",
                minlength: "Mật khẩu phải dài ít nhất 5 kí tự."
            },
            prepasswordregister: {
                required: "Nhập lại mật khẩu không được để trống.",
                minlength: "Nhập lại mật khẩu phải dài ít nhất 5 kí tự.",
                equalTo: "Nhập lại mật khẩu không giống nhau."
            },
            emailregister: {
                required: "Email không được để trống.",
                email: "Email không đúng định dạng."
            },
            fullnameregister: {
                required: "Họ tên không được để trống."
            },
            addressregister: {
                required: "Địa chỉ không được để trống."
            },
            phoneregister: {
                required: "Số điện thoại không được để trống.",
                phone: "Số điện thoại không đúng định dạng."
            }
        },
        submitHandler: function (form) {
            register();
        }
    });


    //Xác nhận đúng email để lấy mật khẩu
    $("form[name='forgetpassemail']").validate({
        rules: {
            keywordemail: {
                required: true,
                email: true
            }
        },
        messages: {
            keywordemail: {
                required: "Email không được để trống.",
                email: "Email không đúng định dạng."
            }
        },
        submitHandler: function (form) {
            confirmEmail();
        }
    });
    //Xác nhận mã từ gmail quên mật khẩu
    $("form[name='submitforgetpass']").validate({
        rules: {
            keywordforget: {
                required: true
            }
        },
        messages: {
            keywordforget: {
                required: "Mã xác nhận không được để trống."
            }
        },
        submitHandler: function (form) {
            submit("forget");
        }
    });
    //Xác nhận mã từ gmail khi đăng kí
    $("form[name='submitregister']").validate({
        rules: {
            keywordregister: {
                required: true
            }
        },
        messages: {
            keywordregister: {
                required: "Mã xác nhận không được để trống."
            }
        },
        submitHandler: function (form) {
            submit("register");
        }
    });
   
    //Đổi mật khẩu từ email quên mật khẩu
    $("form[name='submitchangepass']").validate({
        rules: {
            passnew: {
                required: true,
                minlength: 5
            },
            prepass: {
                required: true,
                minlength: 5,
                equalTo: "#passnew"
            }
        },
        messages: {
            passnew: {
                required: "Mật khẩu mới không được để trống.",
                minlength: "Mật khẩu mới phải dài ít nhất 5 kí tự."
            },
            prepass: {
                required: "Nhập lại mật khẩu không được để trống.",
                minlength: "Nhập lại mật khẩu phải dài ít nhất 5 kí tự.",
                equalTo: "Nhập lại mật khẩu không giống nhau."
            }
        },
        submitHandler: function (form) {
            changePassEmail();
        }
    });
    function changePassEmail() {
        let passNew = $('#passnew').val();
        let prepass = $('#prepass').val();

        disable('#btnsubmitchangepass');
        $.ajax({
            url: "/doi-mat-khau-email",
            data: JSON.stringify({ passNew }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    type: "POST",
                    success: function (data) {
                        if (data == "-1")
                            iziToast.error({
                                timeout: 1500,
                                title: 'Lỗi',
                                message: 'Lỗi hệ thống vui lòng thử lại sau.',
                                position: 'topRight'
                            });
            else
                        {
                            iziToast.success({
                                timeout: 1500,
                                title: 'Thành công',
                                message: 'Đổi mật khẩu thành công.',
                                position: 'topRight'
                            });
                            $('#btnclosechangepass').click();
            }
                        enable('#btnsubmitchangepass');

            },
                error: function (data) {
                    enable('#btnsubmitchangepass');
                    iziToast.error({
                        timeout: 1500,
                        title: 'Lỗi',
                        message: 'Lỗi chưa xác định.',
                        position: 'topRight'
                    });
                }
        })
    }
    $('#btnsendagainforgetpass').click(sendAgain);

})