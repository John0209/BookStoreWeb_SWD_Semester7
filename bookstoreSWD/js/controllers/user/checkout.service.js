let cart = [];
let totalPrice = 0;
window.addEventListener(
  "DOMContentLoaded",
  async () => {
    cart = JSON.parse(localStorage.getItem("cart"));
    totalPrice = cart.reduce(
      (cur, item) => cur + item.book_Price * item.quantity,
      0
    );
    displayOrderDetail();
    displayTotalPrice();
  },
  false
);

let displayOrderDetail = () => {
  let displayOrderDetailTemplate = "";
  for (let item of cart) {
    displayOrderDetailTemplate += `
    <div class="flex-between mt-15">
      <div class="flex-around">
        <img class="img-fluid" src=${
          item.image_URL
        } alt="" style="width: 100; height: 300px;">
        <div class="flex-column">
          <span class="text-opacity">${item.book_Title}</span>
          <span>x ${item.quantity}</span>
        </div>
      </div>
      <span class="text-small">
        VND. ${item.quantity * item.book_Price}
      </span>
    </div>
    `;
  }
  document.getElementById("payment-item").innerHTML =
    displayOrderDetailTemplate;
};

let displayTotalPrice = () => {
  let displayTotalPaymentTemplate = `
    <span>Total payment</span>
    <span class="payment-price">VND.${totalPrice}</span>
  `;
  document.getElementById("total-payment").innerHTML =
    displayTotalPaymentTemplate;
};

function validateForm() {
  let fullName = document.getElementById("fullName").value;
  let address = document.getElementById("address").value;
  let phone = document.getElementById("phone").value;

  if (!fullName) {
    alert("Please enter your full name.");
    return false;
  }

  if (!address) {
    alert("Please enter your address.");
    return false;
  }

  if (!phone) {
    alert("Please enter your phone number.");
    return false;
  }

  return true;
}

async function checkout() {
  if (!validateForm()) {
    return;
  }

  let fullName = document.getElementById("fullName").value;
  let address = document.getElementById("address").value;
  let phone = document.getElementById("phone").value;

  const data = {
    user_Id: JSON.parse(localStorage.getItem("account")).user_Id,
    order_Customer_Name: fullName,
    order_Customer_Address: address,
    order_Customer_Phone: phone,
    order_Date: new Date().toISOString(),
    order_Quantity: cart.reduce((acc, item) => acc + Number(item.quantity), 0),
    order_Amount: totalPrice,
    order_Code: "abc",
  };

  await fetch("https://book0209.azurewebsites.net/api/order/createOrder", {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(data),
  }).then((res, rej) => {
    if (rej) console.log(rej);
  });

  let orderId;
  await fetch("https://book0209.azurewebsites.net/api/order/getOrder")
    .then((res, rej) => {
      if (rej) console.log(rej);
      return res.json();
    })
    .then((data) => {
      let timeArr = [];
      data.forEach((order) => {
        timeArr.push(new Date(order.order_Date).getTime());
      });

      let max = Math.max(...timeArr);

      data = data.filter(
        (order) => new Date(order.order_Date).getTime() === max
      );
      orderId = data[0].order_Id;
    });

  let orderDetailData = [];

  orderDetailData = cart.map((item) => {
    return {
      order_Id: orderId,
      book_Id: item.book_Id,
      order_Detail_Quantity: item.book_Quantity,
      order_Detail_Amount: item.quantity * item.book_Price,
      order_Detail_Price: item.book_Price,
      order_Code: "abc",
    };
  });
  for (let orderDetail of orderDetailData) {
    await fetch(
      "https://book0209.azurewebsites.net/api/orderDetail/createOrderDetail",
      {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(orderDetail),
      }
    ).then((res, rej) => {
      if (res.status !== 200) alert("Something went wrong!!!");
      else {
        localStorage.removeItem("cart");
        window.location.href = "/html/index.html";
      }
    });
  }
}