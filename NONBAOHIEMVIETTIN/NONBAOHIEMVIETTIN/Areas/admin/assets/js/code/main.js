$(function () {
    if (!($('#sidebar').css('display') == 'none' || $('#sidebar').css("visibility") == "hidden")) {
        $('#sidebarToggle').click();
    }
    function login()
    {
        var accLogin = new Object();
        accLogin.username = $('#usernameadmin').val();
        accLogin.password = $('#passwordadmin').val();
        if (accLogin.username == '')
            showToast('Tài khoản không được rỗng');
        else
            if (accLogin.password == '')
                showToast('Mật khẩu không được rỗng');
            else
                $.ajax({
                    url: "/admin/Login/Login",
                    data: JSON.stringify(accLogin),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    type: "POST",
                    success: function (data) {

                        showToast(data.message);
                        if (data.status == 1)
                            location.href = '/admin';

                    },
                    error: function (data) {

                        alert(JSON.stringify(data));
                    }
                })
    }
    $('.btn-user').click(login)
    $('#usernameadmin').keypress(function (e) {
        if (e.which == 13)
            login()
    })
    $('#passwordadmin').keypress(function (e) {
        if (e.which == 13)
            login()
    })
    $('#btnlogout').click(function () {
       location.href="/admin/Login/Logout"
    })
    function delete_product(id,ele) {
        $.ajax({
            url: "/admin/products_admin/delete_product/"+id,
            data: JSON.stringify(id),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            type: "POST",
            success: function (data) {

                showToast(data.message);
                if (data.status == 1) {
                    ele.attr('style', 'display:none !important');
                    $(`#_product_${id}-delete`).text('Đã xoá');
                    $(`#_product_${id}-delete-detail`).text('Đã xoá');
                }

            },
            error: function (data) {

                alert(JSON.stringify(data));
            }
        })
    }
    $('.delete_product').off('click').click(function (e) {
        e.preventDefault();
        var id = Number($(this).data('id'));
        var ele = $(this);
        alertify.confirm('Thông báo', 'Bạn chắc chắn xoá sản phẩm này?', function () {
            delete_product(id,ele);
        }, function () { alertify.error('Huỷ') });
       
    });

    function delete_category(id,ele) {
        $.ajax({
            url: "/admin/category_admin/delete_category/" + id,
            data: JSON.stringify(id),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            type: "POST",
            success: function (data) {

                showToast(data.message);
                if (data.status == 1) {
                    ele.attr('style', 'display:none !important');
                    $(`#_category_${id}-delete`).text('Đã xoá');
                }

            },
            error: function (data) {

                alert(JSON.stringify(data));
            }
        })
    }
    $('.delete_category').off('click').click(function (e) {
        e.preventDefault();
        var id = Number($(this).data('id'));
        var ele = $(this);
        alertify.confirm('Thông báo', 'Bạn chắc chắn xoá loại nón này?', function () {
            delete_category(id,ele);
        }, function () { alertify.error('Huỷ') });

    });

    function delete_production(id,ele) {
        $.ajax({
            url: "/admin/production_admin/delete_production/" + id,
            data: JSON.stringify(id),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            type: "POST",
            success: function (data) {

                showToast(data.message);
                if (data.status == 1) {
                    ele.attr('style', 'display:none !important');
                    $(`#_production_${id}-delete`).text('Đã xoá');
                }

            },
            error: function (data) {

                alert(JSON.stringify(data));
            }
        })
    }
    $('.delete_production').off('click').click(function (e) {
        e.preventDefault();
        var id = Number($(this).data('id'));
        var ele = $(this);

        alertify.confirm('Thông báo', 'Bạn chắc chắn xoá hãng sản xuất này?', function () {
            delete_production(id,ele);
        }, function () { alertify.error('Huỷ') });

    });

    function delete_groupproduct(id,ele) {
        $.ajax({
            url: "/admin/GroupProduct_admin/delete_groupProduct/" + id,
            data: JSON.stringify(id),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            type: "POST",
            success: function (data) {

                showToast(data.message);
                if (data.status == 1) {
                    ele.attr('style', 'display:none !important');
                    $(`#_group-product_${id}-delete`).text('Đã xoá');
                }

            },
            error: function (data) {

                alert(JSON.stringify(data));
            }
        })
    }
    $('.delete_group-product').off('click').click(function (e) {
        e.preventDefault();
        var id = Number($(this).data('id'));
        var ele = $(this);

        alertify.confirm('Thông báo', 'Bạn chắc chắn xoá nhóm nón này?', function () {
            delete_groupproduct(id,ele);
        }, function () { alertify.error('Huỷ') });

    });

    function delete_account(id, ele) {
        $.ajax({
            url: "/admin/Accounts_admin/delete_account/" + id,
            data: JSON.stringify(id),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            type: "POST",
            success: function (data) {

                showToast(data.message);
                if (data.status == 1) {
                    ele.attr('style', 'display:none !important');
                    $(`#_account_${id}-delete`).text('Khoá');
                    $(`#_account_${id}-delete-detail`).text('Khoá');

                }

            },
            error: function (data) {

                alert(JSON.stringify(data));
            }
        })
    }
    $('.delete_account').off('click').click(function (e) {
        e.preventDefault();
        var id = Number($(this).data('id'));
        var ele = $(this);

        alertify.confirm('Thông báo', 'Bạn chắc chắn xoá tài khoản này?', function () {
            delete_account(id, ele);
        }, function () { alertify.error('Huỷ') });

    });

})


