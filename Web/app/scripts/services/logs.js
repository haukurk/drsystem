'use strict';

/**
 * @ngdoc service
 * @name drsystemApp.logs
 * @description
 * # logs
 * Service in the drsystemApp.
 */
angular.module('drsystemApp')
  .service('logsService', function () {
    // AngularJS will instantiate a singleton by calling "new" on this function
    
        var service = {};
 
        service.GetAll = GetAll;
        service.GetById = GetById;
        service.Create = Create;
        service.Update = Update;
        service.Delete = Delete;
 
        return service;
 
        function GetAll() {
            return $http.get(CONFIG.APIURLBASE+'/api/logs').then(handleSuccess, handleError('Error getting all logs'));
        }
 
        function GetById(id) {
            return $http.get(CONFIG.APIURLBASE+'/api/logs/' + id).then(handleSuccess, handleError('Error getting log by id'));
        }
 
        function Create(log) {
            return $http.post(CONFIG.APIURLBASE+'/api/logs', log).then(handleSuccess, handleError('Error creating log'));
        }
 
        function Update(log) {
            return $http.put(CONFIG.APIURLBASE+'/api/logs/' + log.id, log).then(handleSuccess, handleError('Error updating log'));
        }
 
        function Delete(id) {
            return $http.delete(CONFIG.APIURLBASE+'/api/logs/' + id).then(handleSuccess, handleError('Error deleting log'));
        }
 
        // private functions
 
        function handleSuccess(data) {
            return data;
        }
 
        function handleError(error) {
            return function () {
                return { success: false, message: error };
            };
        }
    
  });
