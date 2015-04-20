var config_module = angular.module('drsystem.config', [])
.constant('CONFIG', {
    'APP_NAME': 'Document Review System',
    'APP_VERSION': '0.1',
    'FIRST_URL': 'http://drs.samskip.is',
    'APIURLBASE': 'http://localhost:11115'
});