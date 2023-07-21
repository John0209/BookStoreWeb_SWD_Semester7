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
        if (book.is_Inventory_Status === false) {
          foundBooks = true;
          const row = document.createElement("tr");
          const indexCell = document.createElement("td");
          const imgCell = document.createElement("td");
          const titleCell = document.createElement("td");
          const quantityCell = document.createElement("td");
          const noteCell = document.createElement("td");
          const dataCell = document.createElement("td");
          const userCell = document.createElement("td");
          const restoreCell = document.createElement("td");
          const restoreIcon = document.createElement("i");

          titleCell.textContent = book.book_Title;
          noteCell.textContent = book.inventory_Note;
          quantityCell.textContent = book.inventory_Quantity;
          userCell.textContent = book.user_Name;
          const dateInto = new Date(book.inventory_Date_Into);
          const formattedDate = dateInto.toLocaleDateString("vi-VN");
          dataCell.textContent = formattedDate;
          restoreCell.style.cursor = "pointer";

          restoreIcon.classList.add("fas", "fa-undo");
          restoreIcon.onclick = () => restoreBook(book.inventory_Id);
          restoreCell.appendChild(restoreIcon);

          const imgElement = document.createElement("img");
          imgElement.src = book.image_URL;
          imgElement.width = 100;
          imgElement.height = 120;

          imgCell.appendChild(imgElement);

          row.appendChild(indexCell);
          row.appendChild(imgCell);
          row.appendChild(titleCell);
          row.appendChild(quantityCell);
          row.appendChild(noteCell);
          row.appendChild(dataCell);
          row.appendChild(userCell);
          row.appendChild(restoreCell);

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

function restoreBook(inventoryId) {
  const restoreModal = new bootstrap.Modal(
    document.getElementById("restoreModal")
  );
  restoreModal.show();

  const confirmButton = document.getElementById("confirmRestore");
  confirmButton.onclick = () => confirmRestore(inventoryId);
}

function confirmRestore(inventoryId) {
  fetch(
    `https://book0209.azurewebsites.net/api/inventory/restoreInventory?inventoryId=${inventoryId}`,
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
          window.location.href = "restoreinventory.html";
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
