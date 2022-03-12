window.fbAsyncInit = function () {
 
};

function loginFB() {
    FB.api('me?fields=first_name,middle_name,last_name,id,email,picture', function (responses) {
        let acc = new Object();
        const email = responses.email;

        const userName = responses.email;
        
        const image = responses.picture.data.url;
        const fullname = responses.first_name + ' ' + responses.middle_name + ' ' + responses.last_name;
        acc.email = email;
        acc.username = email;
        acc.fullname = fullname;
        acc.image = image;
        $.ajax({
            url: '/dang-nhap-facebook',
            type: 'POST',
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ acc }),
            success: function (response) {

                if (response.status) {
                    location.href = '/';
                }
                else {
                    $.notify(response.message, 'error');
                }
            },
            error: function (data) {
                alert(JSON.stringify(data));
            }
        });
    });
}
$(function () {
    $('#btnloginface').click(function (e) {
        e.preventDefault();
        FB.init({
            appId: '2844490499183619',
            cookie: true,
            xfbml: true,
            version: 'v13.0'
        });
        FB.login(loginFB, { scope: 'email,public_profile', return_scopes: true });
    })
})
