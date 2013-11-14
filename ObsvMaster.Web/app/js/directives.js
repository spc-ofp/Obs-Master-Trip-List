var directives = angular.module('directives', []);


directives.directive('onBlurChange', function ($parse) {
    return function (scope, element, attr) {
        var fn = $parse(attr['onBlurChange']);
        var hasChanged = false;
        element.on('change', function (event) {
            hasChanged = true;
        });
 
        element.on('blur', function (event) {
            if (hasChanged) {
                scope.$apply(function () {
                    fn(scope, {$event: event});
                });
                hasChanged = false;
            }
        });
    };
});

directives.directive('onEnterBlur', function() {
    return function(scope, element, attrs) {
        element.bind("keydown keypress", function(event) {
            if(event.which === 13) {
                element.blur();
                event.preventDefault();
            }
        });
    };
});

directives.directive('sortBy', function () {
    return {
        template: '<a ng-click="sort(sortvalue)"><span ng-transclude=""></span><span ng-show="sortedby == sortvalue"> <i ng-class="{true: \'fa fa-sort-asc\', false: \'fa fa-sort-desc\'}[sortdir == \'asc\']"></i></span></a>',
        restrict: 'E',
        transclude: true,
        replace: true,
        scope: {
            sortdir: '=',
            sortedby: '=',
            sortvalue: '@',
            onsort: '='
        },
        link: function (scope, element, attrs) {
            scope.sort = function () {
                if (scope.sortedby === scope.sortvalue) {
                    scope.sortdir = ((scope.sortdir === 'asc') ? 'desc' : 'asc');
                } else {
                    scope.sortedby = scope.sortvalue;
                    scope.sortdir = 'asc';
                }
                scope.onsort(scope.sortedby, scope.sortdir);
            };
        }
    };
});

//directives.directive('myTableHeader', function () {
//    return {
//        restrict: 'A',
//        transclude: true,
//        template: '<a ng-click="onClick()" style="cursor:pointer">' +
//                     '<span ng-transclude></span>' +
//                    '&nbsp;<i ng-class="{\'fa fa-arrow-circle-down\' : order === by && !reverse,  \'fa fa-arrow-circle-up\' : order===by && reverse}"></i>' +
//                  '</a>',
//        scope: {
//            order: '=',
//            by: '=',
//            reverse: '='
//        },
//        link: function (scope, element, attrs) {
//            scope.onClick = function () {
//                if (scope.order === scope.by) {
//                    scope.reverse = !scope.reverse
//                } else {
//                    scope.by = scope.order;
//                    scope.reverse = false;
//                }
//            }
//        }
//    };
//});

