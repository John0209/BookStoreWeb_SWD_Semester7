let account = {};
window.addEventListener(
  "load",
  async () => {
    account = JSON.parse(localStorage.getItem("account"));
    console.log(account);
    const response = await fetch(
      `https://book0209.azurewebsites.net/api/user/getUserById?userId=${account.user_Id}`
    )
      .then((res, rej) => {
        if (rej) console.log(rej);
        return res.json();
      })
      .then((data) => {
        account = data;
        localStorage.setItem("account", JSON.stringify(account));
        Object.keys(account).forEach(function (key) {
          if (account[key] === null) {
            account[key] = "-";
          }
        });
      });
    
    displayProfile();
  },
  false
);

let displayProfile = () => {
  let displayProfileTemplate = `
  <div>
    <label class="text-label" for="username">Tên đăng nhập</label>
    <input type="text" id="username" name="username" value=${account.user_Account} disabled />
  </div>

  <div>
    <label class="text-label" for="password">Mật khẩu</label>
    <input type="text" id="password" name="password" value=${account.user_Password} disabled />
  </div>

  <div>
    <label class="text-label" for="fullName">Họ và tên</label>
    <input type="text" id="fullName" name="fullName" value=${account.user_Name} disabled />
  </div>

  <div>
    <label class="text-label" for="roleId">Role ID</label>
    <input type="text" id="roleId" name="roleId" value=${account.role_Id} disabled />
  </div>

  <div>
    <label class="text-label" for="gender">Giới tính</label>
    <select name="gender" id="gender" value=${account.is_User_Gender} disabled />
      <option value="Male">Nam</option>
      <option value="Female">Nữ</option>
      <option value="Others">Khác</option>
    </select>
  </div>

  <div>
    <label class="text-label" for="district">Địa chỉ email</label>
    <input type="text" id="email" name="email" value=${account.user_Email} disabled />
  </div>

  <div>
    <label class="text-label" for="address">Tên Đường</label>
    <input type="text" id="address" name="address" value=${account.user_Address} disabled />
  </div>

  <div>
    <label class="text-label" for="phone">Số Điện Thoại</label>
    <input type="text" id="phone" name="phone" value=${account.user_Phone} disabled />
  </div>
  `;
  document.getElementById("profile-form").innerHTML = displayProfileTemplate;
  document.getElementById("gender").value = account.is_User_Gender;
};
let onEditProfile = () => {
  var inputs = document.getElementsByTagName("input");
  for (const input of inputs) {
    input.disabled = !input.disabled;
  }
  var selects = document.getElementsByTagName("select");
  for (const select of selects) {
    select.disabled = !select.disabled;
  }
  document.querySelector(".fa-save").classList.remove("d-none");
  document.querySelector(".fa-times").classList.remove("d-none");
};
let onCancelProfile = () => {
  var inputs = document.getElementsByTagName("input");
  for (const input of inputs) {
    input.disabled = !input.disabled;
  }
  document.querySelector(".fa-save").classList.add("d-none");
  document.querySelector(".fa-times").classList.add("d-none");
};
let onSaveProfile = async () => {
  let username = document.getElementById("username").value;
  let password = document.getElementById("password").value;
  let fullName = document.getElementById("fullName").value;
  let roleId = document.getElementById("roleId").value;
  let gender = document.getElementById("gender").value;
  let email = document.getElementById("email").value;
  let address = document.getElementById("address").value;
  let phone = document.getElementById("phone").value;

  const data = {
    user_Id: account.user_Id,
    user_Account: username,
    user_Password: password,
    user_Email: email,
    role_Id: roleId,
    user_Name: fullName,
    user_Address: address,
    user_Phone: phone,
    is_User_Gender: gender,
  };
  console.log(data);

  const response = await fetch(
    "https://book0209.azurewebsites.net/api/user/updateUser",
    {
      method: "PUT",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(data),
    }
  ).then((res, rej) => {
    if (rej) console.log(rej);
    if (res.status !== 200) {
      alert("Failed to update!!!");
    } else {
      location.reload();
    }
  });
};
