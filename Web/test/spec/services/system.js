'use strict';

describe('Service: system', function () {

  // load the service's module
  beforeEach(module('drsystemApp'));

  // instantiate service
  var system;
  beforeEach(inject(function (_system_) {
    system = _system_;
  }));

  it('should do something', function () {
    expect(!!system).toBe(true);
  });

});
