$(function () {
	"use strict";
	// chart 1
	Highcharts.chart('chart1', {
		chart: {
			plotBackgroundColor: null,
			plotBorderWidth: null,
			plotShadow: false,
			type: 'pie',
			styledMode: true
		},
		credits: {
			enabled: false
		},
		title: {
			text: 'سهم بازار مرورگر در اردیبهشت 1400'
		},
		tooltip: {
			useHTML: true,
			pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
		},
		accessibility: {
			point: {
				valueSuffix: '%'
			}
		},
		plotOptions: {
			useHTML: true,
			pie: {
				allowPointSelect: true,
				cursor: 'pointer',
				dataLabels: {
					useHTML: true,
					enabled: true,
					format: '<b>{point.name}</b>: {point.percentage:.1f} %'
				}
			}
		},
		series: [{
			name: 'برند ها',
			colorByPoint: true,
			data: [{
				name: 'کروم',
				y: 61.41,
				sliced: true,
				selected: true
			}, {
				name: 'اینترنت اکسپلورر',
				y: 11.84
			}, {
				name: 'فایرفاکس',
				y: 10.85
			}, {
				name: 'مایکروسافت اج',
				y: 4.67
			}, {
				name: 'سافاری',
				y: 4.18
			}, {
				name: 'مرورگر سوگو',
				y: 1.64
			}, {
				name: 'اپرا',
				y: 1.6
			}, {
				name: 'مرورگر QQ',
				y: 1.2
			}, {
				name: 'دیگر',
				y: 2.61
			}]
		}]
	});
	// chart 2
	// Build the chart
	Highcharts.chart('chart2', {
		chart: {
			useHTML: true,
			plotBackgroundColor: null,
			plotBorderWidth: null,
			plotShadow: false,
			type: 'pie',
			styledMode: true
		},
		credits: {
			enabled: false
		},
		title: {
			useHTML: true,
			text: 'سهم بازار مرورگر در اردیبهشت 1400'
		},
		legend: {
			rtl: true
		},
		tooltip: {
			useHTML: true,
			pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
		},
		accessibility: {
			useHTML: true,
			point: {
				useHTML: true,
				valueSuffix: '%'
			}
		},
		plotOptions: {
			useHTML: true,
			direction: 'rtl',
			pie: {
				useHTML: true,
				allowPointSelect: true,
				cursor: 'pointer',
				dataLabels: {
					useHTML: true,
					enabled: false
				},
				useHTML: true,
				showInLegend: true
			}
		},
		series: [{
			useHTML: true,
			name: 'برند ها',
			colorByPoint: true,
			data: [{
				useHTML: true,
				name: 'کروم',
				y: 61.41,
				sliced: true,
				selected: true
			}, {
				name: 'اینترنت اکسپلورر',
				y: 11.84
			}, {
				name: 'فایرفاکس',
				y: 10.85
			}, {
				name: 'مایکروسافت اج',
				y: 4.67
			}, {
				name: 'سافاری',
				y: 4.18
			}, {
				name: 'دیگر',
				y: 7.05
			}]
		}]
	});
	// chart 3
	Highcharts.chart('chart3', {
		chart: {
			useHTML: true,
			type: 'variablepie',
			styledMode: true
		},
		credits: {
			useHTML: true,
			enabled: false
		},
		title: {
			useHTML: true,
			text: 'مقایسه کشورها از نظر تراکم جمعیت و مساحت کل'
		},
		tooltip: {
			useHTML: true,
			headerFormat: '',
			pointFormat: '<span style="color:{point.color}">\u25CF</span> <b> {point.name}</b><br/>' + 'مساحت (کیلومتر مربع) : <b>{point.y}</b><br/>' + 'تراکم جمعیت (مردم در هر کیلومتر مربع) : <b>{point.z}</b><br/>'
		},
		series: [{
			useHTML: true,
			minPointSize: 10,
			useHTML: true,
			innerSize: '20%',
			zMin: 0,
			name: 'کشور',
			data: [{
				name: 'اسپانیا',
				y: 505370,
				z: 92.9
			}, {
				name: 'فرانسه',
				y: 551500,
				z: 118.7
			}, {
				name: 'لهستان',
				y: 312685,
				z: 124.6
			}, {
				name: 'جمهوری چک',
				y: 78867,
				z: 137.5
			}, {
				name: 'ایتالیا',
				y: 301340,
				z: 201.8
			}, {
				name: 'سوییس',
				y: 41277,
				z: 214.5
			}, {
				name: 'آلمان',
				y: 357022,
				z: 235.6
			}]
		}]
	});
	// chart4
	// Make monochrome colors
	var pieColors = (function () {
		var colors = [],
			base = Highcharts.getOptions().colors[0],
			i;
		for (i = 0; i < 10; i += 1) {
			// Start out with a darkened base color (negative brighten), and end
			// up with a much brighter color
			colors.push(Highcharts.color(base).brighten((i - 3) / 7).get());
		}
		return colors;
	}());
	// Build the chart
	Highcharts.chart('chart4', {
		chart: {
			useHTML: true,
			plotBackgroundColor: null,
			plotBorderWidth: null,
			plotShadow: false,
			styledMode: true,
			type: 'pie'
		},
		credits: {
			enabled: false
		},
		title: {
			useHTML: true,
			text: 'سهم بازار مرورگر در یک وبسایت خاص، 1400'
		},
		tooltip: {
			useHTML: true,
			pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
		},
		accessibility: {
			point: {
				valueSuffix: '%'
			}
		},
		plotOptions: {
			useHTML: true,
			pie: {
				useHTML: true,
				allowPointSelect: true,
				cursor: 'pointer',
				colors: pieColors,
				dataLabels: {
					useHTML: true,
					enabled: true,
					format: '<b>{point.name}</b><br>{point.percentage:.1f} %',
					distance: -50,
					filter: {
						useHTML: true,
						property: 'percentage',
						operator: '>',
						value: 4
					}
				}
			}
		},
		series: [{
			useHTML: true,
			name: 'سهم',
			data: [{
				name: 'کروم',
				y: 61.41
			}, {
				name: 'اینترنت اکسپلورر',
				y: 11.84
			}, {
				name: 'فایرفاکس',
				y: 10.85
			}, {
				name: 'مایکروسافت اج',
				y: 4.67
			}, {
				name: 'سافاری',
				y: 4.18
			}, {
				name: 'دیگر',
				y: 7.05
			}]
		}]
	});
	// chart 5
	var colors = Highcharts.getOptions().colors,
		categories = ['کروم', 'فایرفاکس', 'اینترنت اکسپلورر', 'سافاری', 'مایکروسافت اج', 'اپرا', 'دیگر'],
		data = [{
			useHTML: true,
			y: 62.74,
			color: colors[2],
			drilldown: {
				useHTML: true,
				name: 'کروم',
				categories: ['کروم v65.0', 'کروم v64.0', 'کروم v63.0', 'کروم v62.0', 'کروم v61.0', 'کروم v60.0', 'کروم v59.0', 'کروم v58.0', 'کروم v57.0', 'کروم v56.0', 'کروم v55.0', 'کروم v54.0', 'کروم v51.0', 'کروم v49.0', 'کروم v48.0', 'کروم v47.0', 'کروم v43.0', 'کروم v29.0'],
				data: [
					0.1,
					1.3,
					53.02,
					1.4,
					0.88,
					0.56,
					0.45,
					0.49,
					0.32,
					0.29,
					0.79,
					0.18,
					0.13,
					2.16,
					0.13,
					0.11,
					0.17,
					0.26
				]
			}
		}, {
			y: 10.57,
			color: colors[1],
			useHTML: true,
			drilldown: {
				useHTML: true,
				name: 'فایرفاکس',
				categories: ['فایرفاکس v58.0', 'فایرفاکس v57.0', 'فایرفاکس v56.0', 'فایرفاکس v55.0', 'فایرفاکس v54.0', 'فایرفاکس v52.0', 'فایرفاکس v51.0', 'فایرفاکس v50.0', 'فایرفاکس v48.0', 'فایرفاکس v47.0'],
				data: [
					1.02,
					7.36,
					0.35,
					0.11,
					0.1,
					0.95,
					0.15,
					0.1,
					0.31,
					0.12
				]
			}
		}, {
			y: 7.23,
			useHTML: true,
			color: colors[0],
			drilldown: {
				useHTML: true,
				name: 'اینترنت اکسپلورر',
				categories: ['اینترنت اکسپلورر v11.0', 'اینترنت اکسپلورر v10.0', 'اینترنت اکسپلورر v9.0', 'اینترنت اکسپلورر v8.0'],
				data: [
					6.2,
					0.29,
					0.27,
					0.47
				]
			}
		}, {
			y: 5.58,
			useHTML: true,
			color: colors[3],
			drilldown: {
				useHTML: true,
				name: 'سافاری',
				categories: ['سافاری v11.0', 'سافاری v10.1', 'سافاری v10.0', 'سافاری v9.1', 'سافاری v9.0', 'سافاری v5.1'],
				data: [
					3.39,
					0.96,
					0.36,
					0.54,
					0.13,
					0.2
				]
			}
		}, {
			y: 4.02,
			useHTML: true,
			color: colors[5],
			drilldown: {
				useHTML: true,
				name: 'مایکروسافت اج',
				categories: ['مایکروسافت اج v16', 'مایکروسافت اج v15', 'مایکروسافت اج v14', 'مایکروسافت اج v13'],
				data: [
					2.6,
					0.92,
					0.4,
					0.1
				]
			}
		}, {
			y: 1.92,
			useHTML: true,
			color: colors[4],
			drilldown: {
				useHTML: true,
				name: 'اپرا',
				categories: ['اپرا v50.0', 'اپرا v49.0', 'اپرا v12.1'],
				data: [
					0.96,
					0.82,
					0.14
				]
			}
		}, {
			y: 7.62,
			useHTML: true,
			color: colors[6],
			drilldown: {
				useHTML: true,
				name: 'دیگر',
				categories: ['دیگر'],
				data: [
					7.62
				]
			}
		}],
		browserData = [],
		versionsData = [],
		i,
		j,
		dataLen = data.length,
		drillDataLen,
		brightness;
	// Build the data arrays
	for (i = 0; i < dataLen; i += 1) {
		// add browser data
		browserData.push({
			name: categories[i],
			y: data[i].y,
			color: data[i].color
		});
		// add version data
		drillDataLen = data[i].drilldown.data.length;
		for (j = 0; j < drillDataLen; j += 1) {
			brightness = 0.2 - (j / drillDataLen) / 5;
			versionsData.push({
				name: data[i].drilldown.categories[j],
				y: data[i].drilldown.data[j],
				color: Highcharts.color(data[i].color).brighten(brightness).get()
			});
		}
	}
	// Create the chart
	Highcharts.chart('chart5', {
		chart: {
			useHTML: true,
			type: 'pie',
			styledMode: true
		},
		credits: {
			useHTML: true,
			enabled: false
		},
		title: {
			useHTML: true,
			text: 'سهم بازار مرورگر، اردیبهشت 1400'
		},
		subtitle: {
			useHTML: true,
			text: 'منبع : <a href="http://statcounter.com" target="_blank">statcounter.com</a>'
		},
		plotOptions: {
			useHTML: true,
			pie: {
				useHTML: true,
				shadow: false,
				center: ['50%', '50%']
			}
		},
		tooltip: {
			useHTML: true,
			valueSuffix: '%'
		},
		series: [{
			useHTML: true,
			name: 'مرورگر',
			data: browserData,
			size: '60%',
			dataLabels: {
				formatter: function () {
					return this.y > 5 ? this.point.name : null;
				},
				color: '#ffffff',
				useHTML: true,
				distance: -30
			}
		}, {
			name: 'نسخه',
			useHTML: true,
			data: versionsData,
			size: '80%',
			innerSize: '60%',
			dataLabels: {
				useHTML: true,
				formatter: function () {
					// display only if larger than 1
					return this.y > 1 ? '<b>' + this.point.name + ':</b> ' + this.y + '%' : null;
				}
			},
			id: 'versions'
		}],
		responsive: {
			rules: [{
				condition: {
					maxWidth: 400
				},
				chartOptions: {
					series: [{}, {
						id: 'versions',
						dataLabels: {
							useHTML: true,
							enabled: false
						}
					}]
				}
			}]
		}
	});
	// chart 6
	Highcharts.chart('chart6', {
		chart: {
			useHTML: true,
			plotBackgroundColor: null,
			plotBorderWidth: 0,
			styledMode: true,
			plotShadow: false
		},
		credits: {
			enabled: false
		},
		title: {
			useHTML: true,
			text: 'مرورگر<br>اشتراک گذاری<br>1400',
			align: 'center',
			verticalAlign: 'middle',
			y: 60
		},
		tooltip: {
			useHTML: true,
			pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
		},
		accessibility: {
			point: {
				useHTML: true,
				valueSuffix: '%'
			}
		},
		plotOptions: {
			useHTML: true,
			pie: {
				dataLabels: {
					useHTML: true,
					enabled: true,
					distance: -50,
					style: {
						useHTML: true,
						fontWeight: 'bold',
						color: 'white'
					}
				},
				startAngle: -90,
				useHTML: true,
				endAngle: 90,
				center: ['50%', '75%'],
				size: '110%'
			}
		},
		series: [{
			useHTML: true,
			type: 'pie',
			name: 'سهم مرورگر',
			innerSize: '50%',
			data: [
				['کروم', 58.9],
				['فایرفاکس', 13.29],
				['اینترنت اکسپلورر', 13],
				['مایکروسافت اج', 3.78],
				['سافاری', 3.42], {
					name: 'دیگر',
					y: 7.61,
					dataLabels: {
						useHTML: true,
						enabled: false
					}
				}
			]
		}]
	});
	// chart7
	Highcharts.chart('chart7', {
		chart: {
			type: 'bar',
			useHTML: true,
			styledMode: true
		},
		credits: {
			useHTML: true,
			enabled: false
		},
		title: {
			useHTML: true,
			text: 'جمعیت تاریخی جهان بر اساس مناطق'
		},
		subtitle: {
			useHTML: true,
			text: 'منبع : <a href="https://en.wikipedia.org/wiki/World_population">Wikipedia.org</a>'
		},
		xAxis: {
			useHTML: true,
			categories: ['آفریقا', 'آمریکا', 'آسیا', 'اروپا', 'اقیانوسیه'],
			title: {
				useHTML: true,
				text: null
			}
		},
		yAxis: {
			min: 0,
			title: {
				useHTML: true,
				text: 'جمعیت (میلیون)',
				align: 'high'
			},
			labels: {
				useHTML: true,
				overflow: 'justify'
			}
		},
		tooltip: {
			useHTML: true,
			valueSuffix: ' میلیون'
		},
		plotOptions: {
			bar: {
				dataLabels: {
					enabled: true
				}
			}
		},
		legend: {
			useHTML: true,
			layout: 'vertical',
			align: 'right',
			rtl: true,
			verticalAlign: 'top',
			x: -40,
			y: 80,
			floating: true,
			borderWidth: 1,
			backgroundColor: Highcharts.defaultOptions.legend.backgroundColor || '#FFFFFF',
			shadow: true
		},
		credits: {
			enabled: false
		},
		series: [{
			name: 'سال 1800',
			data: [107, 31, 635, 203, 2]
		}, {
			name: 'سال 1900',
			data: [133, 156, 947, 408, 6]
		}, {
			name: 'سال 2000',
			data: [814, 841, 3714, 727, 31]
		}, {
			name: 'سال 2020',
			data: [1216, 1001, 4436, 738, 40]
		}]
	});
	// chart 8
	Highcharts.chart('chart8', {
		chart: {
			useHTML: true,
			type: 'column',
			styledMode: true
		},
		credits: {
			useHTML: true,
			enabled: false
		},
		title: {
			useHTML: true,
			text: 'میانگین بارندگی ماهانه'
		},
		subtitle: {
			useHTML: true,
			text: 'منبع : WorldClimate.com'
		},
		legend: {
			rtl: true
		},
		xAxis: {
			useHTML: true,
			categories: ['فروردین', 'اردیبهشت', 'خرداد', 'تیر', 'مرداد', 'شهریور', 'مهر', 'آبان', 'آذر', 'دی', 'بهمن', 'اسفند'],
			crosshair: true
		},
		yAxis: {
			useHTML: true,
			min: 0,
			title: {
				useHTML: true,
				text: 'بارندگی (میلی متر)'
			}
		},
		tooltip: {
			useHTML: true,
			headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
			pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' + '<td style="padding:0"><b>{point.y:.1f} میلی متر</b></td></tr>',
			footerFormat: '</table>',
			shared: true,
			useHTML: true
		},
		plotOptions: {
			column: {
				pointPadding: 0.2,
				borderWidth: 0
			}
		},
		series: [{
			name: 'تهران',
			data: [49.9, 71.5, 106.4, 129.2, 144.0, 176.0, 135.6, 148.5, 216.4, 194.1, 95.6, 54.4]
		}, {
			name: 'توکیو',
			data: [83.6, 78.8, 98.5, 93.4, 106.0, 84.5, 105.0, 104.3, 91.2, 83.5, 106.6, 92.3]
		}, {
			name: 'لندن',
			data: [48.9, 38.8, 39.3, 41.4, 47.0, 48.3, 59.0, 59.6, 52.4, 65.2, 59.3, 51.2]
		}, {
			name: 'برلین',
			data: [42.4, 33.2, 34.5, 39.7, 52.6, 75.5, 57.4, 60.4, 47.6, 39.1, 46.8, 51.1]
		}]
	});
	// chart 9
	Highcharts.chart('chart9', {
		chart: {
			useHTML: true,
			type: 'bar',
			styledMode: true
		},
		credits: {
			useHTML: true,
			enabled: false
		},
		title: {
			useHTML: true,
			text: 'نمودار ستونی انباشته'
		},
		tooltip: {
			useHTML: true
		},
		xAxis: {
			useHTML: true,
			categories: ['سیب', 'پرتقال', 'گلابی', 'گریپ فروت', 'موز']
		},
		yAxis: {
			useHTML: true,
			min: 0,
			title: {
				useHTML: true,
				text: 'مصرف کل میوه'
			}
		},
		legend: {
			useHTML: true,
			rtl: true,
			reversed: true
		},
		plotOptions: {
			useHTML: true,
			series: {
				useHTML: true,
				stacking: 'normal'
			}
		},
		series: [{
			name: 'رضا',
			data: [5, 3, 4, 7, 2]
		}, {
			name: 'پریسا',
			data: [2, 2, 3, 2, 1]
		}, {
			name: 'ساناز',
			data: [3, 4, 4, 2, 5]
		}]
	});
	// chart 10
	// Create the chart
	Highcharts.chart('chart10', {
		chart: {
			useHTML: true,
			type: 'column',
			styledMode: true
		},
		credits: {
			useHTML: true,
			enabled: false
		},
		title: {
			useHTML: true,
			useHTML: true,
			text: 'سهم بازار مرورگر، خرداد 1400'
		},
		subtitle: {
			useHTML: true,
			useHTML: true,
			text: 'منبع : <a href="http://statcounter.com" target="_blank">statcounter.com</a>'
		},
		accessibility: {
			announceNewData: {
				useHTML: true,
				enabled: true
			}
		},
		xAxis: {
			useHTML: true,
			type: 'category'
		},
		yAxis: {
			title: {
				useHTML: true,
				text: 'درصد کل سهام بازار'
			}
		},
		legend: {
			useHTML: true,
			enabled: false
		},
		plotOptions: {
			useHTML: true,
			series: {
				useHTML: true,
				borderWidth: 0,
				dataLabels: {
					enabled: true,
					format: '{point.y:.1f}%'
				}
			}
		},
		tooltip: {
			useHTML: true,
			headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
			pointFormat: '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:.2f}%</b> از مجموع کل<br/>'
		},
		series: [{
			name: "مرورگر",
			colorByPoint: true,
			data: [{
				name: "کروم",
				y: 62.74,
				drilldown: "کروم"
			}, {
				name: "فایرفاکس",
				y: 10.57,
				drilldown: "فایرفاکس"
			}, {
				name: "اینترنت اکسپلورر",
				y: 7.23,
				drilldown: "اینترنت اکسپلورر"
			}, {
				name: "سافاری",
				y: 5.58,
				drilldown: "سافاری"
			}, {
				name: "مایکروسافت اج",
				y: 4.02,
				drilldown: "مایکروسافت اج"
			}, {
				name: "اپرا",
				y: 1.92,
				drilldown: "اپرا"
			}, {
				name: "دیگر",
				y: 7.62,
				drilldown: null
			}]
		}],
		drilldown: {
			series: [{
				name: "کروم",
				id: "کروم",
				data: [
					["v65.0",
						0.1
					],
					["v64.0",
						1.3
					],
					["v63.0",
						53.02
					],
					["v62.0",
						1.4
					],
					["v61.0",
						0.88
					],
					["v60.0",
						0.56
					],
					["v59.0",
						0.45
					],
					["v58.0",
						0.49
					],
					["v57.0",
						0.32
					],
					["v56.0",
						0.29
					],
					["v55.0",
						0.79
					],
					["v54.0",
						0.18
					],
					["v51.0",
						0.13
					],
					["v49.0",
						2.16
					],
					["v48.0",
						0.13
					],
					["v47.0",
						0.11
					],
					["v43.0",
						0.17
					],
					["v29.0",
						0.26
					]
				]
			}, {
				name: "فایرفاکس",
				id: "فایرفاکس",
				data: [
					["v58.0",
						1.02
					],
					["v57.0",
						7.36
					],
					["v56.0",
						0.35
					],
					["v55.0",
						0.11
					],
					["v54.0",
						0.1
					],
					["v52.0",
						0.95
					],
					["v51.0",
						0.15
					],
					["v50.0",
						0.1
					],
					["v48.0",
						0.31
					],
					["v47.0",
						0.12
					]
				]
			}, {
				name: "اینترنت اکسپلورر",
				id: "اینترنت اکسپلورر",
				data: [
					["v11.0",
						6.2
					],
					["v10.0",
						0.29
					],
					["v9.0",
						0.27
					],
					["v8.0",
						0.47
					]
				]
			}, {
				name: "سافاری",
				id: "سافاری",
				data: [
					["v11.0",
						3.39
					],
					["v10.1",
						0.96
					],
					["v10.0",
						0.36
					],
					["v9.1",
						0.54
					],
					["v9.0",
						0.13
					],
					["v5.1",
						0.2
					]
				]
			}, {
				name: "مایکروسافت اج",
				id: "مایکروسافت اج",
				data: [
					["v16",
						2.6
					],
					["v15",
						0.92
					],
					["v14",
						0.4
					],
					["v13",
						0.1
					]
				]
			}, {
				name: "اپرا",
				id: "اپرا",
				data: [
					["v50.0",
						0.96
					],
					["v49.0",
						0.82
					],
					["v12.1",
						0.14
					]
				]
			}]
		}
	});
	// chart 11
	Highcharts.chart('chart11', {
		chart: {
			useHTML: true,
			type: 'area',
			styledMode: true
		},
		credits: {
			useHTML: true,
			enabled: false
		},
		accessibility: {
			useHTML: true,
			description: 'توضیحات تصویر: یک نمودار منطقه ذخایر هسته ای ایالات متحده آمریکا و اتحاد جماهیر شوروی سوسیالیستی روسیه / روسیه را بین سالهای 1945 و 2017 مقایسه می کند. تعداد سلاح های هسته ای در محور Y و سالها در محور X رسم شده است. نمودار تعاملی است و می توان سطح ذخیره سال به سال را برای هر کشور ردیابی کرد. ایالات متحده 6 سلاح هسته ای در اوایل عصر هسته ای در سال 1945 ذخیره کرده است. این تعداد بتدریج تا سال 1950 که اتحاد جماهیر شوروی با 6 سلاح وارد مسابقه تسلیحاتی می شود ، به 369 سلاح هسته ای افزایش می یابد. در این مرحله ، ایالات متحده شروع به ساخت سریع ذخایر خود می کند که در سال 1966 با 32،040 کلاهک در مقایسه با 7،089 کلاهبرداری اتحاد جماهیر شوروی (شوروی) به پایان رسید. از این اوج در سال 1966 ، با گسترش ذخایر اتحاد جماهیر شوروی (شوروی) ، ذخایر ایالات متحده به تدریج کاهش می یابد. تا سال 1978 اتحاد جماهیر شوروی سوسیالیستی شکاف هسته ای را در 25393 مورد کاهش داد. ذخایر اتحاد جماهیر شوروی سابق به رشد خود ادامه می دهد تا جایی که در سال 1986 در مقایسه با 24401 زرادخانه ایالات متحده به اوج 45000 رسید. از سال 1986 ، ذخایر هسته ای هر دو کشور شروع به سقوط می کند. تا سال 2000 ، این تعداد برای ایالات متحده و روسیه به ترتیب به 10،577 و 21،000 رسیده است. این کاهش ها تا سال 2017 ادامه دارد و در آن زمان ایالات متحده 4،018 سلاح در مقایسه با 4500 سلاح روسیه در اختیار دارد.'
		},
		title: {
			useHTML: true,
			text: 'ذخایر هسته ای ایالات متحده و اتحاد جماهید شوروی سوسیالیستی'
		},
		subtitle: {
			useHTML: true,
			text: 'منبع : <a href="https://thebulletin.org/2006/july/global-nuclear-stockpiles-1945-2006">' + 'thebulletin.org</a> و <a href="https://www.armscontrol.org/factsheets/Nuclearweaponswhohaswhat">' + 'armscontrol.org</a>'
		},
		legend: {
			rtl: true
		},
		xAxis: {
			useHTML: true,
			allowDecimals: false,
			labels: {
				useHTML: true,
				formatter: function () {
					return this.value; // clean, unformatted number for year
				}
			},
			accessibility: {
				useHTML: true,
				rangeDescription: 'محدوده : 1940 تا 2017'
			}
		},
		yAxis: {
			title: {
				useHTML: true,
				text: 'کشور های سلاح هسته ای'
			},
			labels: {
				useHTML: true,
				formatter: function () {
					return this.value / 1000 + 'k';
				}
			}
		},
		tooltip: {
			useHTML: true,
			pointFormat: '{series.name} ذخیره کرده بود <b>{point.y:,.0f}</b><br/>کلاهک در سال {point.x}'
		},
		plotOptions: {
			useHTML: true,
			area: {
				useHTML: true,
				pointStart: 1940,
				marker: {
					useHTML: true,
					enabled: false,
					symbol: 'circle',
					radius: 2,
					states: {
						hover: {
							useHTML: true,
							enabled: true
						}
					}
				}
			}
		},
		series: [{
			name: 'ایالات متحده آمریکا',
			data: [
				null, null, null, null, null, 6, 11, 32, 110, 235,
				369, 640, 1005, 1436, 2063, 3057, 4618, 6444, 9822, 15468,
				20434, 24126, 27387, 29459, 31056, 31982, 32040, 31233, 29224, 27342,
				26662, 26956, 27912, 28999, 28965, 27826, 25579, 25722, 24826, 24605,
				24304, 23464, 23708, 24099, 24357, 24237, 24401, 24344, 23586, 22380,
				21004, 17287, 14747, 13076, 12555, 12144, 11009, 10950, 10871, 10824,
				10577, 10527, 10475, 10421, 10358, 10295, 10104, 9914, 9620, 9326,
				5113, 5113, 4954, 4804, 4761, 4717, 4368, 4018
			]
		}, {
			name: 'اتخاد جماهیر شوروی سوسیالیستی/روسیه',
			data: [null, null, null, null, null, null, null, null, null, null,
				5, 25, 50, 120, 150, 200, 426, 660, 869, 1060,
				1605, 2471, 3322, 4238, 5221, 6129, 7089, 8339, 9399, 10538,
				11643, 13092, 14478, 15915, 17385, 19055, 21205, 23044, 25393, 27935,
				30062, 32049, 33952, 35804, 37431, 39197, 45000, 43000, 41000, 39000,
				37000, 35000, 33000, 31000, 29000, 27000, 25000, 24000, 23000, 22000,
				21000, 20000, 19000, 18000, 18000, 17000, 16000, 15537, 14162, 12787,
				12600, 11400, 5500, 4512, 4502, 4502, 4500, 4500
			]
		}]
	});
	// chart 12
	Highcharts.chart('chart12', {
		chart: {
			useHTML: true,
			styledMode: true
		},
		credits: {
			useHTML: true,
			enabled: false
		},
		title: {
			useHTML: true,
			text: 'نمودار ترکیبی'
		},
		tooltip: {
			useHTML: true
		},
		legend: {
			rtl: true
		},
		xAxis: {
			useHTML: true,
			categories: ['سیب', 'پرتقال', 'گلابی', 'موز', 'آلو']
		},
		labels: {
			items: [{
				useHTML: true,
				html: 'مصرف کل میوه',
				style: {
					useHTML: true,
					left: '83px',
					top: '18px',
					color: ( // theme
						Highcharts.defaultOptions.title.style && Highcharts.defaultOptions.title.style.color) || 'black'
				}
			}]
		},
		series: [{
			type: 'column',
			name: 'رضا',
			data: [3, 2, 1, 3, 4]
		}, {
			type: 'column',
			name: 'پریسا',
			data: [2, 3, 5, 7, 6]
		}, {
			type: 'column',
			name: 'ساناز',
			data: [4, 3, 3, 9, 0]
		}, {
			type: 'spline',
			name: 'میانگین',
			data: [3, 2.67, 3, 6.33, 3.33],
			marker: {
				lineWidth: 2,
				lineColor: Highcharts.getOptions().colors[3],
				fillColor: 'white'
			}
		}, {
			type: 'pie',
			useHTML: true,
			name: 'مصرف کل',
			data: [{
				name: 'رضا',
				y: 13,
				color: Highcharts.getOptions().colors[0] // Jane's color
			}, {
				name: 'پریسا',
				y: 23,
				color: Highcharts.getOptions().colors[1] // John's color
			}, {
				name: 'ساناز',
				y: 19,
				color: Highcharts.getOptions().colors[2] // Joe's color
			}],
			center: [100, 80],
			size: 100,
			useHTML: true,
			showInLegend: false,
			dataLabels: {
				useHTML: true,
				enabled: false
			}
		}]
	});
	// chart 13
	Highcharts.chart('chart13', {
		chart: {
			zoomType: 'xy',
			styledMode: true
		},
		credits: {
			enabled: false
		},
		title: {
			useHTML: true,
			text: 'میانگین دمای ماهانه و بارندگی در توکیو'
		},
		subtitle: {
			useHTML: true,
			text: 'منبع : WorldClimate.com'
		},
		xAxis: [{
			categories: ['فروردین', 'اردیبهشت', 'خرداد', 'تیر', 'مرداد', 'شهریور', 'مهر', 'آبان', 'آذر', 'دی', 'بهمن', 'اسفند'],
			crosshair: true
		}],
		yAxis: [{ // Primary yAxis
			labels: {
				format: '{value}°C',
				style: {
					color: Highcharts.getOptions().colors[1]
				}
			},
			title: {
				text: 'دما',
				style: {
					color: Highcharts.getOptions().colors[1]
				}
			}
		}, { // Secondary yAxis
			title: {
				text: 'بارندگی',
				style: {
					color: Highcharts.getOptions().colors[0]
				}
			},
			labels: {
				format: '{value} mm',
				style: {
					color: Highcharts.getOptions().colors[0]
				}
			},
			opposite: true
		}],
		tooltip: {
			useHTML: true,
			shared: true
		},
		legend: {
			useHTML: true,
			rtl: true,
			layout: 'vertical',
			align: 'left',
			x: 120,
			verticalAlign: 'top',
			y: 100,
			floating: true,
			backgroundColor: Highcharts.defaultOptions.legend.backgroundColor || // theme
				'rgba(255,255,255,0.25)'
		},
		series: [{
			name: 'بارندگی',
			type: 'column',
			yAxis: 1,
			data: [49.9, 71.5, 106.4, 129.2, 144.0, 176.0, 135.6, 148.5, 216.4, 194.1, 95.6, 54.4],
			tooltip: {
				valueSuffix: ' میلی متر'
			}
		}, {
			name: 'دما',
			type: 'spline',
			data: [7.0, 6.9, 9.5, 14.5, 18.2, 21.5, 25.2, 26.5, 23.3, 18.3, 13.9, 9.6],
			tooltip: {
				valueSuffix: '°C'
			}
		}]
	});
	// chart 14
	Highcharts.chart('chart14', {
		chart: {
			useHTML: true,
			type: 'column',
			styledMode: true
		},
		title: {
			useHTML: true,
			text: 'نمودار ستونی با مقادیر منفی'
		},
		legend: {
			rtl: true
		},
		tooltip: {
			useHTML: true
		},
		xAxis: {
			useHTML: true,
			categories: ['سیب', 'پرتقال', 'گلابی', 'گریپ فروت', 'موز']
		},
		credits: {
			useHTML: true,
			enabled: false
		},
		series: [{
			name: 'رضا',
			data: [5, 3, 4, 7, 2]
		}, {
			name: 'پریسا',
			data: [2, -2, -3, 2, 1]
		}, {
			name: 'ساناز',
			data: [3, 4, 4, -2, 5]
		}]
	});
	// chart 15
	Highcharts.chart('chart15', {
		chart: {
			type: 'column',
			useHTML: true,
			styledMode: true
		},
		credits: {
			useHTML: true,
			enabled: false
		},
		title: {
			useHTML: true,
			text: 'نمودار ستونی انباشته شده'
		},
		xAxis: {
			useHTML: true,
			categories: ['سیب', 'پرتقال', 'گلابی', 'گریپ فروت', 'موز']
		},
		yAxis: {
			useHTML: true,
			min: 0,
			title: {
				useHTML: true,
				text: 'مصرف کل میوه'
			},
			stackLabels: {
				enabled: true,
				style: {
					useHTML: true,
					fontWeight: 'bold',
					color: ( // theme
						Highcharts.defaultOptions.title.style && Highcharts.defaultOptions.title.style.color) || 'gray'
				}
			}
		},
		legend: {
			useHTML: true,
			align: 'right',
			rtl: true,
			x: -30,
			verticalAlign: 'top',
			y: 25,
			floating: true,
			backgroundColor: Highcharts.defaultOptions.legend.backgroundColor || 'white',
			borderColor: '#CCC',
			borderWidth: 1,
			shadow: false
		},
		tooltip: {
			useHTML: true,
			headerFormat: '<b>{point.x}</b><br/>',
			pointFormat: '{series.name}: {point.y}<br/>مجموع : {point.stackTotal}'
		},
		plotOptions: {
			column: {
				stacking: 'normal',
				dataLabels: {
					enabled: true
				}
			}
		},
		series: [{
			name: 'رضا',
			data: [5, 3, 4, 7, 2]
		}, {
			name: 'پریسا',
			data: [2, 2, 3, 2, 1]
		}, {
			name: 'ساناز',
			data: [3, 4, 4, 2, 5]
		}]
	});
});