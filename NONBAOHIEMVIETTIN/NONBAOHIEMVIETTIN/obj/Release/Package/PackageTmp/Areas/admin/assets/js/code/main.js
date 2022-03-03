//hàm chuyển đổi tiền tệ
function number_format(number, decimals, dec_point, thousands_sep) {
  // *     example: number_format(1234.56, 2, ',', ' ');
  // *     return: '1 234,56'
  number = (number + '').replace(',', '').replace(' ', '');
  var n = !isFinite(+number) ? 0 : +number,
    prec = !isFinite(+decimals) ? 0 : Math.abs(decimals),
    sep = (typeof thousands_sep === 'undefined') ? ',' : thousands_sep,
    dec = (typeof dec_point === 'undefined') ? '.' : dec_point,
    s = '',
    toFixedFix = function(n, prec) {
      var k = Math.pow(10, prec);
      return '' + Math.round(n * k) / k;
    };
  // Fix for IE parseFloat(0.55).toFixed(0) = 0;
  s = (prec ? toFixedFix(n, prec) : '' + Math.round(n)).split('.');
  if (s[0].length > 3) {
    s[0] = s[0].replace(/\B(?=(?:\d{3})+(?!\d))/g, sep);
  }
  if ((s[1] || '').length < prec) {
    s[1] = s[1] || '';
    s[1] += new Array(prec - s[1].length + 1).join('0');
  }
  return s.join(dec);
}
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
$(function () {
    //slide bar đóng lại khi ở chế độ mobile
    if (!($('#sidebar').css('display') == 'none' || $('#sidebar').css("visibility") == "hidden")) {
        $('#sidebarToggle').click();
    }
    //hàm xử lý login
    function login() {
        var accLogin = new Object();
        accLogin.username = $('#usernameadmin').val();
        accLogin.password = $('#passwordadmin').val();
        var recaptcha = $("#g-recaptcha-response-1").val();
        if (accLogin.username == '')
            $.notify('Tài khoản không được rỗng.','warn');
        else
            if (accLogin.password == '')
                $.notify('Mật khẩu không được rỗng.','warn');
            else {
                disable('.btn-user');
                $.ajax({
                    url: "/admin/Login/Login",
                    data: JSON.stringify({ accLogin, recaptcha}),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    type: "POST",
                    success: function (data) {
                        grecaptcha.reset();

                        if (data.status != 1)
                            $.notify(data.message,'error');
                        else
                            if (data.status == 1)
                                location.href = '/admin';
                        enable('.btn-user');
                    },
                    error: function (data) {
                        $.notify('Lỗi chưa xác định.', 'error');
                    }
                })
            }
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
    //Đăng xuất khỏi hệ thống
    $('#btnlogout').click(function () {
        location.href = "/admin/Login/Logout"
    })
    //hàm xử lý xoá 1 sản phẩm
    function delete_product(id, ele) {
        $.ajax({
            url: "/admin/products_admin/delete_product/" + id,
            data: JSON.stringify(id),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            type: "POST",
            success: function (data) {

                if (data.status == 1) {
                    $.notify(data.message, 'success');

                    ele.attr('style', 'display:none !important');
                    $(`#_product_${id}-delete`).text('Đã xoá');
                    $(`#_product_${id}-delete-detail`).text('Đã xoá');
                }else
                    $.notify(data.message, 'error');
            },
            error: function (data) {
                $.notify('Lỗi chưa xác định.', 'error');
            }
        })
    }
    //Event gọi hàm xoá 1 sản phẩm
    $('.delete_product').off('click').click(function (e) {
        e.preventDefault();
        var id = Number($(this).data('id'));
        var ele = $(this);
        swal({
            title: "Bạn chắc chắn xoá nón này?",
            icon: "warning",
            buttons: true,
            dangerMode: true,
        })
      .then((willDelete) => {
          if (willDelete) {
              delete_product(id, ele);

          }
      });

    });

    //Hàm xử lý xoá 1 loại nón
    function delete_category(id, ele) {
        $.ajax({
            url: "/admin/category_admin/delete_category/" + id,
            data: JSON.stringify(id),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            type: "POST",
            success: function (data) {

                if (data.status == 1) {
                    $.notify(data.message, 'success');

                    ele.attr('style', 'display:none !important');
                    $(`#_category_${id}-delete`).text('Đã xoá');
                }
                else
                    $.notify(data.message, 'error');


            },
            error: function (data) {
                $.notify('Lỗi chưa xác định.', 'error');

            }
        })
    }
    //Event gọi hàm xoá 1 loại nón
    $('.delete_category').off('click').click(function (e) {
        e.preventDefault();
        var id = Number($(this).data('id'));
        var ele = $(this);

        swal({
            title: "Bạn chắc chắn xoá loại nón này?",
            icon: "warning",
            buttons: true,
            dangerMode: true,
        })
    .then((willDelete) => {
        if (willDelete) {
            delete_category(id, ele);
        }
    });

    });
    //Hàm xử lý xoá 1 nhà sản xuất
    function delete_production(id, ele) {
        $.ajax({
            url: "/admin/production_admin/delete_production/" + id,
            data: JSON.stringify(id),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            type: "POST",
            success: function (data) {
                if (data.status == 1) {
                    $.notify(data.message, 'success');

                    ele.attr('style', 'display:none !important');
                    $(`#_production_${id}-delete`).text('Đã xoá');
                }
                else
                    $.notify(data.message, 'error');


            },
            error: function (data) {
                $.notify('Lỗi chưa xác định.', 'error');

            }
        })
    }
    //Event gọi hàm xoá 1 nhà sản xuất
    $('.delete_production').off('click').click(function (e) {
        e.preventDefault();
        var id = Number($(this).data('id'));
        var ele = $(this);
        swal({
            title: "Bạn chắc chắn xoá nhà sản xuất này?",
            icon: "warning",
            buttons: true,
            dangerMode: true,
        })
  .then((willDelete) => {
      if (willDelete) {
          delete_production(id, ele);
      }
  });

    });
    //Hàm xử lý xoá 1 nhóm nón
    function delete_groupproduct(id, ele) {
        $.ajax({
            url: "/admin/GroupProduct_admin/delete_groupProduct/" + id,
            data: JSON.stringify(id),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            type: "POST",
            success: function (data) {

                if (data.status == 1) {
                    $.notify(data.message, 'success');

                    ele.attr('style', 'display:none !important');
                    $(`#_group-product_${id}-delete`).text('Đã xoá');
                }
                else
                    $.notify(data.message, 'error');

            },
            error: function (data) {
                $.notify('Lỗi chưa xác định.', 'error');
            }
        })
    }
    //Event gọi hàm xoá nhóm nón
    $('.delete_group-product').off('click').click(function (e) {
        e.preventDefault();
        var id = Number($(this).data('id'));
        var ele = $(this);
        swal({
            title: "Bạn chắc chắn xoá nhóm nón này?",
            icon: "warning",
            buttons: true,
            dangerMode: true,
        })
.then((willDelete) => {
    if (willDelete) {
        delete_groupproduct(id, ele);
    }
});

    });

    //Hàm xoá quyền
    function delete_role(id) {
        $.ajax({
            url: "/admin/Role_admin/delete_role/" + id,
            data: JSON.stringify(id),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            type: "POST",
            success: function (data) {
                if (data.status == 1) {
                    $.notify(data.message, 'success');
                    $(`#_role_${id}`).hide(200);
                }
                else
                    $.notify(data.message, 'error');
            },
            error: function (data) {
                $.notify('Lỗi chưa xác định.', 'error');
            }
        })
    }
    //Event gọi hàm xoá quyền
    $('.delete_role').off('click').click(function (e) {
        e.preventDefault();
        var id = Number($(this).data('id'));

        swal({
            title: "Bạn chắc chắn xoá quyền này?",
            icon: "warning",
            buttons: true,
            dangerMode: true,
        })
        .then((willDelete) => {
            if (willDelete) {
                delete_role(id);
            }
        });
    });
    //Hàm xử lý xoá 1 tài khoản
    function delete_account(id) {
        $.ajax({
            url: "/admin/Accounts_admin/delete_account/" + id,
            data: JSON.stringify(id),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            type: "POST",
            success: function (data) {

                if (data.status == 1 || data.status==2)
                {
                    $.notify(data.message, 'success');

                    $(`#_account_${id}`).hide(200);
                    if(data.status==2)
                    {
                        location.href = "/admin/Login/Logout"
                    }
                }
                else
                    $.notify(data.message, 'error');


            },
            error: function (data) {
                $.notify('Lỗi chưa xác định.', 'error');

            }
        })
    }
    //Event gọi hàm xoá 1 tài khoản
    $('.delete_account').off('click').click(function (e) {
        e.preventDefault();
        var id = Number($(this).data('id'));
        swal({
            title: "Bạn chắc chắn xoá tài khoản này?",
            icon: "warning",
            buttons: true,
            dangerMode: true,
        })
       .then((willDelete) => {
           if (willDelete) {
               delete_account(id);

           }
       });

    });

    //Hàm reset password của user & admin
    function reset_password(id) {
        $.ajax({
            url: "/admin/Accounts_admin/resetPassword/" + id,
            data: JSON.stringify(id),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            type: "POST",
            success: function (data) {
                if(data.status==1)
                    $.notify(data.message, 'success');
                else
                    $.notify(data.message, 'error');


            },
            error: function (data) {
                $.notify('Lỗi chưa xác định.', 'error');

            }
        })
    }
    //Event gọi hàm reset password
    $('.reset-password').off('click').click(function (e) {
        e.preventDefault();
        var id = Number($(this).data('id'));
        swal({
            title: "Bạn chắc chắn reset mật khẩu tài khoản này?",
            icon: "warning",
            buttons: true,
            dangerMode: true,
        })
       .then((willDelete) => {
           if (willDelete) {
               reset_password(id);
           }
       });

    });
    //Hàm xử lý xoá 1 loại tin tức
    function delete_newstype(id) {
        $.ajax({
            url: "/admin/Newstype_admin/delete_newstype/" + id,
            data: JSON.stringify(id),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            type: "POST",
            success: function (data) {
                if (data.status == 1) {
                    $.notify(data.message, 'success');

                    $(`#_newstype_${id}`).hide(200);
                }
                else
                    $.notify(data.message, 'error');


            },
            error: function (data) {
                $.notify('Lỗi chưa xác định.', 'error');

            }
        })
    }
    //Event gọi hàm xoá loại tin
    $('.delete_newstype').off('click').click(function (e) {
        e.preventDefault();
        var id = Number($(this).data('id'));
        swal({
            title: "Bạn chắc chắn xoá loại tin này?",
            icon: "warning",
            buttons: true,
            dangerMode: true,
        })
      .then((willDelete) => {
          if (willDelete) {
              delete_newstype(id);

          }
      });

    });

    //Hàm xử lý xoá 1 tin tức
    function delete_news(id) {
        $.ajax({
            url: "/admin/News_admin/delete_news/" + id,
            data: JSON.stringify(id),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            type: "POST",
            success: function (data) {

                if (data.status == 1) {
                    $.notify(data.message, 'success');
                    ele.attr('style', 'display:none !important');
                    $(`#_news_${id}`).hide(200);
                }
                else
                    $.notify(data.message, 'error');


            },
            error: function (data) {
                $.notify('Lỗi chưa xác định.', 'error');

            }
        })
    }
    //Event gọi hàm xoá tin tức
    $('.delete_news').off('click').click(function (e) {
        e.preventDefault();
        var id = Number($(this).data('id'));
        swal({
            title: "Bạn chắc chắn xoá tin tức này?",
            icon: "warning",
            buttons: true,
            dangerMode: true,
        })
      .then((willDelete) => {
          if (willDelete) {
              delete_news(id);
          }
      });

    });

    //Hàm xử lý xoá giới thiệu về webstie
    function delete_introduce(id) {
        $.ajax({
            url: "/admin/Introduce_admin/delete_introduce/" + id,
            data: JSON.stringify(id),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            type: "POST",
            success: function (data) {

                if (data.status == 1) {
                    $.notify(data.message, 'success');
                    $(`#_introduce_${id}`).hide(200);
                }
                else
                    $.notify(data.message, 'error');


            },
            error: function (data) {
                $.notify('Lỗi chưa xác định.', 'error');

            }
        })
    }
    //Event gọi hàm xoá giới thiệu
    $('.delete_introduce').off('click').click(function (e) {
        e.preventDefault();
        var id = Number($(this).data('id'));
        swal({
            title: "Bạn chắc chắn xoá giới thiệu này?",
            icon: "warning",
            buttons: true,
            dangerMode: true,
        })
   .then((willDelete) => {
       if (willDelete) {
           delete_introduce(id);
       }
   });


    });

    //Hàm xoá 1 liên hệ
    function delete_contact(id) {
        $.ajax({
            url: "/admin/Contact_admin/delete_contact/" + id,
            data: JSON.stringify(id),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            type: "POST",
            success: function (data) {
                if (data.status == 1) {
                    $.notify(data.message, 'success');

                    $(`#_contact_${id}`).hide(200);
                }
                else
                    $.notify(data.message, 'error');


            },
            error: function (data) {
                $.notify('Lỗi chưa xác định.', 'error');

            }
        })
    }
    //Event gọi hàm xoá liên hệ
    $('.delete_contact').off('click').click(function (e) {
        e.preventDefault();
        var id = Number($(this).data('id'));
        swal({
            title: "Bạn chắc chắn xoá liên hệ này?",
            icon: "warning",
            buttons: true,
            dangerMode: true,
        })
 .then((willDelete) => {
     if (willDelete) {
         delete_contact(id);
     }
 });


    });


    //Hàm xoá 1 phản hồi
    function delete_feedback(id) {
        $.ajax({
            url: "/admin/Feedback_admin/delete_feedback/" + id,
            data: JSON.stringify(id),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            type: "POST",
            success: function (data) {
                if (data.status == 1) {
                    $.notify(data.message, 'success');
                    $(`#_feedback_${id}`).hide(200);
                }
                else
                    $.notify(data.message, 'error');


            },
            error: function (data) {
                $.notify('Lỗi chưa xác định.', 'error');

            }
        })
    }
    //Event gọi hàm xoá 1 phản hồi
    $('.delete_feedback').off('click').click(function (e) {
        e.preventDefault();
        var id = Number($(this).data('id'));
        swal({
            title: "Bạn chắc chắn xoá phản hồi này?",
            icon: "warning",
            buttons: true,
            dangerMode: true,
        })
    .then((willDelete) => {
        if (willDelete) {
            delete_feedback(id);

        }
    });

    });

    //Hàm xoá 1 đăng kí
    function delete_subscribe(id) {
        $.ajax({
            url: "/admin/Subscribe_admin/delete_subscribe/" + id,
            data: JSON.stringify(id),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            type: "POST",
            success: function (data) {
                if (data.status == 1) {
                    $.notify(data.message, 'success');

                    $(`#_subscribe_${id}`).hide(200);
                }
                else
                    $.notify(data.message, 'error');


            },
            error: function (data) {
                $.notify('Lỗi chưa xác định.', 'error');

            }
        })
    }
    //Event gọi hàm xoá 1 đăng kí
    $('.delete_subscribe').off('click').click(function (e) {
        e.preventDefault();
        var id = Number($(this).data('id'));
        swal({
            title: "Bạn chắc chắn xoá đăng kí này?",
            icon: "warning",
            buttons: true,
            dangerMode: true,
        })
 .then((willDelete) => {
     if (willDelete) {
         delete_subscribe(id);


     }
 });
      

    });

    //Hàm xoá 1 đánh giá
    function delete_rate(id) {
        $.ajax({
            url: "/admin/Rate_admin/delete_rate/" + id,
            data: JSON.stringify(id),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            type: "POST",
            success: function (data) {

                if (data.status == 1) {
                    $.notify(data.message, 'success');

                    $(`#_rate_${id}`).hide(200);
                }
                else
                    $.notify(data.message, 'error');


            },
            error: function (data) {
                $.notify('Lỗi chưa xác định.', 'error');

            }
        })
    }
    //Event gọi hàm xoá đánh giá
    $('.delete_rate').off('click').click(function (e) {
        e.preventDefault();
        var id = Number($(this).data('id'));
        swal({
            title: "Bạn chắc chắn xoá đánh giá này?",
            icon: "warning",
            buttons: true,
            dangerMode: true,
        })
.then((willDelete) => {
    if (willDelete) {
               delete_rate(id);
    }
});    

    });
    //Hàm xoá 1 đơn hàng
    function delete_order(id) {
        $.ajax({
            url: "/admin/Order_admin/delete_order/" + id,
            data: JSON.stringify(id),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            type: "POST",
            success: function (data) {

                if (data.status == 1) {
                    $.notify(data.message, 'success');

                    $(`#_order_${id}`).hide(200);
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
            title: "Bạn chắc chắn xoá đơn hàng này?",
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
    //Hàm duyệt 1 đơn hàng
    function confirm_order(id) {
        $.ajax({
            url: "/admin/Order_admin/confirm_order/" + id,
            data: JSON.stringify(id),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            type: "POST",
            success: function (data) {
                if (data.status == 1) {
                    $.notify(data.message, 'success');
                    $('#confirm_order_detail_' + id + ',#confirm_order_' + id).removeClass('text-danger');
                    $('#confirm_order_detail_' + id + ',#confirm_order_' + id).addClass('text-success');
                    $('#confirm_order_detail_' + id + ',#confirm_order_' + id).text('Đã duyệt');
                }
                else
                    $.notify(data.message, 'error');
            },
            error: function (data) {
                $.notify('Lỗi chưa xác định.', 'error');
            }
        })
    }
    //Event gọi hàm duyệt 1 đơn hàng
    $('.confirm_order').off('click').click(function (e) {
        e.preventDefault();
        var id = Number($(this).data('id'));
        swal({
            title: "Bạn chắc chắn duyệt đơn hàng này?",
            icon: "warning",
            buttons: true,
            dangerMode: true,
        })
.then((willDelete) => {
    if (willDelete) {
                   confirm_order(id);


    }
});      
    });

    //Hàm xác nhận đã chuyển tiền của 1 đơn hàng
    function transfer_order(id) {
        $.ajax({
            url: "/admin/Order_admin/transfer_order/" + id,
            data: JSON.stringify(id),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            type: "POST",
            success: function (data) {
                if (data.status == 1) {
                    $.notify(data.message, 'success');

                    $('#transfer_order_detail_' + id + ',#transfer_order_' + id).removeClass('text-danger');
                    $('#transfer_order_detail_' + id + ',#transfer_order_' + id).addClass('text-success');
                    $('#transfer_order_detail_' + id + ',#transfer_order_' + id).text('Đã chuyển tiền');
                }
                else
                    $.notify(data.message, 'error');


            },
            error: function (data) {
                $.notify('Lỗi chưa xác định.', 'error');

            }
        })
    }
    //Event gọi hàm xác nhận đã chuyển tiền
    $('.transfer_order').off('click').click(function (e) {
        e.preventDefault();
        var id = Number($(this).data('id'));
            swal({
                title: "Bạn chắc chắn thanh toán đơn hàng này?",
                icon: "warning",
                buttons: true,
                dangerMode: true,
            })
    .then((willDelete) => {
        if (willDelete) {
           transfer_order(id);
        }
    });    
    });
    //Mảng các nón nhập kho
    var lstreceiptdetail = new Array();
    //Load danh sách nhập kho chưa xác nhận trong mảng
    function loadReceipt() {
        lstreceiptdetail = window.sessionStorage.getItem('receipt') == null ? [] : JSON.parse(window.sessionStorage.getItem('receipt'));
        var html = '';
        $.each(lstreceiptdetail, function (index, item) {
            html += `
             <tr id='${item.idproduct}'>
                                <td>${item.nameproduct}</td>
                                <td>${number_format(item.price)}</td>
                                <td><input value='${item.quantity}' data-id="${item.idproduct}" type='number' class ='quantity-input form-control' min='0' max='100'/></td>
                                <td id='subtotal_${item.idproduct}'>${number_format(item.subtotal)}</td>
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
            $('#subtotal_'+id).text(number_format(quantity*Number(lstreceiptdetail[index].price)));

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
    //Hàm thêm 1 sản phẩm vào nhập kho
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
                                <td>${number_format(item.price)}</td>
                                <td><input value='${item.quantity}' type='number' class ='form-control quantity-input' data-id='${item.idproduct}' min='0' max='100'/></td>
                                <td id='subtotal_${item.idproduct}'>${number_format(item.subtotal)}</td>
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
            $('#subtotal_'+id).text(number_format(quantity*Number(lstreceiptdetail[index].price)));
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
    //Event gọi hàm nhập kho 1 sản phẩm
    $('#btnadd-receipt_detail').click(add_receipt_detail)
    //Hàm xác nhập phiếu nhập kho
    $('#btn-confirm').click(function () {
        var lst = window.sessionStorage.getItem('receipt') == null ? [] : JSON.parse(window.sessionStorage.getItem('receipt'));
        if (lst.toString() == '') {
            $.notify('Bạn chưa chọn sản phẩm nào để nhập.', 'warn');
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
                                window.location.href = '/admin/nhap-kho';
                            }
                        },
                error: function (data) {
                    $.notify('Lỗi chưa xác định.', 'error');

                }
            })
        }
    })


    //hàm xoá 1 phiếu nhập
    function delete_receipt(id) {
        $.ajax({
            url: "/admin/Receipt_admin/delete_receipt/" + id,
            data: JSON.stringify(id),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            type: "POST",
            success: function (data) {
                if (data.status == 1) {
                    $.notify(data.message, 'success');

                    $(`#_receipt_${id}`).hide(200);
                }
                else
                    $.notify(data.message, 'error');


            },
            error: function (data) {
                $.notify('Lỗi chưa xác định.', 'error');

            }
        })
    }
    //Event gọi hàm xoá 1 phiếu nhập
    $('.delete_receipt').off('click').click(function (e) {
        e.preventDefault();
        var id = Number($(this).data('id'));
        swal({
            title: "Bạn chắc chắn xoá phiếu nhập này?",
            icon: "warning",
            buttons: true,
            dangerMode: true,
        })
    .then((willDelete) => {
        if (willDelete) {
            delete_receipt(id);

        }
    });       

    });

    //Hàm xoá 1 đối tác
    function delete_brand(id) {
        $.ajax({
            url: "/admin/Brand_admin/delete_brand/" + id,
            data: JSON.stringify(id),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            type: "POST",
            success: function (data) {

                if (data.status == 1) {
                    $.notify(data.message, 'success');

                    $(`#_brand_${id}`).hide(200);
                }
                else
                    $.notify(data.message, 'error');

            },
            error: function (data) {
                $.notify('Lỗi chưa xác định.', 'error');

            }
        })
    }
    //Event gọi hàm xoá 1 đối tác
    $('.delete_brand').off('click').click(function (e) {
        e.preventDefault();
        var id = Number($(this).data('id'));
        swal({
            title: "Bạn chắc chắn xoá đối tác này?",
            icon: "warning",
            buttons: true,
            dangerMode: true,
        })
.then((willDelete) => {
    if (willDelete) {
        delete_brand(id);
    }
});
       

    });

    //Hàm load các ảnh phụ của 1 sản phẩm
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
                window.sessionStorage.setItem('images', JSON.stringify(images));
                        })
                        </script>`;
                        $('#lstimage').html(html);
                        $('#lstimage').append(script);
                    },
            error: function (data) {
                $.notify('Lỗi chưa xác định.', 'error');
            }
        })
    }
    //Hàm mở modal để chọn ảnh phụ cho nón
    $('.btn-images').off('click').click(function (e) {
        e.preventDefault();
        $('#image-manager').modal('show');
        var id = $(this).data('id');
        loadImages(id);
        $('#idproduct').val(id);

    })
    //Hàm select ảnh trong ckfinder
    $('#btnselectimage').click(function () {
        var images = window.sessionStorage.getItem('images') == null || window.sessionStorage.getItem('images') == '""' ? [] : JSON.parse(window.sessionStorage.getItem('images'));
        console.log(images);
        var finder = new CKFinder();
        finder.selectActionFunction = function (url) {
            var check = true
            $.each(images, function (index, item) {
                if (item === url || ('/' + item) === url) {
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

    //Hàm lưu các ảnh phụ đã chọn cho 1 sản phẩm
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
                    $.notify('Lỗi chưa xác định.', 'error');

                }
        })
    })
})


