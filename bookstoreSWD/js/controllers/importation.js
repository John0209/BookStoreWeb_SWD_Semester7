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

// Lấy dữ liệu từ API getImportation
function fetchImportation() {
  fetch("http://bookstoreprn-001-site1.itempurl.com/api/importations")
  //fetch("https://book0209.azurewebsites.net/api/importation/getImportation")
    .then((response) => response.json())
    .then((importations) => {
      const bgContainer = document.getElementById("bg-container");

      // Kiểm tra dữ liệu
      if (importations.length === 0) {
        const noDataDiv = document.createElement("div");
        noDataDiv.innerText = "No data available.";
        bgContainer.appendChild(noDataDiv);
        return;
      }

      let hasValidData = false;

      importations.forEach((importation, importIndex) => {
        // Kiểm tra is_Import_Status
        if (
          importation.is_Import_Status === 1 ||
          importation.is_Import_Status === 2
        ) {
          hasValidData = true;

          // Tạo phần tử bg mới cho mỗi importation
          const bgDiv = document.createElement("div");
          bgDiv.className = "bg";
          bgDiv.style.padding = "20px 10px 10px 10px";
          bgDiv.style.border = "1px solid #cbd2dc";
          bgDiv.style.borderRadius = "15px";

          const titleDiv = document.createElement("div");
          titleDiv.className = "title";
          titleDiv.style.display = "flex";
          titleDiv.style.justifyContent = "space-between";
          titleDiv.style.margin = "0 45px 0 45px";

          const importationStt = document.createElement("h6");
          importationStt.className = "importation-stt cl-text-ad";
          importationStt.innerText = `List ${importIndex + 1}`;
          titleDiv.appendChild(importationStt);

          const importationDate = document.createElement("h6");
          importationDate.className = "importation-date cl-text-ad";
          const importDate = new Date(importation.import_Date_Done);
          importationDate.innerText = `Date of entry: ${
            importDate.toISOString().split("T")[0]
          }`;
          titleDiv.appendChild(importationDate);

          const importationUser = document.createElement("h6");
          importationUser.className = "importation-user cl-text-ad";
          importationUser.innerText = importation.user_Name;
          importationUser.style.marginRight = "10px";

          if (importation.is_Import_Status === 2) {
            const deleteIcon = document.createElement("i");
            deleteIcon.className = "fas fa-trash-alt";
            deleteIcon.style.cursor = "pointer";
            deleteIcon.onclick = () => deleteBook(importation.import_Id);

            const userDeleteDiv = document.createElement("div");
            userDeleteDiv.style.display = "flex";
            userDeleteDiv.style.alignItems = "center";

            userDeleteDiv.appendChild(importationUser);
            userDeleteDiv.appendChild(deleteIcon);

            titleDiv.appendChild(userDeleteDiv);
          } else {
            titleDiv.appendChild(importationUser);
          }

          bgDiv.appendChild(titleDiv);

          const bookListDiv = document.createElement("div");
          bookListDiv.className = "book-list";
          bookListDiv.style.margin = "0 35px 0 35px";

          // Gọi API getByImportId?import_Id với từng importation ID
          fetch(
            `http://bookstoreprn-001-site1.itempurl.com/api/importation-detail/${importation.import_Id}`
            //`https://book0209.azurewebsites.net/api/importationDetail/getByImportId?import_Id=${importation.import_Id}`
          )
            .then((response) => response.json())
            .then((details) => {
              details.forEach((detail, detailIndex) => {
                const bookDetailDiv = document.createElement("div");
                bookDetailDiv.className = "book-detail";
                bookDetailDiv.style.display = "flex";
                bookDetailDiv.style.marginTop = "25px";

                const bookStt = document.createElement("h6");
                bookStt.innerText = `${detailIndex + 1}`;
                bookDetailDiv.appendChild(bookStt);

                const bookImg = document.createElement("img");
                bookImg.src = detail.image_URL;
                bookImg.style.height = "120px";
                bookImg.style.width = "100px";
                bookImg.style.margin = "0 20px";
                bookDetailDiv.appendChild(bookImg);

                const bookInfoDiv = document.createElement("div");
                bookInfoDiv.className = "mx-5";

                const bookTitle = document.createElement("h6");
                bookTitle.innerText = detail.book_Title;
                bookInfoDiv.appendChild(bookTitle);

                const bookPrice = document.createElement("p");
                bookPrice.innerText = `${detail.import_Detail_Price} đ`;
                bookInfoDiv.appendChild(bookPrice);

                bookDetailDiv.appendChild(bookInfoDiv);

                const bookQuantity = document.createElement("p");
                bookQuantity.className = "quantity px-2";
                bookQuantity.innerText = detail.import_Detail_Quantity;
                bookDetailDiv.appendChild(bookQuantity);

                const bookTotal = document.createElement("p");
                bookTotal.style.marginLeft = "150px";
                bookTotal.style.marginTop = "25px";
                bookTotal.innerText = `${detail.import_Detail_Amount} đ`;
                bookDetailDiv.appendChild(bookTotal);

                bookListDiv.appendChild(bookDetailDiv);
              });

              bgDiv.appendChild(bookListDiv);

              // Thêm phần tử importation-amount vào cuối mỗi bgDiv
              const importationAmountDiv = document.createElement("div");
              importationAmountDiv.className =
                "d-flex justify-content-end mt-2 mx-4";

              const importationAmount = document.createElement("h6");
              importationAmount.className = "p-2 cl-text-ad";
              importationAmount.style.border = "1px solid #cbd2dc";
              importationAmount.style.borderRadius = "15px";
              importationAmount.id = "importation-amount";
              importationAmount.innerText = `Total: ${importation.import_Amount} đ`;

              importationAmountDiv.appendChild(importationAmount);
              bgDiv.appendChild(importationAmountDiv);

              bgContainer.appendChild(bgDiv);
            })
            .catch((error) => {
              console.log("Error:", error);
            });
        }
      });

      // Kiểm tra dữ liệu hợp lệ
      if (!hasValidData) {
        const noValidDataDiv = document.createElement("div");
        noValidDataDiv.innerText = "No valid data available.";
        bgContainer.appendChild(noValidDataDiv);
      }
    })
    .catch((error) => {
      console.log("Error:", error);
    });
}

