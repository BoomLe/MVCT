//function highlightInput(input) {
//    // alert("vy khoi  ")
//    // Xóa class "highlight" khỏi tất cả các thẻ input
//    var inputs = document.getElementsByClassName("form-item");
//    for (var i = 0; i < inputs.length; i++) {
//        inputs[i].classList.remove("highlight");
//    }

//    // Thêm class "highlight" vào thẻ input được nhấp vào
//    input.classList.add("highlight");
//}
//function isValidEmail(email) {
//    // Biểu thức chính quy để kiểm tra định dạng email
//    var emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
//    return emailPattern.test(email);
//}
//function isValidPassword(password) {
//    return password.length >= 6;
//}
//function login(event) {
//    //event.preventDefault();
//    console.log("co vao");
//    let iUserName = document.getElementById("userName");
//    let iPassWord = document.getElementById("passWord");

//    let boxUs = document.getElementById("form-item-username");
//    let BoxPa = document.getElementById("form-item-password");


//    let alertBox = document.getElementById("alert-container");

//    //   var inputs = document.getElementsByClassName("form-item");

//    let us = iUserName.value;
//    let pa = iPassWord.value;

//    // check username empty
//    if (us === "") {

//        console.log(us.value);
//        boxUs.classList.add('login_wrong')

//        alertBox.classList.add("login_wrong");
//        alertBox.style.display = "block";
//        // return
//    }

//      // check username not empty
//    if (us !== "") {
      
//        boxUs.classList.remove('login_wrong')

//        let inputEmail2 = document.getElementById("alert-input-email");
//        inputEmail2.style.display = "none";

//        alertBox.classList.add("login_wrong");
//        alertBox.style.display = "block";
//    }


//    //   check pass empty
//    if (pa === "") {
//        BoxPa.classList.add('login_wrong')

//        alertBox.classList.add("login_wrong");
//        alertBox.style.display = "block";
//    }
//    // check pass not empty 
//    if (pa !== "") {
  
//        BoxPa.classList.remove('login_wrong')

//        let inputEmail2 = document.getElementById("alert-input-password");
//        inputEmail2.style.display = "none";

//        alertBox.classList.add("login_wrong");
//        alertBox.style.display = "block";

//        console.log("co vao check đúng")
//    }



//    var listAlert = document.getElementsByClassName('item-alert');

//    if (us != "" && pa != "") {
//        alertBox.style.display = 'none'
//        for (let i = 0; i < listAlert.length; i++) {
//            listAlert[i].style.display = 'block'
//        }
//    }



//}//function highlightInput(input) {
//    // alert("vy khoi  ")
//    // Xóa class "highlight" khỏi tất cả các thẻ input
//    var inputs = document.getElementsByClassName("form-item");
//    for (var i = 0; i < inputs.length; i++) {
//        inputs[i].classList.remove("highlight");
//    }

//    // Thêm class "highlight" vào thẻ input được nhấp vào
//    input.classList.add("highlight");
//}
//function isValidEmail(email) {
//    // Biểu thức chính quy để kiểm tra định dạng email
//    var emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
//    return emailPattern.test(email);
//}
//function isValidPassword(password) {
//    return password.length >= 6;
//}
//function login(event) {
//    //event.preventDefault();
//    console.log("co vao");
//    let iUserName = document.getElementById("userName");
//    let iPassWord = document.getElementById("passWord");

//    let boxUs = document.getElementById("form-item-username");
//    let BoxPa = document.getElementById("form-item-password");


//    let alertBox = document.getElementById("alert-container");

//    //   var inputs = document.getElementsByClassName("form-item");

//    let us = iUserName.value;
//    let pa = iPassWord.value;

//    // check username empty
//    if (us === "") {

//        console.log(us.value);
//        boxUs.classList.add('login_wrong')

//        alertBox.classList.add("login_wrong");
//        alertBox.style.display = "block";
//        // return
//    }

//      // check username not empty
//    if (us !== "") {
      
//        boxUs.classList.remove('login_wrong')

//        let inputEmail2 = document.getElementById("alert-input-email");
//        inputEmail2.style.display = "none";

//        alertBox.classList.add("login_wrong");
//        alertBox.style.display = "block";
//    }


//    //   check pass empty
//    if (pa === "") {
//        BoxPa.classList.add('login_wrong')

//        alertBox.classList.add("login_wrong");
//        alertBox.style.display = "block";
//    }
//    // check pass not empty 
//    if (pa !== "") {
  
//        BoxPa.classList.remove('login_wrong')

//        let inputEmail2 = document.getElementById("alert-input-password");
//        inputEmail2.style.display = "none";

//        alertBox.classList.add("login_wrong");
//        alertBox.style.display = "block";

//        console.log("co vao check đúng")
//    }



//    var listAlert = document.getElementsByClassName('item-alert');

//    if (us != "" && pa != "") {
//        alertBox.style.display = 'none'
//        for (let i = 0; i < listAlert.length; i++) {
//            listAlert[i].style.display = 'block'
//        }
//    }



//}
