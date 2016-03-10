describe('ForexController', function () {
    beforeEach(module('forexControllers'));
    beforeEach(module('forexServices'));

    var $controller;
    beforeEach(inject(function (_$controller_) {
        $controller = _$controller_;
    }));

    describe("The existence of ForexCtr", function () {
        it('should have ForexCtr', function () {
            var $scope = {};
            var controller = $controller('ForexCtrl', { $scope: $scope });
            expect($controller).toBeDefined();
        });

        it('should have ForexService', inject(function (Forex) {
            expect(Forex).toBeDefined();
        }));
    });

}); 