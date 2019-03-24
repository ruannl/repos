(function (angular) {
	'use strict';

	var settings = {};
	var appConfig = function ($routeProvider, $httpProvider, $locationProvider, $compileProvider, openWeatherMapProvider) {

		openWeatherMapProvider.setApiKey('0ab456f7ab8ac816c98fbbbc40a180c6');
		openWeatherMapProvider.setApiVersion('2.5');
		openWeatherMapProvider.init();

		//$httpProvider.defaults.headers.common["X-Requested-With"] = 'XMLHttpRequest';

		var t = function (response) {
			console.log(response);
			return {
				redirectTo: '/rl'
			};
		};

		//$routeProvider
		//    .when('/rl', {
		//        templateUrl: 'rl/Home/Index',
		//        controller: 'HomeController'
		//    })
		//    .when('/rl/timesheet', {
		//        templateUrl: 'timesheet.htm'
		//        //controller: 'TimesheetCtrl',
		//        //controllerAs: 'timesheet'
		//    })
		//    .when('/rl/calender', {
		//        templateUrl: '/Calender/Index',
		//        controller: 'CalendarController'
		//    })
		//    .when('/rl/cv', {
		//        templateUrl: 'cv.htm'
		//    })
		//    .otherwise({
		//        redirectTo: '/rl'
		//    });

		//$locationProvider.html5Mode({
		//    enabled: true,
		//    requireBase: true
		//});

		$compileProvider.aHrefSanitizationWhitelist(/^\s*(https?|ftp|mailto|javascript):/);
	};
	var runApp = function ($location) {
		console.log('path:', $location.$$path);
	};

	//controllers
	var baseController = function ($scope, $route, $window, $anchorScroll) {
		//$scope.template = 'cv.htm';
		$scope.template = 'cv.htm';
		$scope.redirectToTemplate = function (template) {
			$scope.template = template;
			//$location.hash('top');
			$anchorScroll('top');
		};

		$scope.$on('$stateChangeStart', function (a, b, c) {
			console.log('a', a);
			console.log('b', b);
			console.log('c', c);
		});

		console.log($route.routes);

		$scope.redirectToView = function () {
		};
		$scope.ShowContextMenu = function () {
		};

		//var body = $document[0].body;
		//var bodyElement = angular.element(body);
		//bodyElement.removeClass('weather-body');

		if ($window.innerWidth <= 1024) {
			$scope.layout = 'container-fluid';
			//$scope.titles = [
			//	{
			//		name: 'C# development',
			//		className: 'c-sharp'
			//	}, {
			//		name: 'Single page applications',
			//		className: 'angular'
			//	}, {
			//		name: 'JavaScript',
			//		className: 'javascript'
			//	}, {
			//		name: 'Microsoft SQL',
			//		className: 'sql'
			//	}, {
			//		name: 'MySQL',
			//		className: 'mysql'
			//	}, {
			//		name: 'SQL Lite',
			//		className: 'sql-lite'
			//	}, {
			//		name: 'bootstrap',
			//		className: 'bootstrap'
			//	}, {
			//		name: 'jQuery',
			//		className: 'jquery'
			//	}, {
			//		name: 'Office Integration',
			//		className: 'msoffice'
			//	}, {
			//		name: 'Intuit',
			//		className: 'quick-books'
			//	}, {
			//		name: 'Cloud Development',
			//		className: 'azure'
			//	}, {
			//		name: 'HTML 5',
			//		className: 'html5'
			//	}, {
			//		name: 'CSS 3',
			//		className: 'css3'
			//	}, {
			//		name: 'iText pdf',
			//		className: 'itext'
			//	}, {
			//		name: 'Kendo UI',
			//		className: 'kendo'
			//	}, {
			//		name: 'Node JS',
			//		className: 'nodejs'
			//	}, {
			//		name: 'Web API',
			//		className: 'webapi'
			//	}, {
			//		name: 'WCF Services',
			//		className: 'wcf'
			//	}, {
			//		name: 'Apps',
			//		className: 'google-developers'
			//	}, {
			//		name: 'Integration',
			//		className: 'micros-fidelio'
			//	}
			//];
		} else {

			$scope.layout = 'container';
			//$scope.titles = [
			//	{
			//		name: 'C# development with a description',
			//		className: 'c-sharp'
			//	}, {
			//		name: 'Single page applications',
			//		className: 'angular'
			//	}, {
			//		name: 'JavaScript',
			//		className: 'javascript'
			//	}, {
			//		name: 'Microsoft SQL',
			//		className: 'sql'
			//	}, {
			//		name: 'MySQL',
			//		className: 'mysql'
			//	}, {
			//		name: 'SQL Lite',
			//		className: 'sql-lite'
			//	}, {
			//		name: 'bootstrap',
			//		className: 'bootstrap'
			//	}, {
			//		name: 'jQuery',
			//		className: 'jquery'
			//	}, {
			//		name: 'Office Integration',
			//		className: 'msoffice'
			//	}, {
			//		name: 'Intuit',
			//		className: 'quick-books'
			//	}, {
			//		name: 'Cloud Development',
			//		className: 'azure'
			//	}, {
			//		name: 'HTML 5',
			//		className: 'html5'
			//	}, {
			//		name: 'CSS 3',
			//		className: 'css3'
			//	}, {
			//		name: 'iText pdf',
			//		className: 'itext'
			//	}, {
			//		name: 'Kendo UI',
			//		className: 'kendo'
			//	}, {
			//		name: 'Node JS',
			//		className: 'nodejs'
			//	}, {
			//		name: 'Web API',
			//		className: 'webapi'
			//	}, {
			//		name: 'WCF Services',
			//		className: 'wcf'
			//	}, {
			//		name: 'Apps',
			//		className: 'google-developers'
			//	}, {
			//		name: 'Integration',
			//		className: 'micros-fidelio'
			//	}
			//];
		}

		angular.element($window).bind('resize', function () {
			if ($window.innerWidth <= 1024) {
				$scope.layout = 'container-fluid';
			} else {
				$scope.layout = 'container';
			}
		});
	};
	var cvController = function ($scope, $anchorScroll, dataService) {

		$scope.model = {
			submitted: false,
			showThankYou: function () {
				return $scope.model.submitted && $scope.contactForm.$submitted;
			},
			titles: [{
				name: 'C# development',
				className: 'c-sharp'
			}, {
				name: 'Single page applications',
				className: 'angular'
			}, {
				name: 'JavaScript',
				className: 'javascript'
			}, {
				name: 'Microsoft SQL',
				className: 'sql'
			}, {
				name: 'MySQL',
				className: 'mysql'
			}, {
				name: 'SQL Lite',
				className: 'sql-lite'
			}, {
				name: 'bootstrap',
				className: 'bootstrap'
			}, {
				name: 'jQuery',
				className: 'jquery'
			}, {
				name: 'Office Integration',
				className: 'msoffice'
			}, {
				name: 'Intuit',
				className: 'quick-books'
			}, {
				name: 'Cloud Development',
				className: 'azure'
			}, {
				name: 'HTML 5',
				className: 'html5'
			}, {
				name: 'CSS 3',
				className: 'css3'
			}, {
				name: 'iText pdf',
				className: 'itext'
			}, {
				name: 'Kendo UI',
				className: 'kendo'
			}, {
				name: 'Node JS',
				className: 'nodejs'
			}, {
				name: 'Web API',
				className: 'webapi'
			}, {
				name: 'WCF Services',
				className: 'wcf'
			}, {
				name: 'Apps',
				className: 'google-developers'
			}, {
				name: 'Integration',
				className: 'micros-fidelio'
			}],
			languages: [
				{
					id: 'csharp',
					name: 'C#'
				}, {
					id: 'aspnet',
					name: 'Asp.Net'
				}, {
					id: 'js',
					name: 'Javascript'
				}, {
					id: 'html',
					name: 'HTML'
				}, {
					id: 'css',
					name: 'CSS'
				}, {
					id: 'sql',
					name: 'T-SQL'
				}, {
					id: 'vb',
					name: 'Visual Basic 6'
				}, {
					id: 'vbs',
					name: 'VB Script'
				}
			],
			tools: [
				{
					id: 'visualstudio',
					name: 'Visual Studio 2010 - 2017'
				}, {
					id: 'sqlms',
					name: 'SQL Server Management Studio'
				}, {
					id: 'ps',
					name: 'Powershell'
				}, {
					id: 'iis',
					name: 'IIS 6 - 7'
				}, {
					id: 'mysql',
					name: 'MySQL Workbench'
				}, {
					id: 'fb',
					name: 'Fire Bird'
				}
			],
			packages: [
				{
					id: 'core',
					name: '.Net CORE'
				}, {
					id: 'net',
					name: '.Net Framework'
				}, {
					id: 'ps',
					name: 'Powershell'
				}, {
					id: 'ef',
					name: 'Entity Framework'
				}, {
					id: 'nh',
					name: 'N-Hibernate'
				}, {
					id: 'mvc',
					name: 'MVC'
				}, {
					id: 'api',
					name: 'Web API'
				}, {
					id: 'wcf',
					name: 'Wcf Services'
				}, {
					id: 'owin',
					name: 'Owin'
				}, {
					id: 'ninject',
					name: 'Ninject'
				}, {
					id: 'itext',
					name: 'ITEXT Pdf Creation'
				}, {
					id: 'angularjs',
					name: 'AngularJs'
				}, {
					id: 'jquery',
					name: 'JQuery'
				}, {
					id: 'signalr',
					name: 'Signalr'
				}, {
					id: 'bootstrap',
					name: 'bootstrap'
				}, {
					id: 'kendo',
					name: 'Kendo UI'
				}, {
					id: 'node',
					name: 'Node JS'
				}, {
					id: 'mstest',
					name: 'MS TEST'
				}, {
					id: 'nunit',
					name: 'NUnit'
				}, {
					id: 'jasmine',
					name: 'Jasmine'
				}, {
					id: 'specflow',
					name: 'Specflow'
				}
			],
			domain: [{
				id: 'agile',
				name: 'Agile / Scrum'
			}, {
				id: 'ci',
				name: 'Continious Integration'
			}, {
				id: 'ci',
				name: 'Remote Server Configuration using Powershell'
			}, {
				id: 'spa',
				name: 'Single Page Applications'
			}, {
				id: 'tdd',
				name: 'Test Driven Development'
			}, {
				id: 'wf',
				name: 'Warerfall'
			}],
			projects: [{
				id: 'webstir',
				name: 'Web Submissions for Issuer Regulation'
			}, {
				id: 'sens',
				name: 'Stock Exchange News Service'
			}, {
				id: 'mds',
				name: 'JSE - Master Data System'
			}, {
				id: 'if',
				name: 'G-Connect - In-Flight Wi-Fi'
			}, {
				id: 'pb',
				name: 'G-Connect - Prepaid Billing '
			}, {
				id: 'ey',
				name: 'Ernst & Young - CAAT Analysis'
			}, {
				id: 'investec',
				name: 'Investec - Retail Foreign Exchange Calculator'
			}, {
				id: 'vc',
				name: 'Vodacom - Online Billing Integration'
			}, {
				id: 'ussd',
				name: 'Vodacom - USSD Integration'
			}, {
				id: 'wisp',
				name: 'WISP - Platform Integration'
			}],
			contact: {
				name: null,
				surname: null,
				email: null,
				message: null
			}
		};

		$scope.submitContactForm = function (isValid) {
			var contactForm = $scope.contactForm.$$element;
			if (isValid) {
				dataService.SaveMessage($scope.model.contact)
					.then(function (response) {
						console.log(response);
						$scope.model.submitted = true;
						contactForm.addClass('fadeDown');

					},
						function (response) {
							console.error(response.data);
						});
			} else {
				contactForm.addClass('animated');
				contactForm.addClass('shake');

				if ($scope.contactForm.name.$untouched) {
					$scope.contactForm.name.$setDirty();
				}

				if ($scope.contactForm.surname.$untouched) {
					$scope.contactForm.surname.$setDirty();
				}

				if ($scope.contactForm.email.$untouched) {
					$scope.contactForm.email.$setDirty();
				}

				if ($scope.contactForm.message.$untouched) {
					$scope.contactForm.message.$setDirty();
				}
				setTimeout(function () {
					contactForm.removeClass('animated');
					contactForm.removeClass('shake');
				},
					2000);
			}
		};

		$anchorScroll('top');

		console.log('CvController.model', $scope.model);
	};
	var homeController = function ($scope) {
		$scope.model = {

		};
	};
	var timesheetController = function ($scope, $storageService, $interval, $filter, $q) {
		var handleAddWeekResponse = function (response) {
			$scope.model.timesheetWeek = response;
		};
		var handleAddWeekError = function (response) {
			console.error(response);
		};
		var handleSaveResponse = function (response) {
			$scope.model.timesheet = response;
			$scope.model.timesheetWeek = undefined;
		};
		var handleSaveException = function (response) {
			console.error(response);
		};
		var calcDuration = function () {
			var duration = Math.abs(new Date() - $scope.model.startTime);
			$scope.model.totalTime = $storageService.msToTime(duration).substring(0, 5);
			$scope.model.hoursCompleted = $scope.model.endTime < new Date();
		};
		var bindTimesheet = function (response) {
			$scope.model.timesheet = response;
			calcDuration();
		};
		var bindTimeList = function (response) {
			$scope.model.times = response;
		};
		var bindWeekList = function (response) {
			if (!response) {
				return;
			}

			$scope.model.weeks = response;

			var currentWeek = null;
			$scope.model.weeks.forEach(function (week) {
				var today = new Date();
				var t = today;
				t.setDate(t.getDate() + 6);

				if (new Date(week.start) <= today && new Date(week.start) <= t) {
					currentWeek = week;
				}
			});
			$scope.model.week = currentWeek;
		};
		var weekInTimesheet = function (week) {
			var exists = false;
			if ($scope.model.timesheet) {
				$scope.model.timesheet.data.forEach(function (w) {
					if (w.id === week.id) {
						exists = true;
					}
				});
			}

			return exists;
		};
		var addWeek = function (week) {
			var defer = $q.defer();

			setTimeout(function () {
				var days = [];
				if (week) {
					if (weekInTimesheet($scope.model.week)) {
						defer.reject('week already in timesheet');
					} else {
						var weekStart = new Date($scope.model.week.start);

						for (var i = 0; i < 7; i++) {
							weekStart.setDate(weekStart.getDate() + i);

							var day = {
								date: new Date($filter('date')(weekStart, 'yyyy-MM-dd')),
								dayName: $filter('date')(weekStart, 'EEE'),
								day: $filter('date')(weekStart, 'dd'),
								month: $filter('date')(weekStart, 'MMM'),
								hours: 0,
								comment: null
							};

							days.push({
								day: day,
								hours: 0
							});
							weekStart = new Date($scope.model.week.start);
						}

						$scope.model.addButtonText = 'Save';
						$scope.model.timesheetWeek = days;

						defer.resolve(days);
					}
				} else {
					defer.reject('invalid timesheet data');
				}
			});

			return defer.promise;
		};
		var loadDefaults = function () {
			if ($scope.model) {
				$storageService.TimeList().then(bindTimeList);
				$storageService.WeekList().then(bindWeekList);
				$storageService.GetTimeSheet().then(bindTimesheet);
			}

			setTimeout(function () {
				console.log('timesheetcontroller.scope', $scope);
			});
		};

		$scope.model = {
			times: undefined,
			weeks: undefined,
			defaultTime: '09:00',
			startTime: undefined,
			endTime: undefined,
			remainingTime: undefined,
			totalTime: undefined,
			lunchTime: 30,
			hoursCompleted: false,
			week: undefined,
			timesheetWeek: undefined,
			timesheet: undefined,
			addButtonText: 'Add'
		};

		$scope.timesheetController = {
			SaveTimesheet: function () {
				if ($scope.model.addButtonText === 'Add') {
					addWeek($scope.model.week).then(handleAddWeekResponse, handleAddWeekError);
				} else {
					var week = {
						id: $scope.model.week.id,
						days: $scope.model.timesheetWeek
					};
					$scope.model.timesheet.data.push(week);
					$storageService.SaveTimesheet($scope.model.timesheet)
						.then(handleSaveResponse, handleSaveException);
				}
			},
			ResetWeek: function () {
				$storageService.ResetTimesheet();
				$scope.model.timesheet = {
					data: [],
					total: 0
				};
				$scope.model.timesheetWeek = undefined;
				$scope.model.addButtonText = 'Add';
			},
			TimesheetTotal: function () {
				var total = 0;
				if (!$scope.model.timesheet) {
					return '';
				} else {
					$scope.model.timesheet.data.forEach(function (item) {
						item.week.days.forEach(function (day) {
							total += parseInt(day.hours);
						});
					});
					return total;
				}
			},
			WeekTotal: function (week) {
				return $storageService.WeekTotal(week);
			}
		};

		var timer = $interval(function () {
		},
			1000);

		$scope.$watch('model.defaultTime',
			function (newval) {
				if (newval !== null) {
					var today = new Date();
					var startTime = new Date(today.getFullYear(),
						today.getMonth(),
						today.getDate(),
						parseInt($scope.model.defaultTime.substring(0, 2)),
						parseInt($scope.model.defaultTime.substring(3, 5)),
						0,
						0);

					$scope.model.startTime = startTime;
				}
			});

		$scope.$watch('model.startTime',
			function (newval) {
				if (newval !== null) {
					var endTime = new Date($scope.model.startTime);
					endTime.setMinutes(endTime.getMinutes() + 60 * 8 + $scope.model.lunchTime);
					$scope.model.endTime = endTime;
				}
			});

		$scope.$watch('model.lunchTime',
			function (newval) {
				if (newval !== null) {
					var endTime = new Date($scope.model.startTime);
					endTime.setMinutes(endTime.getMinutes() + 60 * 8 + $scope.model.lunchTime);
					$scope.model.endTime = endTime;
				}
			});

		$scope.$watch('model.week',
			function (newval) {
				if (newval !== null) {
					console.log(newval);
				}
			});

		$scope.$on('$viewContentLoaded',
			function (event) {
				// code that will be executed ... 
				// every time this view is loaded
				console.log(event);
			});

		$scope.$on('$destroy',
			function () {
				$interval.cancel(timer);
			});

		setTimeout(function () {
			loadDefaults();
		});

	};
	var accountingController = function ($scope, $sce, accountingService) {
		$scope.model = {
			bank: null
		};

		$scope.SubmitFiles = function () {
			if (!$scope.files) {
				$scope.errorMsg = 'no file selected';
				return;
			}

			$scope.files.forEach(function (file) {
				accountingService.UploadFile(file, $scope.model.bank)
					.then(function (response) {
						console.log('accountingService.UploadFile response', response);
					});

			});
		};

		$scope.uploadFiles = function (files, errFiles) {
			$scope.files = files;
			$scope.errFiles = errFiles;

			//angular.forEach(files, function (file) {

			//    file.upload = Upload.upload({
			//        url: 'https://angular-file-upload-cors-srv.appspot.com/upload',
			//        data: { file: file }
			//    });

			//    file.upload.then(function (response) {
			//        setTimeout(function () {
			//            file.result = response.data;
			//        });
			//    }, function (response) {
			//        if (response.status > 0)
			//            $scope.errorMsg = response.status + ': ' + response.data;
			//    }, function (evt) {
			//        file.progress = Math.min(100, parseInt(100.0 *
			//            evt.loaded / evt.total));
			//    });
			//});
		};

		accountingService.BankList()
			.then(function (response) {
				$scope.model.banks = response;
			},
				function (response) {
					$scope.errorMsg = $sce.trustAsHtml(response.data);
				});
	};
	var weatherController = function ($scope, $document, $filter, calendarService, geolocatorService, openWeatherMapService) {
		var body = $document[0].body;
		var bodyElement = angular.element(body);
		bodyElement.addClass('weather-body');

		var getCurrentWeatherResponse = function (response) {
			$scope.currentWeather = response;
		};

		var handleGeolocatorError = function (error) {
			$log.error(error);
		};

		var GetCurrentWeather = function (location) {
			openWeatherMapService.getCurrentWeather(location.coords.latitude, location.coords.longitude)
				.then(getCurrentWeatherResponse, handleGeolocatorError);
		};

		var GetWeatherForecast = function (location) {
			$scope.loaded = false;
			$scope.FiveDayForecast = {};
			$scope.todaysForecast = [];
			$scope.tomorrowsForecast = [];
			$scope.remainingForecast = [];

			openWeatherMapService.getWeatherForecast(location.coords.latitude, location.coords.longitude)
				.then(function (response) {
					var today = $filter('date')(new Date(), 'yyyy-MM-dd');
					var tomorrow = new Date();
					tomorrow.setDate(tomorrow.getDate() + 1);
					tomorrow = $filter('date')(tomorrow, 'yyyy-MM-dd');

					$scope.FiveDayForecast = angular.copy(response);
					$scope.todaysForecast = $filter('filter')(response.items,
						{
							date: today
						});
					$scope.tomorrowsForecast = $filter('filter')(response.items,
						{
							date: tomorrow
						});
					var remainingForecast = response.items.slice();
					$scope.remainingForecast = remainingForecast.splice(2, remainingForecast.length - 2);

					console.log('FiveDayForecast', $scope.FiveDayForecast);
					console.log('todaysForecast', $scope.todaysForecast);
					console.log('tomorrowsForecast', $scope.tomorrowsForecast);
					console.log('remainingForecast', $scope.remainingForecast);

					$scope.loaded = true;
				});
		};

		//calendarService.UpcomingEvents().then(function (response) {
		//    console.info('response', response);
		//}, function (error) {
		//    console.error(error);
		//});

		//geolocatorService.watchPosition().then(GetCurrentWeather, handleGeolocatorError);
		geolocatorService.watchPosition().then(GetWeatherForecast, handleGeolocatorError);
	};

	//directives
	var rlScrambler = function ($q) {
		'use strict';
		return {
			//require: 'ngModel',
			restrict: 'E',
			replace: true,
			transclude: true,
			scope: {
				scrambleItems: '=scrambleItems'
			},
			template: function () {
				var html =
					'<div id="scramble-directive" class="scramble-container animated fadeIn  {{scrambleClass}}" ng-transclude><div class="scramble-item"><span class="">{{scrambleItem}}</span></div></div> ';
				return html;
			},
			link: function (s, e, a) {
				var chars = '!<>-_\\/[]{}—=+*^?#________';
				var itemId = 0;
				var complete = 0;
				var counter = 0;
				var output = '';

				var charactersQueue = [];
				var setTextDefer = $q.defer();

				if (!s.scrambleItem) {
					s.scrambleItem = '';
				}
				if (!s.scrambleClass) {
					s.scrambleClass = '';
				}

				function randomChar() {
					return chars[Math.floor(Math.random() * chars.length)];
				}

				function setText(item, className) {
					var oldItem = s.scrambleItem || '';
					var newItem = item || '';
					var len = Math.max(oldItem.length, newItem.length);
					charactersQueue = [];

					for (var i = 0; i < len; i++) {
						var from = oldItem[i] || '';
						var to = newItem[i] || '';
						var start = Math.floor(Math.random() * 40);
						var end = start + Math.floor(Math.random() * 40);
						var char = null;

						charactersQueue.push({
							from,
							to,
							start,
							end,
							char,
							item,
							className
						});
					}

					setTimeout(function () {
						update();
					});

					return setTextDefer.promise;
				}

				function update() {
					output = '';
					complete = 0;
					var className = charactersQueue[0].className;

					for (var i = 0; i < charactersQueue.length; i++) {
						var item = charactersQueue[i];

						if (counter >= item.end) {
							complete++;
							output += item.to;
						} else if (counter >= item.start) {
							var char = item.char;
							if (!char || Math.random() < 0.28) {
								char = randomChar();
							}
							item.char = char;
							output += char;
						} else {
							output += item.from;
						}
					}

					s.$apply(function () {
						if (!output) {
							output = randomChar();
						}

						s.scrambleItem = output;
						s.scrambleClass = className;

					});

					if (complete === charactersQueue.length) {
						counter = 0;
						e.removeClass('fadeIn');
						e.removeClass('slow');

						setTextDefer.resolve(output);

					} else {
						counter++;
						setTimeout(function () {
							update();
						},
							15);
					}
				}

				function next() {
					output = '';
					if (s.scrambleItems && s.scrambleItems.length > 0) {
						e.addClass('fadeIn');
						e.addClass('slow');

						setText(s.scrambleItems[itemId].name, s.scrambleItems[itemId].className)
							.then(function () {
								setTimeout(next, 5000);
							});

						itemId = (itemId + 1) % s.scrambleItems.length;
					}
				}

				e.on('$destroy',
					function () {
						//setTimeout.cancel(t);
						//setTimeout.cancel(t3);
					});

				setTimeout(function () {
					next();
				});
			}
		};
	};
	var rlTumbler = function ($interval, $q, $animate) {
		'use strict';
		$animate.enabled(true);
		return {
			restrict: 'E',
			replace: true,
			transclude: true,
			scope: {
				tumblrItems: '=tumblrItems'
			},
			template: function () {
				return '<ul class="tumblr" tumblr-items ng-transclude><li class="tumblr-item animated" ng-repeat="item in tumblrEntries" ng-animate="animate">{{item.name}}</li></ul>';
			},
			link: function (s, e, a) {
				s.rowCount = parseInt(a.rowCount || 0);
				s.rowIndex = 0;

				if (!s.tumblrItems) {
					s.tumblrItems = [];
				}

				s.originalList = angular.copy(s.tumblrItems);
				console.log(s.tumblrItems);
				s.tumblrEntries = [];
				for (var j = 0; j < s.rowCount; j++) {
					s.tumblrEntries.push(s.originalList[j]);
					s.rowIndex = j;
				}

				$animate.enabled(e);

				var addItem = function (rowIndex) {
					var defer = $q.defer();
					s.tumblrEntries = s.tumblrEntries || [];

					setTimeout(function () {
						s.tumblrEntries.shift();
						if (s.tumblrEntries.indexOf(s.originalList[rowIndex]) <= 0) {
							s.tumblrEntries.push(s.originalList[rowIndex]);
						}
						defer.resolve();
					});
					return defer.promise;
				};

				var next = function () {
					if (s.rowIndex >= s.originalList.length) {
						s.rowIndex = 0;
						addItem(s.rowIndex)
							.then(function () {
								s.rowIndex = 1;
							});
					} else {
						var idx = s.rowIndex;
						addItem(s.rowIndex)
							.then(function () {
								s.rowIndex = idx + 1;
							});
					}
				};

				var duration = function () {
					var min = Math.ceil(1500);
					var max = Math.floor(2500);

					return Math.floor(Math.random() * (max - min)) + min;
				};
				var x = parseInt(duration());
				var interval = $interval(next, x);

				e.on('$destroy',
					function () {
						$interval.cancel(interval);
					});
				s.$watch('tumblrEntries',
					function (newval, oldval) {
						if (!newval || newval === oldval) {
							return;
						}
						console.log('newval', newval);
					});

				next();
			}
		};
	};
	var clock = function ($interval) {
		'use strict';
		return {
			scope: true,
			transclude: true,
			template: "<span class='clock'>" +
				"<span class='clock-time' >{{ date | date: timeFormat }}</span>&nbsp;" +
				"<span class='clock-seconds' ng-transclude>{{ date | date: 'ss'}}</span><br/>" +
				"<span class='clock-text' ng-transclude>{{ date | date: 'EEEE, MMMM dd'}}</span>" +
				'</span >',
			link: function ($s, $e, $a) {
				$s.timeFormat = $a.format === '12' ? 'hh:mm' : 'HH:mm';

				function updateTime() {
					$s.date = new Date();
				}

				var stopTime = $interval(updateTime, 1000);
				$e.on('$destroy',
					function () {
						$interval.cancel(stopTime);
					});
			}
		};
	};
	var timer = function ($interval) {
		'use strict';
		return {
			scope: true,
			transclude: true,
			template: "<span class='clock'>" +
				"<span class='clock-time' >{{ date | date: timeFormat }}</span>&nbsp;" +
				"<span class='clock-seconds' ng-transclude>{{ date | date: 'ss'}}</span><br/>" +
				"<span class='clock-text' ng-transclude>{{ date | date: 'EEEE, MMMM dd'}}</span>" +
				'</span >',
			link: function ($s, $e, $a) {
				$s.timeFormat = $a.format === '12' ? 'hh:mm' : 'HH:mm';

				function updateTime() {
					$s.date = new Date();
				}

				var stopTime = $interval(updateTime, 1000);
				$e.on('$destroy',
					function () {
						$interval.cancel(stopTime);
					});
			}
		};
	};

	var windowSize = function ($window) {
		'use strict';
		var template = function () {
			return '<div ng-transclude>{{innerWidth}}px x {{innerHeight}}px</div>';
		};

		return {
			link: function (scope) {
				scope.innerWidth = $window.innerWidth;
				scope.innerHeight = $window.innerHeight;

				angular.element($window)
					.bind('resize',
						function () {
							scope.innerWidth = $window.innerWidth;
							scope.innerHeight = $window.innerHeight;
							scope.$digest();
						});

			},
			restrict: 'E',
			template: template,
			replace: true,
			transclude: true
		};
	};
	var cellHighlight = function () {
		return {
			restrict: 'C',
			link: function postLink(scope, iElement, iAttrs) {
				iElement.find('td')
					.mouseover(function () {
						$(this).parent('tr').css('opacity', '0.7');
					})
					.mouseout(function () {
						$(this).parent('tr').css('opacity', '1.0');
					});
			}
		};
	};
	var context = function () {
		return {
			restrict: 'A',
			scope: '@&',
			compile: function (tElement, tAttrs, transclude) {
				return {
					post: function postLink(scope, iElement, iAttrs, controller) {
						var ul = $('#' + iAttrs.context), last = null;

						ul.css({
							'display': 'none'
						});
						$(iElement)
							.bind('contextmenu',
								function (event) {
									event.preventDefault();
									ul.css({
										position: 'fixed',
										display: 'block',
										left: event.clientX + 'px',
										top: event.clientY + 'px'
									});
									last = event.timeStamp;
								});
						//$(iElement).click(function(event) {
						//  ul.css({
						//    position: "fixed",
						//    display: "block",
						//    left: event.clientX + 'px',
						//    top: event.clientY + 'px'
						//  });
						//  last = event.timeStamp;
						//});

						$(document)
							.click(function (event) {
								var target = $(event.target);
								if (!target.is('.popover') && !target.parents().is('.popover')) {
									if (last === event.timeStamp) {
										return;
									}
									ul.css({
										'display': 'none'
									});
								}
							});
					}
				};
			}
		};
	};
	var rlRightClick = function ($parse) {
		return function (scope, element, attrs) {
			var fn = $parse(attrs.ngRightClick);
			element.bind('contextmenu',
				function (event) {
					scope.$apply(function () {
						event.preventDefault();
						fn(scope,
							{
								$event: event
							});
					});
				});
		};
	};
	var jseMore = function () {
		return {
			restrict: 'E',
			replace: true,
			transclude: true,
			templateUrl: 'jse-more.htm'
		};
	};
	var gconnectMore = function () {
		return {
			restrict: 'E',
			replace: true,
			transclude: true,
			templateUrl: 'g-connect-more.htm'
		};
	};
	var bytesMore = function () {
		return {
			restrict: 'E',
			replace: true,
			transclude: true,
			templateUrl: 'bytes-more.htm'
		};
	};
	var itiMore = function () {
		return {
			restrict: 'E',
			replace: true,
			transclude: true,
			templateUrl: 'iti-more.htm'
		};
	};
	var cruxMore = function () {
		return {
			restrict: 'E',
			replace: true,
			transclude: true,
			templateUrl: 'crux-more.htm'
		};
	};
	var aboutMe = function () {
		return {
			restrict: 'E',
			replace: true,
			transclude: true,
			templateUrl: 'about-me.htm'
		};
	};
	var iconTitle = function () {
		return {
			restrict: 'E',
			replace: true,
			transclude: true,
			scope: {
				title: '@title',
				icon: '@icon',
				id: '='
			},
			link: function (s, e, a) {
				s.title = a.title;
				s.icon = a.icon;
			},
			templateUrl: 'icon-title.htm'
		};
	};
	var timeline = function () {
		return {
			restrict: 'E',
			replace: true,
			transclude: true,
			templateUrl: 'timeline.htm'
		};
	};
	var contactFormTemplate = function () {
		return {
			restrict: 'E',
			replace: true,
			transclude: true,
			templateUrl: 'contact.htm'
		};
	};

	//services
	var storage = function ($localStorage, $filter, $q) {
		'use strict';

		var weekTotal = function (week) {
			var weeklyTotal = 0;
			week.days.forEach(function (day) {
				weeklyTotal += parseInt(day.hours);
			});

			return weeklyTotal;
		};
		var timesheetTotal = function (timesheet) {
			var timesheetTotal = 0;
			timesheet.data.forEach(function (week) {
				timesheetTotal += weekTotal(week.week);
			});
			return timesheetTotal;
		};

		return {
			SaveTimesheet: function (timesheet) {
				var def = $q.defer();
				var total = 0;

				setTimeout(function () {
					if (!$localStorage.timesheet) {
						$localStorage.timesheet = {
							data: [],
							total: 0
						};
					}

					var ts = {
						data: [],
						total: 0
					};
					timesheet.data.forEach(function (week) {
						total += weekTotal(week);
						ts.data.push({
							week: week,
							total: total
						});
					});

					$localStorage.timesheet.data = angular.copy(ts.data);
					$localStorage.timesheet.total = total;

					def.resolve($localStorage.timesheet);

				});

				return def.promise;
			},
			GetTimeSheet: function () {
				var defer = $q.defer();
				setTimeout(function () {
					if (!$localStorage.timesheet) {
						$localStorage.timesheet = {
							data: [],
							total: 0
						};
					}
					defer.resolve($localStorage.timesheet);
				});

				return defer.promise;
			},
			WeekTotal: function (week) {
				if (week) {
					return weekTotal(week);
				}
			},
			ResetTimesheet: function () {

				$localStorage.timesheets = [];
				$localStorage.timesheet = null;
				return true;
			},
			msToTime: function (duration) {
				var milliseconds = parseInt(duration % 1000 / 100),
					seconds = parseInt(duration / 1000 % 60),
					minutes = parseInt(duration / (1000 * 60) % 60),
					hours = parseInt(duration / (1000 * 60 * 60) % 24);

				hours = hours < 10 ? '0' + hours : hours;
				minutes = minutes < 10 ? '0' + minutes : minutes;
				seconds = seconds < 10 ? '0' + seconds : seconds;

				return hours + ':' + minutes + ':' + seconds;

				//var weeks = function () {

				//};

				//var times = function () {
				//    var quarterHours = ['00', '15', '30', '45'];
				//    var times = [];
				//    for (var i = 0; i < 24; i++) {
				//        for (var j = 0; j < 4; j++) {
				//            var time = i + ':' + quarterHours[j];
				//            if (i < 10) {
				//                time = '0' + time;
				//            }
				//            times.push(time);
				//        }
				//    }
				//    return times;
				//};
			},
			TimeList: function () {
				var defer = $q.defer();
				setTimeout(function () {
					var quarterHours = ['00', '15', '30', '45'];
					var times = [];
					for (var i = 0; i < 24; i++) {
						for (var j = 0; j < 4; j++) {
							var time = i + ':' + quarterHours[j];
							if (i < 10) {
								time = '0' + time;
							}
							times.push(time);
						}
					}
					defer.resolve(times);
				});
				return defer.promise;
			},
			WeekList: function () {
				var defer = $q.defer();

				setTimeout(function () {
					var weeksArray = [];
					var start = new Date(new Date().getFullYear(), 0, 1);
					var end;

					for (var i = 1; i < 53; i++) {
						end = new Date(start);
						end.setDate(end.getDate() + 6);

						var week = {
							id: i,
							start: $filter('date')(start, 'yyyy/MM/dd'),
							end: $filter('date')(end, 'yyyy/MM/dd')
						};
						weeksArray.push(week);
						start = new Date(end);
						start.setDate(start.getDate() + 1);
					}

					defer.resolve(weeksArray);
				});

				return defer.promise;
			}
		};
	};
	var accountingService = function ($http, $location, $q, Upload) {
		'use strict';
		return {
			UploadFile: function (file, bank) {

				var defer = $q.defer();

				var handleUploadResponse = function (response) {
					file.result = response.data;
					console.info('handleUploadResponse', response);
					defer.resolve(response.data);
				};

				var handleUploadError = function (response) {
					console.error('handleUploadError', response);
				};

				var handleUploadProgress = function (response) {
					file.progress = Math.min(100, parseInt(100.0 * response.loaded / response.total));
					console.info('file upload progress', file.progress);
				};

				setTimeout(function () {
					if (!bank) {
						defer.reject('invalid bank');
						return;
					}

					if (!bank.BankId) {
						defer.reject('invalid bank');
						return;
					}

					if (!bank.BankId > 0) {
						defer.reject('invalid bank');
						return;
					}

					file.upload = Upload.upload({
						url: $location.$$absUrl + '/upload',
						data: {
							file: file,
							bankId: bank.BankId
						}
					})
						.then(handleUploadResponse, handleUploadError, handleUploadProgress);
				});

				return defer.promise;
			},
			BankList: function () {
				return $http({
					url: $location.$$absUrl + '/BankList',
					method: 'GET'
				})
					.then(function (response) {
						return response.data;
					});
			}
		};
	};
	var dataService = function ($http, $q, $location) {
		'use strict';

		console.log('dataservice',$location.$$absUrl);
		return {
			SaveMessage: function (contact) {
				return $http({
					url: $location.$$absUrl + 'Home/SubmitMessage',
					method: 'POST',
					data: {
						name: contact.name,
						surname: contact.surname,
						email: contact.email,
						message: contact.message
					}
				});
			}
		};
	};
	var calendarService = function ($http, $location) {
		'use strict';
		return {
			UpcomingEvents: function () {
				return $http({
					url: $location.$$absUrl + '/UpcomingEvents',
					method: 'GET'
				});
			}
		};
	};
	var geolocatorService = function ($window, $q, $log) {

		var currentPositionDefer = $q.defer();
		var watchPositionDefer = $q.defer();

		var options = {
			enableHighAccuracy: true
		};

		var browserSupportsGeolocation = function () {
			return 'geolocation' in $window.navigator;
		};

		var handleGetCurrentPositionError = function (error) {
			$log.error(error);
		};

		var handleGetCurrentPositionResponse = function (data) {
			currentPositionDefer.resolve(data);
		};

		var handleWatchPositionResponse = function (response) {
			watchPositionDefer.resolve(response);
		};

		var handleWatchPositionError = function (error) {
			watchPositionDefer.error(error);
		};

		var currentPosition = function () {

			if (browserSupportsGeolocation()) {
				$window.navigator.geolocation.getCurrentPosition(handleGetCurrentPositionResponse,
					handleGetCurrentPositionError,
					options);
			} else {
				$log.error('goalocation not supported');
			}

			return currentPositionDefer.promise;
		};

		var watchPosition = function () {

			if (browserSupportsGeolocation()) {
				var id = $window.navigator.geolocation.watchPosition(handleWatchPositionResponse,
					handleWatchPositionError,
					options);
				$log.info(id);
			} else {
				$log.error(' goalocation not supported');
			}

			return watchPositionDefer.promise;
		};

		var clearWatch = function (id) {
			if (browserSupportsGeolocation()) {
				$window.navigator.geolocation.clearWatch(id);
			} else {
				$log.error(' goalocation not supported');
			}
		};

		return {
			currentPosition: currentPosition,
			watchPosition: watchPosition,
			clearWatch: clearWatch
		};
	};
	var openWeatherMapService = function ($http, $q, $filter) {

		var currentWeatherDefer = $q.defer();
		var weatherForecastDefer = $q.defer();

		function OpenWeatherMap() {
			this.apiKey = settings.apiKey;
		}

		var handleCurrentWeatherResponse = function (response) {
			var data = response.data;
			var responseObject = {
				date: new Date(1000 * data.dt).toLocaleDateString(),
				city: data.name,
				coord: data.coord,
				clouds: data.clouds.all,
				description: data.weather[0].main,
				icon: data.weather[0].icon,
				humidity: data.main.humidity,
				pressure: data.main.pressure,
				temp: {
					now: $filter('number')(data.main.temp, 0),
					min: $filter('number')(data.main.temp_min, 0),
					max: $filter('number')(data.main.temp_max, 0)
				},
				sunrise: $filter('date')(new Date(1000 * data.sys.sunrise), 'HH:mm'),
				sunset: $filter('date')(new Date(1000 * data.sys.sunset), 'HH:mm'),
				visibility: data.visibility,
				wind: {
					degrees: data.wind.deg,
					speed: data.wind.speed
				}
			};

			currentWeatherDefer.resolve(responseObject);
		};

		var handleCurrentWeatherError = function (error) {
			currentWeatherDefer.reject(error);
		};

		var handleWeatherForecastResponse = function (response) {
			var data = response.data;
			var list = data.list;
			var items = [];

			angular.forEach(list,
				function (item) {
					items.push(
						{
							date: $filter('date')(new Date(item.dt_txt), 'yyyy-MM-dd'),
							time: $filter('date')(new Date(item.dt_txt), 'HH:mm'),
							clouds: item.clouds.all + ' %',
							description: item.weather[0].description,
							icon: item.weather[0].icon,
							humidity: item.main.humidity + ' %',
							pressure: item.main.pressure + ' hPa',
							temp: {
								now: $filter('number')(item.main.temp, 0),
								min: $filter('number')(item.main.temp_min, 0),
								max: $filter('number')(item.main.temp_max, 0)
							},
							wind: {
								degrees: item.wind.deg + 'degrees',
								speed: item.wind.speed + ' meter/sec'
							}
						}
					);
				});

			var responseObject = {
				city: data.city.name,
				items: items
			};

			weatherForecastDefer.resolve(responseObject);
		};

		var handleWeatherForecastError = function (error) {
			weatherForecastDefer.error(error);
		};

		OpenWeatherMap.prototype.getCurrentWeather = function (lat, long) {

			$http({
				url: settings.weatherUrl,
				method: 'GET',
				params: {
					APPID: settings.apiKey,
					lat: lat,
					lon: long,
					units: 'metric'
				}
			})
				.then(handleCurrentWeatherResponse, handleCurrentWeatherError);

			return currentWeatherDefer.promise;
		};

		OpenWeatherMap.prototype.getWeatherForecast = function (lat, long) {

			$http({
				url: settings.forecastUrl,
				method: 'GET',
				params: {
					APPID: settings.apiKey,
					lat: lat,
					lon: long,
					units: 'metric'
				}
			})
				.then(handleWeatherForecastResponse, handleWeatherForecastError);

			return weatherForecastDefer.promise;
		};

		return new OpenWeatherMap(); // Singleton
	};

	//providers
	var openWeatherMapProvider = function () {
		settings.apiKey = null;
		settings.apiVersion = '2.5';

		this.setApiKey = function (key) {
			settings.apiKey = key;
		}, this.setApiVersion = function (version) {
			settings.apiVersion = version;
		};
		this.getApiKey = function () {
			return settings.apiKey;
		};
		this.getApiVersion = function () {
			return settings.apiVersion;
		};
		this.init = function () {
			settings.weatherUrl = `http://api.openweathermap.org/data/${settings.apiVersion}/weather`;
			settings.forecastUrl = `http://api.openweathermap.org/data/${settings.apiVersion}/forecast`;
		};
		this.$get = ['$http', '$q', '$filter', openWeatherMapService];
	};

	angular.module('rlApp', ['ngRoute', 'ngStorage', 'ngFileUpload', 'ngAnimate'])
		.value('settings', settings)
		.constant('apiUrl', 'api/files/')
		.config(['$routeProvider', '$httpProvider', '$locationProvider', '$compileProvider', 'openWeatherMapProvider', appConfig ])
		.run(['$location', runApp])
		.factory('$storageService', ['$localStorage', '$filter', '$q', storage])
		.factory('accountingService', ['$http', '$location', '$q', 'Upload', accountingService])
		.factory('dataService', ['$http', '$q', '$location', dataService])
		.factory('calendarService', ['$http', '$location', calendarService])
		.factory('geolocatorService', ['$window', '$q', '$log', geolocatorService])
		.factory('openWeatherMapService', ['$http', '$q', '$filter', openWeatherMapService])
		.directive('rlScrambler', ['$q', rlScrambler])
		.directive('rlTumbler', ['$interval', '$q', '$animate', rlTumbler])
		.directive('clock', clock)
		.directive('windowSize', ['$window', windowSize])
		.directive('context', context)
		.directive('iconTitle', iconTitle)
		.directive('aboutMe', aboutMe)
		.directive('jseMore', jseMore)
		.directive('gconnectMore', gconnectMore)
		.directive('bytesMore', bytesMore)
		.directive('itiMore', itiMore)
		.directive('cruxMore', cruxMore)
		.directive('contactFormTemplate', contactFormTemplate)
		.directive('timeline', timeline)
		.directive('cellHighlight', cellHighlight)
		.directive('rlRightClick', ['$parse', rlRightClick])
		.provider('openWeatherMap', [openWeatherMapProvider])
		.controller('BaseController', ['$scope', '$route', '$window', '$anchorScroll', baseController])
		.controller('CvController', ['$scope', '$anchorScroll', 'dataService', cvController])
		.controller('HomeController', ['$scope', homeController])
		.controller('TimesheetController',
			['$scope', '$storageService', '$interval', '$filter', '$q', timesheetController])
		.controller('AccountingController', ['$scope', '$sce', 'accountingService', accountingController])
		.controller('WeatherController',
			[
				'$scope', '$document', '$filter', 'calendarService', 'geolocatorService', 'openWeatherMapService',
				weatherController
			]);

	angular.module('infinite-scroll').value('THROTTLE_MILLISECONDS', 250);

})(angular);