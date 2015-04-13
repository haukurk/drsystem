'use strict';

describe('Directive: recentDirective', function () {

  // load the directive's module
  beforeEach(module('drsystemApp'));

  var element,
    scope;

  beforeEach(inject(function ($rootScope) {
    scope = $rootScope.$new();
  }));

  it('should make hidden element visible', inject(function ($compile) {
    element = angular.element('<recent-directive></recent-directive>');
    element = $compile(element)(scope);
    expect(element.text()).toBe('this is the recentDirective directive');
  }));
});
