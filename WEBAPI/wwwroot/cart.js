$('.addtoCart').on('click', function () {
    let product = {
        ProductImage: $('.productImage').attr('src'),
        ProductName: $('.productname').text()
    };
})

function addToCart(productId, button) {
    // Find the closest product card to get the selected variant
    const productCard = button.closest('.product-cart-wrap');
    const variantSelect = productCard.querySelector('.variantSelect');

    let variantId, quantity, unitType, price, description, img, name;

    if (variantSelect) {
        variantId = variantSelect.value;
        const selectedOption = variantSelect.options[variantSelect.selectedIndex];
        quantity = selectedOption.getAttribute('data-quantity');
        unitType = selectedOption.getAttribute('data-unittype');
        price = selectedOption.getAttribute('data-price');
        description = selectedOption.getAttribute('data-description');
        img = selectedOption.getAttribute('data-img');
        name = selectedOption.getAttribute('data-name');
    } else {
        // If no variants, use product default values
        const defaultImg = productCard.querySelector('.productImage').src;
        const productName = productCard.querySelector('.productname').innerText;
        price = productCard.querySelector('.product-price span').innerText.replace('₹', '').trim(); // Get price without currency symbol
        img = defaultImg;
        name = productName;
        quantity = 1; // Default quantity if no variant is selected
    }

    // Create product object
    const product = {
        productId: productId,
        variantId: variantId || null,
        quantity: quantity,
        unitType: unitType || '',
        price: price,
        description: description || '',
        img: img,
        name: name
    };

    // Get cart from local storage or initialize it
    let cart = JSON.parse(localStorage.getItem('cart')) || [];

    // Check if the product is already in the cart
    const existingProductIndex = cart.findIndex(item => item.productId === productId && item.variantId === variantId);

    if (existingProductIndex > -1) {
        // If it exists, update the quantity
        cart[existingProductIndex].quantity = parseInt(cart[existingProductIndex].quantity) + parseInt(quantity);
    } else {
        // If it doesn't exist, add the product
        cart.push(product);
    }

    // Save cart back to local storage
    localStorage.setItem('cart', JSON.stringify(cart));

    alert('Product added to cart!');
}