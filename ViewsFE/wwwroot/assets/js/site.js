// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
window.createChart = (elementId, chartOptions, chartData) => {
    var chart = new ApexCharts(document.querySelector("#" + elementId), {
        chart: {
            type: 'bar'
        },
        series: chartData,
        options: chartOptions
    });

    chart.render();
};

window.updateChart = (elementId, chartData) => {
    var chart = ApexCharts.getChartByID(elementId);
    chart.updateSeries(chartData);
};
