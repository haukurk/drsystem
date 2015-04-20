'use strict';

/**
 * @ngdoc function
 * @name drsystemApp.controller:MainCtrl
 * @description
 * # MainCtrl
 * Controller of the drsystemApp
 */
angular.module('drsystemApp')
  .controller('MainCtrl', ['$location','$rootScope', '$scope', 'authService', 'reviewsService', function ($location, $rootScope, $scope, authService, reviewsService) {
      
      $scope.reviews = [];
      $scope.spinner = {active:true};
      
      reviewsService.GetAll().
      success(function(data) {
          $scope.reviews = data;
          $scope.spinner = {active:false};
      }).
      error(function(data, status) {
          window.alert('error loading reviews: '+data+status);
          $scope.spinner = {active:false};
      });
      
  }]);
