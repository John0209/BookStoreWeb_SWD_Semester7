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

async function renderTable() {
  const url = "https://book0209.azurewebsites.net/api/request/getRequest";

  const getTodo = async (url) => {
    return await fetch(url);
  };

  const todo = await getTodo(url);
  const data = await todo.json();

  function formatDate(dateString) {
    const date = new Date(dateString);
    const formattedDate = date.toISOString().split("T")[0];
    return formattedDate;
  }

  var contentHTML = "";
  for (var i = 0; i < data.length; i++) {
    var currentRequestBook = data[i];

    if (currentRequestBook.is_Request_Status > 0) {
      var contentTr = `<tr> 
        <td>
            ${
              currentRequestBook.is_Request_Status === 1 && currentRequestBook.is_RequestBook_Status === false
                ? `<input
                type="checkbox"
                class="myCheckbox"
                value='${currentRequestBook.request_Id}'
              />`
                : ""
            }
        </td>
        <td><img class="bookImg img-fluid" src="${
          currentRequestBook.request_Image_Url
        }" /></td>
        <td>${currentRequestBook.request_Book_Name}</td>
        <td>${currentRequestBook.request_Quantity}</td>
        <td>${currentRequestBook.request_Price}</td>
        <td>${currentRequestBook.request_Note}</td>
        <td>${formatDate(currentRequestBook.request_Date)}</td>
        <td>${formatDate(currentRequestBook.request_Date_Done)}</td>
        <td>${
          currentRequestBook.is_Request_Status === 1
            ? "<p style='color: #f0b01d;'>Processing</p>"
            : currentRequestBook.is_Request_Status === 2
            ? "<p style='color: rgb(17, 232, 17);'>Done</p>"
            : "<p style='color: red'>Undone</p>"
        }</td>
        <td>${
          currentRequestBook.is_RequestBook_Status === true && currentRequestBook.is_Request_Status === 1
            ? `<i onclick='getBook("${currentRequestBook.request_Id}")' class='fa-solid fa-circle-plus'></i>`
            : ""
        }</td>
        <td>
        ${
          currentRequestBook.is_Request_Status === 1 
          ? `<i onclick='getRequestNote("${currentRequestBook.request_Id}")' class='fa-solid fa-triangle-exclamation'></i>`
          : ""
        }
      </td>
        </tr>
        `;
      contentHTML = contentHTML + contentTr;
    }
  }
  document.getElementById("book-table-body").innerHTML = contentHTML;
}

renderTable();

function emptyValidate(value, idError) {
  if (value.length == 0) {
    document.getElementById(idError).innerText = "This field is required";
    return false;
  } else {
    document.getElementById(idError).innerText = "";
    return true;
  }
}

function numberValidate(value, idError) {
  if (value < 1) {
    document.getElementById(idError).innerText =
      "This number must be larger than 0";
    return false;
  } else {
    document.getElementById(idError).innerText = "";
    return true;
  }
}

function isImage(url) {
  return /\.(jpg|jpeg|png|webp|avif|gif|svg)$/.test(url);
}

function imgValidate(value, idError) {
  if (isImage(value) == false) {
    document.getElementById(idError).innerText = "Please fill in an image link";
    return false;
  } else {
    document.getElementById(idError).innerText = "";
    return true;
  }
}

async function getBook(id) {
  var modal = document.getElementById("myModal1");

  $(modal).modal("show");

  const url1 = `https://book0209.azurewebsites.net/api/request/getRequestById?requestId=${id}`;

  localStorage.setItem("id", JSON.stringify(id));

  const getTodo1 = async (url1) => {
    return await fetch(url1);
  };

  const todo1 = await getTodo1(url1);
  const data1 = await todo1.json();
  console.log("data1: ", data1);

  const url2 = `https://book0209.azurewebsites.net/api/category/getCategoryById?CategoryId=${data1.category_Id}`;

  const getTodo2 = async (url2) => {
    return await fetch(url2);
  };

  const todo2 = await getTodo2(url2);
  const data2 = await todo2.json();

  document.getElementById("bookName").innerHTML = data1.request_Book_Name;
  document.getElementById("quantity").innerHTML = data1.request_Quantity;
  document.getElementById("money").innerHTML = data1.request_Price;
  document.getElementById("cate").innerHTML = data2.category_Name;
  document.getElementById("date").innerHTML = data1.request_Date;
  document.getElementById("note").innerHTML = data1.request_Note;
}

