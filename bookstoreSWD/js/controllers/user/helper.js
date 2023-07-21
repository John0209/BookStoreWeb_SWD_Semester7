function handleCart(book, quantity = 1, type = "add") {
  let cart = localStorage.getItem("cart");
  if (cart == null) {
      cart = [];
  } else {
      cart = JSON.parse(cart);
  }
  // increase quantity if book already in cart
  let bookInCart = false;
  for (let i = 0; i < cart.length; i++) {
      if (cart[i].book_Id == book.book_Id) {
          if (type === "add") {
              cart[i].quantity += quantity;
          } else if (type === "subtract") {
              cart[i].quantity -= quantity;
              if (cart[i].quantity <= 0) {
                  cart.splice(i, 1); // Remove the book from cart if quantity is 0 or less
              }
          } else if (type === "remove") {
              cart.splice(i, 1);
          }
          bookInCart = true;
          break;
      }
  }
  // add book to cart if not already in cart
  if (!bookInCart) {
      book.quantity = 1;
      cart.push(book);
  }
  localStorage.setItem("cart", JSON.stringify(cart));
}