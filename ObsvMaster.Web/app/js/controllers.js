var controllers = angular.module('controllers', [])

controllers.controller("ObsvMasterListCtrl", ['$scope', 'ObsvMasterResource', '$q', ObsvMasterListCtrl]);
controllers.controller("ObsvMasterCreateCtrl", ['$scope', '$location', 'ObsvMasterResource', ObsvMasterCreateCtrl]);
controllers.controller("ObsvMasterEditCtrl", ['$scope', '$location', 'ObsvMasterResource', '$routeParams','$q', ObsvMasterEditCtrl]);

function GetHeader() {
    return [
        { title: 'Vessel', value: 'VesselName' },
        { title: 'Dates', value: 'StartDate' },
        { title: 'Ports', value: 'StartPortName' },
        { title: 'Obsv Code', value: 'ObsvCode' },
        { title: 'Obsv Trip', value: 'ObsvTripCode' },
        { title: 'Obsv Prog', value: 'ObsvProgCode' },
        { title: 'Status', value: 'Status' },
        { title: 'Updated', value: 'LastModifiedDate' },
        { title: 'By', value: 'LastModifiedBy' }
    ]
}

function ObsvMasterCreateCtrl($scope, $location, ObsvMasterResource) {
    $scope.validateForm = function () { return true; }

    $scope.selectedObserver = { Code: '', FirstName: '', FamilyName: '' };

    $scope.headers = [
        { title: 'Vessel', value: 'VesselName' },
        { title: 'Dates', value: 'StartDate' },
        { title: 'Ports', value: 'StartPortName' },
        { title: 'Obsv Code', value: 'ObsvCode' },
        { title: 'Obsv Trip', value: 'ObsvTripCode' },
        { title: 'Obsv Prog', value: 'ObsvProgCode' },
        { title: 'Status', value: 'Status' },
        { title: 'Updated', value: 'LastModifiedDate' },
        { title: 'By', value: 'LastModifiedBy' }
    ];

    $scope.statusList = ObsvMasterResource.getAllStatus();

    $scope.portTypeAhead = function (viewValue) {
        if (viewValue.length < 10) {
            return ObsvMasterResource.getPortsLookUp({ search: viewValue }).$promise;
        } else {
            console.log("not searching for value:", viewValue);
            return "";
        }
    };

    $scope.setKnownObserver = function ($item, $model, $label) {
        $scope.selectedObserver = $item;
    };

    $scope.observerTypeAhead = function (viewValue) {
        if (viewValue.length < 10) {
            return ObsvMasterResource.getObserverLookUp({ search: viewValue }).$promise;
        } else {
            console.log("not searching for value:", viewValue);
            return "";
        }
    };

    $scope.label = function (observer) {
        if (observer && observer.Code)
            return '[' + observer.Code + '] -' + observer.FirstName + ' ' + observer.FamilyName;
        else
            return observer;
    };

    $scope.obsvProgTypeAhead = function (viewValue) {
        if (viewValue.length < 10) {
            return ObsvMasterResource.getObsProgLookUp({ search: viewValue }).$promise;
        } else {
            console.log("not searching for value:", viewValue);
            return "";
        }
    };

    $scope.vesselTypeAhead = function (viewValue) {
        if (viewValue.length < 10) {
            return ObsvMasterResource.getVesselsLookUp({ search: viewValue }).$promise;
        } else {
            console.log("not searching for value:", viewValue);
            return "";
        }
    };


    $scope.save = function () {
        ObsvMasterResource.save($scope.trip, function () {
            $location.path('/');
        }, function () {
            console.log("insert failed");
        });
    };

}

