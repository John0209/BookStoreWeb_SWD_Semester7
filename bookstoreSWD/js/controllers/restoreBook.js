function fetchBooks() {
  fetch("https://book0209.azurewebsites.net/api/book/getBook")
    .then((response) => response.json())
    .then((data) => {
      const tableBody = document.getElementById("book-table-body");
      tableBody.innerHTML = "";
      let foundBooks = false;
      let count = 0;

      data.forEach((book) => {
        if (book.is_Book_Status === false) {
          foundBooks = true;
          count++;

          const row = document.createElement("tr");
          const sttCell = document.createElement("td");
          const imgCell = document.createElement("td");
          const titleCell = document.createElement("td");
          const priceCell = document.createElement("td");
          const quantityCell = document.createElement("td");
          const restoreCell = document.createElement("td");
          const restoreIcon = document.createElement("i");

          sttCell.textContent = count;
          titleCell.textContent = book.book_Title;

          restoreCell.style.cursor = "pointer";
          // titleCell.style.cursor = "pointer";
          // titleCell.addEventListener("click", () => {
          //   window.location.href = `admindetail.html?bookId=${book.book_Id}`;
          // });
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

          // restoreIcon.addEventListener("click", () => {
          //   const bookId = book.book_Id; // Lấy bookId từ đối tượng book
          //   restoreBook(bookId); // Gọi hàm restoreBook với bookId
          // });
          restoreIcon.classList.add("fas", "fa-undo");
          restoreIcon.onclick = () => restoreBook(book.book_Id);

          restoreCell.appendChild(restoreIcon);

          priceCell.textContent = book.book_Price;
          // quantityCell.textContent = book.book_Quantity;

          row.appendChild(sttCell);
          row.appendChild(imgCell);
          row.appendChild(titleCell);
          row.appendChild(priceCell);
          row.appendChild(quantityCell);
          row.appendChild(restoreCell);

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

function restoreBook(bookId) {
  const restoreModal = new bootstrap.Modal(
    document.getElementById("restoreModal")
  );
  restoreModal.show();

  const confirmButton = document.getElementById("confirmRestore");
  confirmButton.onclick = () => confirmRestore(bookId);
}

function confirmRestore(bookId) {
  fetch(
    `https://book0209.azurewebsites.net/api/book/restoreBook?bookId=${bookId}`,
    {
      method: "PATCH",
    }
  )
    .then((response) => {
      if (response.ok) {
        fetchBooks();
        Swal.fire({
          icon: "success",
          title: "Restore Successfully",
          text: "Data has been restore successfully.",
        }).then(() => {
          window.location.href = "/html/restoreBook.html";
        });
      } else {
        console.error("Error retstore:", response.status, response.statusText);
      }
    })
    .catch((error) => {
      console.error("Error restore:", error);
    });
}

fetchBooks();
