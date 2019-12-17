(function (angular) {
	'use strict';

	var settings = {};

	var appConfig = function ($routeProvider, $httpProvider, $locationProvider, $compileProvider) { };
	//controllers
	var baseController = function ($scope, $route, $window, $location, $http, $anchorScroll) {

		$scope.template = 'banking.htm';
		$scope.model = {
			loading: false
		};

		console.info('location', $location);
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

		console.log('route.routes', $route.routes);

		$scope.redirectToView = function () { };
		$scope.ShowContextMenu = function () { };

		var settings = {
			Get: function () {
				return $http({
					url: $location.$$absUrl + 'Banking/Home/GetSettings',
					method: 'GET'
				}).then(function (response) {
					return response.data;
				});
			}
		};
		//var body = $document[0].body;
		//var bodyElement = angular.element(body);
		//bodyElement.removeClass('weather-body');

		if ($window.innerWidth <= 1024) {
			$scope.layout = 'container-fluid';
		} else {
			$scope.layout = 'container';
		}

		angular.element($window).bind('resize', function () {
			if ($window.innerWidth <= 1024) {
				$scope.layout = 'container-fluid';
			} else {
				$scope.layout = 'container';
			}
		});

		setTimeout(function () {
			//$scope.settings = settings.Get();
			//console.info('settings', $scope.settings);
		});

	};
	angular.module('rlBanking', ['ngRoute', 'ngFileUpload', 'ngAnimate']).value('settings', settings)
		.constant('apiUrl', 'api/files/')
		.config(['$routeProvider', '$httpProvider', '$locationProvider', '$compileProvider', 'openWeatherMapProvider', appConfig])
		.run(['$location', runApp])
		.controller('BaseController', ['$scope', '$route', '$location', baseController])

		;

})(angular);