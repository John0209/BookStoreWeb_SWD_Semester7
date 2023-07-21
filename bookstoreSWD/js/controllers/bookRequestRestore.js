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

  var contentHTML = "";
  for (var i = 0; i < data.length; i++) {
    var currentRequestBook = data[i];

    if (currentRequestBook.is_Request_Status == 0) {
      var contentTr = `<tr> 
        <td><img class="bookImg img-fluid" src="${
          currentRequestBook.request_Image_Url
        }" /></td>
        <td>${currentRequestBook.request_Book_Name}</td>
        <td>${currentRequestBook.request_Quantity}</td>
        <td>${currentRequestBook.request_Price}</td>
        <td>${currentRequestBook.request_Note}</td>
        <td>${currentRequestBook.request_Date}</td>
        <td>${currentRequestBook.request_Date_Done}</td>
        <td>${
          currentRequestBook.is_Request_Status === 1
            ? "<p style='color: #f0b01d;'>Processing</p>"
            : currentRequestBook.is_Request_Status === 2
            ? "<p style='color: rgb(17, 232, 17);'>Done</p>"
            : "<p style='color: red'>Undone</p>"
        }</td>
        <td>${
          currentRequestBook.is_RequestBook_Status === true
            ? "<p style='color: rgb(7, 214, 7);'>New</p>"
            : "<p style='color: red;'>Old</p>"
        }
        </td>
        <td>${
          currentRequestBook.is_Request_Status === 2
            ? `<i onclick='getRequestDelete("${currentRequestBook.request_Id}")' class='fa-solid fa-trash-can'></i>`
            : ""
        }</td>
        <td>
        ${
          currentRequestBook.is_Request_Status === 3
            ? `<i onclick='getRequestUpdate("${currentRequestBook.request_Id}")' class="fa-solid fa-pen-to-square"></i>`
            : ""
        }
        </td>
        <td>

            <i onclick='getRequestRestore(${currentRequestBook.request_Id})' class="fa-solid fa-tent-arrow-turn-left"></i>
        </td>
        
        </tr>
        `;
      contentHTML = contentHTML + contentTr;
    }
  }
  document.getElementById("book-table-body").innerHTML = contentHTML;
}

renderTable();

function getRequestRestore(id) {
    var modal = document.getElementById("myModal2");
  
    $(modal).modal("show");
  
    console.log(id);
  
    localStorage.setItem("id2", JSON.stringify(id));
  }

async function restoreRequest() {

    const data = localStorage.getItem("id2");
    const requestId = JSON.parse(data);
    console.log("requestId: ", requestId);

    const url = `https://book0209.azurewebsites.net/api/request/restoreRequest?requestId=${requestId}`;

    fetch(url, {
        method: "PATCH",
        headers: { "Content-type": "application/json" },
        body: JSON.stringify({
          request_Id: requestId,
        }),
      }).then((res) => {
        renderTable();
        Swal.fire({
            icon: "success",
            title: "Restore successfully",
          });
      });
}
