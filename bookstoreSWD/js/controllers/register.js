function validate() {
  var username = document.getElementById("username").value;
  var password = document.getElementById("password").value;
  var repassword = document.getElementById("repassword").value;
  var name1 = document.getElementById("name").value;
  var email = document.getElementById("email").value;
  var address = document.getElementById("address").value;
  var phone = document.getElementById("phone").value;

  if (
    username.length == 0 ||
    password.length == 0 ||
    repassword.length == 0 ||
    name1.length == 0 ||
    email.length == 0 ||
    address.length == 0 ||
    phone.length == 0
  ) {
    document.getElementById("notification").innerHTML =
      "Please fill in all the fields";
  } else if (repassword != password) {
    document.getElementById("notification").innerHTML =
      "Please enter the exact password you have enter";
    let input1 = document.getElementById("password");
    input1.value = "";
    let input2 = document.getElementById("repassword");
    input2.value = "";
  }
}

async function register() {
  const url = "https://book0209.azurewebsites.net/api/user/createUserFE";

  var username = document.getElementById("username").value;
  console.log("username: ", username);
  var password = document.getElementById("password").value;
  console.log("password: ", password);
  var repassword = document.getElementById("repassword").value;
  console.log("repassword: ", repassword);
  var name1 = document.getElementById("name").value;
  console.log("name1: ", name1);
  var email = document.getElementById("email").value;
  console.log("email: ", email);
  var address = document.getElementById("address").value;
  console.log("address: ", address);
  var phone = document.getElementById("phone").value;
  console.log("phone: ", phone);

  if (
    username.length == 0 ||
    password.length == 0 ||
    repassword.length == 0 ||
    name1.length == 0 ||
    email.length == 0 ||
    address.length == 0 ||
    phone.length == 0
  ) {
    document.getElementById("notification").innerHTML =
      "Please fill in all the fields";
  } else if (repassword != password) {
    document.getElementById("notification").innerHTML =
      "Please enter the exact password you have enter";
    let input1 = document.getElementById("password");
    input1.value = "";
    let input2 = document.getElementById("repassword");
    input2.value = "";
  }
  fetch(url, {
    method: "POST",
    headers: { "Content-type": "application/json;charset=UTF-8" },
    body: JSON.stringify({
      user_Id: "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      role_Id: 3,
      user_Account: username,
      user_Password: password,
      user_Name: name1,
      user_Email: email,
      user_Address: address,
      user_Phone: phone,
      is_User_Gender: "",
      is_User_Status: true,
    }),
  }).then((response) => {
    window.location.href = "login.html";
  });
}
