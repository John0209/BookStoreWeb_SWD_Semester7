// Lấy bookId từ query parameter trong URL
const queryString = window.location.search;
const urlParams = new URLSearchParams(queryString);
const bookId = urlParams.get("bookId");

// Lấy các phần tử DOM
const imageUrlInput = document.getElementById("image-url");
const bookTitleInput = document.getElementById("book-title");
const bookAuthorInput = document.getElementById("book-author");
const bookDescriptionInput = document.getElementById("book-description");
const bookPriceInput = document.getElementById("book-price");
const bookQuantityInput = document.getElementById("book-quantity");
const bookYearPublicInput = document.getElementById("book-year-public");

function init() {
  const data = localStorage.getItem("user");
  const user = JSON.parse(data);
  console.log("user: ", user);

  if (user.role_Id == 1) {
    document.getElementById("censored").style.bottom = null;
    document.getElementById("disable").classList.remove("disable");
  }

  if (user.role_Id == 2) {
    document.getElementById("staff").href = "bookRequestStaff.html";
  }
}

init();

// Gửi request lấy thông tin sách từ API
function fetchBookDetails() {
  fetch(
    `https://book0209.azurewebsites.net/api/book/getBookDetail?bookId=${bookId}`
  )
    .then((response) => response.json())
    .then((book) => {
      // Lưu trữ các giá trị từ book vào các biến tạm
      const tempBookId = book.book_Id;
      const tempCategoryId = book.category_Id;
      const tempCategoryName = book.category_Name;
      const tempBookISBN = book.book_ISBN;
      const tempBookStatus = book.is_Book_Status;

      // Đổ dữ liệu lên các trường nhập liệu
      const bookImage = document.getElementById("image-url");
      // Sử dụng chỉ số 0 để lấy ảnh đầu tiên trong mảng image_URL
      bookImage.src = book.image_URL[0];
      bookTitleInput.value = book.book_Title;
      bookAuthorInput.value = book.book_Author;
      bookDescriptionInput.value = book.book_Description;
      bookPriceInput.value = book.book_Price;
      bookQuantityInput.value = book.book_Quantity;
      bookYearPublicInput.value = book.book_Year_Public;

      // Đặt sự kiện click cho nút "Cập nhật"
      const saveButton = document.getElementById("save-button");
      saveButton.addEventListener("click", function () {
        // Tiến hành cập nhật sách khi người dùng nhấn nút "Save"
        updateBook(
          tempBookId,
          tempCategoryId,
          tempCategoryName,
          tempBookISBN,
          tempBookStatus
        );
      });
    })
    .catch((error) => {
      console.error("Error fetching book:", error);
    });
}

function updateBook(
  tempBookId,
  tempCategoryId,
  tempCategoryName,
  tempBookISBN,
  tempBookStatus
) {
  // Lấy giá trị từ các trường nhập liệu
  const updatedBook = {
    book_Id: tempBookId,
    category_Id: tempCategoryId,
    category_Name: tempCategoryName,
    image_URL: [imageUrlInput.value],
    book_Title: bookTitleInput.value,
    book_Author: bookAuthorInput.value,
    book_Description: bookDescriptionInput.value,
    book_Price: bookPriceInput.value,
    book_Quantity: bookQuantityInput.value,
    book_Year_Public: bookYearPublicInput.value,
    book_ISBN: tempBookISBN,
    is_Book_Status: tempBookStatus,
  };

  // Kiểm tra các trường hợp lỗi
  if (isNaN(updatedBook.book_Price) || updatedBook.book_Price <= 0) {
    alert("Please enter a valid book price.");
    return;
  }

  if (isNaN(updatedBook.book_Quantity) || updatedBook.book_Quantity <= 0) {
    alert("Please enter a valid book quantity.");
    return;
  }

  if (
    isNaN(updatedBook.book_Year_Public) ||
    updatedBook.book_Year_Public <= 0 ||
    updatedBook.book_Year_Public > 2023
  ) {
    // Năm xuất bản không hợp lệ
    alert("Please enter a valid publication year");
    return;
  }

  // Gửi yêu cầu cập nhật sách lên API
  fetch(`https://book0209.azurewebsites.net/api/book/updateBook`, {
    method: "PUT",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(updatedBook),
  })
    .then((response) => {
      if (response.ok) {
        Swal.fire({
          icon: "success",
          title: "Update Successfully",
        }).then(() => {
          window.location.href = "productlist.html";
        });
      } else {
        console.error(
          "Error updating book:",
          response.status,
          response.statusText
        );
      }
    })
    .catch((error) => {
      console.error("Error updating book:", error);
    });
}

function deleteBook() {
  const deleteModal = new bootstrap.Modal(
    document.getElementById("deleteModal")
  );
  deleteModal.show();
}

// Xác nhận xóa sách
function confirmDelete() {
  fetch(
    `https://book0209.azurewebsites.net/api/book/deleteBook?bookId=${bookId}`,
    {
      method: "PATCH",
    }
  )
    .then((response) => {
      if (response.ok) {
        Swal.fire({
          icon: "success",
          title: "Deleted Successfully",
          text: "The book has been deleted successfully.",
        }).then(() => {
          window.location.href = "productlist.html";
        });
      } else {
        console.error(
          "Error deleting order:",
          response.status,
          response.statusText
        );
      }
    })
    .catch((error) => {
      console.error("Error deleting order:", error);
    });
}

fetchBookDetails();
