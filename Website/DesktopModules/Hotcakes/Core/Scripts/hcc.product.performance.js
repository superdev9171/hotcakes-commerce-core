﻿$(function () {
	$.fn.productPerformance = function (action) {
		if (action == "resizeChart") {
			var chartDiv = $(".hcProductPerformanceChart");
			var chartCanvas = $("#hcPerformanceChart");
			var ctx = chartCanvas.get(0).getContext('2d');

			var chartOpt = window.productPerformanceChart.options;
			var chartData = window.productPerformanceChart.data;

			window.productPerformanceChart.destroy();

			ctx.canvas.height = chartDiv.height();
			chartCanvas.css("height", chartDiv.height());

			ctx.canvas.width = chartDiv.width();
			chartCanvas.css("width", chartDiv.width());


			window.productPerformanceChart = new Chart(ctx, {
				type: 'line',
				data: chartData,
				options: chartOpt
			});
			return;
		}
		

		return this.each(function () {
			var self = $(this);

			self.pcModel = null;
			self.pwModel = null;
			self.events = null;
			self.productId = self.find(".hcProductId").val();
			self.chart = self.find(".hcProductPerformanceChart");
			self.periodSelector = self.find("select.hcPeriodSelector");
			self.createBundleBtn = self.find(".hcCreateBundle");
			//self.performanceChart = self.find("#hcPerformanceChart");
			

			self.bindPerformanceChartData = function () {
				var self = this;
				var scope = self.find(".hcProductPerformanceData");

				// skip if section doesn't exist
				if (!scope.length)
					return;

				scope.ajaxLoader("start");
                
				$.post(hcc.getResourceUrl("Performance.ashx"),
					{
						"method": "GetProductPerformanceData",
						"productId": self.productId,
						"period": self.periodSelector.val()
					},
					function (data) {
						if (self.pcModel) {
							ko.mapping.fromJS(data, self.pcModel);
						} else {
							self.pcModel = ko.mapping.fromJS(data);
							ko.applyBindings(self.pcModel, scope[0]);
						}
						//self.loadChart(data);
						self.loadProductPerformanceChart(data);
					}).always(function () {
						scope.ajaxLoader("stop");
					});
			};

			self.bindPurchasedWithData = function () {
				var self = this;
				var scope = self.find(".hcProductPurchasedWith");
                
				// skip if section doesn't exist
				if (!scope.length)
					return;

				scope.ajaxLoader("start");
                
				$.post(hcc.getResourceUrl("Performance.ashx"),
					{
						"method": "GetProductPurchasedWithData",
						"productId": self.productId,
						"period": self.periodSelector.val()
					},
					function (data) {
						if (self.pwModel) {
							ko.mapping.fromJS(data, self.pwModel);
						} else {
                        	self.pwModel = ko.mapping.fromJS(data);
							ko.applyBindings(self.pwModel, scope[0]);
						}
					}).always(function () {
						scope.ajaxLoader("stop");
					});
			};

			self.loadProductPerformanceChart = function (performanceData) {
				var ctx = document.getElementById("hcPerformanceChart").getContext("2d");

				var imageChageData = [];
				var copyChangeData = [];
				var priceChageData = [];
				var multiChangeData = [];

				$.each(performanceData.Events, function (indexInArray, value) {
					if(value == null) {
						imageChageData.push(value);
						copyChangeData.push(value);
						priceChageData.push(value);
						multiChangeData.push(value);
					}
					
					if (value != null && value.length) {
						//var isMultiChange = value.length > 1;
						//if (isMultiChange) { multiChangeData.push("Multiple Updates"); }
						$.each(value, function (i, v) {
							switch (v) {
								case "ProductImagesChanged":
									imageChageData.push("Image Update");
									break;
								case "ProductCopyChanged":
									copyChangeData.push("Copy Update");
									break;
								case "ProductPriceChanged":
									priceChageData.push("Price Update");
									break;
							}
						});
					}
				});

				var chartData = {
					labels: performanceData.ChartLabels,
					datasets: [
						{
							type: 'line',
							label: performanceData.BouncedName,
							borderColor: 'rgba(240,79,48,0.0)',
							backgroundColor: 'rgba(240,79,48,0.5)',
							lineTension: 0,
							pointRadius: 1,
							data: performanceData.BouncedData
						},
						{
							type: 'line',
							label: performanceData.AbandonedName,
							borderColor: 'rgba(240,151,39,0.0)',
							backgroundColor: 'rgba(240,151,39,0.5)',
							lineTension: 0,
							pointRadius: 1,
							data: performanceData.AbandonedData
						},
						{
							type: 'line',
							label: performanceData.PurchasedName,
							borderColor: 'rgba(13,178,186,0.0)',
							backgroundColor: 'rgba(13,178,186,0.5)',
							lineTension: 0,
							pointRadius: 1,
							data: performanceData.PurchasedData
						},
						{
							type: 'milestone',
							label: "Image Update",
							borderColor: 'rgba(115, 186, 13, 1)',
							backgroundColor: 'rgba(115, 186, 13, 1)',
							lineTension: 0,
							pointRadius: 1,
							data: imageChageData
						},
						{
							type: 'milestone',
							label: "Copy Update",
							borderColor: 'rgba(186, 13, 111, 1)',
							backgroundColor: 'rgba(186, 13, 111, 1)',
							lineTension: 0,
							pointRadius: 1,
							data: copyChangeData
						},
						{
							type: 'milestone',
							label: "Price Update",
							borderColor: 'rgba(131, 13, 186, 1)',
							backgroundColor: 'rgba(131, 13, 186, 1)',
							lineTension: 0,
							pointRadius: 1,
							data: priceChageData
						},
						//{
						//	type: 'milestone',
						//	label: "Multiple Updates",
						//	borderColor: 'rgba(0, 0, 0, 1)',
						//	backgroundColor: 'rgba(0, 0, 0, 1)',
						//	lineTension: 0,
						//	pointRadius: 1,
						//	data: multiChangeData
						//},
					]
				}

				var chartOptions = {
					//datasetFill : true,
					responsive: true,
					maintainAspectRatio: true,
					scales: {
						xAxes: [{
							gridLines: {
								display: true,
								drawBorder: true,
								drawOnChartArea: false,
							},
							ticks: {
								maxRotation: 0,
								minRotation: 0,
							}
						}],
						yAxes: [{
							gridLines: {
								display: true,
								drawBorder: true,
								drawOnChartArea: true,
							},
						}],
					},
					tooltips: {
						enabled: true,
						
						callbacks: {
							title: function(tooltipItems, data) {
								return '';
							},
							label: function(tooltipItem, data) {
								var dataset = data.datasets[tooltipItem.datasetIndex];
								var datasetLabel = dataset.label || '';
								var dataPoint =  dataset.type == "milestone" ? '' : " : " + dataset.data[tooltipItem.index];
								return datasetLabel + dataPoint;
							}
						}
					},
					legend: {
						display: true,
						labels : {
							boxWidth: 12,
							generateLabels: function (chart) {
								var data = chart.data;
								var helpers = Chart.helpers;
								return helpers.isArray(data.datasets) ? data.datasets.filter(function (d) { return d.type != 'milestone' }).map(function (dataset, i) {
									return {
										text: dataset.label,
										fillStyle: (!helpers.isArray(dataset.backgroundColor) ? dataset.backgroundColor : dataset.backgroundColor[0]),
										hidden: !chart.isDatasetVisible(i),
										lineCap: dataset.borderCapStyle,
										lineDash: dataset.borderDash,
										lineDashOffset: dataset.borderDashOffset,
										lineJoin: dataset.borderJoinStyle,
										lineWidth: 0, //dataset.borderWidth,
										strokeStyle: dataset.borderColor,
										pointStyle: dataset.pointStyle,
										datasetIndex: i
									};

								}, this) : [];
							}
						}

					},
					milestone_legend: {
						display: true,
						labels: {
							boxWidth: 12,
						}
					},
				};

				if (window.productPerformanceChart != null) {
					window.productPerformanceChart.destroy();
				}

				window.productPerformanceChart = new Chart(ctx, {
					type: 'line',
					data: chartData,
					options: chartOptions
				});
			};

			self.createBundle = function (e) {
				e.preventDefault();

				var scope = self.find(".hcProductPurchasedWith");
				scope.ajaxLoader("start");

				var selectedCheckboxes = self.find("[type=checkbox]:checked");
				var productIds = selectedCheckboxes.map(function (index, element) {
					return $(element).data("productid");
				}).get();

				$.post(hcc.getResourceUrl("Performance.ashx"),
				{
					"method": "CreateBundle",
					"productId": self.productId,
					"productIds": JSON.stringify(productIds)
				},
				function (data) {
					window.location.href = data;
				}).always(function () {
					scope.ajaxLoader("stop");
				});
			}

			self.periodSelector.change($.proxy(self.bindPerformanceChartData, self));
			self.periodSelector.change($.proxy(self.bindPurchasedWithData, self));
			self.createBundleBtn.click($.proxy(self.createBundle, self));

			self.periodSelector.change();
		});
	}
	$(".hcProductPerformance").productPerformance();

});
