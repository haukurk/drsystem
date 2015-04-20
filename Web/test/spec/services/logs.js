'use strict';

describe('Service: logsService', function () {

  // load the service's module
  beforeEach(module('drsystemApp'));

  // instantiate service
  var logs;
  beforeEach(inject(function (_logs_) {
    logs = _logs_;
  }));

  it('should do something', function () {
    expect(!!logs).toBe(true);
  });

});
