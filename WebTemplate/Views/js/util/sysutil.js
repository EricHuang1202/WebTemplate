var webSitePathName = 'http://localhost:50367/api/';
var myApp = angular.module('myApp', []);

myApp.factory('session', function ($http, $q) {
    return {
        setSession:
            function (param, value) {
                var deferred = $q.defer();
                var data = { "Param": param, "Value": value };
                $http({
                    method: 'POST',
                    url: webSitePathName + "Session/SetSession",
                    data: data,
                }).then(function mySuccess(response) {
                    deferred.resolve(response);
                }, function myError(response) {
                    alert("Set Session Error!")
                });

                return deferred.promise;
            },
        getSession:
            function (param) {
                var deferred = $q.defer();
                var data = { "Param": param };
                var sessionValue;
                $http({
                    method: 'POST',
                    url: webSitePathName + "Session/GetSession",
                    data: data,
                }).then(function mySuccess(response) {
                    deferred.resolve(response);
                    sessionValue = response.data.Value;
                }, function myError(response) {
                    alert("Get Session Error!")
                });

                return deferred.promise;
            },
        delSession:
            function (param) {
                var deferred = $q.defer();
                var data = { "Param": param };
                $http({
                    method: 'POST',
                    url: webSitePathName + "Session/DelSession",
                    data: data,
                }).then(function mySuccess(response) {
                    deferred.resolve(response);
                }, function myError(response) {
                    alert("Delete Session Error!")
                });

                return deferred.promise;
            }
    };
});

myApp.factory('cookie', function ($http, $q) {
    return {
        setCookie:
        function (name, value) {
            var Days = 30;  //此 cookie 將被保存 30 天
            var exp = new Date();    //new Date("December 31, 9998");
            exp.setTime(exp.getTime() + Days * 24 * 60 * 60 * 1000);
            document.cookie = name + "=" + escape(value) + ";expires=" + exp.toGMTString();
        },
        getCookie:
        function (name) {
            var arr = document.cookie.match(new RegExp("(^| )" + name + "=([^;]*)(;|$)"));
            if (arr != null) return unescape(arr[2]); return null;
        },
        delCookie:
        function (name) {
            var exp = new Date();
            exp.setTime(exp.getTime() - 1);
            var cval = this.getCookie(name);
            if (cval != null) document.cookie = name + "=" + cval + ";expires=" + exp.toGMTString();
        }
    };
});
