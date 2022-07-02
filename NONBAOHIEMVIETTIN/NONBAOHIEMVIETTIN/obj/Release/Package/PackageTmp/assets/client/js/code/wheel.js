$(function () {
  const btnStart = $(".btn-start");
  const wheel = $(".wheel");
  const check_authen = $("#btn-authentication");
  let timer = 7000;
  let isRotating = false;
  let currentRotate = 0;

  const listGift = [
    {
      name: "Chúc bạn may mắn lần sau",
      percent: 20 / 100,
      key: -1,
    },
    {
      name: "Phiếu giảm giá 1 ngày",
      percent: 25 / 100,
      key: 1,
    },
    {
      name: "Mất lượt",
      percent: 10 / 100,
      key: -1,
    },
    {
      name: "Phiếu giảm giá 7 ngày",
      percent: 25 / 100,
      key: 7,
    },
    {
      name: "Thêm lượt",
      percent: 10 / 100,
      key: 0,
    },
    {
      name: "100 xu",
      percent: 10 / 100,
      key: 100,
    },
  ];
  const size = listGift.length;
  const rotate = 360 / size;
  const skewY = 90 - rotate;
  (() => {
    const html = listGift
      .map(
        (item, index) =>
          `
          <li style='transform:  rotate(${
            rotate * index
          }deg) skewY(-${skewY}deg)'>
          <p class='text-item ${index % 2 == 0 && "even"}'
              style='transform: skewY(${skewY}deg)
              rotate(${rotate / 2}deg)'
          >
          <b>${item.name}</b>
          </p>
          </li>
          `
      )
      .join("");
    wheel.html(html);
  })();
  const rotateWheel = (currentRotate, index) => {
    wheel.css(
      "transform",
      `rotate(${currentRotate - index * rotate - rotate / 2}deg)`
    );
  };
  const getGift = (randomNumber) => {
    let currentPercent = 0;
    let list = [];
    listGift.forEach((item, index) => {
      currentPercent += item.percent;
      randomNumber <= currentPercent &&
        list.push({
          ...item,
          index,
        });
    });
    return list[0];
  };
  const start = () => {
    const random = Math.random();
    const gift = getGift(random);
    currentRotate += 360 * 10;
    rotateWheel(currentRotate, gift.index);
    showGift(gift);
  };
  const showGift = (gift) => {
    isRotating = true;
    btnStart.removeAttr("style");
    setTimeout(async () => {
      const { status, code, discount, spin, coin } = await AddGift(gift);
      btnStart.attr("style", "cursor:pointer;");
      isRotating = false;
      if (status) {
        if (gift.key > 0 && gift.key != 100)
          swal({
            title: "Phần thưởng",
            text: `${gift.name}               
                    Mã: ${code}
                    Giảm giá: ${discount} %
                `,
            icon: "success",
            button: "Ok",
          });
        else {
          swal({
            title: "Phần thưởng",
            text: gift.name,
            icon: "success",
            button: "Ok",
          });
          if (gift.key == 0) $(".acc_spin").text(spin);
          else if (gift.key == 100) $(".acc_coin").text(coin);
        }
      } else {
        swal({
          title: "Lỗi",
          text: "Có lỗi xảy ra. Vui lòng thử lại.",
          icon: "error",
          button: "Ok",
        });
      }
    }, timer);
  };
  //điểm danh và quay thưởng
  (async () => {
    const { status, message } = await $.ajax({
      url: "/kiem-tra-dang-nhap",
      data: JSON.stringify({}),
      contentType: "application/json; charset=utf-8",
      dataType: "json",
      type: "POST",
      success(data) {
        return data;
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
    //quay thưởng
    btnStart.click(function () {
      if (status && !isRotating) {
        (async () => {
          const { status, message, coin, spin, checkSpin } = await CheckSpin();
          if (status) {
            if (checkSpin) $(".acc_spin").text(spin);
            else $(".acc_coin").text(coin);
            start();
          } else {
            iziToast.error({
              timeout: 1500,
              title: "Lỗi",
              message: message,
              position: "topRight",
            });
          }
        })();
      } else
        iziToast.error({
          timeout: 1500,
          title: "Lỗi",
          message: message,
          position: "topRight",
        });
    });
    //diểm danh
    check_authen.click(() => {
      if (status) {
        //thông báo điểm danh
        (async () => {
          const { status, message, spin } = await Attendance();
          if (status) {
            iziToast.success({
              timeout: 1500,
              title: "Thành công",
              message: message,
              position: "topRight",
            });
            $(".acc_spin").text(spin);
          } else
            iziToast.warning({
              timeout: 1500,
              title: "Cảnh báo",
              message: message,
              position: "topRight",
            });
        })();
      } else
        iziToast.error({
          timeout: 1500,
          title: "Lỗi",
          message: message,
          position: "topRight",
        });
    });
    //modal nạp tiền
    $("#btn-recharge").click(() =>
      $("#modal_recharge").modal({ backdrop: "static" })
    );
    //nạp tiền
    $("#btn_submit_recharge").click(() => {
      const input_amount_money = $("#amount_money");
      let amount_money = input_amount_money.val();

      if (amount_money == "") {
        iziToast.warning({
          timeout: 1500,
          title: "Cảnh báo",
          message: "Chưa nhập số tiền.",
          position: "topRight",
        });
        return;
      }
      amount_money = parseInt(amount_money);
      if (amount_money < 10000) {
        iziToast.warning({
          timeout: 1500,
          title: "Cảnh báo",
          message: "Số tiền tổi thiểu là 10.000đ.",
          position: "topRight",
        });
      } else if (amount_money % 1000 != 0) {
        iziToast.warning({
          timeout: 1500,
          title: "Cảnh báo",
          message: "Số tiền phải là bội số của 1000.",
          position: "topRight",
        });
      } else {
        $.ajax({
          url: "/nap-tien",
          data: JSON.stringify({ amount_money }),
          contentType: "application/json; charset=utf-8",
          dataType: "json",
          type: "POST",
          success(data) {
            if (data.status) {
              window.location.href = data.urlCheckout;
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
    //modal rút tiền
    $("#btn-withdraw").click(() =>
      $("#modal_withdraw").modal({ backdrop: "static" })
    );
    //rút tiền
    $("#btn_submit_withdraw").click(() => {
      const input_amount_money = $("#amount_money_withdraw");
      let amount_money = input_amount_money.val();
      const bank_number = $("#bank_number").val();
      const bank_name = $("#bank_name").val();
      const fullname = $("#fullname").val();
      if (amount_money === "") {
        iziToast.warning({
          timeout: 1500,
          title: "Cảnh báo",
          message: "Chưa nhập số xu.",
          position: "topRight",
        });
      } else if (bank_number === "") {
        iziToast.warning({
          timeout: 1500,
          title: "Cảnh báo",
          message: "Số tài khoản không được rỗng.",
          position: "topRight",
        });
      } else if (bank_name === "") {
        iziToast.warning({
          timeout: 1500,
          title: "Cảnh báo",
          message: "Tên ngân hàng không được rỗng.",
          position: "topRight",
        });
      } else if (fullname === "") {
        iziToast.warning({
          timeout: 1500,
          title: "Cảnh báo",
          message: "Họ tên người nhận không được rỗng.",
          position: "topRight",
        });
      } else {
        amount_money = parseInt(amount_money);
        if (amount_money < 200) {
          iziToast.warning({
            timeout: 1500,
            title: "Cảnh báo",
            message: "Số xu tối thiểu rút là 200xu.",
            position: "topRight",
          });
        } else {
          $.ajax({
            url: "/rut-tien",
            data: JSON.stringify({ bank_number,bank_name,fullname,amount_money }),
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
                input_amount_money.val("200");
                $("#btn_close_withdraw").click();
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
      }
    });
  })();
  //điểm danh cho khách hàng
  const Attendance = async () => {
    return await $.ajax({
      url: "/diem-danh",
      data: JSON.stringify({}),
      contentType: "application/json; charset=utf-8",
      dataType: "json",
      type: "POST",
      success(data) {
        return data;
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
    return attendance;
  };
  //kiểm tra lượt quay còn hay không
  const CheckSpin = async () => {
    return await $.ajax({
      url: "/kiem-tra-luot-quay",
      data: JSON.stringify({}),
      contentType: "application/json; charset=utf-8",
      dataType: "json",
      type: "POST",
      success(data) {
        return data;
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
  };
  //thêm phần thưởng
  const AddGift = async (gift) => {
    const name = gift.name;
    const key = gift.key;
    return await $.ajax({
      url: "/them-phan-thuong",
      data: JSON.stringify({ key, name }),
      contentType: "application/json; charset=utf-8",
      dataType: "json",
      type: "POST",
      success(data) {
        return data;
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
  };
  //lấy các phần thưởng mới nhất
  const getNewGifts = async () => {
    return await $.ajax({
      url: "/lay-phan-thuong-moi-nhat",
      data: JSON.stringify({}),
      contentType: "application/json; charset=utf-8",
      dataType: "json",
      type: "POST",
      success(data) {
        return data;
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
  };
  //mỗi 30s lấy phần thưởng mới nhất
  (() => {
    setInterval(async () => {
      const { status, list_gift } = await getNewGifts();

      if (status) {
        const ul = $(".list-wheel");
        const lis = $(".item-wheel");
        if (listGift.length == 9)
          for (var i = 0; i < list_gift.length; i++)
            lis[lis.length - 1].remove();
        const html = list_gift
          .map((item) => {
            return `
          <li class="item-wheel">
             <span class="title_account">${item.fullname}</span>
             <span class="title_title">vừa tham gia vòng quay nhận được </span>
             <span class="title_money">"${item.gift_name}" .</span>
           </li>
          `;
          })
          .join("");
        ul.prepend(html);
      }
    }, 30000);
  })();
});
