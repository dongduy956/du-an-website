$(function () {
  var province;
  var district;
  province = JSON.parse(data);
  console.log(province);
  province.forEach((element) => {
    $("#province").append(
      `<option  value="${element.name}">${element.name}</option>`
    );
  });
  $("#province").change(function () {
    $("#district").html('<option value="-1">Chọn quận/huyện</option>');
    $("#town").html('<option  value = "-1"> Chọn phường/xã </option>');
    var value = $(this).val();
    province.forEach((element) => {
      if (element.name == value) {
        console.log(element);
        district = element.districts;
        element.districts.forEach((element1) => {
          $("#district").append(
            `<option  value="${element1.name}">${element1.name}</option>`
          );
        });
      }
    });
  });

  $("#district").change(function () {
    $("#town").html('<option value = "-1"> Chọn phường/xã </option>');
    var value = $(this).val();
    district.forEach((element) => {
      if (element.name == value) {
        element.wards.forEach((element1) => {
          $("#town").append(
            `<option  value="${element1.name}">${element1.name}</option>`
          );
        });
      }
    });
  });
});
