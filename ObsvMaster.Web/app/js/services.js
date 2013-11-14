var services = angular.module('services', []);

services.value('Info', {
    title: 'Observer Master Trip List',
    version: '1.0'
});

services.factory('ObsvMasterResource', function ($resource) {
    return $resource('../api/obsvmaster/:action',
        //{ id: '@Id' },
        {},
        {
            //getAll: { method: 'GET', isArray: true },
            getAllVessels: { method: 'GET', params: { action: 'GetVesselNames' }, isArray: true },
            getVesselsLookUp: { method: 'GET', params: { action: 'GetVesselLookUp' }, isArray: true },
            getPortsLookUp: { method: 'GET', params: { action: 'GetPortLookUp' }, isArray: true },
            getObsProgLookUp: { method: 'GET', params: { action: 'GetObsProgLookUp' }, isArray: true },
            getAllPorts: { method: 'GET', params: { action: 'GetPortNames' }, isArray: true },
            update: { method: 'PUT' },
            getCount: { method: 'GET', params: { action: 'GetCount' }, isArray: false },
            getHistory: { method: 'GET', params: { action: 'GetHistory' }, isArray: true },
            getAllStatus: { method: 'GET', params: { action: 'GetAllStatus' }, isArray: true },
            getAllTrips: { method: 'GET', params: { action: 'GetAllTrips' }, isArray: false }
        }
    );
});



