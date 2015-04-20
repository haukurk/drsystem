'use strict';

/**
 * @ngdoc service
 * @name drsystemApp.system
 * @description
 * # system
 * Service in the drsystemApp.
 */
angular.module('drsystemApp')
  .service('systemService', function () {
    // AngularJS will instantiate a singleton by calling "new" on this function
    
    var service = {};
 
        service.GetAll = GetAll;
        service.GetById = GetById;
        service.Create = Create;
        service.Update = Update;
        service.Delete = Delete;
 
        return service;
 
        function GetAll() {
            return $http.get(CONFIG.APIURLBASE+'/api/systems').then(handleSuccess, handleError('Error getting all systems'));
        }
 
        function GetById(id) {
            return $http.get(CONFIG.APIURLBASE+'/api/systems/' + id).then(handleSuccess, handleError('Error getting system by id'));
        }
 
        function Create(system) {
            return $http.post(CONFIG.APIURLBASE+'/api/systems', system).then(handleSuccess, handleError('Error creating system'));
        }
 
        function Update(system) {
            return $http.put(CONFIG.APIURLBASE+'/api/systems/' + system.id, user).then(handleSuccess, handleError('Error updating system'));
        }
 
        function Delete(id) {
            return $http.delete(CONFIG.APIURLBASE+'/api/systems/' + id).then(handleSuccess, handleError('Error deleting system'));
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
