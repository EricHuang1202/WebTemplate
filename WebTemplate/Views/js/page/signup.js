var signupApp = angular.module('signupApp', []);

signupApp.controller('signupCtrl', function ($scope, $http) {
    $scope.signup = function ($event, form1) {

        if (form1.$valid) {

            if ($scope.accPwd != $scope.chkPwd) {
                $scope.showMsg = true;
                $scope.errorMsg = "兩次輸入密碼不符!";
                return;
            }
            $scope.showMsg = false;

            var data = { "AccId": $scope.accId, "AccName": $scope.accName, "AccPwd": $scope.accPwd, "Email": $scope.email };
            $http({
                method: 'POST',
                url: webSitePathName + "SignUp/SignUp",
                data: data,
            }).then(function mySuccess(response) {
                alert(response.data.Message);
                window.location = 'login.html';

            }, function myError(response) {
                alert(response.data.Message);
            });
        }
        else {
            $scope.showMsg = true;

            if (!form1.accName.$valid) {
                if (form1.accName.$viewValue == "" || form1.accName.$viewValue == null) {
                    $scope.errorMsg = "請輸入姓名";
                }
                else {
                    $scope.errorMsg = "姓名格式不正確";
                }
            }
            else if (!form1.accId.$valid) {
                if (form1.accId.$viewValue == "" || form1.accId.$viewValue == null) {
                    $scope.errorMsg = "請輸入帳號";
                }
                else {
                    $scope.errorMsg = "帳號格式不正確";
                }
            }
            else if (!form1.email.$valid) {
                if (form1.email.$viewValue == "" || form1.email.$viewValue == null) {
                    $scope.errorMsg = "請輸入Email";
                }
                else {
                    $scope.errorMsg = "Email格式不正確";
                }
            }
            else if (!form1.accPwd.$valid) {
                if (form1.accPwd.$viewValue == "" || form1.accPwd.$viewValue == null) {
                    $scope.errorMsg = "請輸入密碼";
                }
                else {
                    $scope.errorMsg = "密碼格式不正確";
                }
            }
            else if (!form1.chkPwd.$valid) {
                if (form1.chkPwd.$viewValue == "" || form1.chkPwd.$viewValue == null) {
                    $scope.errorMsg = "請輸入確認密碼";
                }
                else {
                    $scope.errorMsg = "確認密碼格式不正確";
                }
            }

        }
        // 取消 submit 原本預設的送出動作
        $event.preventDefault();
    };

});


