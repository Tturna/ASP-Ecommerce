// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

document.addEventListener('DOMContentLoaded', () => {
    fetch('/ShoppingCart/GetCart')
        .then(response => response.text())
        .then(data => {
            document.getElementById('shoppingCart').innerHTML = data;
        })
        .catch(error => console.error('Error:', error));
});

document.addEventListener('click', (e) => {
    const cart = document.getElementById('shoppingCart');
    
    if (!cart.contains(e.target) && e.target.id !== 'cartButton') {
        cart.style.display = 'none';
    }
});

const toggleCart = () => {
    const cart = document.getElementById('shoppingCart');
    cart.style.display = cart.style.display === 'none' ? 'block' : 'none';
};

const addItemToCart = (id) => {
    fetch(`/ShoppingCart/Add/${id}`)
        .then(response => response.text())
        .then(data => {
            console.log(data);
            document.getElementById('shoppingCart').innerHTML = data;
        })
        .catch(error => console.error('Error:', error));
};

const removeItemFromCart = (id) => {
    fetch(`/ShoppingCart/Remove/${id}`)
        .then(response => response.text())
        .then(data => {
            document.getElementById('shoppingCart').innerHTML = data;
        })
        .catch(error => console.error('Error:', error));
};

const removeItemsFromCart = (id) => {
    fetch(`/ShoppingCart/RemoveAll/${id}`)
        .then(response => response.text())
        .then(data => {
            document.getElementById('shoppingCart').innerHTML = data;
        })
        .catch(error => console.error('Error:', error))
}

const emptyCart = () => {
    fetch('/ShoppingCart/EmptyCart')
        .then(response => response.text())
        .then(data => {
            document.getElementById('shoppingCart').innerHTML = data;
        })
        .catch(error => console.error('Error:', error));
}