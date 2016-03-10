
describe('Forex Trading App', function () {
    it('should have a title', function () {
        browser.get('/#/login');
        expect(browser.getTitle()).toEqual('Forex Trading');
    });

    it('should redirect to login page when not logged in', function () {
        browser.get('/#/forex');
        browser.getLocationAbsUrl().then(function (url) {
            expect(url).toEqual('/login');
        });
    });

    afterEach(function () {
        browser.get('/#/logout');
    });
});

describe('Forex Overview', function () {
    
    beforeEach(function () {
        browser.get('/#/login'); //Login
        element(by.model('userSelected')).$('[value="number:1"]').click();
        browser.get('/#/forex'); 
    });
    
    it('should show a certain amount of currencies I can buy', function () {
        var rateList = element.all(by.repeater('rate in rates'));
        expect(rateList.count()).toBe(513);
    });

    it('should turn green when I buy the currency', function () {
        var rateList = element.all(by.repeater('rate in rates'));
        rateList.first().element(by.model('orderAmount')).sendKeys('666');
        rateList.first().element(by.tagName('button')).click();

        expect(rateList.first().getAttribute('class')).toMatch('btn-success');
    });
    /*
    it('should at an entry to my wallet when I buy the currency', function () {
        browser.get('/#/wallet');
        var orderListSize = element.all(by.repeater('cur in trades')).count();

        browser.get('/#/forex');
        var rateList = element.all(by.repeater('rate in rates'));
        rateList.first().element(by.model('orderAmount')).sendKeys('666');
        rateList.first().element(by.tagName('button')).click();

        browser.get('/#/wallet');
        expect(element.all(by.repeater('cur in trades')).count()).toBe(orderListSize + 1);
        
    });*/

    afterEach(function () {
        browser.get('/#/logout');
    });
})