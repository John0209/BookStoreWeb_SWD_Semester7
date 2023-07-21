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
function fetchBooks() {
  fetch("https://book0209.azurewebsites.net/api/book/getBook")
    .then((response) => response.json())
    .then((data) => {
      const tableBody = document.getElementById("book-table-body");
      tableBody.innerHTML = "";
      let foundBooks = false;
      let count = 0;

      data.forEach((book) => {
        if (book.is_Book_Status === true) {
          foundBooks = true;
          count++;

          const row = document.createElement("tr");
          const sttCell = document.createElement("td");
          const imgCell = document.createElement("td");
          const titleCell = document.createElement("td");
          const priceCell = document.createElement("td");
          const quantityCell = document.createElement("td");
          const editCell = document.createElement("td");
          const editIcon = document.createElement("i");

          sttCell.textContent = count;
          titleCell.textContent = book.book_Title;
          titleCell.style.cursor = "pointer"; // Áp dụng CSS style cursor pointer

          titleCell.addEventListener("click", () => {
            window.location.href = `admindetail.html?bookId=${book.book_Id}`;
          });
          if (book.image_URL) {
            const image = document.createElement("img");
            image.src = book.image_URL;
            image.alt = "Book Image";
            image.style.width = "100px";
            image.style.height = "120px";
            imgCell.appendChild(image);
          } else {
            imgCell.textContent = "No Image";
          }

          // Tạo biểu tượng chỉnh sửa
          editIcon.classList.add("far", "fa-edit");
          editIcon.addEventListener("click", () => {
            window.location.href = `adminedit.html?bookId=${book.book_Id}`;
          });
          editCell.appendChild(editIcon);

          priceCell.textContent = book.book_Price;
          // quantityCell.textContent = book.book_Quantity;

          row.appendChild(sttCell);
          row.appendChild(imgCell);
          row.appendChild(titleCell);
          row.appendChild(priceCell);
          row.appendChild(quantityCell);
          row.appendChild(editCell);

          sttCell.classList.add("bold-column");
          titleCell.classList.add("bold-column");
          priceCell.classList.add("bold-column");

          const quantityContent = document.createElement("div");
          quantityContent.textContent = book.book_Quantity;
          quantityContent.classList.add("quantity-column");
          quantityCell.appendChild(quantityContent);

          tableBody.appendChild(row);
        }
      });
      if (!foundBooks) {
        // Nếu không có cuốn sách nào được tìm thấy, hiển thị thông báo
        const noDataRow = document.createElement("tr");
        const noDataCell = document.createElement("td");
        noDataCell.textContent = "No data available in table";
        noDataCell.colSpan = 8;
        noDataCell.classList.add("no-data-message");
        noDataRow.appendChild(noDataCell);
        tableBody.appendChild(noDataRow);
      }
    })
    .catch((error) => {
      console.error("Error fetching books:", error);
    });
}
function searchBooks() {
  const searchInput = document.getElementById("search-input");
  const searchQuery = searchInput.value;
  let count = 0; // Biến đếm số thứ tự

  fetch(
    `https://book0209.azurewebsites.net/api/book/searchBook?nameBook=${searchQuery}`
  )
    .then((response) => response.json())
    .then((data) => {
      const tableBody = document.getElementById("book-table-body");
      tableBody.innerHTML = ""; // Xóa dữ liệu trước đó trên bảng

      data.forEach((book) => {
        if (book.is_Book_Status === true) {
          count++; // Tăng biến đếm số thứ tự

          const row = document.createElement("tr");
          const sttCell = document.createElement("td");
          const imgCell = document.createElement("td");
          const titleCell = document.createElement("td");
          const priceCell = document.createElement("td");
          const quantityCell = document.createElement("td");

          sttCell.textContent = count;

          // Tạo biểu tượng chỉnh sửa
          const editIcon = document.createElement("i");
          editIcon.classList.add("far", "fa-edit");
          editIcon.addEventListener("click", () => {
            window.location.href = `adminedit.html?bookId=${book.book_Id}`;
          });
          const editCell = document.createElement("td");
          editCell.appendChild(editIcon);

          titleCell.textContent = book.book_Title;
          titleCell.style.cursor = "pointer"; // Áp dụng CSS style cursor pointer
          titleCell.addEventListener("click", () => {
            window.location.href = `admindetail.html?bookId=${book.book_Id}`;
          });

          if (book.image_URL) {
            const image = document.createElement("img");
            image.src = book.image_URL;
            image.alt = "Book Image";
            image.style.width = "100px";
            image.style.height = "120px";
            imgCell.appendChild(image);
          } else {
            imgCell.textContent = "No Image";
          }

          priceCell.textContent = book.book_Price;
          // quantityCell.textContent = book.book_Quantity;

          row.appendChild(sttCell);
          row.appendChild(imgCell);
          row.appendChild(titleCell);
          row.appendChild(priceCell);
          row.appendChild(quantityCell);
          row.appendChild(editCell);

          sttCell.classList.add("bold-column");
          titleCell.classList.add("bold-column");
          priceCell.classList.add("bold-column");

          const quantityContent = document.createElement("div");
          quantityContent.textContent = book.book_Quantity;
          quantityContent.classList.add("quantity-column");
          quantityCell.appendChild(quantityContent);

          tableBody.appendChild(row);
        }
      });
    })
    .catch((error) => {
      console.error("Error searching books:", error);
      const tableBody = document.getElementById("book-table-body");
      tableBody.innerHTML = "";

      const errorRow = document.createElement("tr");
      const errorCell = document.createElement("td");
      // errorCell.textContent = "No results found";
      errorCell.textContent = `${searchInput.value} doesn't exist`;
      errorCell.colSpan = 8;
      errorCell.classList.add("error-message");
      errorRow.appendChild(errorCell);
      tableBody.appendChild(errorRow);
    });
}

fetchBooks();
