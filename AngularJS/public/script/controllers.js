//Controllers//
'use strict';
///////////////

var forexControllers = angular.module('forexControllers', []);

forexControllers.controller('LoginCtrl', ['$scope', 'User',
  function($scope, User) {
    $scope.user = User.get({}, function(user) {
      $scope.user = user.Users;
      //console.log(user.Users);
    });

  }]
);

forexControllers.controller('WalletCtrl', ['$scope',
  function($scope) {

  }]
);

forexControllers.controller('ForexCtrl', ['$scope', 'Forex',
  function($scope, Forex) {
    $scope.forex = Forex.get({}, function(forex) {
      $scope.rates = forex.rates;
      /*console.log(forex.rates);*/
    });

  }]
);
