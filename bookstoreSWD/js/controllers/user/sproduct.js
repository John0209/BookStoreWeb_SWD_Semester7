let globalBook = null;
document.addEventListener("DOMContentLoaded", function () {
  const mainImg = document.getElementById("MainImg");
  const smallImg = document.querySelector(".small-img");
  const bookTitle = document.querySelector(".bookTitle");
  const bookPrice = document.querySelector(".bookPrice");
  const bookDetail = document.querySelector(".bookDetail");
  const cartCount = document.querySelector("#cart-count");
  const addToCartBtn = document.querySelector("#addToCartBtn");

  // Get the book ID from the URL
  const urlParams = new URLSearchParams(window.location.search);
  const bookId = urlParams.get("productId");

  // Fetch the book details from the server
  async function fetchBookDetail() {
    try {
      const response = await fetch(
        `https://book0209.azurewebsites.net/api/book/getBookDetail?bookId=${bookId}`
      );
      const book = await response.json();
      globalBook = book;

      mainImg.src = book.image_URL[0];
      smallImg.src = book.image_URL[1];
      bookTitle.textContent = book.book_Title;
      bookPrice.textContent = `VND ${book.book_Price}`;
      bookDetail.textContent = book.book_Description;
    } catch (error) {
      console.error("Error fetching data:", error);
    }
  }

  fetchBookDetail();
  addToCartBtn.addEventListener("click", function () {
    handleCart(globalBook, cartCount.value * 1, "add");
  });
  let handleCart = (book, amount) => {
    let cartsCached = JSON.parse(localStorage.getItem('cart'));
    let model = {
      ...book,
      quantity: amount,
    }
    if(cartsCached){
      let bookTemp = {};
      bookTemp = cartsCached.find((bookCart) => book.book_Id === bookCart.book_Id);
      if(bookTemp){
        cartsCached.find((bookCart) => book.book_Id === bookCart.book_Id).quantity += amount;
      }else{
        cartsCached.push(model);
      }
      console.log(cartsCached);
    }else{
      cartsCached = [model]
    }
    localStorage.setItem('cart', JSON.stringify(cartsCached));
  }
});

