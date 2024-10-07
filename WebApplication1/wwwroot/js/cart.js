function addToCart(productId, element) {
    debugger;
    var uniqueKey = productId;
    let cart = JSON.parse(localStorage.getItem('cart')) || {
        count: 0,
        total: 0,
        items: {},
    };
    let productDetails = {};
    if ($(element).closest('.product-cart-wrap').length > 0) {
        let productElement = $(element).closest('.product-cart-wrap');
        productDetails = {
            product: {
                product_id: productId,
                name: productElement.find('.productname').text(),
                image_url: productElement.find('.productImage').attr('src'),
                price: parseFloat(productElement.find('.product-price span').text().replace('₹', '')),
                unit: productElement.find('.unit').text(),
                varientId : '0'
            },
            quantity: 1
        };
    }
    else if ($(element).closest('.card.variants').length > 0) {
        let cardElement = $(element).closest('.card.variants');
        productDetails = {
            product: {
                product_id: productId,
                name: $('#variantModal .modal-title').text(),
                image_url: cardElement.find('.varient-image img').attr('src'),
                unit: cardElement.find('.varient-unit').text().trim(),
                price: parseFloat(cardElement.find('.varient-price span').first().text().replace('₹', '').trim()),
                varientId: cardElement.find('#varientid').val()
            },
            quantity: 1
        };
        uniqueKey = productId + 'V' + productDetails.product.varientId;
    }

    else if ($(element).closest('.productDetail').length > 0) {
        let modalElement = $(element).closest('.productDetail');
        productDetails = {
            product: {
                product_id: productId,
                name: modalElement.find('.title-detail').text().trim(),
                image_url: modalElement.find('.carousel-item.active img').attr('src'),
                price: parseFloat(modalElement.find('.current-price').text().replace('₹', '')),
                unit: modalElement.find('#unit').val(),
            },
            quantity: parseInt(modalElement.find('.qty-val').text())
        };
    }

    if (cart.items[uniqueKey]) {
        cart.items[uniqueKey].quantity += productDetails.quantity;
    } else {
        cart.items[uniqueKey] = productDetails;
    }

    cart.count += productDetails.quantity;
    cart.total += productDetails.product.price * productDetails.quantity;

    localStorage.setItem('cart', JSON.stringify(cart));
    $(element).addClass('d-none');
    $(`#quantity-buttons-${productId}`).removeClass('d-none');
    $(`#quantity-${productId}`).text(cart.items[uniqueKey].quantity);
    QAlert(1, 'Product added to cart successfully!');
}

function initializeCart() {
    debugger
    let cart = JSON.parse(localStorage.getItem('cart')) || { items: {} };
    for (let productId in cart.items) {
        if (cart.items.hasOwnProperty(productId)) {
            $(`#addToCart-${productId}`).addClass('d-none');
            $(`#quantity-buttons-${productId}`).removeClass('d-none');
            $(`#quantity-${productId}`).text(cart.items[productId].quantity);
        }
    }
}

function updateQuantity(productId, change) {
    debugger    
    let cart = JSON.parse(localStorage.getItem('cart'));
    if (cart && cart.items[productId]) {
        cart.items[productId].quantity += change;
        if (cart.items[productId].quantity <= 0) {
            delete cart.items[productId];
            cart.count -= 1;
            $(`#quantity-buttons-${productId}`).addClass('d-none');
            $(`#addToCart-${productId}`).removeClass('d-none');
        }
        else {
            $(`#quantity-${productId}`).text(cart.items[productId].quantity);
            cart.count += change
            if (change == 1) {
                cart.total += cart.items[productId].product.price
            } else {
                cart.total -= cart.items[productId].product.price
            }
        }
        localStorage.setItem('cart', JSON.stringify(cart));
    }
}

$(document).ready(initializeCart);
