//Controllers//
'use strict';
///////////////

var forexControllers = angular.module('forexControllers', []);

forexControllers.controller('LoginCtrl', ['$scope', '$location', 'User',
  function ($scope, $location, User) {

    if ($.parseJSON(sessionStorage.user)) {
          $location.path('wallet/' + $.parseJSON(sessionStorage.user.Id));
  	}

  	$scope.users = User.get({}, function (list) {
  	    console.log(list);
  	    $scope.users = list.Users;
  	});

  	$scope.onChange = function () {
  	    for (var i = 0; i < $scope.users.length; i++) {
  	        if ($scope.users[i].Id == $scope.userSelected) {
  	            console.log($scope.users[i]);
  	            sessionStorage.setItem('user', JSON.stringify($scope.users[i]));
  	            $location.path('wallet/' + $scope.users[i].Id);
  	            break;
  	  	    }
  	    }
  	}

  }]
);

forexControllers.controller('WalletCtrl', ['$scope', '$routeParams', '$http',
 function ($scope, $routeParams, $http) {
 	$scope.userid = $routeParams.id;

 	$http.get('/api/wallet/' + $scope.userid).then(function (response) {
 	    var fTrades = [];
 	    var amount = response.data.Amount;
 		angular.forEach(response.data.Trades, function (value, key) {
 			if (value.SellRate)
 				value.profit = (value.SellRate - value.BuyRate) * value.Amount;
 			value.BuyDate = new Date(value.BuyDate * 1000);
 			if (value.SellDate)
 				value.SellDate = new Date(value.SellDate * 1000);
 			fTrades.push(value);
 		});

 		$scope.Amount = amount;
 		$scope.trades = fTrades;
 	})

 	$scope.checkProfit = function (trade) {
 		if (trade.SellRate) {
 			var profit = (trade.SellRate - trade.BuyRate);
 			if (profit>0)
 				return "success";
 			if (profit<0)
 				return "danger";
 		}
 	}

 }]
);

forexControllers.controller('ForexCtrl', ['$scope','$http','Forex',
  function ($scope, $http, Forex) {
  	$scope.forex = Forex.get({}, function (forex) {
  	    $scope.rates = forex.rates;
  		/*console.log(forex.rates);*/
  	});

  	$scope.success = false;

  	$scope.order = function (amount, currency) {
  	    var user = $.parseJSON(sessionStorage.user);
  	    if (amount > 0) {
  	        var data = {
  	            "UserId": user, // Creates new user..
  	            "Currency": currency,
  	            "BuyDate": 1457357484,
  	            "SellDate": 1457368284,
  	            "Amount": amount
  	        };
  	        $http.post('/api/order/', data).then(function successCallback(response) {
  	            $scope.success = true;
  	        }, function errorCallback(response) {
  	            console.log("Error :" +response)
  	        });
  	    }
  	}

  }]
);

forexControllers.controller('UsersCtrl', ['$scope', '$http', '$route', 'User',
  function ($scope, $http, $route, User) {
      $scope.users = User.get({}, function (user) {
          $scope.users = user.Users;
      });

      $scope.delete = function (user) {
          console.log("delete user " + user.Id);

          $http.delete('/api/user/' + user.Id).then(function successCallback(response) {
              console.log(response);
              $route.reload();
          }, function errorCallback(response) {
              console.log("Error :" + response)
          });

      }
  }]
);