function deleteBook(import_Id) {
  const deleteModal = new bootstrap.Modal(
    document.getElementById("deleteModal")
  );
  deleteModal.show();

  const confirmButton = document.getElementById("confirmDeleteButton");
  confirmButton.onclick = () => confirmDelete(import_Id);
}

function confirmDelete(import_Id) {
  fetch(
    `https://book0209.azurewebsites.net/api/importation/deleteImporatation?importId=${import_Id}`,
    {
      method: "PATCH",
    }
  )
    .then((response) => {
      if (response.ok) {
        fetchImportation();
        Swal.fire({
          icon: "success",
          title: "Deleted Successfully",
          text: "The importation has been deleted successfully.",
        }).then(() => {
          window.location.href = "importation.html";
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

function exportExcel() {
  const month = document.getElementById("monthInput").value;

  // Gọi API để tải file Excel
  fetch(
    `https://book0209.azurewebsites.net/api/importation/exportExcel?month=${month}`
  )
    .then((response) => {
      // Xử lý phản hồi từ API và tạo file để tải về
      return response.blob();
    })
    .then((blob) => {
      // Tạo đường dẫn URL tạm thời để tải file
      const url = window.URL.createObjectURL(blob);
      // Tạo một thẻ a ẩn để tải file
      const a = document.createElement("a");
      a.style.display = "none";
      a.href = url;
      a.download = `export_${month}.xlsx`;
      // Thêm thẻ a vào body và click để tải file
      document.body.appendChild(a);
      a.click();
      // Xóa đường dẫn URL tạm thời sau khi tải xong
      window.URL.revokeObjectURL(url);
    })
    .catch((error) => {
      console.error("Lỗi khi tải file:", error);
    });
}

fetchImportation();
