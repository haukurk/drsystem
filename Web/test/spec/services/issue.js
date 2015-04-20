'use strict';

describe('Service: issueService', function () {

  // load the service's module
  beforeEach(module('drsystemApp'));

  // instantiate service
  var issue;
  beforeEach(inject(function (_issue_) {
    issue = _issue_;
  }));

  it('should do something', function () {
    expect(!!issue).toBe(true);
  });

});
