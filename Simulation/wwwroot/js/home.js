"use strict";

const connection = new signalR.HubConnectionBuilder()
    .withUrl("/homeHub")
    .configureLogging(signalR.LogLevel.Information)
    .build();

$(function () {
    new Chart(document.getElementById('chart-machine-1').getContext('2d'), {
        type: 'pie',
        data: {
            labels: ["OK", "NG"],
            datasets: [{
                data: [90, 10],
                backgroundColor: [
                    '#66b032', '#e4491d'
                ]
            }]
        },
        options: {
            responsive: true,
            maintainAspectRatio: false,
            title: {
                display: false,
                text: null,
            },
            plugins: {
                legend: {
                    position: 'right',
                    labels: {
                        color: '#667085',
                    }
                },
                tooltip: {
                    enabled: false,
                },
                datalabels: {
                    formatter: (value, context) => {
                        const datapoints = context.chart.data.datasets[0].data;
                        function totalSum(total, datapoint) {
                            return total + datapoint;
                        }
                        const totalValue = datapoints.reduce(totalSum, 0);
                        const percentageValue = (value / totalValue * 100).toFixed(2);
                        return `${percentageValue}%`;
                    },
                    color: '#000000',
                    font: {
                        weight: 'bold',
                        size: 13,
                    },
                    align: 'center',
                    anchor: 'center'
                }
            }
        },
        plugins: [ChartDataLabels]
    });

    new Chart(document.getElementById('chart-machine-2').getContext('2d'), {
        type: 'pie',
        data: {
            labels: ["OK", "NG"],
            datasets: [{
                data: [90, 10],
                backgroundColor: [
                    '#66b032', '#e4491d'
                ]
            }]
        },
        options: {
            responsive: true,
            maintainAspectRatio: false,
            plugins: {
                legend: {
                    position: 'right',
                    labels: {
                        color: '#667085',
                    }
                },
                tooltip: {
                    enabled: false,
                },
                datalabels: {
                    formatter: (value, context) => {
                        const datapoints = context.chart.data.datasets[0].data;
                        function totalSum(total, datapoint) {
                            return total + datapoint;
                        }
                        const totalValue = datapoints.reduce(totalSum, 0);
                        const percentageValue = (value / totalValue * 100).toFixed(2);
                        return `${percentageValue}%`;
                    },
                    color: '#000000',
                    font: {
                        weight: 'bold',
                        size: 13,
                    },
                    align: 'center',
                    anchor: 'center'
                }
            }
        },
        plugins: [ChartDataLabels]
    });

    new Chart(document.getElementById('chart-yield-today').getContext('2d'), {
        type: 'bar',
        data: {
            labels: ['May 20, 2024', 'May 21, 2024', 'May 22, 2024', 'May 23, 2024', 'May 24, 2024', 'May 25, 2024', 'May 26, 2024'],
            datasets: [
                {
                    label: 'Machine 1',
                    data: [3050, 2000, 3000, 4000, 5000, 4000, 3500],
                    backgroundColor: '#165BAA',
                    borderRadius: 3,
                },
                {
                    label: 'Machine 2',
                    data: [5050, 4500, 3700, 4400, 2800, 3700, 4000],
                    backgroundColor: '#F765A3',
                    borderRadius: 3,
                }
            ]
        },
        options: {
            maintainAspectRatio: false,
            plugins: {
                legend: {
                    position: 'bottom',
                    labels: {
                        color: '#667085',
                    }
                },
            },
            responsive: true,
            barThickness: 40,
            scales: {
                x: {
                    stacked: true,
                    barPercentage: 0.5,
                },
                y: {
                    stacked: true,
                    barPercentage: 0.5,
                }
            }
        }
    });

    new Chart(document.getElementById('chart-defect-rate').getContext('2d'), {
        type: 'line',
        data: {
            labels: ['May 20, 2024', 'May 21, 2024', 'May 22, 2024', 'May 23, 2024', 'May 24, 2024', 'May 25, 2024'],
            datasets: [
                {
                    label: 'Machine 1',
                    data: [0.3, 0.9, 0.4, 0.6, 0.5, 0.4],
                },
                {
                    label: 'Machine 2',
                    data: [0.5, 0.45, 0.37, 0.44, 0.28, 0.37],
                }
            ]
        },
        options: {
            maintainAspectRatio: false,
            responsive: true,
            plugins: {
                legend: {
                    position: 'bottom',
                }
            }
        },
    });

    new Chart(document.getElementById('chart-error-detect').getContext('2d'), {
        type: 'doughnut',
        data: {
            labels: ['BLACK DOT', 'DIRTY', 'GLUE', 'NG SUS', 'BLUE', 'NG HOLE'],
            datasets: [
                {
                    label: 'Dataset 1',
                    data: [10, 17, 26, 10, 18, 19],
                    backgroundColor: [
                        '#D956CC',
                        '#FABBFB',
                        '#F492F6',
                        '#FBBBF1',
                        '#EB77ED',
                        '#F2BBFB',
                    ]
                }
            ]
        },
        options: {
            maintainAspectRatio: false,
            responsive: true,
            plugins: {
                legend: {
                    position: 'bottom',
                },
                tooltip: {
                    enabled: false,
                },
                datalabels: {
                    formatter: (value, context) => {
                        const datapoints = context.chart.data.datasets[0].data;
                        function totalSum(total, datapoint) {
                            return total + datapoint;
                        }
                        const totalValue = datapoints.reduce(totalSum, 0);
                        const percentageValue = (value / totalValue * 100).toFixed(2);
                        return `${percentageValue}%`;
                    },
                    color: '#000000',
                    font: {
                        weight: 'bold',
                        size: 11,
                    },
                    align: 'center',
                    anchor: 'center'
                }
            }
        },
        plugins: [ChartDataLabels]
    });
});