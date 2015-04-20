'use strict';

/**
 * @ngdoc service
 * @name drsystemApp.user
 * @description
 * # user
 * Service in the drsystemApp.
 */
angular.module('drsystemApp')
  .service('userService', ['$http', 'CONFIG', function ($http, CONFIG) {
    // AngularJS will instantiate a singleton by calling "new" on this function
    
      var service = {};
 
        service.GetAll = GetAll;
        service.GetById = GetById;
        service.GetByUsername = GetByUsername;
        service.Create = Create;
        service.Update = Update;
        service.Delete = Delete;
 
        return service;
 
        function GetAll() {
            return $http.get(CONFIG.APIURLBASE+'/api/users').then(handleSuccess, handleError('Error getting all users'));
        }
 
        function GetById(id) {
            return $http.get(CONFIG.APIURLBASE+'/api/users/' + id).then(handleSuccess, handleError('Error getting user by id'));
        }
 
        function GetByUsername(username) {
            return $http.get(CONFIG.APIURLBASE+'/api/users/' + username).then(handleSuccess, handleError('Error getting user by username'));
        }
 
        function Create(user) {
            return $http.post(CONFIG.APIURLBASE+'/api/users', user).then(handleSuccess, handleError('Error creating user'));
        }
 
        function Update(user) {
            return $http.put(CONFIG.APIURLBASE+'/api/users/' + user.username, user).then(handleSuccess, handleError('Error updating user'));
        }
 
        function Delete(username) {
            return $http.delete(CONFIG.APIURLBASE+'/api/users/' + username).then(handleSuccess, handleError('Error deleting user'));
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

    }
  ]);
