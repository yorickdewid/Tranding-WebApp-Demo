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
        templateUrl: 'partials/login.html',
        controller: 'LoginCtrl'
      }).
      when('/wallet', {
        templateUrl: 'partials/wallet.html',
        controller: 'WalletCtrl'
      }).
      when('/forex', {
        templateUrl: 'partials/forex.html',
        controller: 'ForexCtrl'
      }).
      otherwise({
        redirectTo: '/login'
      });
  }]);
