async function login() {
  const url = "https://book0209.azurewebsites.net/api/user/login"; // Replace with your actual API endpoint

  var username = document.getElementById("username").value;
  console.log("username: ", username);
  var password = document.getElementById("password").value;
  console.log("password: ", password);

  // Data to be sent in the request body
  const data = {
    user_Account: username,
    user_Password: password,
  };

  // Configuration for the fetch request
  const requestOptions = {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
      // Add any other required headers here
    },
    body: JSON.stringify(data),
  };

  fetch(url, requestOptions)
    .then((response) => {
      if (response.status != 200) {
        document.getElementById("notification").innerHTML =
          "Incorect username or password";
      }
      return response.json();
    }) // Parse response as JSON
    .then((data) => {
      localStorage.setItem("user", JSON.stringify(data));
      if (data.role_Id == 1) {
        window.location.href = "bookRequestAdmin.html";
      } else if (data.role_Id == 2) {
        window.location.href = "bookRequestStaff.html";
      } else {
        window.location.href = "index.html";
      }
    })
    .catch((error) => {
      console.error("Error occurred during login:", error);
    });
}

function sendPasswordResetEmail() {
  const email = document.getElementById("email").value;

  const api_recoverPassword = `https://book0209.azurewebsites.net/api/user/recoverPassword?email=${encodeURIComponent(
    email
  )}`;
  console.log(`${encodeURIComponent(email)}`);
  fetch(api_recoverPassword)
    .then((response) => {
      if (response.status === 200) {
        alert("Password reset email sent successfully.");
      } else if (response.status === 400) {
        alert("Email not found or other error.");
      } else {
        alert("Unexpected status code: " + response.status);
      }
    })
    .catch((error) => {
      console.error("Error sending password reset email:", error);
      alert("Error sending password reset email. Please try again later.");
    });
}
