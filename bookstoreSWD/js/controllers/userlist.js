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

function fetchUsers() {
  fetch("https://book0209.azurewebsites.net/api/user/getUser")
    .then((response) => response.json())
    .then((data) => {
      const tableBody = document.getElementById("user-table-body");

      tableBody.innerHTML = "";
      let foundUsers = false;
      data.forEach((user, index) => {
        // Kiểm tra nếu roleId là 2 hoặc 3 thì mới hiển thị
        if (user.role_Id === 2 || user.role_Id === 3) {
          foundUsers = true;
          const row = document.createElement("tr");
          row.innerHTML = `
            <td class="bold-column">${index + 1}</td> 
            <td class="bold-column">${user.user_Account}</td>
            <td class="bold-column">${user.user_Email}</td>
            <td class="bold-column">${user.user_Phone}</td>
            <td class="bold-column">
            <select class="role-select" data-userid="${user.user_Id}">
              <option value="1" ${
                user.role_Id === 1 ? "selected" : ""
              }>Admin</option>
              <option value="2" ${
                user.role_Id === 2 ? "selected" : ""
              }>Staff</option>
              <option value="3" ${
                user.role_Id === 3 ? "selected" : ""
              }>User</option>
            </select>
          </td>
            <td>
              <select class="status-select" data-userid="${user.user_Id}">
                <option value="true" ${
                  user.is_User_Status ? "selected" : ""
                }>Active</option>
                <option value="false" ${
                  !user.is_User_Status ? "selected" : ""
                }>Inactive</option>
              </select>
            </td>
          `;
          tableBody.appendChild(row);
        }
      });

      if (!foundUsers) {
        const noDataRow = document.createElement("tr");
        const noDataCell = document.createElement("td");
        noDataCell.textContent = "No data available in table";
        noDataCell.colSpan = 8;
        noDataCell.classList.add("no-data-message");
        noDataRow.appendChild(noDataCell);
        tableBody.appendChild(noDataRow);
      }

      const statusSelects = document.querySelectorAll(".status-select");
      statusSelects.forEach((select) => {
        select.addEventListener("change", function () {
          const userId = this.getAttribute("data-userid");
          const newStatus = this.value === "true" ? true : false;
          updateUserStatus(userId, newStatus);
        });
      });
      const roleSelects = document.querySelectorAll(".role-select");
      roleSelects.forEach((select) => {
        select.addEventListener("change", function () {
          const userId = this.getAttribute("data-userid");
          const newRole = this.value;
          updateUserRole(userId, newRole);
        });
      });
    })
    .catch((error) => {
      console.log(error);
    });
}

function getUserRole(roleId) {
  if (roleId === 1) {
    return "Admin";
  } else if (roleId === 2) {
    return "Staff";
  } else if (roleId === 3) {
    return "User";
  } else {
    return "Unknown";
  }
}
function searchUser() {
  const searchInput = document.getElementById("search-input");
  const searchQuery = searchInput.value;

  fetch(
    `https://book0209.azurewebsites.net/api/user/getUserByName?userName=${searchQuery}`
  )
    .then((response) => response.json())
    .then((data) => {
      const tableBody = document.getElementById("user-table-body");

      tableBody.innerHTML = "";
      data.forEach((user, index) => {
        const row = document.createElement("tr");
        row.innerHTML = `
          <td class="bold-column">${index + 1}</td> 
          <td class="bold-column">${user.user_Account}</td>
          <td class="bold-column">${user.user_Email}</td>
          <td class="bold-column">${user.user_Phone}</td>
          <td class="bold-column">
            <select class="role-select" data-userid="${user.user_Id}">
              <option value="1" ${
                user.role_Id === 1 ? "selected" : ""
              }>Admin</option>
              <option value="2" ${
                user.role_Id === 2 ? "selected" : ""
              }>Staff</option>
              <option value="3" ${
                user.role_Id === 3 ? "selected" : ""
              }>User</option>
            </select>
          </td>
          
          <td>
            <select class="status-select" data-userid="${user.user_Id}">
              <option value="true" ${
                user.is_User_Status ? "selected" : ""
              }>Active</option>
              <option value="false" ${
                !user.is_User_Status ? "selected" : ""
              }>Inactive</option>
            </select>
          </td>
        `;
        tableBody.appendChild(row);
      });

      const statusSelects = document.querySelectorAll(".status-select");
      statusSelects.forEach((select) => {
        select.addEventListener("change", function () {
          const userId = this.getAttribute("data-userid");
          const newStatus = this.value === "true" ? true : false;
          updateUserStatus(userId, newStatus);
        });
      });
      const roleSelects = document.querySelectorAll(".role-select");
      roleSelects.forEach((select) => {
        select.addEventListener("change", function () {
          const userId = this.getAttribute("data-userid");
          const newRole = this.value;
          updateUserRole(userId, newRole);
        });
      });
    })
    .catch((error) => {
      console.log(error);
      const tableBody = document.getElementById("user-table-body");
      tableBody.innerHTML = "";

      const errorRow = document.createElement("tr");
      const errorCell = document.createElement("td");
      errorCell.textContent = `${searchInput.value} doesn't exist`;
      errorCell.colSpan = 8;
      errorCell.classList.add("error-message");
      errorRow.appendChild(errorCell);
      tableBody.appendChild(errorRow);
    });
}

function updateUserRole(userId, newRole) {
  const requestData = {
    userId: userId,
    roleID: newRole,
  };

  fetch(
    `https://book0209.azurewebsites.net/api/user/updateRole?userId=${userId}&roleID=${newRole}`,
    {
      method: "PATCH",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(requestData),
    }
  )
    .then((response) => response.json())
    .then((data) => {
      console.log(data);
    })
    .catch((error) => {
      console.log(error);
    });
}

function updateUserStatus(userId, newStatus) {
  const requestData = {
    is_User_Status: newStatus,
  };
  if (!newStatus) {
    fetch(
      `https://book0209.azurewebsites.net/api/user/deleteUser?userId=${userId}`,
      {
        method: "PATCH",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(requestData),
      }
    )
      .then((response) => response.json())
      .then((data) => {
        console.log(data);
      })
      .catch((error) => {
        console.log(error);
      });
  } else {
    fetch(
      `https://book0209.azurewebsites.net/api/user/restoreUser?userId=${userId}`,
      {
        method: "PATCH",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(requestData),
      }
    )
      .then((response) => response.json())
      .then((data) => {
        console.log(data);
      })
      .catch((error) => {
        console.log(error);
      });
  }
}
fetchUsers();
