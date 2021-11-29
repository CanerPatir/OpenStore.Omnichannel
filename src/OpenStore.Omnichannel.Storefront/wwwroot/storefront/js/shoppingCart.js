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

    async addItem(variantId, quantity) {
        return await this.shoppingCartClient.addItemToCart(variantId, quantity);
    }

    async changeItemQuantity(itemId, quantity) {
        return await this.shoppingCartClient.changeItemQuantityOfCart(itemId, quantity);
    }

    async removeItem(variantId, itemId) {
        return await this.shoppingCartClient.removeItemFromCart(itemId);
    }
}


