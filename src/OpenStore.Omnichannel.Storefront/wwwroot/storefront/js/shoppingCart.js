class ShoppingCartClient {

    async addItemToCart(variantId, quantity) {
        const response = await fetch(`/checkout/shoppingCart/items?variantId=${variantId}&quantity=${quantity}`, {
            method: 'POST',
        })

        if (response.ok === false) {
            return false;
        }

        return await response.text();
    }

    async removeItemFromCart(itemId) {
        const response = await fetch(`/checkout/shoppingCart/items/${itemId}`, {
            method: 'DELETE',
        });
        return response.ok;
    }

    async changeItemQuantityOfCart(itemId, quantity) {
        const response = await fetch(`/checkout/shoppingCart/items/${itemId}?quantity=${quantity}`, {
            method: 'POST',
        });
        return response.ok;
    }
}

class ShoppingCart {

    constructor() {
        this.shoppingCartClient = new ShoppingCartClient();
    }

    _updateBadge(val) {
        const cartBadge = $('#flyout-cart-notify-badge');
        cartBadge.text(parseInt(cartBadge.text()) + val);
        cartBadge.delay(100).fadeOut().fadeIn('slow');
    }

    async addItem(variantId, quantity) {
        const result = await this.shoppingCartClient.addItemToCart(variantId, quantity);
        if (result !== false) {
            this._updateBadge(1);
        }
        return result;
    }

    async changeItemQuantity(itemId, quantity) {
        return await this.shoppingCartClient.changeItemQuantityOfCart(itemId, quantity);
    }

    async removeItem(itemId) {
        const result =  await this.shoppingCartClient.removeItemFromCart(itemId);
        if (result !== false) {
            this._updateBadge(-1);
        }
        return result;
    }
}


