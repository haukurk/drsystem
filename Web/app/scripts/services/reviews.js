'use strict';

/**
 * @ngdoc service
 * @name drsystemApp.reviews
 * @description
 * # reviews
 * Service in the drsystemApp.
 */
angular.module('drsystemApp')
  .service('reviewsService', ['$http', 'CONFIG', function ($http, CONFIG) {
    // AngularJS will instantiate a singleton by calling "new" on this function
    
    var service = {};
  
        function GetAll() {
            return $http.get(CONFIG.APIURLBASE+'/api/reviews');
        }
 
        function GetById(id) {
            return $http.get(CONFIG.APIURLBASE+'/api/reviews/' + id).then(handleSuccess, handleError('Error getting user by id'));
        }
 
        function Create(review) {
            return $http.post(CONFIG.APIURLBASE+'/api/reviews', review).then(handleSuccess, handleError('Error creating user'));
        }
 
        function Update(review) {
            return $http.put(CONFIG.APIURLBASE+'/api/reviews/' + review.id, review).then(handleSuccess, handleError('Error updating review'));
        }
 
        function Delete(id) {
            return $http.delete(CONFIG.APIURLBASE+'/api/reviews/' + id).then(handleSuccess, handleError('Error deleting review'));
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
    
        service.GetAll = GetAll;
        service.GetById = GetById;
        service.Create = Create;
        service.Update = Update;
        service.Delete = Delete;
 
        return service;
      
  }]);