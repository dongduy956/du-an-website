var OAUTHURL = 'https://accounts.google.com/o/oauth2/auth?';
var VALIDURL = 'https://www.googleapis.com/oauth2/v1/tokeninfo?access_token=';
var SCOPE = 'https://www.googleapis.com/auth/userinfo.profile https://www.googleapis.com/auth/userinfo.email';
var CLIENTID = '302904279340-gqup44b82jtuobr4ig89568sl5pvcc85.apps.googleusercontent.com';
var REDIRECT = 'http://localhost:62429/Accounts/Login';
var LOGOUT = 'http://localhost:62429/Accounts/Login';
var TYPE = 'token';
var _url = OAUTHURL + 'scope=' + SCOPE + '&client_id=' + CLIENTID + '&redirect_uri=' + REDIRECT + '&response_type=' + TYPE;
var acToken;
var tokenType;
var expiresIn;
var user;
var loggedIn = false;

function login() {
    console.log(3)
    var win = window.open(_url, "windowname1", 'width=800, height=600');
    var pollTimer = window.setInterval(function () {
        try {
            console.log(win.document.URL);
            if (win.document.URL.indexOf(REDIRECT) != -1) {
                window.clearInterval(pollTimer);
                var url = win.document.URL;
                acToken = gup(url, 'access_token');
                tokenType = gup(url, 'token_type');
                expiresIn = gup(url, 'expires_in');

                win.close();
                validateToken(acToken);
            }
        }
        catch (e) {

        }
    }, 500);
}

function gup(url, name) {
    namename = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
    var regexS = "[\\#&]" + name + "=([^&#]*)";
    var regex = new RegExp(regexS);
    var results = regex.exec(url);
    if (results == null)
        return "";
    else
        return results[1];
}

function validateToken(token) {

    getUserInfo();
    $.ajax(
        {
            url: VALIDURL + token,
            data: null,
            success: function (responseText) {
            }, error: function (data) {
                alert(JSON.stringify(data));
            }
        });
}
function getUserInfo() {


    $.ajax({

        url: 'https://www.googleapis.com/oauth2/v1/userinfo?access_token=' + acToken,
        data: null,
        success: function (resp) {
            user = resp;
            $.ajax({

                url: '/Accounts/LoginGoogle/',
                type: 'POST',
                data: {
                    username: user.email,
                    email: user.email,
                    fullname: user.name,
                    image: user.picture
                },
                success: function (data) {
                if(data.status==1)
                    location.href = "/";
                else
                {
                    showToast(data.message);
                    location.href = "/dang-nhap.html";
                }
                }, error: function (data) {
                    alert(JSON.stringify(data));
                }

                //dataType: "jsonp"

            });

        }, error: function (data) {

            alert(JSON.stringify(data));
        }


    })
   


}