function ObsvMasterEditCtrl($scope, $location, ObsvMasterResource, $routeParams,$q) {
    //$scope.vesselList = ObsvMasterResource.getAllVessels();
    //$scope.portList = ObsvMasterResource.getAllPorts();
    
    $scope.validateForm = function () { return true; }

    $scope.selectedObserver = { Code: '', FirstName: '', FamilyName: '' };

    $scope.headers = [
        { title: 'Vessel', value: 'VesselName' },
        { title: 'Dates', value: 'StartDate' },
        { title: 'Ports', value: 'StartPortName' },
        { title: 'Obsv Code', value: 'ObsvCode' },
        { title: 'Obsv Trip', value: 'ObsvTripCode' },
        { title: 'Obsv Prog', value: 'ObsvProgCode' },
        { title: 'Status', value: 'Status' },
        { title: 'Updated', value: 'LastModifiedDate' },
        { title: 'By', value: 'LastModifiedBy' }
    ];

    $scope.statusList = ObsvMasterResource.getAllStatus();

    $scope.portTypeAhead = function (viewValue) {
        if (viewValue.length < 10) {
            return ObsvMasterResource.getPortsLookUp({ search: viewValue }).$promise;
        } else {
            console.log("not searching for value:", viewValue);
            return "";
        }
    };

    $scope.setKnownObserver = function ($item, $model, $label) {
        $scope.selectedObserver = $item;
    };

    $scope.observerTypeAhead = function (viewValue) {
        if (viewValue.length < 10) {
            return ObsvMasterResource.getObserverLookUp({ search: viewValue }).$promise;
        } else {
            console.log("not searching for value:", viewValue);
            return "";
        }
    };

    $scope.label = function (observer) {
        if (observer && observer.Code)
            return '[' + observer.Code + '] -' + observer.FirstName + ' ' + observer.FamilyName;
        else
            return observer;
    };

    $scope.obsvProgTypeAhead = function (viewValue) {
        if (viewValue.length < 10) {
            return ObsvMasterResource.getObsProgLookUp({ search: viewValue }).$promise;
        } else {
            console.log("not searching for value:", viewValue);
            return "";
        }
    };

    $scope.vesselTypeAhead = function (viewValue) {
        if (viewValue.length < 10) {
            return ObsvMasterResource.getVesselsLookUp({ search: viewValue }).$promise;
        } else {
            console.log("not searching for value:", viewValue);
            return "";
        }
    };

    $scope.save = function () {
        var startDate = moment($scope.trip.StartDate).format("YYYY-MM-DDT") + "12:00:00Z";
        $scope.trip.StartDate = startDate;
        var endDate = moment($scope.trip.EndDate).format("YYYY-MM-DDT") + "12:00:00Z";
        $scope.trip.EndDate = endDate;
        ObsvMasterResource.update({ id: $scope.trip.Id }, $scope.trip, function () {
            $location.path('/');
        });
    }

    return $q.all([
        ObsvMasterResource.get({ id: $routeParams.itemId }).$promise,
        ObsvMasterResource.getHistory({ id: $routeParams.itemId }).$promise
    ]).then(function (data) {
        $scope.trip = data[0];
        $scope.historyList = data[1];
        if ($scope.trip.ObsvCode) {
            $scope.selectedObserver = ObsvMasterResource.getObserver({ code: $scope.trip.ObsvCode });
        }
        $scope.trip.StartDate = moment($scope.trip.StartDate).format("YYYY-MM-DDTHH:mm:ssZ");
        $scope.trip.EndDate = moment($scope.trip.EndDate).format("YYYY-MM-DDTHH:mm:ssZ");
        
    }, function () {
        $scope.trip = {};
        $scope.historyList = {};
        $scope.selectedObserver = {};
    });
}

