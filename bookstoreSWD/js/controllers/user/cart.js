document.addEventListener("DOMContentLoaded", function() {
    const SHIPPING_FEE = 0;
    let cartBody = document.querySelector("#cart tbody");
    let subTotal = document.querySelector("#cart-subtotal");
    let shippingFee = document.querySelector("#shipping-fee");
    let total = document.querySelector("#cart-total");
    renderCart();

    // renderCart function will render the cart items in localStorage to cartBody
    function renderCart() {
        let cart = localStorage.getItem("cart");
        if (!cart) {
            cart = [];
        } else {
            cart = JSON.parse(cart);
        }
        const money = cart.reduce(
            (total, val) => (total += val.book_Price * val.quantity),
            0
        );
        subTotal.innerHTML = money;
        shippingFee.innerHTML = SHIPPING_FEE || "Free";
        total.innerHTML = SHIPPING_FEE ? money + SHIPPING_FEE : money;
        cartBody.innerHTML = "";
        for (let i = 0; i < cart.length; i++) {
            const book = cart[i];
            const product = document.createElement("tr");
            product.innerHTML = `
            <td>
                <img src=${book.image_URL} alt=${book.book_Title} />
            </td>
                <td>${book.book_Title}</td>
            <td>${book.book_Price}</td>
            <td>
                <button class="subtractBtn">
                    <i class="fa fa-minus"></i>
                </button>
                ${book.quantity}
                <button class="addBtn">
                    <i class="fa fa-plus"></i>
                </button></td>
            <td>${book.book_Price * book.quantity}</td>
            <td>
                <button class="removeBtn">
                    <i class="far fa-times" onclick="removeBookFromCart('${book.book_Id}')"></i>
                </button>
            </td>
            `;
            cartBody.appendChild(product);
        }

        let addBtns = document.querySelectorAll(".addBtn");
        addBtns.forEach((btn, index) => {
            if (btn) {
                btn.addEventListener("click", function(event) {
                    handleCart(cart[index], 1, "add");
                    renderCart();
                });
            }
        });
        let subtractBtns = document.querySelectorAll(".subtractBtn");
        subtractBtns.forEach((btn, index) => {
            if (btn) {
                btn.addEventListener("click", function(event) {
                    handleCart(cart[index], 1, "subtract");
                    renderCart();
                });
            }
        });
        let removeBtns = document.querySelectorAll(".removeBtn");
        removeBtns.forEach((btn, index) => {
            if (btn) {
                btn.addEventListener("click", function(event) {
                    handleCart(cart[index], null, "remove");
                    renderCart();
                });
            }
        });
    }
});
let paymentHandler = () => {
    let accountCached = JSON.parse(localStorage.getItem("account"));
    if(accountCached){
      window.location.href = "/html/checkout.html";
    }else{
      if(confirm("You are not logged in! Click to log in.")){
        window.location.href = "ui/login.html";
      }
    }
}
let removeBookFromCart = (bookId) => {
    booksInCart = booksInCart.filter(book => book.book_Id !== bookId)
    localStorage.setItem('cart', JSON.stringify(booksInCart));
    location.reload();
}