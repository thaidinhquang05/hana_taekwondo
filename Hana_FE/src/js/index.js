// Set new default font family and font color to mimic Bootstrap's default styling
(Chart.defaults.global.defaultFontFamily = "Nunito"),
    '-apple-system,system-ui,BlinkMacSystemFont,"Segoe UI",Roboto,"Helvetica Neue",Arial,sans-serif';
Chart.defaults.global.defaultFontColor = "#858796";

function number_format(number, decimals, dec_point, thousands_sep) {
    // *     example: number_format(1234.56, 2, ',', ' ');
    // *     return: '1 234,56'
    number = (number + "").replace(",", "").replace(" ", "");
    var n = !isFinite(+number) ? 0 : +number,
        prec = !isFinite(+decimals) ? 0 : Math.abs(decimals),
        sep = typeof thousands_sep === "undefined" ? "," : thousands_sep,
        dec = typeof dec_point === "undefined" ? "." : dec_point,
        s = "",
        toFixedFix = function (n, prec) {
            var k = Math.pow(10, prec);
            return "" + Math.round(n * k) / k;
        };
    // Fix for IE parseFloat(0.55).toFixed(0) = 0;
    s = (prec ? toFixedFix(n, prec) : "" + Math.round(n)).split(".");
    if (s[0].length > 3) {
        s[0] = s[0].replace(/\B(?=(?:\d{3})+(?!\d))/g, sep);
    }
    if ((s[1] || "").length < prec) {
        s[1] = s[1] || "";
        s[1] += new Array(prec - s[1].length + 1).join("0");
    }

    return s.join(dec);
}

$(async () => {
    $(document).ajaxStart(() => {
        $(".loading-div").show();
    });

    $(document).ajaxStop(() => {
        $(".loading-div").hide();
    });

    let date = new Date();
    let currentMonth = String(date.getMonth() + 1).padStart(2, "0");
    let currentYear = date.getFullYear();

    let earningData = await getEarningValue(currentMonth, currentYear);
    let spendingValue = await getSpendingValue(currentMonth, currentYear);
    let spendingMonthly = spendingValue.monthly;
    let spendingSrcData = [
        spendingMonthly.electricSpending,
        spendingMonthly.waterSpending,
        spendingMonthly.rentSpending,
        spendingMonthly.anotherSpending,
    ];
    renderOverviewChart(earningData, spendingValue.spendingData);
    renderSpendingDoughnutChart(spendingSrcData);
});

async function getEarningValue(month, year) {
    let data;

    await $.ajax({
        url: `${API_START_URL}/api/Tuition/GetEarningValueByMonth/${month}/${year}`,
        method: "GET",
        contentType: "application/json",
        success: (res) => {
            $(".earning-monthly").empty();
            $(".earning-monthly").append(
                "VND " + number_format(res.data.monthly)
            );

            $(".earning-annual").empty();
            $(".earning-annual").append(
                "VND " + number_format(res.data.annual)
            );

            data = res.data.earningData;
        },
        error: (xhr) => {
            $.toast({
                heading: "Error!!!",
                text: xhr.responseJSON?.message,
                icon: "error",
                position: "top-right",
                showHideTransition: "plain",
            });
        },
    });

    return data;
}

let toastDisplayed = false;
let lastDisplayedDate = null;

const now = new Date();
if (!toastDisplayed || lastDisplayedDate.getDate() !== now.getDate()) {
    $.ajax({
        url: `${API_START_URL}/api/Tuition/GetDeadlineTutions`,
        type: "GET",
        contentType: "application/json",
        success: function (data) {
            $.toast({
                heading: "Notification",
                text: `You have ${data.data.length} notifications !!!`,
                icon: "notification",
                position: "top-right",
                showHideTransition: "plain",
            });
        },
    });

    toastDisplayed = true;
    lastDisplayedDate = now;
}

async function getSpendingValue(month, year) {
    let spendingValue;

    await $.ajax({
        url: `${API_START_URL}/api/Spending/GetSpendingValue/${month}/${year}`,
        method: "GET",
        contentType: "application/json",
        success: (res) => {
            let data = res.data;
            spendingValue = {
                monthly: data.monthly,
                spendingData: data.spendingData,
                spendingAnnual: data.spendingAnnual,
            };
            $(".spending-monthly").empty();
            $(".spending-monthly").append(
                `VND ${number_format(data.monthly.total)}`
            );

            $(".spending-annual").empty();
            $(".spending-annual").append(
                `VND ${number_format(data.spendingAnnual)}`
            );
        },
        error: (xhr) => {
            $.toast({
                heading: "Error!!!",
                text: xhr.responseJSON?.message,
                icon: "error",
                position: "top-right",
                showHideTransition: "plain",
            });
        },
    });

    return spendingValue;
}