async function createBook() {
  const data = localStorage.getItem("id");
  var id = JSON.parse(data);
  console.log(id);

  const url1 = `https://book0209.azurewebsites.net/api/request/getRequestById?requestId=${id}`;

  const getTodo1 = async (url1) => {
    return await fetch(url1);
  };

  const todo1 = await getTodo1(url1);
  const data1 = await todo1.json();
  console.log("data1: ", data1);

  const url3 = `https://book0209.azurewebsites.net/api/book/createBook`;

  var author = document.getElementById("author").value;

  fetch(url3, {
    method: "POST",
    headers: { "Content-type": "application/json" },
    body: JSON.stringify({
      request_Id: id,
      book_Id: "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      category_Id: data1.category_Id,
      category_Name: "",
      image_URL: data1.request_Image_Url,
      book_Title: data1.request_Book_Name,
      book_Author: author,
      book_Description: data1.request_Note,
      book_Price: data1.request_Price,
      book_Quantity: data1.request_Quantity,
      book_Year_Public: 0,
      book_ISBN: 0,
      is_Book_Status: true,
    }),
  })
    .then((response) => {
      var closeButton = document.querySelector(
        '.btn.btn-danger[data-dismiss="modal"]'
      );
      closeButton.click();
      renderTable();
    })
    .catch(function (err) {
      console.log("err: ", err);
    });
}

var requests;

async function tickInvent() {
  const dataS = localStorage.getItem("user");
  const user = JSON.parse(dataS);

  var checkboxes = document.querySelectorAll(".myCheckbox");

  var checkedValues = [];

  checkboxes.forEach(function (checkbox) {
    if (checkbox.checked) {
      checkedValues.push(checkbox.value);
    }
  });

  async function getObjectById(id) {
    const url = `https://book0209.azurewebsites.net/api/request/getRequestById?requestId=${id}`;
    try {
      const response = await fetch(url);
      if (response.ok) {
        const data = await response.json();
        return data;
      } else {
        throw new Error("Request failed for ID: " + id);
      }
    } catch (error) {
      throw new Error("Request failed for ID: " + id);
    }
  }

  var requests = [];

  async function getRequests() {
    try {
      for (let i = 0; i < checkedValues.length; i++) {
        const id = checkedValues[i];
        console.log(id);
        const data = await getObjectById(id);
        requests.push(data);
      }
    } catch (error) {
      console.log(error);
    }
  }

  await getRequests();

  console.log(requests);

  var importationAmount = 0;
  var importationQuantity = 0;

  requests.map((item) => {
    importationAmount += item.request_Amount;
    importationQuantity += item.request_Quantity;
  });

  console.log("importationAmount: ", importationAmount);
  console.log("importationQuantity: ", importationQuantity);

  const url2 = `https://book0209.azurewebsites.net/api/importation/createImportation`;

  fetch(url2, {
    method: "POST",
    headers: { "Content-type": "application/json" },
    body: JSON.stringify({
      import_Id: "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      user_Id: user.user_Id,
      import_Quantity: importationQuantity,
      import_Amount: importationAmount,
      import_Date_Done: "2023-07-14T14:24:37.426Z",
      is_Import_Status: 1,
    }),
  })
    .then(async function () {
      const url3 = `https://book0209.azurewebsites.net/api/importation/getImportIdJustCreated`;
      const getTodo3 = async (url3) => {
        return await fetch(url3);
      };
      const todo3 = await getTodo3(url3);
      const data3 = await todo3.json();

      const url4 = `https://book0209.azurewebsites.net/api/importationDetail/createImportationDetail`;

      for (let i = 0; i < requests.length; i++) {
        const el = requests[i];

        fetch(url4, {
          method: "POST",
          headers: { "Content-type": "application/json" },
          body: JSON.stringify({
            import_Detail_Id: "3fa85f64-5717-4562-b3fc-2c963f66afa6",
            request_Id: el.request_Id,
            import_Id: data3,
            book_Id: el.book_Id,
            import_Detail_Quantity: el.request_Quantity,
            import_Detail_Price: el.request_Price,
            import_Detail_Amount: el.request_Amount,
          }),
        });
      }
      
      Swal.fire({
        icon: "success",
        title: "Add successfully",
        text: "Items have been move to importation.",
      });
      renderTable();
    })
    .catch(function (err) {
      console.log("err: ", err);
    });
    renderTable();
}

