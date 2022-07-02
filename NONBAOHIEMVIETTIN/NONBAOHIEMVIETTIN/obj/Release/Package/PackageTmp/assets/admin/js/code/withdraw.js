$(function () {
  const btnSubmitWithdraw = $("#btn_submit_withdraw");
  const btnRefuseWithdraw = $("#btn_refuse_withdraw");
  const note_withdrarw = $("#withdraw_note");
  $(".btn_confirm_withdraw")
    .off("click")
    .click(function () {
      $("#modal_withdraw").modal({ backdrop: "static" });
      const id = $(this).data("id");
      btnSubmitWithdraw.attr("data-id", id);
      btnRefuseWithdraw.attr("data-id", id);
    });
  btnSubmitWithdraw.click(function () {
    const note = note_withdrarw.val() ?? "";
    swal({
      title: "Bạn chắc chắn xác nhận lệnh rút tiền này?",
      icon: "warning",
      buttons: true,
      dangerMode: true,
    }).then((willDelete) => {
      if (willDelete) {
        const id = $(this).data("id");
        $.ajax({
          url: "/Withdraw_admin/ConfirmWithdraw",
          data: JSON.stringify({ id, note }),
          contentType: "application/json; charset=utf-8",
          dataType: "json",
          type: "POST",
          success({ status, message }) {
            $("#btn_close_withdraw").click();
            if (status) {
              iziToast.success({
                timeout: 1500,
                title: "Thành công",
                message: message,
                position: "topRight",
              });
              $(`#_withdraw_${id} .status span`)
              .removeClass('text-primary')
              .addClass('text-success')
              .text("Thành công");
              $(`#modal_withdraw_${id} .status span`)
              .removeClass('text-primary')
              .addClass('text-success')
              .text("Thành công");
              $(`#_withdraw_${id} .btn_confirm_withdraw`).remove();
              $(`#modal_withdraw_${id} .note span`).text(note);
            
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
  btnRefuseWithdraw.click(function () {
    const note = note_withdrarw.val() ?? "";
    swal({
      title: "Bạn chắc chắn từ chối lệnh rút tiền này?",
      icon: "warning",
      buttons: true,
      dangerMode: true,
    }).then((willDelete) => {
      if (willDelete) {
        const id = $(this).data("id");
        
        $.ajax({
          url: "/Withdraw_admin/RefuseWithdraw",
          data: JSON.stringify({ id, note }),
          contentType: "application/json; charset=utf-8",
          dataType: "json",
          type: "POST",
          success({ status, message }) {
            $("#btn_close_withdraw").click();
            if (status) {
              iziToast.success({
                timeout: 1500,
                title: "Thành công",
                message: message,
                position: "topRight",
              });
              $(`#_withdraw_${id} .status span`)
              .removeClass('text-primary')
              .addClass('text-danger')
              .text("Thất bại");
              $(`#modal_withdraw_${id} .status span`)
              .removeClass('text-primary')
              .addClass('text-danger')
              .text("Thất bại");
              $(`#modal_withdraw_${id} .note span`).text(note);
              $(`#_withdraw_${id} .btn_confirm_withdraw`).remove();
              
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
