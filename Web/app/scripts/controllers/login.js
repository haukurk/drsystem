'use strict';

/**
 * @ngdoc function
 * @name drsystemApp.controller:LoginCtrl
 * @description
 * # LoginCtrl
 * Controller of the drsystemApp
 */
angular.module('drsystemApp')
  .controller('LoginCtrl', ['$scope', 'authService', '$location', function ($scope, authService, $location) {
      
      $scope.login = function () {
          
          $scope.spinner = {active:true};
          $scope.error = null;

          authService.Login($scope.username, $scope.password, function(data, status) {
                if(status === 200) {
                    authService.SetCredentials($scope.username, $scope.password, data.fullname, data.email);
                    $location.path('/');
                } else {
                    $scope.error = data.message;
                    console.log(data);
                }
              $scope.spinner = {active:false};
            });
          
      };
      
      $scope.logout = function () {
          authService.ClearCredentials();
      };
      
  }]);