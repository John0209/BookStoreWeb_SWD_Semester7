document.addEventListener("DOMContentLoaded", function() {
    let products = document.querySelector("#bestSeller .pro-container");
    let bookData = [];

    async function fetchBestSellers(url) {
        try {
            const response = await fetch(url);
            const jsonData = await response.json();
            bookData = jsonData.filter(book => book.category_Name === "Novel");
            console.log(bookData);
            renderBestSellers(bookData.slice(0, 4)); // Display only the first 4 best sellers in the Truyện tranh category
        } catch (error) {
            console.error("Error fetching data:", error);
        }
    }

    function renderBestSellers(data) {
        // Clear existing products
        products.innerHTML = "";

        // Loop through the data and create product elements
        for (let i = 0; i < data.length; i++) {
            const book = data[i];
            const productId = book.book_Id;

            const product = document.createElement("div");
            product.classList.add("pro");

            const image = document.createElement("img");
            image.src = book.image_URL;
            image.alt = book.book_Title;
            addOnclickRedirectToProduct(image, productId);

            const des = document.createElement("div");
            des.classList.add("des");
            addOnclickRedirectToProduct(des, productId);

            const category = document.createElement("span");
            category.classList.add("bookCategory");
            category.textContent = book.category_Name;

            const title = document.createElement("h4");
            title.classList.add("bookTitle");
            title.textContent = book.book_Title;
            addOnclickRedirectToProduct(title, productId);

            const star = document.createElement("div");
            star.classList.add("star");
            star.innerHTML = `
          <i class="fas fa-star"></i>
          <i class="fas fa-star"></i>
          <i class="fas fa-star"></i>
          <i class="fas fa-star"></i>
          <i class="fas fa-star"></i>
        `;

            const price = document.createElement("h4");
            price.classList.add("bookPrice");
            price.textContent = `$${book.book_Price}`;

            const cartBtn = document.createElement("button");
            const cartIcon = document.createElement("i");
            cartIcon.classList.add("fal", "fa-shopping-cart", "cart");
            cartBtn.appendChild(cartIcon);
            cartBtn.addEventListener("click", function() {
                handleCart(book);
            });

            des.appendChild(category);
            des.appendChild(title);
            des.appendChild(star);
            des.appendChild(price);

            product.appendChild(image);
            product.appendChild(des);
            product.appendChild(cartBtn);

            products.appendChild(product);
        }
    }

    function addOnclickRedirectToProduct(element, productId) {
        element.addEventListener("click", function() {
            redirectToProduct(productId);
        });
    }

    function redirectToProduct(productId) {
        window.location.href = `sproduct.html?productId=${productId}`;
    }
    fetchBestSellers("https://book0209.azurewebsites.net/api/book/getBook");
});