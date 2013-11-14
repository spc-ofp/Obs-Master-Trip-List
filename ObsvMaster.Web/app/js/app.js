﻿var ObsvMasterApp = angular.module("ObsvMasterApp", ['ngRoute', 'services', 'ngResource', 'controllers', 'ui.directives', 'directives', 'ui.bootstrap']).
    config(function ($routeProvider) {
        $routeProvider.
            when('/edit/:itemId', { controller: 'ObsvMasterEditCtrl', templateUrl: 'partials/ObsvMasterDetail.html' }).
            when('/new', { controller: 'ObsvMasterCreateCtrl', templateUrl: 'partials/ObsvMasterDetail.html' }).
            when('/', { controller: 'ObsvMasterListCtrl', templateUrl: 'partials/ObsvMasterList.html' }).
            //when('/test', { controller: 'ObsvMasterListCtrlNew', templateUrl: 'partials/ObsvMasterListNew.html' }).
            otherwise({ redirectTo: '/' });
    });