async function addInvent() {
  const dataS = localStorage.getItem("user");
  const user = JSON.parse(dataS);
  // Chọn những request để thực hiện add
  // Get all checkboxes with the specified class name
  var checkboxes = document.querySelectorAll(".myCheckbox");

  // Create an empty array to store the values of checked checkboxes
  var checkedValues = [];

  // Iterate over the checkboxes
  checkboxes.forEach(function (checkbox) {
    // Check if the checkbox is checked
    if (checkbox.checked) {
      // Add the value of the checked checkbox to the array
      checkedValues.push(checkbox.value);
    }
  });

  requests = [];

  async function getObjectById() {
    const url1 = `https://book0209.azurewebsites.net/api/request/getRequestById?requestId=${id}`;
    // Make a GET request for the object with the specified ID
    fetch(url1)
      .then(function (response) {
        // Check if the request was successful
        if (response.ok) {
          // Parse the response JSON
          return response.json();
        } else {
          throw new Error("Request failed for ID: " + id);
        }
      })
      .then(function (data) {
        requests.push(data);

        // console.log(importationAmount);
        // return { importationAmount : importationAmount, importationQuantity : importationQuantity}
      });
  }

  var id;
  for (let i = 0; i < checkedValues.length; i++) {
    id = checkedValues[i];
    console.log("yes");
    getObjectById(id);
  }
  console.log("no");

  console.log(requests);
  console.log(requests[0]);

  var importationAmount = 0;
  var importationQuantity = 0;

  requests.map((item) => {
    importationAmount += item.request_Amount;
    importationQuantity += item.request_Quantity;
  });

  console.log("importationAmount: ", importationAmount);
  console.log("importationQuantity: ", importationQuantity);

  const url2 = `https://book0209.azurewebsites.net/api/importation/createImportation`;

  fetch(url2, {
    method: "POST",
    headers: { "Content-type": "application/json" },
    body: JSON.stringify({
      import_Id: "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      user_Id: user.user_Id,
      import_Quantity: importationQuantity,
      import_Amount: importationAmount,
      import_Date_Done: "2023-07-14T14:24:37.426Z",
      is_Import_Status: 1,
    }),
  })
    .then(async function () {
      const url3 = `https://book0209.azurewebsites.net/api/importation/getImportIdJustCreated`;
      const getTodo3 = async (url3) => {
        return await fetch(url3);
      };
      const todo3 = await getTodo3(url3);
      const data3 = await todo3.json();
      console.log("data3: ", data3);
    })
    .catch(function (err) {
      console.log("err: ", err);
    });
}

function getRequestNote(id) {
  var modal = document.getElementById("myModal3");

  $(modal).modal("show");

  console.log(id);

  localStorage.setItem("id3", JSON.stringify(id));
}

async function noteRequest() {
  const data = localStorage.getItem("id3");
  const requestId = JSON.parse(data);
  console.log("requestId: ", requestId);

  var note =document.getElementById("noteRequest").value

  const url =
    `https://book0209.azurewebsites.net/api/request/updateUnDoneRequest?requestId=${requestId}&note=${note}`;

  fetch(url, {
    method: "PATCH",
    headers: { "Content-type": "application/json" },
    body: JSON.stringify({
      request_Id: requestId,
      note: note
    }),
  }).then((res) => {
    renderTable();
  });
  Swal.fire({
    icon: "success",
    title: "Delete successfully",
  });
}



