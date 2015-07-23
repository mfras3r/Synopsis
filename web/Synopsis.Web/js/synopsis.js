angular.module("synopsis", ["ui.bootstrap"])
	.controller("SynopsisController", [
		"$scope", "$http", "$log", "$timeout",
		function($scope, $http, $log, $timeout) {
			$scope.url = "";
			$scope.alerts = [];
			$scope.curAlertId = 0;

			$scope.closeAlert = function(index) {
				if (0 > index || index >= $scope.alerts.length) {
					return;
				}
				$scope.alerts.splice(index, 1);
			};

			$scope.addAlert = function(alert) {
				var id = $scope.curAlertId,
					foo = {
						id: id
					};

				$scope.curAlertId++;

				foo = angular.extend(foo, alert);
				$scope.alerts.push(foo);
			};

			$scope.clearAlerts = function () {
				$scope.alerts = [];
			};

			$scope.request = function(isValid) {
				if (!isValid) {
					return;
				}
				$scope.clearAlerts();
				$scope.addAlert({
					type: 'info',
					loading: true,
					msg: 'Loading... please wait.'
				});

				// Reset results scope.
				$scope.words = [];
				$scope.images = [];
				$scope.showResults = false;

				$http.get("/request", {
						params: {
							url: $scope.url
						}
					})
					.success(function (data, status, headers, config) {
						$scope.clearAlerts();

						$log.debug("Received: ", data);

						$scope.images = data.images;
						$log.debug("Setting images: ", $scope.images);

						$log.debug("Setting words: ", $scope.words);
						$scope.words = data.words;

						$scope.showResults = true;
					})
					.error(function (data, status, headers, config) {
						$scope.clearAlerts();

						$scope.addAlert({
							type: 'danger',
							msg: data.message
						});

						$log.warn(data);
					});
			};
		}
	]);