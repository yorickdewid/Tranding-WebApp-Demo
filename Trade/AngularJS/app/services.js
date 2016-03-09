//Services
'use strict';
///////////////

var userServices = angular.module('userServices', ['ngResource']);
var forexServices = angular.module('forexServices', ['ngResource']);

forexServices.factory('Forex', ['$resource',
  function($resource){
    return $resource('api/forex', {}, {
      query: {method:'GET', params:{}, isArray:true}
    });
  }]
);

userServices.factory('User', ['$resource',
  function($resource){
   return $resource('api/user', {}, {
      query: {method:'GET', params:{}, isArray:true}
    });
  }]
);
