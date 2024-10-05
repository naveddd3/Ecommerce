function addToCart(productId, element) {
    debugger;
    let cart = JSON.parse(localStorage.getItem('cart')) || [];
    let productDetails = {};
    if ($(element).closest('.product-cart-wrap').length > 0) {
        let productElement = $(element).closest('.product-cart-wrap');
        productDetails = { 
            productid: productId,
            name: productElement.find('.productname').text(),
            image: productElement.find('.productImage').attr('src'),
            price: parseFloat(productElement.find('.product-price span').text().replace('₹', '')),
            quantity: productElement.find('.unit').text()
        };
        let variantSelect = productElement.find('.variantSelect');
        if (variantSelect.length > 0) {
            let selectedVariant = variantSelect.find('option:selected');
            productDetails.quantity = parseInt(selectedVariant.attr('data-quantity'));
            productDetails.unitType = selectedVariant.attr('data-unittype');
        }
    } else if ($(element).closest('tr').length > 0) {
        let rowElement = $(element).closest('tr');
        productDetails = {
            productid: productId,
            name: rowElement.find('td:nth-child(2)').text(),
            image: rowElement.find('td img').attr('src'),
            price: parseFloat(rowElement.find('td:nth-child(3)').text().replace('₹', '')),
            quantity: 1
        };
    }
    else if ($(element).closest('.modal-body').length > 0) {
        let modalElement = $(element).closest('.modal-body');
        productDetails = {
            productid: productId,
            name: modalElement.find('.title-detail').text(),
            image: modalElement.find('.carousel-item.active img').attr('src'),
            price: parseFloat(modalElement.find('.current-price').text().replace('$', '')),
            quantity: parseInt(modalElement.find('.qty-val').text())
        };
    } 
    let existingProductIndex = cart.findIndex(item => item.id == productId);
    if (existingProductIndex > -1) {
        cart[existingProductIndex].quantity += productDetails.quantity;
    } else {
        cart.push(productDetails);
    }
    localStorage.setItem('cart', JSON.stringify(cart));
    QAlert(1,'Product added to cart successfully!');
}