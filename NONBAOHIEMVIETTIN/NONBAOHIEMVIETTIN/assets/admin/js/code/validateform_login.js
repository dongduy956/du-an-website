//hàm xử lý nhớ mật khẩu
function checkRember() {
    let check = JSON.parse(window.localStorage.getItem('admin'));
    if (check != null) {
        $('#usernameadmin').val(check.username);
        $('#passwordadmin').val(check.password);
        $('#customCheck').prop('checked', true);
    }
    else {
        $('#usernameadmin').val('');
        $('#passwordadmin').val('');
        $('#customCheck').prop('checked', false);
    }
}
$(window).on('load', function (event) {
    //kiểm tra nhớ mật khẩu
    checkRember();
});
//hàm xử lý login
function login() {
    var accLogin = new Object();
    accLogin.username = $('#usernameadmin').val();
    accLogin.password = $('#passwordadmin').val();
    var recaptcha = $("#g-recaptcha-response-1").val();
    disable('.btn-user');
    $.ajax({
        url: "/Login/Login",
        data: JSON.stringify({ accLogin, recaptcha}),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        type: "POST",
        success: function (data) {
            if (data.status != 1) {
                iziToast.error({
                    timeout: 1500,
                    title: 'Lỗi',
                    message: data.message,
                    position: 'topRight'
                });
                if (data.status != -2)
                    grecaptcha.reset();
            }
            else
                if (data.status == 1) {
                    if ($('#customCheck').is(":checked"))
                        window.localStorage.setItem('admin',
                            JSON.stringify({
                                "username": accLogin.username,
                                "password": accLogin.password
                            }));
                    else
                        window.localStorage.removeItem('admin');
                    location.href = '/';
                }
            enable('.btn-user');
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
$(function () {
    //validate và xử lý đăng nhập
    $("form[name='login']").validate({
        rules: {
            usernameadmin: {
                required: true,
                minlength: 5
            },
            passwordadmin: {
                required: true,
                minlength: 5
            }
        },
        messages: {
            usernameadmin: {
                required: "Tên đăng nhập không được để trống.",
                minlength: "Tên đăng nhập phải dài ít nhất 5 kí tự."
            },
            passwordadmin: {
                required: "Mật khẩu không được để trống.",
                minlength: "Mật khẩu phải dài ít nhất 5 kí tự."
            }
        },
        submitHandler: function (form) {
            login();
        }
    });
})