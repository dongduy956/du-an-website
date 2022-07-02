$(function () {
  $(".delete-withdraw")
    .off("click")
    .click(function() {
      
      swal({
        title: "Bạn chắc chắn huỷ lệnh rút tiền này?",
        icon: "warning",
        buttons: true,
        dangerMode: true,
      }).then((willDelete) => {
        if (willDelete) {
          const _id = $(this).data("id");
          console.log(_id);
          $.ajax({
            url: "/huy-lenh-rut-tien",
            data: JSON.stringify({ _id }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            type: "POST",
            success({ status, message, coin }) {
              if (status) {
                iziToast.success({
                  timeout: 1500,
                  title: "Thành công",
                  message: message,
                  position: "topRight",
                });
                $("#withdraw-" + _id)
                  .hide(500)
                  .remove();
                $(".acc_coin").text(coin);
              } else {
                iziToast.warning({
                  timeout: 1500,
                  title: "Lỗi",
                  message: message,
                  position: "topRight",
                });
              }
            },
            error(data) {
              iziToast.error({
                timeout: 1500,
                title: "Lỗi",
                message: "Lỗi chưa xác định.",
                position: "topRight",
              });
            },
          });
        }
      });
    });
});
