'use strict';

/**
 * @ngdoc directive
 * @name drsystemApp.directive:recentDirective
 * @description
 * # recentDirective
 */
angular.module('drsystemApp')
  .directive('recentDirective', function () {
    return {
      template: '<div></div>',
      restrict: 'E',
      link: function postLink(scope, element, attrs) {
        element.text('this is the recentDirective directive');
      }
    };
  });
