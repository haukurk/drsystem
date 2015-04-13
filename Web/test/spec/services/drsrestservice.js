'use strict';

describe('Service: drsRestService', function () {

  // load the service's module
  beforeEach(module('drsystemApp'));

  // instantiate service
  var drsRestService;
  beforeEach(inject(function (_drsRestService_) {
    drsRestService = _drsRestService_;
  }));

  it('should do something', function () {
    expect(!!drsRestService).toBe(true);
  });

});
