function highlightInput(input) {
    // alert("vy khoi  ")
    // Xóa class "highlight" khỏi tất cả các thẻ input
    var inputs = document.getElementsByClassName("form-item");
    for (var i = 0; i < inputs.length; i++) {
        inputs[i].classList.remove("highlight");
    }

    // Thêm class "highlight" vào thẻ input được nhấp vào
    input.classList.add("highlight");
}
function isValidEmail(email) {
    // Biểu thức chính quy để kiểm tra định dạng email
    var emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return emailPattern.test(email);
}
function isValidPassword(password) {
    return password.length >= 6;
}
function login(event) {
    //event.preventDefault();
    console.log("co vao");
    let iUserName = document.getElementById("userName");
    let iPassWord = document.getElementById("passWord");

    let boxUs = document.getElementById("form-item-username");
    let BoxPa = document.getElementById("form-item-password");


    let alertBox = document.getElementById("alert-container");

    //   var inputs = document.getElementsByClassName("form-item");

    let us = iUserName.value;
    let pa = iPassWord.value;

    // check username empty
    if (us === "") {

        console.log(us.value);
        boxUs.classList.add('login_wrong')

        alertBox.classList.add("login_wrong");
        alertBox.style.display = "block";
        // return
    }

    // check username not empty
    if (us !== "") {

        boxUs.classList.remove('login_wrong')

        let inputEmail2 = document.getElementById("alert-input-email");
        inputEmail2.style.display = "none";

        alertBox.classList.add("login_wrong");
        alertBox.style.display = "block";
    }


    //   check pass empty
    if (pa === "") {
        BoxPa.classList.add('login_wrong')

        alertBox.classList.add("login_wrong");
        alertBox.style.display = "block";
    }
    // check pass not empty 
    if (pa !== "") {

        BoxPa.classList.remove('login_wrong')

        let inputEmail2 = document.getElementById("alert-input-password");
        inputEmail2.style.display = "none";

        alertBox.classList.add("login_wrong");
        alertBox.style.display = "block";

        console.log("co vao check đúng")
    }



    var listAlert = document.getElementsByClassName('item-alert');

    if (us != "" && pa != "") {
        alertBox.style.display = 'none'
        for (let i = 0; i < listAlert.length; i++) {
            listAlert[i].style.display = 'block'
        }
    }



}



// lưu danh sách accpet checkout để đẩy xuống back end 
var highlightedCheckout = [];
function hightLight(id) {
    console.log("có vô hightLight", id)
    var row = document.getElementById(id);
    row.classList.toggle('acpect'); // Toggle class 'acpect' cho phần tử



    // toggle id để gửi xuống back end
    // Nếu id đã có trong danh sách, gỡ id đó ra khỏi danh sách
    if (highlightedCheckout.includes(id)) {
        highlightedCheckout = highlightedCheckout.filter(item => item !== id);
    } else {
        // Nếu id chưa có trong danh sách, thêm id đó vào danh sách
        highlightedCheckout.push(id);
    }
}

function saveListAcceptCheckout() {

    console.log("co vô để gửi")
    fetch('/save-check-out/', {
        method: 'POST', // Phương thức HTTP (POST trong trường hợp này)
        headers: {
            'Content-Type': 'application/json' // Định dạng dữ liệu là JSON
        },
        body: JSON.stringify(highlightedCheckout) // Chuyển đổi danh sách thành chuỗi JSON và gửi xuống backend
    })
        .then(response => response.json()) // Chuyển đổi phản hồi từ backend thành đối tượng JSON
        .then(data => {
            console.log(data); // In phản hồi từ backend ra console để kiểm tra
            // Có thể thực hiện các thao tác tiếp theo với dữ liệu nhận được từ backend
        })
        .catch(error => {
            console.error('Lỗi khi gửi dữ liệu xuống backend:', error); // Xử lý lỗi nếu có
        });
    console.log(highlightedCheckout); // In danh sách id ra console để kiểm tra
}