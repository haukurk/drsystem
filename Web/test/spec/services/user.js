'use strict';

describe('Service: userService', function () {

  // load the service's module
  beforeEach(module('drsystemApp'));

  // instantiate service
  var user;
  beforeEach(inject(function (_user_) {
    user = _user_;
  }));

  it('should do something', function () {
    expect(!!user).toBe(true);
  });

});
