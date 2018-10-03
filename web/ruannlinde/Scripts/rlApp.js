(function (angular) {
    'use strict';

    var appConfig = function ($routeProvider, $locationProvider) {
        $routeProvider
            .when('/rl/timesheet', {
                templateUrl: 'timesheet.htm'
                //controller: 'TimesheetCtrl',
                //controllerAs: 'timesheet'
            })
            .when('/rl', {
                templateUrl: 'cv.htm'
            })
            .when('/rl/cv', {
                templateUrl: 'cv.htm'
            })
            .otherwise({
                redirectTo: '/'
            });

        $locationProvider.html5Mode({
            enabled: true,
            requireBase: true
        });
    };

    //controllers
    var baseController = function ($rootScope, $scope, $location, $route, $window, $anchorScroll) {
        $scope.template = 'cv.htm';
        $scope.redirectToTemplate = function (template) {
            $scope.template = template;
            //$location.hash('top');
            $anchorScroll('top');
        };

        $rootScope.$on('$viewContentLoaded', function (event) {
            //$location.hash('top');
            //$anchorScroll('top');
        });

        $scope.redirectToView = function () { };
        $scope.ShowContextMenu = function () { };

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

        $scope.titles = [
            { name: 'C# development', className: 'c-sharp' },
            { name: 'AngularJs - Single page applications', className: 'angular' },
            { name: 'JavaScript', className: 'javascript' },
            { name: 'Microsoft SQL', className: 'sql' },
            { name: 'MySQL', className: 'mysql' },
            { name: 'SQL Lite', className: 'sql-lite' },
            { name: 'bootstrap', className: 'bootstrap' },
            { name: 'jQuery', className: 'jquery' },
            { name: 'Office Integration', className: 'msoffice' },
            { name: 'Intuit - quickbooks developer', className: 'quick-books' },
            { name: 'Cloud Development with', className: 'azure' },
            { name: 'HTML 5', className: 'html5' },
            { name: 'CSS 3', className: 'css3' },
            { name: 'iText pdf generation', className: 'itext' },
            { name: 'Kendo UI', className: 'kendo' },
            { name: 'Node JS', className: 'nodejs' },
            { name: 'Web API', className: 'webapi' },
            { name: 'WCF Services', className: 'wcf' },
            { name: 'Google Apps Integration', className: 'google-developers' },
            { name: 'Micros Fidelio Integration', className: 'micros-fidelio' }
        ];

    };
    var homeController = function (scope) {
        scope.model = {};
        console.log('scope.model', scope.model);
    };
    var timesheetController = function ($scope, $storageService, $interval, $filter, $q) {
        var handleAddWeekResponse = function (response) {
            $scope.model.timesheetWeek = response;
            console.log('model', $scope.model);
        };
        var handleAddWeekError = function (response) { console.error(response); };
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
            if (!response) return;

            $scope.model.weeks = response;

            var currentWeek = null;
            $scope.model.weeks.forEach(function (week) {
                var today = new Date();
                var t = today;
                t.setDate(t.getDate() + 6);

                if (new Date(week.start) <= today && new Date(week.start) <= t) currentWeek = week;
            });
            $scope.model.week = currentWeek;
        };
        var weekInTimesheet = function (week) {
            var exists = false;
            if ($scope.model.timesheet) {
                $scope.model.timesheet.data.forEach(function (w) {
                    if (w.id === week.id) exists = true;
                });
            }

            return exists;
        };
        var addWeek = function (week) {
            var defer = $q.defer();

            setTimeout(function () {
                var days = [];
                if (week) {
                    if (weekInTimesheet($scope.model.week))
                        defer.reject('week already in timesheet');
                    else {
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

                            days.push({ day: day, hours: 0 });
                            weekStart = new Date($scope.model.week.start);
                        }

                        $scope.model.addButtonText = 'Save';
                        $scope.model.timesheetWeek = days;

                        defer.resolve(days);
                    }
                } else defer.reject('invalid timesheet data');
            });

            return defer.promise;
        };
        var loadDefaults = function () {
            if ($scope.model) {
                $storageService.TimeList().then(bindTimeList);
                $storageService.WeekList().then(bindWeekList);
                $storageService.GetTimeSheet().then(bindTimesheet);
            }
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
                if ($scope.model.addButtonText === 'Add')
                    addWeek($scope.model.week).then(handleAddWeekResponse, handleAddWeekError);

                else {
                    var week = { id: $scope.model.week.id, days: $scope.model.timesheetWeek };
                    $scope.model.timesheet.data.push(week);
                    $storageService.SaveTimesheet($scope.model.timesheet)
                        .then(handleSaveResponse, handleSaveException);
                }
            },
            ResetWeek: function () {
                $storageService.ResetTimesheet();
                $scope.model.timesheet = { data: [], total: 0 };
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

        var timer = $interval(function () { }, 1000);

        $scope.$watch('model.defaultTime', function (newval) {
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

        $scope.$watch('model.startTime', function (newval) {
            if (newval !== null) {
                var endTime = new Date($scope.model.startTime);
                endTime.setMinutes(endTime.getMinutes() + 60 * 8 + $scope.model.lunchTime);
                $scope.model.endTime = endTime;
            }
        });

        $scope.$watch('model.lunchTime', function (newval) {
            if (newval !== null) {
                var endTime = new Date($scope.model.startTime);
                endTime.setMinutes(endTime.getMinutes() + 60 * 8 + $scope.model.lunchTime);
                $scope.model.endTime = endTime;
            }
        });

        $scope.$on('$viewContentLoaded', function (event) {
            // code that will be executed ... 
            // every time this view is loaded
            console.log(event);
        });

        $scope.$on('$destroy', function () {
            $interval.cancel(timer);
        });

        setTimeout(function () {
            loadDefaults();
        });

    };

    var accountingController = function ($scope, $sce, accountingService) {
        $scope.model = { bank: null };

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

    //directives
    var scrambler = function ($q, $location) {
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
                var directive = '<div id="scramble-directive" class="scramble-container animated fadeIn  {{scrambleClass}}" ng-transclude><div class="scramble-item"><span class="">{{scrambleItem}}</span></div></div> ';

                return directive;
            },
            link: function (s, e, a) {
                var chars = '!<>-_\\/[]{}—=+*^?#________';
                var itemId = 0;
                var complete = 0;
                var counter = 0;
                var output = '';
                var imgUrl = $location.$$absUrl + '/Content/Images/';
                var charactersQueue = [];
                var setTextDefer = $q.defer();

                if (!s.scrambleItem)
                    s.scrambleItem = '';
                if (!s.scrambleClass)
                    s.scrambleClass = '';

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

                        charactersQueue.push({ from, to, start, end, char, item, className });
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
                            if (!char || Math.random() < 0.28) char = randomChar();
                            item.char = char;
                            output += char;
                        } else output += item.from;
                    }

                    s.$apply(function () {
                        if (!output)
                            output = randomChar();

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
                        }, 15);
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

                e.on('$destroy', function () {
                    //setTimeout.cancel(t);
                    //setTimeout.cancel(t3);
                });

                setTimeout(function () {
                    next();
                });
            }
        };
    };
    var clock = function ($interval) {
        'use strict';
        return {
            scope: true,
            transclude: true,
            template: "<span class='clock'>" + "   <span class='clock-time' >{{ date | date: timeFormat }}</span>&nbsp;" + "   <span class='clock-seconds' ng-transclude>{{ date | date: 'ss'}}</span><br/>" + "   <span class='clock-text' ng-transclude>{{ date | date: 'EEEE, MMMM dd'}}</span>" + '</span >',
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

                angular.element($window).bind('resize', function () {
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
                        var ul = $('#' + iAttrs.context),
                            last = null;

                        ul.css({
                            'display': 'none'
                        });
                        $(iElement)
                            .bind('contextmenu', function (event) {
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
                                    if (last === event.timeStamp)
                                        return;
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
            element.bind('contextmenu', function (event) {
                scope.$apply(function () {
                    event.preventDefault();
                    fn(scope, { $event: event });
                });
            });
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
                        $localStorage.timesheet = { data: [], total: 0 };
                    }

                    var ts = { data: [], total: 0 };
                    timesheet.data.forEach(function (week) {
                        total += weekTotal(week);
                        ts.data.push({ week: week, total: total });
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
                    if (!$localStorage.timesheet)
                        $localStorage.timesheet = { data: [], total: 0 };
                    defer.resolve($localStorage.timesheet);
                });

                return defer.promise;
            },
            WeekTotal: function (week) {
                if (week)
                    return weekTotal(week);
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
                    hours = parseInt (duration / (1000 * 60 * 60) % 24);

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
                            if (i < 10) time = '0' + time;
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
                        data: { file: file, bankId: bank.BankId }
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

    angular.module('rlApp', ['ngRoute', 'ngStorage', 'ngFileUpload'])
        .config(['$routeProvider', '$locationProvider', appConfig])
        .controller('baseController', ['$rootScope', '$scope', '$location', '$route', '$window', '$anchorScroll', baseController])
        .controller('homeController', ['$scope', homeController])
        .controller('timesheetController', ['$scope', '$storageService', '$interval', '$filter', '$q', timesheetController])
        .controller('accountingController', ['$scope', '$sce', 'accountingService', accountingController])
        .directive('scrambler', ['$q', '$location', scrambler])
        .directive('clock', clock)
        .directive('windowSize', ['$window', windowSize])
        .directive('context', context)
        .directive('cellHighlight', cellHighlight)
        .directive('rlRightClick', ['$parse', rlRightClick])
        .factory('$storageService', ['$localStorage', '$filter', '$q', storage])
        .factory('accountingService', ['$http', '$location', '$q', 'Upload', accountingService])
        .constant('apiUrl', 'api/files/');

})(angular);