function renderOverviewChart(earningData, spendingData) {
    new Chart($("#overviewChart"), {
        type: "line",
        data: {
            labels: [
                "Jan",
                "Feb",
                "Mar",
                "Apr",
                "May",
                "Jun",
                "Jul",
                "Aug",
                "Sep",
                "Oct",
                "Nov",
                "Dec",
            ],
            datasets: [
                {
                    label: "Earnings",
                    lineTension: 0.3,
                    backgroundColor: "rgba(78, 115, 223, 0.05)",
                    borderColor: "rgba(78, 115, 223, 1)",
                    pointRadius: 3,
                    pointBackgroundColor: "rgba(78, 115, 223, 1)",
                    pointBorderColor: "rgba(78, 115, 223, 1)",
                    pointHoverRadius: 3,
                    pointHoverBackgroundColor: "rgba(78, 115, 223, 1)",
                    pointHoverBorderColor: "rgba(78, 115, 223, 1)",
                    pointHitRadius: 10,
                    pointBorderWidth: 2,
                    data: earningData,
                },
                {
                    label: "Spendings",
                    lineTension: 0.3,
                    backgroundColor: "rgba(249, 155, 125, 0.05)",
                    borderColor: "rgba(249, 155, 125, 1)",
                    pointRadius: 3,
                    pointBackgroundColor: "rgba(231, 97, 97, 1)",
                    pointBorderColor: "rgba(231, 97, 97, 1)",
                    pointHoverRadius: 3,
                    pointHoverBackgroundColor: "rgba(231, 97, 97, 1)",
                    pointHoverBorderColor: "rgba(231, 97, 97, 1)",
                    pointHitRadius: 10,
                    pointBorderWidth: 2,
                    data: spendingData,
                },
            ],
        },
        options: {
            maintainAspectRatio: false,
            layout: {
                padding: {
                    left: 10,
                    right: 25,
                    top: 25,
                    bottom: 0,
                },
            },
            scales: {
                xAxes: [
                    {
                        time: {
                            unit: "date",
                        },
                        gridLines: {
                            display: false,
                            drawBorder: false,
                        },
                        ticks: {
                            maxTicksLimit: 12,
                        },
                    },
                ],
                yAxes: [
                    {
                        ticks: {
                            maxTicksLimit: 10,
                            padding: 10,
                            // Include a dollar sign in the ticks
                            callback: function (value, index, values) {
                                return "VND" + number_format(value);
                            },
                        },
                        gridLines: {
                            color: "rgb(234, 236, 244)",
                            zeroLineColor: "rgb(234, 236, 244)",
                            drawBorder: false,
                            borderDash: [2],
                            zeroLineBorderDash: [2],
                        },
                    },
                ],
            },
            legend: {
                display: true,
            },
            tooltips: {
                backgroundColor: "rgb(255,255,255)",
                bodyFontColor: "#858796",
                titleMarginBottom: 10,
                titleFontColor: "#6e707e",
                titleFontSize: 14,
                borderColor: "#dddfeb",
                borderWidth: 1,
                xPadding: 15,
                yPadding: 15,
                displayColors: false,
                intersect: false,
                mode: "index",
                caretPadding: 10,
                callbacks: {
                    label: function (tooltipItem, chart) {
                        var datasetLabel =
                            chart.datasets[tooltipItem.datasetIndex].label ||
                            "";
                        return (
                            datasetLabel +
                            ": VND" +
                            number_format(tooltipItem.yLabel)
                        );
                    },
                },
            },
        },
    });
}

function renderSpendingDoughnutChart(spendingSrcData) {
    new Chart($("#spendingSourcesChart"), {
        type: "doughnut",
        data: {
            labels: ["Electric", "Water", "Rent", "Another"],
            datasets: [
                {
                    data: spendingSrcData,
                    backgroundColor: [
                        "#4e73df",
                        "#1cc88a",
                        "#36b9cc",
                        "#858796",
                    ],
                    hoverBackgroundColor: [
                        "#2e59d9",
                        "#17a673",
                        "#2c9faf",
                        "#60616f",
                    ],
                    hoverBorderColor: "rgba(234, 236, 244, 1)",
                },
            ],
        },
        options: {
            maintainAspectRatio: false,
            tooltips: {
                backgroundColor: "rgb(255,255,255)",
                bodyFontColor: "#858796",
                borderColor: "#dddfeb",
                borderWidth: 1,
                xPadding: 15,
                yPadding: 15,
                displayColors: false,
                caretPadding: 10,
                callbacks: {
                    label: function (tooltipItem, data) {
                        var dataLabel = data.labels[tooltipItem.index];
                        var value =
                            ": VND" +
                            number_format(
                                data.datasets[tooltipItem.datasetIndex].data[
                                    tooltipItem.index
                                ]
                            );
                        if (Array.isArray(dataLabel)) {
                            dataLabel = dataLabel.slice();
                            dataLabel[0] += value;
                        } else {
                            dataLabel += value;
                        }
                        return dataLabel;
                    },
                },
            },
            legend: {
                display: false,
            },
            cutoutPercentage: 80,
        },
    });
}
