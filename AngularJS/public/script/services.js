//Services
'use strict';
///////////////

var userServices = angular.module('userServices', ['ngResource']);
var forexServices = angular.module('forexServices', ['ngResource']);



forexServices.factory('Forex', ['$resource',
  function($resource){
    return $resource('data/forex.json', {}, {
      query: {method:'GET', params:{}, isArray:true}
    });
  }]
);



userServices.factory('User', ['$resource',
  function($resource){
    return $resource('data/user.json', {}, {
      query: {method:'GET', params:{}, isArray:true}
    });
  }]
);
