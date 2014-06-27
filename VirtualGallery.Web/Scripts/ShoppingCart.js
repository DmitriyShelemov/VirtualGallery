function ShoppingCart(selector, name) {
    this.selector = selector;
    this.name = name;
}

(function (prototype) {
    var cookieName = "cart";
    var getData = function () {

        var value = $.cookie(cookieName);
        var data = null;
        if (value != null) {
            try {
                data = JSON.parse(value);
            } catch (e) {
            }
        }

        if (!data || !data.lots) {
            data = { lots: [] };
        }

        return data;
    };

    var addLot = function (pictureId) {

        var data = getData();
        if (data.lots.indexOf(pictureId) == -1) {
            data.lots.push(pictureId);
            $.cookie(cookieName, JSON.stringify(data), { expires: 365, path: '/' });

            return true;
        }

        return false;
    };

    var removeLot = function (pictureId) {

        var data = getData();
        if (data.lots.indexOf(pictureId) > -1) {
            data.lots.pop(pictureId);
            $.cookie(cookieName, JSON.stringify(data), { expires: 365, path: '/' });

            return true;
        }

        return false;
    };

    prototype.add = function (pictureId) {
        var result = false;
        if (pictureId) {
            result = addLot(pictureId);
            this.refresh();
        }

        return result;
    };

    prototype.setField = function (name, val) {
        var data = getData();
        data[name] = val;
        $.cookie(cookieName, JSON.stringify(data), { expires: 365, path: '/' });
    };

    prototype.getField = function (name, val) {
        return getData()[name];
    };

    prototype.remove = function (pictureId) {
        var result = false;
        if (pictureId) {
            result = removeLot(pictureId);
            this.refresh();
        }

        return result;
    };

    prototype.refresh = function () {

        var lotsCount = getData().lots.length;
        if (lotsCount > 0) {
            $(this.selector).text(this.name + " (" + getData().lots.length + ")");
        } else {
            $(this.selector).text(this.name);
        }
    };

    prototype.gets = function () {
        return getData().lots;
    };

    prototype.cleanup = function () {
        var data = getData();
        data.lots = new Array();
        $.cookie(cookieName, JSON.stringify(data), { expires: 365, path: '/' });
    };

})(ShoppingCart.prototype);
