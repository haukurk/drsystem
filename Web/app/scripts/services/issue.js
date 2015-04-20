'use strict';

/**
 * @ngdoc service
 * @name drsystemApp.issue
 * @description
 * # issue
 * Service in the drsystemApp.
 */
angular.module('drsystemApp')
  .service('issueService', function () {
    // AngularJS will instantiate a singleton by calling "new" on this function
    
            var service = {};
 
        service.GetAll = GetAll;
        service.GetById = GetById;
        service.Create = Create;
        service.Update = Update;
        service.Delete = Delete;
 
        return service;
 
        function GetAll() {
            return $http.get(CONFIG.APIURLBASE+'/api/issues').then(handleSuccess, handleError('Error getting all issue'));
        }
 
        function GetById(id) {
            return $http.get(CONFIG.APIURLBASE+'/api/issues/' + id).then(handleSuccess, handleError('Error getting issue by id'));
        }
 
        function Create(issue) {
            return $http.post(CONFIG.APIURLBASE+'/api/issues', issue).then(handleSuccess, handleError('Error creating issue'));
        }
 
        function Update(issue) {
            return $http.put(CONFIG.APIURLBASE+'/api/issues/' + issue.id, log).then(handleSuccess, handleError('Error updating issue'));
        }
 
        function Delete(id) {
            return $http.delete(CONFIG.APIURLBASE+'/api/issues/' + id).then(handleSuccess, handleError('Error deleting issue'));
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
