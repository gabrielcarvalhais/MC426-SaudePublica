function validate(event) {
  event.preventDefault()
  var email = document.getElementById("email").value;
  var password = document.getElementById("password").value;

  if (email === "") {
    document.getElementById("email").style.border = "2px solid red";
    document.getElementById("email").focus();
    return false;
  }

  if (password === "") {
    document.getElementById("password").style.border = "2px solid red";
    document.getElementById("password").focus();
    return false;
  }

  alert("Login successful!");
}
