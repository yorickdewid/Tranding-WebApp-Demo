describe('ForexController', function () {
    beforeEach(module('forexControllers'));
    beforeEach(module('forexServices'));

    var $controller;
    beforeEach(inject(function (_$controller_) {
        $controller = _$controller_;
    }));

    describe("Forex", function () {
        it('should have ForexCtrl', function () {
            expect($controller('ForexCtrl', { $scope: {} })).toBeDefined();
        });

        it('should have ForexFactory', inject(function (Forex) {
            expect(Forex).toBeDefined();
        }));
    });

    describe("$scope.checkProfit", function () {
        var $scope, controller;

        beforeEach(function () {
            $scope = {};
            controller = $controller('WalletCtrl', { $scope: $scope, $routeParams: {} });
        });

        it('should be success when you make profit', function () {
            var trade = {};
            trade.BuyRate = 22;
            trade.SellRate = 33;
            expect($scope.checkProfit(trade)).toBe('success');
        });

        it('should be danger when you make loss', function () {
            var trade = {};
            trade.BuyRate = 33;
            trade.SellRate = 22;
            expect($scope.checkProfit(trade)).toBe('danger');
        });
    });
   
    describe("$scope.sell", function () {
        var $scope, controller, $httpBackend;

        beforeEach(inject(function (_$httpBackend_) {
            $httpBackend = _$httpBackend_;

            $httpBackend
                .when('GET', '/api/wallet/1')
                .respond(
                {
                    "Id": 1,
                    "UserId": {
                        "Id": 1,
                        "Name": "Arie Kaas"
                    },
                    "Amount": 1000.0,
                    "Trades":
                        [{
                            "Id": 1036,
                            "UserId": 1,
                            "Currency": "JEP",
                            "BuyDate": 1457607630,
                            "SellDate": 0,
                            "BuyRate": 0.700816,
                            "SellRate": 0.0,
                            "Amount": 16
                        }]
                }
            );
          
            $httpBackend.when('GET','/api/forex/JEP').respond(
                { "Id": 584, "Code": "JEP", "Ratio": 0.700816 }
            );

            $httpBackend.when('PUT', '/api/order/').respond({});

            $scope = {};
            controller = $controller('WalletCtrl', { $scope: $scope, $routeParams: {id:1} });
        }));

        it('should give the correct profitcalculation', function () {
            $httpBackend.flush();
            var order = $scope.trades[0];
            var amount = order.amount;
            var ratio = 0.700816;

            expect(order.SellRate).toBe(0);
            expect(order.profit).not.toBeDefined();

            $scope.sell(order);
            $httpBackend.flush();

            expect(order.SellRate).toBe(ratio);
            expect(order.profit).toBe((order.SellRate - order.BuyRate) * order.Amount);
        });
    });
});