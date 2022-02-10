$(function () {
    if (!($('#sidebar').css('display') == 'none' || $('#sidebar').css("visibility") == "hidden")) {
        $('#sidebarToggle').click();
    }
    function login() {
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
        location.href = "/admin/Login/Logout"
    })
    function delete_product(id, ele) {
        $.ajax({
            url: "/admin/products_admin/delete_product/" + id,
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
            delete_product(id, ele);
        }, function () { alertify.error('Huỷ') });

    });

    function delete_category(id, ele) {
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
            delete_category(id, ele);
        }, function () { alertify.error('Huỷ') });

    });

    function delete_production(id, ele) {
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
            delete_production(id, ele);
        }, function () { alertify.error('Huỷ') });

    });

    function delete_groupproduct(id, ele) {
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
            delete_groupproduct(id, ele);
        }, function () { alertify.error('Huỷ') });

    });


    function delete_role(id) {
        $.ajax({
            url: "/admin/Role_admin/delete_role/" + id,
            data: JSON.stringify(id),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            type: "POST",
            success: function (data) {

                showToast(data.message);
                if (data.status == 1) {
                    $(`#_role_${id}`).hide(200);
                }

            },
            error: function (data) {

                alert(JSON.stringify(data));
            }
        })
    }
    $('.delete_role').off('click').click(function (e) {
        e.preventDefault();
        var id = Number($(this).data('id'));

        alertify.confirm('Thông báo', 'Bạn chắc chắn xoá quyền này?', function () {
            delete_role(id);
        }, function () { alertify.error('Huỷ') });

    });

    function delete_account(id) {
        $.ajax({
            url: "/admin/Accounts_admin/delete_account/" + id,
            data: JSON.stringify(id),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            type: "POST",
            success: function (data) {

                showToast(data.message);
                if (data.status == 1)
                    $(`#_account_${id}`).hide(200);


            },
            error: function (data) {

                alert(JSON.stringify(data));
            }
        })
    }
    $('.delete_account').off('click').click(function (e) {
        e.preventDefault();
        var id = Number($(this).data('id'));
        alertify.confirm('Thông báo', 'Bạn chắc chắn xoá tài khoản này?', function () {
            delete_account(id);
        }, function () { alertify.error('Huỷ') });

    });


    function reset_password(id) {
        $.ajax({
            url: "/admin/Accounts_admin/resetPassword/" + id,
            data: JSON.stringify(id),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            type: "POST",
            success: function (data) {

                showToast(data.message);

            },
            error: function (data) {

                alert(JSON.stringify(data));
            }
        })
    }
    $('.reset-password').off('click').click(function (e) {
        e.preventDefault();
        var id = Number($(this).data('id'));
        alertify.confirm('Thông báo', 'Bạn chắc chắn reset mật khẩu tài khoản này?', function () {
            reset_password(id);
        }, function () { alertify.error('Huỷ') });

    });

    function delete_newstype(id) {
        $.ajax({
            url: "/admin/Newstype_admin/delete_newstype/" + id,
            data: JSON.stringify(id),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            type: "POST",
            success: function (data) {

                showToast(data.message);
                if (data.status == 1) {
                    $(`#_newstype_${id}`).hide(200);
                }

            },
            error: function (data) {

                alert(JSON.stringify(data));
            }
        })
    }
    $('.delete_newstype').off('click').click(function (e) {
        e.preventDefault();
        var id = Number($(this).data('id'));

        alertify.confirm('Thông báo', 'Bạn chắc chắn xoá loại tin này?', function () {
            delete_newstype(id);
        }, function () { alertify.error('Huỷ') });

    });


    function delete_news(id) {
        $.ajax({
            url: "/admin/News_admin/delete_news/" + id,
            data: JSON.stringify(id),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            type: "POST",
            success: function (data) {

                showToast(data.message);
                if (data.status == 1) {
                    ele.attr('style', 'display:none !important');
                    $(`#_news_${id}`).hide(200);
                }

            },
            error: function (data) {

                alert(JSON.stringify(data));
            }
        })
    }
    $('.delete_news').off('click').click(function (e) {
        e.preventDefault();
        var id = Number($(this).data('id'));

        alertify.confirm('Thông báo', 'Bạn chắc chắn xoá tin này?', function () {
            delete_news(id);
        }, function () { alertify.error('Huỷ') });

    });


    function delete_introduce(id) {
        $.ajax({
            url: "/admin/Introduce_admin/delete_introduce/" + id,
            data: JSON.stringify(id),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            type: "POST",
            success: function (data) {

                showToast(data.message);
                if (data.status == 1) {
                    $(`#_introduce_${id}`).hide(200);
                }

            },
            error: function (data) {

                alert(JSON.stringify(data));
            }
        })
    }
    $('.delete_introduce').off('click').click(function (e) {
        e.preventDefault();
        var id = Number($(this).data('id'));

        alertify.confirm('Thông báo', 'Bạn chắc chắn xoá giới thiệu này?', function () {
            delete_introduce(id);
        }, function () { alertify.error('Huỷ') });

    });


    function delete_contact(id) {
        $.ajax({
            url: "/admin/Contact_admin/delete_contact/" + id,
            data: JSON.stringify(id),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            type: "POST",
            success: function (data) {

                showToast(data.message);
                if (data.status == 1) {
                    $(`#_contact_${id}`).hide(200);
                }

            },
            error: function (data) {

                alert(JSON.stringify(data));
            }
        })
    }
    $('.delete_contact').off('click').click(function (e) {
        e.preventDefault();
        var id = Number($(this).data('id'));
        alertify.confirm('Thông báo', 'Bạn chắc chắn xoá liên hệ này?', function () {
            delete_contact(id);
        }, function () { alertify.error('Huỷ') });

    });



    function delete_feedback(id) {
        $.ajax({
            url: "/admin/Feedback_admin/delete_feedback/" + id,
            data: JSON.stringify(id),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            type: "POST",
            success: function (data) {

                showToast(data.message);
                if (data.status == 1) {
                    $(`#_feedback_${id}`).hide(200);
                }

            },
            error: function (data) {

                alert(JSON.stringify(data));
            }
        })
    }
    $('.delete_feedback').off('click').click(function (e) {
        e.preventDefault();
        var id = Number($(this).data('id'));
        alertify.confirm('Thông báo', 'Bạn chắc chắn xoá phản hồi này?', function () {
            delete_feedback(id);
        }, function () { alertify.error('Huỷ') });

    });


    function delete_subscribe(id) {
        $.ajax({
            url: "/admin/Subscribe_admin/delete_subscribe/" + id,
            data: JSON.stringify(id),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            type: "POST",
            success: function (data) {

                showToast(data.message);
                if (data.status == 1) {
                    $(`#_subscribe_${id}`).hide(200);
                }

            },
            error: function (data) {

                alert(JSON.stringify(data));
            }
        })
    }
    $('.delete_subscribe').off('click').click(function (e) {
        e.preventDefault();
        var id = Number($(this).data('id'));
        alertify.confirm('Thông báo', 'Bạn chắc chắn xoá đăng kí này?', function () {
            delete_subscribe(id);
        }, function () { alertify.error('Huỷ') });

    });


    function delete_rate(id) {
        $.ajax({
            url: "/admin/Rate_admin/delete_rate/" + id,
            data: JSON.stringify(id),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            type: "POST",
            success: function (data) {

                showToast(data.message);
                if (data.status == 1) {
                    $(`#_rate_${id}`).hide(200);
                }

            },
            error: function (data) {

                alert(JSON.stringify(data));
            }
        })
    }
    $('.delete_rate').off('click').click(function (e) {
        e.preventDefault();
        var id = Number($(this).data('id'));
        alertify.confirm('Thông báo', 'Bạn chắc chắn xoá đánh giá này?', function () {
            delete_rate(id);
        }, function () { alertify.error('Huỷ') });

    });
    function delete_order(id) {
        $.ajax({
            url: "/admin/Order_admin/delete_order/" + id,
            data: JSON.stringify(id),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            type: "POST",
            success: function (data) {

                showToast(data.message);
                if (data.status == 1) {
                    $(`#_order_${id}`).hide(200);
                }

            },
            error: function (data) {

                alert(JSON.stringify(data));
            }
        })
    }
    $('.delete_order').off('click').click(function (e) {
        e.preventDefault();
        var id = Number($(this).data('id'));
        alertify.confirm('Thông báo', 'Bạn chắc chắn xoá đơn hàng này?', function () {
            delete_order(id);
        }, function () { alertify.error('Huỷ') });

    });
    function confirm_order(id) {
        $.ajax({
            url: "/admin/Order_admin/confirm_order/" + id,
            data: JSON.stringify(id),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            type: "POST",
            success: function (data) {

                showToast(data.message);
                if (data.status == 1) {
                    $('#confirm_order_detail_' + id + ',#confirm_order_' + id).removeClass('text-danger');
                    $('#confirm_order_detail_' + id + ',#confirm_order_' + id).addClass('text-success');
                    $('#confirm_order_detail_' + id + ',#confirm_order_' + id).text('Đã duyệt');
                }

            },
            error: function (data) {

                alert(JSON.stringify(data));
            }
        })
    }

    $('.confirm_order').off('click').click(function (e) {
        e.preventDefault();
        var id = Number($(this).data('id'));
        alertify.confirm('Thông báo', 'Bạn chắc chắn duyệt đơn hàng này?', function () {
            confirm_order(id);
        }, function () { alertify.error('Huỷ') });

    });


    function transfer_order(id) {
        $.ajax({
            url: "/admin/Order_admin/transfer_order/" + id,
            data: JSON.stringify(id),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            type: "POST",
            success: function (data) {

                showToast(data.message);
                if (data.status == 1) {
                    $('#transfer_order_detail_' + id + ',#transfer_order_' + id).removeClass('text-danger');
                    $('#transfer_order_detail_' + id + ',#transfer_order_' + id).addClass('text-success');
                    $('#transfer_order_detail_' + id + ',#transfer_order_' + id).text('Đã chuyển tiền');
                }

            },
            error: function (data) {

                alert(JSON.stringify(data));
            }
        })
    }

    $('.transfer_order').off('click').click(function (e) {
        e.preventDefault();
        var id = Number($(this).data('id'));
        alertify.confirm('Thông báo', 'Bạn chắc chắn thanh toán đơn hàng này?', function () {
            transfer_order(id);
        }, function () { alertify.error('Huỷ') });

    });
    var lstreceiptdetail = new Array();
    function loadReceipt() {
        lstreceiptdetail = window.sessionStorage.getItem('receipt') == null ? [] : JSON.parse(window.sessionStorage.getItem('receipt'));

        var html = '';
        $.each(lstreceiptdetail, function (index, item) {
            html += `
             <tr id='${item.idproduct}'>
                                <td>${item.nameproduct}</td>
                                <td>${item.price}</td>
                                <td><input value='${item.quantity}' data-id="${item.idproduct}" type='number' class ='quantity-input form-control' min='0' max='100'/></td>
                                <td id='subtotal_${item.idproduct}'>${item.subtotal}</td>
                                <td>
                                <div class ="tool d-flex align-items-center flex-column justify-content-center">
                                    <a data-id="${item.idproduct}" title="Xoá sản phẩm ${item.nameproduct}" class ="mt-1 mb-1 delete_receipt-detail d-sm-inline-block btn btn-sm btn-primary shadow-sm">
                                        <i class ="far fa-trash-alt"></i>
                                    </a>
                                </div> </td>
                            </tr>
            `;
        })
        var scripts = `<script>
            var lstreceiptdetail=JSON.parse(window.sessionStorage.getItem('receipt'));
            console.log(lstreceiptdetail);
            $('.quantity-input').off('change').change(function() {
              var id = $(this).data('id');
              var quantity=Number($(this).val());

            var index = lstreceiptdetail.findIndex(function (item) {
                return item.idproduct == id;
        });
           if(quantity==0)
        {
            lstreceiptdetail.splice(index, 1);
            $('#' +id).hide(200);
        }
        else{
            lstreceiptdetail[index].quantity=quantity;
            lstreceiptdetail[index].subtotal=quantity*lstreceiptdetail[index].price;
            $('#subtotal_'+id).text(quantity*Number(lstreceiptdetail[index].price));

        }
       window.sessionStorage.setItem('receipt', JSON.stringify(lstreceiptdetail));
            })
            $('.delete_receipt-detail').off('click').click(function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            $('#' + id).hide(200);
            var index = lstreceiptdetail.findIndex(function (item) {
                return item.idproduct == id;
        });
            lstreceiptdetail.splice(index, 1);
       window.sessionStorage.setItem('receipt', JSON.stringify(lstreceiptdetail));
        })  </script>`;
        $('#table-detail tbody').html(html);
        $('#table-detail tbody').append(scripts);
    }
    loadReceipt();
    function add_receipt_detail() {
        lstreceiptdetail = window.sessionStorage.getItem('receipt') == null ? [] : JSON.parse(window.sessionStorage.getItem('receipt'));
        var receiptdetail = new Object();
        var idproduct = Number($('#product-name').val());
        var name_product = $('#product-name :selected').text();
        var quantity = Number($('#quantity').val());
        var price = Number($('#price').val());
        receiptdetail.nameproduct = name_product;
        receiptdetail.idproduct = idproduct;
        receiptdetail.quantity = quantity;
        receiptdetail.price = price;
        receiptdetail.subtotal = price * quantity;
        var index = lstreceiptdetail.findIndex(function (item) {
            return item.idproduct == receiptdetail.idproduct;
        });
        if (index != -1) {
            lstreceiptdetail[index].quantity += quantity;
            lstreceiptdetail[index].price = price;
            lstreceiptdetail[index].subtotal = lstreceiptdetail[index].quantity * price;
        }
        else {
            lstreceiptdetail.push(receiptdetail);
        }
        window.sessionStorage.setItem('receipt', JSON.stringify(lstreceiptdetail));

        var html = '';
        $.each(lstreceiptdetail, function (index, item) {
            html += `
             <tr id='${item.idproduct}'>
                                <td>${item.nameproduct}</td>
                                <td>${item.price}</td>
                                <td><input value='${item.quantity}' type='number' class ='form-control quantity-input' data-id='${item.idproduct}' min='0' max='100'/></td>
                                <td id='subtotal_${item.idproduct}'>${item.subtotal}</td>
                                <td>
                                <div class ="tool d-flex align-items-center flex-column justify-content-center">
                                    <a data-id="${item.idproduct}" title="Xoá sản phẩm ${item.nameproduct}" class ="mt-1 mb-1 delete_receipt-detail d-sm-inline-block btn btn-sm btn-primary shadow-sm">
                                        <i class ="far fa-trash-alt"></i>
                                    </a>
                                </div> </td>
                            </tr>
            `;
        })
        var scripts = `<script>
            var lstreceiptdetail=JSON.parse(window.sessionStorage.getItem('receipt'));
            console.log(lstreceiptdetail);
            $('.quantity-input').off('change').change(function() {
              var id = $(this).data('id');
              var quantity=Number($(this).val());

            var index = lstreceiptdetail.findIndex(function (item) {
                return item.idproduct == id;
        });
           if(quantity==0)
        {
            lstreceiptdetail.splice(index, 1);
            $('#' +id).hide(200);
        }
        else{
            lstreceiptdetail[index].quantity=quantity;
            lstreceiptdetail[index].subtotal=quantity*Number(lstreceiptdetail[index].price);
            $('#subtotal_'+id).text(quantity*Number(lstreceiptdetail[index].price));
        }
       window.sessionStorage.setItem('receipt', JSON.stringify(lstreceiptdetail));
            })
            $('.delete_receipt-detail').off('click').click(function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            $('#' + id).hide(200);
            var index = lstreceiptdetail.findIndex(function (item) {
                return item.idproduct == id;
        });
            lstreceiptdetail.splice(index, 1);
       window.sessionStorage.setItem('receipt', JSON.stringify(lstreceiptdetail));
        })  </script>`;
        $('#table-detail tbody').html(html);
        $('#table-detail tbody').append(scripts);

    }
    $('#btnadd-receipt_detail').click(add_receipt_detail)

    $('#btn-confirm').click(function () {
        var lst = window.sessionStorage.getItem('receipt') == null ? [] : JSON.parse(window.sessionStorage.getItem('receipt'));
        if (lst.toString() == '') {
            showToast('Bạn chưa chọn sản phẩm nào để nhập');
        }
        else {
            lstreceiptdetail = new Array();
            $.each(lst, function (index, item) {
                var receiptdetail = new Object();
                receiptdetail.idproduct = item.idproduct;
                receiptdetail.price = item.price;
                receiptdetail.quantity = item.quantity;
                lstreceiptdetail.push(receiptdetail);
            })
            $.ajax({
                url: "/admin/Receipt_admin/Create/",
                data: JSON.stringify({ lstreceiptdetail}),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        type: "POST",
                        success: function (data) {
                            if (data.status == 1)
                            {
                                window.sessionStorage.removeItem('receipt');
                                window.location.href = '/admin/nhap-kho.html';
                            }
                        },
                error: function (data) {

                    alert(JSON.stringify(data));
                }
            })
        }
    })



    function delete_receipt(id) {
        $.ajax({
            url: "/admin/Receipt_admin/delete_receipt/" + id,
            data: JSON.stringify(id),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            type: "POST",
            success: function (data) {

                showToast(data.message);
                if (data.status == 1) {
                    $(`#_receipt_${id}`).hide(200);
                }

            },
            error: function (data) {

                alert(JSON.stringify(data));
            }
        })
    }
    $('.delete_receipt').off('click').click(function (e) {
        e.preventDefault();
        var id = Number($(this).data('id'));
        alertify.confirm('Thông báo', 'Bạn chắc chắn xoá phiếu nhập này?', function () {
            delete_receipt(id);
        }, function () { alertify.error('Huỷ') });

    });


    function delete_brand(id) {
        $.ajax({
            url: "/admin/Brand_admin/delete_brand/" + id,
            data: JSON.stringify(id),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            type: "POST",
            success: function (data) {

                showToast(data.message);
                if (data.status == 1) {
                    $(`#_brand_${id}`).hide(200);
                }

            },
            error: function (data) {

                alert(JSON.stringify(data));
            }
        })
    }
    $('.delete_brand').off('click').click(function (e) {
        e.preventDefault();
        var id = Number($(this).data('id'));
        alertify.confirm('Thông báo', 'Bạn chắc chắn xoá đối tác này?', function () {
            delete_brand(id);
        }, function () { alertify.error('Huỷ') });

    });
    function loadImages(id) {
        $.ajax({
            url: "/admin/Products_admin/LoadImages/",
            data: JSON.stringify({ id}),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    type: "POST",
                    success: function (res) {
                        var html = '';
                        var images = window.sessionStorage.setItem('images', JSON.stringify(res.data));
                        $.each(res.data, function (index, item) {
                            html += `<div><img src='/${item}'> <a href='' class='btn-delete-image'><i class='fa fa-times'></i></a></div>`;
                        })
                        var script = `<script>
                              $('.btn-delete-image').off('click').click(function (e) {
                e.preventDefault();
                var images = window.sessionStorage.getItem('images') == null || window.sessionStorage.getItem('images') == '""' ?[]: JSON.parse(window.sessionStorage.getItem('images'));
                $(this).parent().remove();
                var src = $(this).parent().find('img').attr('src');
                $.each(images, function (index, item) {
                    if (item === src||('/'+item)==src) {
                        images.splice(index, 1);
                        }
                        })
                        console.log(JSON.stringify(images));
                window.sessionStorage.setItem('images', JSON.stringify(images));
                        })
                        </script>`;
                        $('#lstimage').html(html);
                        $('#lstimage').append(script);
                    },
            error: function (data) {

                alert(JSON.stringify(data));
            }
        })
    }

    $('.btn-images').off('click').click(function (e) {
        e.preventDefault();
        $('#image-manager').modal('show');
        var id = $(this).data('id');
        loadImages(id);
        $('#idproduct').val(id);

    })
    $('#btnselectimage').click(function () {
        var images = window.sessionStorage.getItem('images') == null || window.sessionStorage.getItem('images') == '""' ? [] : JSON.parse(window.sessionStorage.getItem('images'));
        console.log(images);
        var finder = new CKFinder();
        finder.selectActionFunction = function (url) {
            var check = true
            $.each(images, function (index, item) {
                if (item === url||('/'+item)===url) {
                    check = false;
                }
            })

            if (check) {
                images.push(url);
                window.sessionStorage.setItem('images', JSON.stringify(images));
                $('#lstimage').append(`<div><img src='${url}'> <a href='' class='btn-delete-image'><i class='fa fa-times'></i></a></div>`);
            }
            $('.btn-delete-image').off('click').click(function (e) {
                e.preventDefault();
                var images = window.sessionStorage.getItem('images') == null || window.sessionStorage.getItem('images') == '""' ? [] : JSON.parse(window.sessionStorage.getItem('images'));
                $(this).parent().remove();
                var src = $(this).parent().find('img').attr('src');
                $.each(images, function (index, item) {
                    if (item === src) {
                        images.splice(index, 1);
                    }
                })
                window.sessionStorage.setItem('images', JSON.stringify(images));
            })
            
        }
        finder.popup();
    })
   

    $('#btnsave').click(function () {
        var id = $('#idproduct').val();
        var images = window.sessionStorage.getItem('images') == null ? [] : JSON.parse(window.sessionStorage.getItem('images'));
        console.log(JSON.stringify(images));
        $.ajax({
            url: "/admin/Products_admin/SaveImages/",
            data: JSON.stringify({ id, images}),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    type: "POST",
                    success: function (data) { 
                        swal({
                            title: "Thông báo",
                            text: "Lưu thành công",
                            icon: "success",
                            button: "Ok",
                        });
                    },
                    error: function (data) {

                alert(JSON.stringify(data));
            }
            })
    })
})


