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

function formatDateTime(dateTimeString) {
  const dateTime = new Date(dateTimeString);
  const year = dateTime.getFullYear();
  const month = String(dateTime.getMonth() + 1).padStart(2, "0");
  const day = String(dateTime.getDate()).padStart(2, "0");
  const hours = String(dateTime.getHours()).padStart(2, "0");
  const minutes = String(dateTime.getMinutes()).padStart(2, "0");

  return `${day}-${month}-${year} ${hours}:${minutes}`;
}

function fetchOrders() {
  fetch("https://book0209.azurewebsites.net/api/order/getOrder")
    .then((response) => response.json())
    .then((data) => {
      const tableBody = document.querySelector("#orderTable tbody");
      tableBody.innerHTML = "";

      data.forEach((item) => {
        if (
          item.is_Order_Status === 1 ||
          item.is_Order_Status === 2 ||
          item.is_Order_Status === 3 ||
          item.is_Order_Status === 5
        ) {
          const row = document.createElement("tr");

          const orderIdCell = document.createElement("td");
          const dateCell = document.createElement("td");
          const customerNameCell = document.createElement("td");
          const amountCell = document.createElement("td");
          const customerPaymentCell = document.createElement("td");

          orderIdCell.textContent = item.order_Code;
          orderIdCell.classList.add("order-Id-Cell");

          dateCell.textContent = formatDateTime(item.order_Date);
          customerNameCell.textContent = item.order_Customer_Name;
          amountCell.textContent = item.order_Amount;
          customerPaymentCell.textContent = item.order_Amount;

          orderIdCell.addEventListener("click", () => {
            const orderId = item.order_Id;
            openDetail(orderId);
          });

          row.appendChild(orderIdCell);
          row.appendChild(dateCell);
          row.appendChild(customerNameCell);
          row.appendChild(amountCell);
          row.appendChild(customerPaymentCell);

          tableBody.appendChild(row);
        }
      });
    })
    .catch((error) => {
      console.error("Error:", error);
    });
}

function searchOrder() {
  const searchInput = document.getElementById("search-input").value;
  fetch(
    "https://book0209.azurewebsites.net/api/order/searchByOrderCode?orderCode=" +
      searchInput
  )
    .then((response) => {
      if (!response.ok) {
        throw new Error("Order code doesn't exist");
      }
      return response.json();
    })
    .then((data) => {
      const tableBody = document.querySelector("#orderTable tbody");
      tableBody.innerHTML = "";

      if (
        data &&
        (data.is_Order_Status === 1 ||
          data.is_Order_Status === 2 ||
          data.is_Order_Status === 3 ||
          data.is_Order_Status === 5)
      ) {
        const row = document.createElement("tr");

        const orderIdCell = document.createElement("td");
        const dateCell = document.createElement("td");
        const customerNameCell = document.createElement("td");
        const amountCell = document.createElement("td");
        const customerPaymentCell = document.createElement("td");

        orderIdCell.textContent = data.order_Code;
        orderIdCell.classList.add("order-Id-Cell");

        dateCell.textContent = formatDateTime(data.order_Date);
        customerNameCell.textContent = data.order_Customer_Name;
        amountCell.textContent = data.order_Amount;
        customerPaymentCell.textContent = data.order_Amount;

        orderIdCell.addEventListener("click", () => {
          const orderId = data.order_Id;
          openDetail(orderId);
        });

        row.appendChild(orderIdCell);
        row.appendChild(dateCell);
        row.appendChild(customerNameCell);
        row.appendChild(amountCell);
        row.appendChild(customerPaymentCell);

        tableBody.appendChild(row);
      } else {
        const errorRow = document.createElement("tr");
        const errorCell = document.createElement("td");
        errorCell.textContent = `${searchInput} doesn't exist`;
        errorCell.colSpan = 6;
        errorCell.classList.add("error-message");
        errorRow.appendChild(errorCell);
        tableBody.appendChild(errorRow);
      }
    })
    .catch((error) => {
      console.error("Error:", error);
      const tableBody = document.querySelector("#orderTable tbody");
      tableBody.innerHTML = "";

      const errorRow = document.createElement("tr");
      const errorCell = document.createElement("td");
      errorCell.textContent = error.message;
      errorCell.colSpan = 6;
      errorCell.classList.add("error-message");
      errorRow.appendChild(errorCell);
      tableBody.appendChild(errorRow);
    });
}

