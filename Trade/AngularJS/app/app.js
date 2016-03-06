//Application//
'use strict';
///////////////

var forexApp = angular.module('forexApp', [
    'ngRoute',
	'forexControllers',
    'forexServices',
    'userServices'
]);


forexApp.config(['$routeProvider',
  function($routeProvider) {
    $routeProvider.
      when('/login', {
        templateUrl: 'AngularJs/partials/login.html',
        controller: 'LoginCtrl'
      }).
      when('/wallet', {
       templateUrl: 'AngularJs/partials/wallet.html',
        controller: 'WalletCtrl'
      }).
      when('/forex', {
       templateUrl: 'AngularJs/partials/forex.html',
        controller: 'ForexCtrl'
      }).
      otherwise({
        redirectTo: '/login'
      });
  }]);
