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
  fetch("https://book0209.azurewebsites.net/api/inventory/getInventory")
    .then((response) => response.json())
    .then((data) => {
      const tableBody = document.getElementById("book-table-body");
      tableBody.innerHTML = "";

      let foundBooks = false;

      data.forEach((book) => {
        if (book.is_Inventory_Status === true) {
          foundBooks = true;
          const row = document.createElement("tr");
          const indexCell = document.createElement("td");
          const imgCell = document.createElement("td");
          const titleCell = document.createElement("td");
          const quantityCell = document.createElement("td");
          const noteCell = document.createElement("td");
          const dataCell = document.createElement("td");
          const userCell = document.createElement("td");
          const deleteCell = document.createElement("td");

          titleCell.textContent = book.book_Title;
          noteCell.textContent = book.inventory_Note;
          quantityCell.textContent = book.inventory_Quantity;
          userCell.textContent = book.user_Name;
          const dateInto = new Date(book.inventory_Date_Into);
          const formattedDate = dateInto.toLocaleDateString("vi-VN");
          dataCell.textContent = formattedDate;

          const deleteIcon = document.createElement("i");
          deleteIcon.className = "fa fa-trash";
          deleteIcon.onclick = () => deleteBook(book.inventory_Id);

          const imgElement = document.createElement("img");
          imgElement.src = book.image_URL;
          imgElement.width = 100;
          imgElement.height = 120;

          imgCell.appendChild(imgElement);
          deleteCell.appendChild(deleteIcon);

          row.appendChild(indexCell);
          row.appendChild(imgCell);
          row.appendChild(titleCell);
          row.appendChild(quantityCell);
          row.appendChild(noteCell);
          row.appendChild(dataCell);
          row.appendChild(userCell);
          row.appendChild(deleteCell);

          indexCell.classList.add("bold-column");
          titleCell.classList.add("bold-column");

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

function deleteBook(inventory_Id) {
  const deleteModal = new bootstrap.Modal(
    document.getElementById("deleteModal")
  );
  deleteModal.show();

  const confirmButton = document.getElementById("confirmDeleteButton");
  confirmButton.onclick = () => confirmDelete(inventory_Id);
}

function confirmDelete(inventory_Id) {
  fetch(
    `https://book0209.azurewebsites.net/api/inventory/deleteInventory?inventoryId=${inventory_Id}`,
    {
      method: "PATCH",
    }
  )
    .then((response) => {
      if (response.ok) {
        fetchBooks();
        Swal.fire({
          icon: "success",
          title: "Deleted Successfully",
          text: "The inventory has been deleted successfully.",
        }).then(() => {
          window.location.href = "inventory.html";
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

function searchBooks() {
  const searchInput = document.getElementById("search-input");
  const searchQuery = searchInput.value;

  fetch(
    `https://book0209.azurewebsites.net/api/inventory/searchInventory?bookName=${searchQuery}`
  )
    .then((response) => response.json())
    .then((data) => {
      const tableBody = document.getElementById("book-table-body");
      tableBody.innerHTML = ""; // Xóa dữ liệu trước đó trên bảng
      data.forEach((book) => {
        if (book.is_Inventory_Status === true) {
          const row = document.createElement("tr");
          const indexCell = document.createElement("td");
          const imgCell = document.createElement("td");
          const titleCell = document.createElement("td");
          const quantityCell = document.createElement("td");
          const noteCell = document.createElement("td");
          const dataCell = document.createElement("td");
          const userCell = document.createElement("td");
          const deleteCell = document.createElement("td");

          titleCell.textContent = book.book_Title;
          noteCell.textContent = book.inventory_Note;
          quantityCell.textContent = book.inventory_Quantity;
          userCell.textContent = book.user_Name;
          const dateInto = new Date(book.inventory_Date_Into);
          const formattedDate = dateInto.toLocaleDateString("vi-VN");
          dataCell.textContent = formattedDate;

          const deleteIcon = document.createElement("i");
          deleteIcon.className = "fa fa-trash";
          deleteIcon.onclick = () => deleteBook(book.inventory_Id);

          const imgElement = document.createElement("img");
          imgElement.src = book.image_URL;
          imgElement.width = 100;
          imgElement.height = 120;

          imgCell.appendChild(imgElement);
          deleteCell.appendChild(deleteIcon);

          row.appendChild(indexCell);
          row.appendChild(imgCell);
          row.appendChild(titleCell);
          row.appendChild(quantityCell);
          row.appendChild(noteCell);
          row.appendChild(dataCell);
          row.appendChild(userCell);
          row.appendChild(deleteCell);

          indexCell.classList.add("bold-column");
          titleCell.classList.add("bold-column");

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
