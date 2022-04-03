function recaptchaCallback() {
    var mq = window.matchMedia("(max-width: 450px)");
    mq.addListener(recaptchaRenderer);
    recaptchaRenderer(mq);
}
function recaptchaRenderer(mq) {
    var recaptcha = $('.g-recaptcha').eq(0);
    var data = recaptcha.data();
    var parent = recaptcha.parent();

    recaptcha.empty().remove();

    var recaptchaClone = recaptcha.clone();
    parent.append(recaptchaClone);
    recaptchaClone.data(data);

    var options = {
        'sitekey': data['sitekey'],
        'size': 'compact'
    };
    if (!mq.matches) {
        options['size'] = 'normal';
    }
    grecaptcha.render(recaptchaClone.get(0), options);
}
