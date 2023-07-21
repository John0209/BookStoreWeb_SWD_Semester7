document.addEventListener('DOMContentLoaded', function() {
    const relatedProductContainer = document.querySelector('#product1 .pro-container');

    // Get the product ID from the URL
    const urlParams = new URLSearchParams(window.location.search);
    const productId = urlParams.get('productId');

    // Fetch the product details from the server
    async function fetchProductDetail() {
        try {
            const response = await fetch(
                `https://book0209.azurewebsites.net/api/book/getBookDetail?bookId=${productId}`
            );
            const product = await response.json();

            displayRelatedProducts(product.category_Id, product.book_Id);
        } catch (error) {
            console.error('Error fetching data:', error);
        }
    }

    // Fetch related products based on category, excluding the current product
    async function displayRelatedProducts(categoryId, currentProductId) {
        try {
            const response = await fetch(
                `https://book0209.azurewebsites.net/api/book/getBookByCategory?categoryId=${categoryId}`
            );
            const products = await response.json();

            for (let i = 0; i < products.length; i++) {
                const product = products[i];
                const productId = product.book_Id;

                // Skip the current product
                if (productId === currentProductId) {
                    continue;
                }

                const productElement = document.createElement('div');
                productElement.classList.add('pro');
                productElement.addEventListener('click', function() {
                    redirectToProduct(productId);
                });

                const image = document.createElement('img');
                image.src = product.image_URL;
                image.alt = product.book_Title;

                const des = document.createElement('div');
                des.classList.add('des');

                const category = document.createElement('span');
                category.classList.add('bookCategory');
                category.textContent = product.category_Name;

                const title = document.createElement('h4');
                title.classList.add('bookTitle');
                title.textContent = product.book_Title;

                const star = document.createElement('div');
                star.classList.add('star');
                star.innerHTML = `
            <i class="fas fa-star"></i>
            <i class="fas fa-star"></i>
            <i class="fas fa-star"></i>
            <i class="fas fa-star"></i>
            <i class="fas fa-star"></i>
          `;

                const price = document.createElement('h4');
                price.classList.add('bookPrice');
                price.textContent = `VND ${product.book_Price}`;

                const cartLink = document.createElement('a');
                cartLink.href = '#';
                const cartIcon = document.createElement('i');
                cartIcon.classList.add('fal', 'fa-shopping-cart', 'cart');
                cartLink.appendChild(cartIcon);

                des.appendChild(category);
                des.appendChild(title);
                des.appendChild(star);
                des.appendChild(price);

                productElement.appendChild(image);
                productElement.appendChild(des);
                productElement.appendChild(cartLink);

                relatedProductContainer.appendChild(productElement);
            }
        } catch (error) {
            console.error('Error fetching related products:', error);
        }
    }

    function redirectToProduct(productId) {
        window.location.href = `sproduct.html?productId=${productId}`;
    }

    fetchProductDetail();
});