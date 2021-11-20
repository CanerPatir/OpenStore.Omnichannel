class ShoppingCart {
    constructor() {
        this.shoppingCartClient = new ShoppingCartClient();
    }

    async addItem(variantId, quantity) {
        const cartIdStorageKey = "__cartId";
        let cartId = sessionStorage.getItem(cartIdStorageKey)
        if (cartId === undefined) {
            cartId = await this.shoppingCartClient.createShoppingCart();
            sessionStorage.setItem(cartIdStorageKey, cartId);
        }
        return await this.shoppingCartClient.addItemToCart(cartId, variantId, quantity);
    }

    async changeItemQuantity(variantId, quantity) {
        const cartIdStorageKey = "__cartId";
        let cartId = sessionStorage.getItem(cartIdStorageKey)

        await this.shoppingCartClient.changeItemQuantityOfCart(cartId, variantId, quantity);
    }
}

class ShoppingCartClient {

    async createShoppingCart() {
        const response = await fetch(`shoppingCart`, {
            method: 'POST',
        });

        return await response.text();
    }

    async addItemToCart(cartId, variantId, quantity) {
        const response = await fetch(`shoppingCart/${cartId}/items?variantId=${variantId}&quantity=${quantity}`, {
            method: 'POST',
        });

        return await response.text();
    }

    async removeItemFromCart(cartId, itemId) {
        await fetch(`shoppingCart/${cartId}/items/${itemId}`, {
            method: 'DELETE',
        });
    }

    async changeItemQuantityOfCart(cartId, itemId, quantity) {
        await fetch(`shoppingCart/${cartId}/items/${itemId}?quantity=${quantity}`, {
            method: 'POST',
        });
    }

    async bindCartToUser(cartId, userId) {
        await fetch(`shoppingCart/${cartId}/bind-to-user//${userId}`, {
            method: 'POST',
        });
    }
}