function openDetail(orderId) {
  console.log("Clicked orderId:", orderId);

  fetch(
    "https://book0209.azurewebsites.net/api/orderDetail/getByOrderId?Order_id=" +
      orderId
  )
    .then((response) => response.json())
    .then((data) => {
      console.log(data);

      const tableBody = document.querySelector("#order-items tbody");

      tableBody.innerHTML = "";

      data.forEach((item) => {
        // Create a new table row
        const row = document.createElement("tr");

        const imageCell = document.createElement("td");
        const titleCell = document.createElement("td");
        const quantityCell = document.createElement("td");
        const amountCell = document.createElement("td");

        imageCell.innerHTML = `<img src="${item.image_URL}" alt="Book Image" width="50">`;
        titleCell.textContent = item.book_Title;
        quantityCell.textContent = item.order_Detail_Quantity;
        amountCell.textContent = item.order_Detail_Amount;

        row.appendChild(imageCell);
        row.appendChild(titleCell);
        row.appendChild(quantityCell);
        row.appendChild(amountCell);

        tableBody.appendChild(row);
      });
    })
    .catch((error) => {
      console.error("Error:", error);
    });

  fetch(
    "https://book0209.azurewebsites.net/api/order/getOrderByOrderId?OrderId=" +
      orderId
  )
    .then((response) => response.json())
    .then((orderDetails) => {
      // Hiển thị thông tin chi tiết đơn hàng
      const orderCodeElement = document.querySelector("#order-code");
      const orderDateElement = document.querySelector("#order-date");
      const orderStatusElement = document.querySelector("#order-status");
      const orderRecipientElement = document.querySelector("#order-recipient");
      const orderPhoneElement = document.querySelector("#order-phone");
      const orderAddressElement = document.querySelector("#order-address");
      const orderQuantityElement = document.querySelector(
        "#order-total-quantity"
      );
      const orderAmountElement = document.querySelector("#order-total-amount");

      orderCodeElement.textContent = orderDetails.order_Code;
      orderDateElement.textContent = formatDateTime(orderDetails.order_Date);

      if (orderDetails.is_Order_Status === 1) {
        orderStatusElement.textContent = "Processing";
        orderStatusElement.classList.add("order-status-processing");
      } else if (orderDetails.is_Order_Status === 2) {
        orderStatusElement.textContent = "Completed";
        orderStatusElement.classList.add("order-status-done");
      } else if (orderDetails.is_Order_Status === 3) {
        orderStatusElement.textContent = "Cancelled";
        orderStatusElement.classList.add("order-status-fail");
      } else if (orderDetails.is_Order_Status === 5) {
        orderStatusElement.textContent = "To Confirm";
        orderStatusElement.classList.add("order-status-confirm");
      } else {
        orderStatusElement.textContent = "Deleted";
      }

      orderRecipientElement.textContent = orderDetails.order_Customer_Name;
      orderPhoneElement.textContent = orderDetails.order_Customer_Phone;
      orderAddressElement.textContent = orderDetails.order_Customer_Address;
      orderQuantityElement.textContent = orderDetails.order_Quantity;
      orderAmountElement.textContent = orderDetails.order_Amount;
    })
    .catch((error) => {
      console.error("Error:", error);
    });

  // Show the modal
  const detailModal = new bootstrap.Modal(
    document.getElementById("detailModal")
  );
  detailModal.show();
}

function deleteOrder(order_Id) {
  const deleteModal = new bootstrap.Modal(
    document.getElementById("deleteModal")
  );
  deleteModal.show();

  const confirmButton = document.getElementById("confirmDeleteButton");
  confirmButton.onclick = () => confirmDelete(order_Id);
}

function confirmDelete(order_Id) {
  fetch(
    `https://book0209.azurewebsites.net/api/order/deleteOrder?orderId=${order_Id}`,
    {
      method: "PATCH",
    }
  )
    .then((response) => {
      if (response.ok) {
        fetchOrders();
        Swal.fire({
          icon: "success",
          title: "Deleted Successfully",
          text: "The order has been deleted successfully.",
        }).then(() => {
          window.location.href = "orderlist.html";
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

fetchOrders();
