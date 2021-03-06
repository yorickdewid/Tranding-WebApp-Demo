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
      when('/wallet/:id', {
       templateUrl: 'AngularJs/partials/wallet.html',
        controller: 'WalletCtrl'
      }).
      when('/forex', {
       templateUrl: 'AngularJs/partials/forex.html',
        controller: 'ForexCtrl'
      }).
      when('/users', {
          templateUrl: 'AngularJs/partials/users.html',
          controller:'UsersCtrl'
      }).
      when('/logout', {
      	templateUrl: 'AngularJs/partials/login.html',
      	controller: 'AuthCtrl'
      }).
      otherwise({
        redirectTo: '/login'
      });
  }]);
