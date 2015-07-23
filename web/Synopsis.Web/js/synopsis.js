angular.module("synopsis", ["ui.bootstrap"])
	.controller("SynopsisController", [
		"$scope", "$http", "$log", '$timeout',
		function ($scope, $http, $log, $timeout) {
			$scope.url = "";
			$scope.alerts = [];

			$scope.request = function (isValid) {
				if (!isValid) {
					return;
				}

				// Reset results scope.
				$scope.words = [];
				$scope.images = [];
				$scope.loading = true;
				$scope.showResults = false;

				$http.get("/request", {
						params: {
							url: $scope.url
						}
					})
					.success(function(data, status, headers, config) {
						$log.debug("Received: ", data);
						$scope.images = data.images;
						$log.debug("Setting images: ", $scope.images);

						$log.debug("Setting words: ", $scope.words);
						$scope.words = data.words;

						$scope.loading = false;
						$scope.showResults = true;
					})
					.error(function(data, status, headers, config) {
						$log.warn(data);
						$scope.error = true;
						$scope.errorMessage = data.message;
						$scope.loading = false;

						$timeout(function () {
							$scope.error = false;
						}, 10000);
					});
			};
		}
	]);