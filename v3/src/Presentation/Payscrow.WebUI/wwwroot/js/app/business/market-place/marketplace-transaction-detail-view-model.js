var MarketPlaceTransactionDetailViewModel = function (transactionId) {
    var self = this;

    self.getStatusClass = function (item) {
        var classes = {
            1: { class: 'badge-warning' },
            2: { class: 'badge-primary' },
            3: { class: 'badge-light-success' },
            4: { class: 'badge-success' }
        };

        return classes[item.status.id()]?.class;
    }
}