function ObsvMasterListCtrl($scope, ObsvMasterResource, $q) {
    $scope.totalItems = 0;

    //default criteria that will be sent to the server
    $scope.filterCriteria = {
        pageNumber: 1,
        pageSize: 50,
        sortDir: 'asc',
        sortedBy: 'VesselName',
        offset: 0,
    };

    $scope.isLoading = false;

    $scope.cancelFilter = function () {
        $scope.filterCriteria.vesselName = undefined;
        $scope.filterCriteria.dateYear = undefined;
        $scope.filterCriteria.port = undefined;
        $scope.filterCriteria.obsvCode = undefined;
        $scope.filterCriteria.obsvTripCode = undefined;
        $scope.filterCriteria.obsvProgCode = undefined;
        $scope.filterCriteria.lastModifiedDateYear = undefined;
        $scope.filterCriteria.lastModifiedBy = undefined;
        $scope.fetchResult();
    };

    $scope.askDelete = function () {
        var itemId = this.trip.Id;
        bootbox.confirm("Confirm delete?", function (result) {
            if (result === true) {
                $scope.delete(itemId);
                $scope.$apply();
            }
        });
    }

    $scope.delete = function (itemId) {
        ObsvMasterResource.delete({ id: itemId }, function () {
            $("#trip_" + itemId).fadeOut();
            toastr.success("Check List element deleted!");
        });
    }

    //called when navigate to another page in the pagination
    $scope.selectPage = function (page) {
        $scope.filterCriteria.pageNumber = page;
        $scope.fetchResult();
    };

    //$scope.headers = GetHeader();

    $scope.headers = [
        { title: 'Vessel', value: 'VesselName' },
        { title: 'Dates', value: 'StartDate' },
        { title: 'Ports', value: 'StartPortName' },
        { title: 'Obsv Code', value: 'ObsvCode' },
        { title: 'Obsv Trip', value: 'ObsvTripCode' },
        { title: 'Obsv Prog', value: 'ObsvProgCode' },
        { title: 'Status', value: 'Status' },
        { title: 'Updated', value: 'LastModifiedDate' }
    ];

    //Will make an ajax call to fill the drop down menu in the filter of the states

    //The function that is responsible of fetching the result from the server and setting the grid to the new result
    $scope.fetchResult = function () {
        $scope.filterCriteria.offset = ($scope.filterCriteria.pageNumber - 1) * $scope.filterCriteria.pageSize;
        $scope.isLoading = true;
        return $q.all([
            ObsvMasterResource.query($scope.filterCriteria).$promise,
            ObsvMasterResource.getCount($scope.filterCriteria).$promise
        ]).then(function (data) {
            $scope.items = data[0];
            $scope.totalItems = data[1].count;
            $scope.isLoading = false;
        }, function () {
            $scope.items = [];
            $scope.totalItems = 0;
            $scope.isLoading = false;
        });
    };

    $scope.exportToCsv = function () {
        //console.log($.param($scope.filterCriteria));
        window.open('../api/obsvmaster/GetAllTrips?' + $.param($scope.filterCriteria));
        //ObsvMasterResource.getAllTrips($scope.filterCriteria);
    }

    //Will be called when filtering the grid, will reset the page number to one
    $scope.filterResult = function () {
        $scope.filterCriteria.pageNumber = 1;
        $scope.fetchResult().then(function () {
            //The request fires correctly but sometimes the ui doesn't update, that's a fix
            $scope.filterCriteria.pageNumber = 1;
        });
    };

    //call back function that we passed to our custom directive sortBy, will be called when clicking on any field to sort
    $scope.onSort = function (sortedBy, sortDir) {
        $scope.filterCriteria.sortDir = sortDir;
        $scope.filterCriteria.sortedBy = sortedBy;
        $scope.filterCriteria.pageNumber = 1;
        $scope.fetchResult().then(function () {
            //The request fires correctly but sometimes the ui doesn't update, that's a fix
            $scope.filterCriteria.pageNumber = 1;
        });
    };

    //manually select a page to trigger an ajax request to populate the grid on page load
    $scope.selectPage(1);

    $scope.portTypeAhead = function (viewValue) {
        if (viewValue.length < 10) {
            return ObsvMasterResource.getPortsLookUp({ search: viewValue }).$promise;
        } else {
            console.log("not searching for value:", viewValue);
            return "";
        }
    };

    $scope.vesselTypeAhead = function (viewValue) {
        if (viewValue.length < 10) {
            return ObsvMasterResource.getVesselsLookUp({ search: viewValue }).$promise;
        } else {
            console.log("not searching for value:", viewValue);
            return "";
        }
    };

    $scope.obsvProgTypeAhead = function (viewValue) {
        if (viewValue.length < 10) {
            return ObsvMasterResource.getObsProgLookUp({ search: viewValue }).$promise;
        } else {
            console.log("not searching for value:", viewValue);
            return "";
        }
    };

    $scope.statusList = ObsvMasterResource.getAllStatus();

    return $scope;
}

