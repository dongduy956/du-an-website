
// Set new default font family and font color to mimic Bootstrap's default styling
Chart.defaults.global.defaultFontFamily = 'Nunito', '-apple-system,system-ui,BlinkMacSystemFont,"Segoe UI",Roboto,"Helvetica Neue",Arial,sans-serif';
Chart.defaults.global.defaultFontColor = '#858796';
// Bar Chart Example

$(document).ready(function () {
    var now = new Date();
    var month = (now.getMonth() + 1);
    var day = now.getDate();
    if (month < 10)
        month = "0" + month;
    if (day < 10)
        day = "0" + day;
    var today = now.getFullYear() + '-' + month + '-' + day;
    $('#year').val(today);
    var year = now.getFullYear();
    function loadMoneysYear(year) {
        $.ajax({
            url: "/Dashboard/LoadMoneysYear/",
            data: JSON.stringify({ year }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            type: "POST",
            success: function (data) {
                var moneys = data.moneys;
                var ctx = document.getElementById("myBarChart");
                var myBarChart = new Chart(ctx, {
                    type: 'bar',
                    data: {
                        labels: ["T1", "T2", "T3", "T4", "T5", "T6", "T7", "T8", "T9", "T10", "T11", "T12"],
                        datasets: [{
                            label: "Tiền",
                            backgroundColor: "#4e73df",
                            hoverBackgroundColor: "#2e59d9",
                            borderColor: "#4e73df",
                            data: moneys,
                        }],
                    },
                    options: {
                        maintainAspectRatio: false,
                        layout: {
                            padding: {
                                left: 0,
                                right: 0,
                                top: 0,
                                bottom: 0
                            }
                        },
                        scales: {
                            xAxes: [{
                                time: {
                                    unit: 'month'
                                },
                                gridLines: {
                                    display: true,
                                    drawBorder: true
                                },
                                ticks: {
                                    maxTicksLimit: 12
                                },
                                maxBarThickness: 25,
                            }],
                            yAxes: [{
                                ticks: {
                                    
                                    maxTicksLimit: 5,
                                    padding: 10,
                                    // Include a dollar sign in the ticks
                                    callback: function (value, index, values) {
                                        return number_format(value) + 'đ';
                                    }
                                },
                                gridLines: {
                                    color: "rgb(234, 236, 244)",
                                    zeroLineColor: "rgb(234, 236, 244)",
                                    drawBorder: false,
                                    borderDash: [2],
                                    zeroLineBorderDash: [2]
                                }
                            }],
                        },
                        legend: {
                            display: false
                        },
                        tooltips: {
                            titleMarginBottom: 10,
                            titleFontColor: '#6e707e',
                            titleFontSize: 14,
                            backgroundColor: "rgb(255,255,255)",
                            bodyFontColor: "#858796",
                            borderColor: '#dddfeb',
                            borderWidth: 1,
                            xPadding: 15,
                            yPadding: 15,
                            displayColors: true,
                            caretPadding: 10,
                            callbacks: {
                                label: function (tooltipItem, chart) {
                                    var datasetLabel = chart.datasets[tooltipItem.datasetIndex].label || '';
                                    return datasetLabel + ': ' + number_format(tooltipItem.yLabel) + ' đ';
                                }
                            }
                        },
                    }
                });
                console.log(moneys)
            },
            error: function (data) {

                console.log(JSON.stringify(data));
            }
        })
    }
    loadMoneysYear( year);

    $('#year').change(function () {
        var date = new Date($(this).val());
        month = (date.getMonth() + 1);
        year = date.getFullYear();
        $('.chart-bar').html('<canvas id="myBarChart"></canvas>');
        loadMoneysYear( year);
        $('#mess-year').text(year);
    })
});

