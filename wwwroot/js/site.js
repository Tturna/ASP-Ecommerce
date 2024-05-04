// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

document.addEventListener('DOMContentLoaded', () => {
    let cartDictJson = window.localStorage.getItem('cartDict');
    
    fetch('/ShoppingCart/GetCart', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(cartDictJson)
    })
        .then(response => response.text())
        .then(data => {
            document.getElementById('shoppingCart').innerHTML = data;
        })
        .catch(error => console.error('Error:', error));
});

let cart = document.getElementById('shoppingCart');
let user_dropdown = document.getElementById('user-dropdown');

document.addEventListener('click', (e) => {
    if (e.target.id === 'logoutButton') {
        window.localStorage.removeItem('cartDict');
    }
    
    if (!cart.contains(e.target) && e.target.id !== 'cartButton') {
        cart.style.display = 'none';
    }
    
    if (!user_dropdown.contains(e.target) && e.target.id !== 'user-name') {
        user_dropdown.style.display = 'none';
    }
});

document.getElementById('user-name').addEventListener('click', (e) => {
    e.preventDefault();
    user_dropdown.style.display = user_dropdown.style.display === 'none' ? 'block' : 'none';
});

const toggleCart = () => {
    const cart = document.getElementById('shoppingCart');
    cart.style.display = cart.style.display === 'none' ? 'block' : 'none';
};

const addItemToCart = (id) => {
    let cartDictJson = window.localStorage.getItem('cartDict');
    
    if (cartDictJson === null)
    {
        cartDictJson = '{}';
    }
    
    let cartDict = JSON.parse(cartDictJson);
    
    if (cartDict[id] === undefined)
    {
        cartDict[id] = 1;
    }
    else
    {
        cartDict[id]++;
    }
    
    cartDictJson = JSON.stringify(cartDict);
    window.localStorage.setItem('cartDict', cartDictJson);
    
    fetch(`/ShoppingCart/Add/${id}`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            'ProductId': id,
            'LocalStorageItemDictJson': cartDictJson
        })
    
    })
        .then(response => response.text())
        .then(data => {
            document.getElementById('shoppingCart').innerHTML = data;
        })
        .catch(error => console.error('Error:', error));
};

const removeItemFromCart = (id, quantity = 1) => {
    if (quantity === 0) return;
    
    let cartDictJson = window.localStorage.getItem('cartDict');
    
    if (cartDictJson !== null)
    {
        let cartDict = JSON.parse(cartDictJson);
        
        if (cartDict[id] !== undefined)
        {
            cartDict[id] -= quantity;
            
            if (quantity < 0 || cartDict[id] <= 0)
            {
                delete cartDict[id];
            }
        
            cartDictJson = JSON.stringify(cartDict);
            window.localStorage.setItem('cartDict', cartDictJson);
        }
    }
    
    fetch(`/ShoppingCart/Remove/${id}`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            ProductId: id,
            Quantity: quantity,
            LocalStorageItemDictJson: cartDictJson
        })
    })
        .then(response => response.text())
        .then(data => {
            document.getElementById('shoppingCart').innerHTML = data;
        })
        .catch(error => console.error('Error:', error));
};

const emptyCart = () => {
    window.localStorage.removeItem('cartDict');
    
    fetch('/ShoppingCart/EmptyCart')
        .then(response => response.text())
        .then(data => {
            document.getElementById('shoppingCart').innerHTML = data;
        })
        .catch(error => console.error('Error:', error));
}