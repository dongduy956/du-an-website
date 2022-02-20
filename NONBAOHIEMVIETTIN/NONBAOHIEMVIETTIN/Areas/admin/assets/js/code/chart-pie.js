// Set new default font family and font color to mimic Bootstrap's default styling
Chart.defaults.global.defaultFontFamily = 'Nunito', '-apple-system,system-ui,BlinkMacSystemFont,"Segoe UI",Roboto,"Helvetica Neue",Arial,sans-serif';
Chart.defaults.global.defaultFontColor = '#858796';
// Pie Chart Example

$(document).ready(function () {
    var now = new Date();
    var month = (now.getMonth() + 1);
    var day = now.getDate();
    if (month < 10)
        month = "0" + month;
    if (day < 10)
        day = "0" + day;
    var today = now.getFullYear() + '-' + month + '-' + day;
    $('#day').val(today);
    var year = now.getFullYear();
    function loadMoneysDay(day,month, year) {
        $.ajax({
            url: "/admin/Dashboard/LoadMoneysDay/",
            data: JSON.stringify({ day,month,year }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            type: "POST",
            success: function (data) {
                var ctx = document.getElementById("myPieChart");
                var myPieChart = new Chart(ctx, {
                    type: 'pie',
                    data: {
                        labels: ["Số đơn hàng", "Số phiếu nhập"],
                        datasets: [{
                            data: [data.countOrders, data.countReceipts],
                            backgroundColor: ['#4e73df', '#1cc88a'],
                            hoverBackgroundColor: ['#2e59d9', '#17a673'],
                            hoverBorderColor: "rgba(234, 236, 244, 1)",
                        }],
                    },

                    options: {
                        maintainAspectRatio: false,
                        tooltips: {
                            titleMarginBottom: 10,
                            titleFontColor: '#6e707e',
                            titleFontSize: 14,
                            backgroundColor: "rgb(255,255,255)",
                            bodyFontColor: "#858796",
                            borderColor: '#dddfeb',
                            borderWidth: 1,
                            xPadding: 10,
                            yPadding: 10,
                            displayColors: true,
                            caretPadding: 10,                           
                        },                                         
                        legend: {
                            display: false
                        }
                    },
                });
            },
            error: function (data) {

                console.log(JSON.stringify(data));
            }
        })
    }

    loadMoneysDay(day,month, year);
    $('#day').change(function () {
        var date = new Date($(this).val());
        month = (date.getMonth() + 1);
        year = date.getFullYear();
        day = date.getDate();
        $('.chart-pie').html('<canvas id="myPieChart"></canvas>');
        loadMoneysDay(day, month, year);
        $('#mess-day').text(' '+day+'/'+month+'/'+year);
    })
});

