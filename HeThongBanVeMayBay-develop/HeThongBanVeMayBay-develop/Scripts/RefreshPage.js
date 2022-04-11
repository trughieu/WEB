function timedRefresh(timeoutPeriod) {
    setTimeout(function () {
        location.reload(true);
    }, timeoutPeriod);
}