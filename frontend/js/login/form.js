function validate() {
  var username = document.getElementById("username").value;
  var password = document.getElementById("password").value;

  if (username === "") {
    document.getElementById("username").style.border = "2px solid red";
    document.getElementById("username").focus();
    document.getElementById("error-message").innerHTML = "Please enter your username";
    return false;
  }

  if (password === "") {
    document.getElementById("password").style.border = "2px solid red";
    document.getElementById("password").focus();
    document.getElementById("error-message").innerHTML = "Please enter your password";
    return false;
  }

  alert("Login successful!");
}