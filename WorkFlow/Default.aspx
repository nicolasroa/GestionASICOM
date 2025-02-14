<%@ Register Assembly="Anthem" Namespace="Anthem" TagPrefix="anthem" %>

<!DOCTYPE html>

<html lang="en">
<head runat="server">
    <meta http-equiv="Content-Type  X-Content-Type-Options: nosniff" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <meta http-equiv="Expires" content="0" />
    <meta http-equiv="Last-Modified" content="0" />
    <meta http-equiv="Cache-Control" content="no-cache, mustrevalidate" />
    <meta http-equiv="Pragma" content="no-cache" />

    <title>Nuevo Capital</title>
    <script src="Scripts/Chart.min.js"></script>
	<script src="Scripts/utils.js"></script>
    <link href="../Content/Estilo/jquery-ui.css" rel="stylesheet" />
    <link href="../Content/bootstrap.css" rel="stylesheet" />
    <link href="../Content/Site.css" rel="stylesheet" />
    <link href="../Content/responsive.dataTables.min.css" rel="stylesheet" />
    <link href="../Content/tipped.css" rel="stylesheet" />
    <link href="../Content/bootstrap-dialog.min.css" rel="stylesheet" />
    <link href="~/favicon.ico" rel="shortcut icon" type="image/x-icon" />



    <style>
        .vcenter {
            vertical-align: middle;
        }
    </style>
    <script type="text/javascript">

        var MONTHS = ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'];
        var color = Chart.helpers.color;
       
        var barChartData = {
            labels: ['January', 'February', 'March', 'April', 'May', 'June', 'July'],
            datasets: [{
                label: 'Dataset ppp',
                backgroundColor: color(window.chartColors.red).alpha(0.5).rgbString(),
                borderColor: window.chartColors.red,
                borderWidth: 1,
                data: [
                    randomScalingFactor(),
                    randomScalingFactor(),
                    randomScalingFactor(),
                    randomScalingFactor(),
                    randomScalingFactor(),
                    randomScalingFactor(),
                    randomScalingFactor()
                ]
            }, {
                label: 'Dataset 2',
                backgroundColor: color(window.chartColors.blue).alpha(0.5).rgbString(),
                borderColor: window.chartColors.blue,
                borderWidth: 1,
                data: [
                    randomScalingFactor(),
                    randomScalingFactor(),
                    randomScalingFactor(),
                    randomScalingFactor(),
                    randomScalingFactor(),
                    randomScalingFactor(),
                    randomScalingFactor()
                ]
            }]

        };

        window.onload = function () {
            var ctx = document.getElementById('canvas').getContext('2d');
            window.myBar = new Chart(ctx, {
                type: 'bar',
                data: barChartData,
                options: {
                    responsive: true,
                    legend: {
                        position: 'top',
                    },
                    title: {
                        display: true,
                        text: 'Chart.js Bar Chart'
                    }
                }
            });

        };

        document.getElementById('randomizeData').addEventListener('click', function () {
            var zero = Math.random() < 0.2 ? true : false;
            barChartData.datasets.forEach(function (dataset) {
                dataset.data = dataset.data.map(function () {
                    return zero ? 0.0 : randomScalingFactor();
                });

            });
            window.myBar.update();
        });

        var colorNames = Object.keys(window.chartColors);
        document.getElementById('addDataset').addEventListener('click', function () {
            var colorName = colorNames[barChartData.datasets.length % colorNames.length];
            var dsColor = window.chartColors[colorName];
            var newDataset = {
                label: 'Dataset ' + (barChartData.datasets.length + 1),
                backgroundColor: color(dsColor).alpha(0.5).rgbString(),
                borderColor: dsColor,
                borderWidth: 1,
                data: []
            };

            for (var index = 0; index < barChartData.labels.length; ++index) {
                newDataset.data.push(randomScalingFactor());
            }

            barChartData.datasets.push(newDataset);
            window.myBar.update();
        });

        document.getElementById('addData').addEventListener('click', function () {
            if (barChartData.datasets.length > 0) {
                var month = MONTHS[barChartData.labels.length % MONTHS.length];
                barChartData.labels.push(month);

                for (var index = 0; index < barChartData.datasets.length; ++index) {
                    // window.myBar.addData(randomScalingFactor(), index);
                    barChartData.datasets[index].data.push(randomScalingFactor());
                }

                window.myBar.update();
            }
        });

        document.getElementById('removeDataset').addEventListener('click', function () {
            barChartData.datasets.pop();
            window.myBar.update();
        });

        document.getElementById('removeData').addEventListener('click', function () {
            barChartData.labels.splice(-1, 1); // remove the label first

            barChartData.datasets.forEach(function (dataset) {
                dataset.data.pop();
            });

            window.myBar.update();
        });



    </script>


</head>
<body class="body">
    <form runat="server" id="Formulario">
        
      

        <table style="width:100%;">
            <tr>
                <td rowspan="2">Ingresos</td>
                <td colspan="2" style="text-align: center">Deudor</td>
                <td colspan="2" style="text-align: center">Codeudor</td>
                <td colspan="2" style="text-align: center">Fiador/Aval</td>
            </tr>
            <tr>
                <td>$</td>
                <td>UF</td>
                <td>$</td>
                <td>UF</td>
                <td>$</td>
                <td>UF</td>
            </tr>
            <tr>
                <td>Dato 1</td>
                <td>m$Dato1Deudor</td>
                <td>mUFDato1Deudor</td>
                <td>m$Dato1Codeudor</td>
                <td>mUFDato1Codeudor</td>
                <td>m$Dato1Aval</td>
                <td>mUFDato1Aval</td>
            </tr>
            <tr>
                <td>Dato 2</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>Dato 3</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>Dato 4</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>Total Ingresos</td>
                <td>TotalDeudor$</td>
                <td>TotalDeudorUF</td>
                <td>TotalCodeudor$</td>
                <td>TotalCodeudorUF</td>
                <td>TotalAval$</td>
                <td>TotalAvalUF</td>
            </tr>
            <tr>
                <td>TOTAL INGRESOS COMPLEMENTADOS</td>
                <td colspan="6">$ TotalIngresos$</td>
            </tr>
        </table>
        
      

    </form>
</body>
</html>
