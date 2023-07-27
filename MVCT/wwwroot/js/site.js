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
function generatePDF() {
    // Tạo một đối tượng jsPDF mới
 
        window.print();
 
}

function hello() {
    console.log("hello");
}

// lưu danh sách accpet checkout để đẩy xuống back end 
var highlightedCheckout = [];
function hightLight(id) {
    console.log("có vô hightLight", id)
    var row = document.getElementById(id);
    row.classList.toggle('click-choose-row'); // Toggle class 'acpect' cho phần tử



    // toggle id để gửi xuống back end
    // Nếu id đã có trong danh sách, gỡ id đó ra khỏi danh sách
    if (highlightedCheckout.includes(id)) {
        highlightedCheckout = highlightedCheckout.filter(item => item !== id);
    } else {
        // Nếu id chưa có trong danh sách, thêm id đó vào danh sách
        highlightedCheckout.push(id);
    }
}

function register(returnUrl) {
    console.log("có vào")
    let us = document.getElementById("registerModel_UserName").value
    let email = document.getElementById("registerModel_Email").value
    let pass = document.getElementById("registerModel_Password").value
    let confirm = document.getElementById("registerModel_ConfirmPassword").value

    console.log(us)
    // gửi data

    fetch('/register-acount/', {
        method: 'POST', // Phương thức HTTP (POST trong trường hợp này)
        headers: {
            'Content-Type': 'application/json' // Định dạng dữ liệu là JSON
        },
        body: JSON.stringify({
            UserName: us, // Danh sách id đã được chọn
            Email: email,
            Password: pass,// Ngày đã chọn,
            ConfirmPassword: confirm,
            returnUrl: returnUrl
        }) // Chuyển đổi danh sách thành chuỗi JSON và gửi xuống backend
    })
        .then(response => response.json()) // Chuyển đổi phản hồi từ backend thành đối tượng JSON
        .then(data => {
            console.log("back end gửi về", data); // In phản hồi từ backend ra console để kiểm tra
            // Có thể thực hiện các thao tác tiếp theo với dữ liệu nhận được từ backend
            //if (data.success) {
            //    location.reload(); // Load lại trang nếu data.Success là true
            //}
        })
        .catch(error => {
            console.error('Lỗi khi gửi dữ liệu xuống backend:', error); // Xử lý lỗi nếu có
        });


}
function saveListAcceptCheckout(idMN) {

    let date = document.getElementById("selectedDate").value;

    console.log("co vô để gửi", highlightedCheckout, "ngày là ", date)
    fetch('/save-check-out/', {
        method: 'POST', 
        headers: {
            'Content-Type': 'application/json' 
        },
        body: JSON.stringify({
            highlightedCheckout: highlightedCheckout, // Danh sách id đã được chọn
            date: date,
            idManagerCheckOut : idMN// Ngày đã chọn
        }) // Chuyển đổi danh sách thành chuỗi JSON và gửi xuống backend
    })
        .then(response => response.json()) // Chuyển đổi phản hồi từ backend thành đối tượng JSON
        .then(data => {
            console.log("back end gửi về" ,data); // In phản hồi từ backend ra console để kiểm tra
            // Có thể thực hiện các thao tác tiếp theo với dữ liệu nhận được từ backend
            if (data.success) {
                location.reload(); // Load lại trang nếu data.Success là true
            }
        })
        .catch(error => {
            console.error('Lỗi khi gửi dữ liệu xuống backend:', error); // Xử lý lỗi nếu có
        });
    console.log(highlightedCheckout); // In danh sách id ra console để kiểm tra
}


function showRegisterBox() {
    var registerBox = document.getElementById("registerbox");
    console.log("co vao")
    // Toggle class "active" cho "registerbox"
    if (registerBox.classList.contains("active-register-box")) {
        registerBox.classList.remove("active-register-box");
        registerBox.classList.add("no-active-register-box");
    } else {
        registerBox.classList.add("active-register-box");
        registerBox.classList.remove("no-active-register-box");
    }
}

function showAlterTimeSheetBox(id) {
    var registerBox = document.getElementById("alter-row-timesheet");
    let idTimeSheet = document.getElementById("Id-TimeSheet");
    idTimeSheet.value = id

    console.log("co vao")
    // Toggle class "active" cho "registerbox"
    if (registerBox.classList.contains("active-register-box")) {
        registerBox.classList.remove("active-register-box");
        registerBox.classList.add("no-active-register-box");
    } else {
        registerBox.classList.add("active-register-box");
        registerBox.classList.remove("no-active-register-box");
    }
}

function showDeleteTimeSheetBox(id) {
    var registerBox = document.getElementById("alter-delete");
    let idTimeSheet = document.getElementById("confirm-delete-input");
    idTimeSheet.value = id

    console.log("co vao")
    // Toggle class "active" cho "registerbox"
    if (registerBox.classList.contains("active-register-box")) {
        registerBox.classList.remove("active-register-box");
        registerBox.classList.add("no-active-register-box");
    } else {
        registerBox.classList.add("active-register-box");
        registerBox.classList.remove("no-active-register-box");
    }
}

function toggleElement() {
    var element = document.getElementById("toggleElement");
    var arrow = document.getElementById("arrowToClick");

    console.log(arrow)
    var currentMargin = parseInt(getComputedStyle(element).marginLeft);

    if (currentMargin >= -5) {
        element.style.marginLeft = "-125px"; // Giả sử thành phần có chiều rộng 100px
        arrow.style.marginLeft = "12px";

    } else {
        element.style.marginLeft = "10px";
        arrow.style.marginLeft = "-23px"
    }
}
function toggleElement2() {
    var element = document.getElementById("logoutForm");
    var arrow = document.getElementById("arrowToClick2");

    //console.log(arrow)
    var currentMargin = parseInt(getComputedStyle(element).marginLeft);

    if (currentMargin >= -5) {
        element.style.marginLeft = "-200px"; // Giả sử thành phần có chiều rộng 100px
        arrow.style.marginLeft = "12px";

    } else {
        element.style.marginLeft = "25px";
        arrow.style.marginLeft = "-102px"
    }
}

let isRecaptchaConfirmed = false;

function setUpReponseRecaptcha() {
    console.log("có vào")
    var response = grecaptcha.getResponse();
    console.log("mã recaptcha là ",response)
}

function recaptchaCallback(response) {
    // Lưu giá trị response vào thẻ input
    let rp = document.getElementById("ReponseCaptcha")
    
    rp.value = response;
    isRecaptchaConfirmed = true;
    console.log("mã capcha la", rp.value)
    console.log("thẻ value", rp)

}



/// gọi lấy address
// Hàm để thực hiện yêu cầu khi trang web vừa tải lên
var listPlace;
function selectCity(id) {
    let boxCity = document.getElementById("PlacedistrictId");
    boxCity.innerHTML = "";
    console.log(boxCity);
    console.log("danh sách",listPlace);

    for (let i = 0; i < listPlace.length; i++) {
        if (listPlace[i].dependId == id) {

            console.log("có vào", listPlace[i].dependId,id )
            let option = document.createElement("option");
            option.value = listPlace[i].id;
            option.text = listPlace[i].place;

            boxCity.appendChild(option);
        }
    }
    console.log(id);
}

function getDataOnPageLoad() {
    fetch('/Address/GetAddress/') // Thay thế '/api/endpoint' bằng URL của endpoint backend của bạn
        .then(response => response.json())
        .then(data => {
            console.log('Data từ backend:', data);
            listPlace = data;

            let boxCity = document.getElementById("PlaceCityId");
            boxCity.innerHTML = "";
            boxCity.innerHTML = '<option value="">-- Chọn tỉnh/thành phố --</option>';
            for (let i = 0; i < listPlace.length; i++) {
                if (listPlace[i].dependId === 0) {
                    let option = document.createElement("option");
                    option.value = listPlace[i].id;
                    option.text = listPlace[i].place;

                    boxCity.appendChild(option);
                }
            }
           
            // Gán sự kiện change cho combobox
            boxCity.addEventListener("change", function () {
                let selectedOption = boxCity.options[boxCity.selectedIndex];
                if (selectedOption !== null) {
                    selectCity(selectedOption.value); // Gọi hàm selectCity với giá trị id của tùy chọn
                }
            });
        })
        .catch(error => {
            // Xử lý lỗi nếu có
            console.error('Error:', error);
        });
}
// Gọi hàm getDataOnPageLoad() khi trang web đã được tải lên hoàn toàn
document.addEventListener('DOMContentLoaded', getDataOnPageLoad);


function addAddressForUser() {
    let idUser = document.getElementById("idUserAddAddress").value
    let idCity = document.getElementById("PlaceCityId").value
    let idDistrict = document.getElementById("PlacedistrictId").value




    const dataToSend = {
        IdUser: idUser,
        IdCity: idCity,
        IdDistrict: idDistrict
    };

    fetch('/Member/AddressForUser/', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(dataToSend)
    })
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok.');
            }
            return response.json(); // Chuyển đổi response thành JSON và trả về promise
        })
        .then(dataFromServer => {
            // Xử lý dữ liệu từ backend ở đây
            console.log('Data from server:', dataFromServer);
            // Bạn có thể sử dụng dữ liệu từ server trong hàm này
            if (dataFromServer.succcess) {
                // Nếu dữ liệu được xử lý thành công trên server, reload lại trang web
                location.reload();
            } else {
                // Xử lý trường hợp dữ liệu không thành công nếu cần
            }
        })
        .catch(error => {
            // Xử lý lỗi kết nối hoặc response lỗi
            console.error('Error:', error);
        });

}

function validateFormRegister(event) {

    //event.preventDefault();

   
    // check select 
    const selectCity = document.getElementById('PlaceCityId');
    const selectDistrict = document.getElementById('PlacedistrictId');

    const cityValue = selectCity.value;
    const districtValue = selectDistrict.value;

    console.log("có chạy vô hàm", cityValue, districtValue)
    if (cityValue == '' || districtValue == '') {
        event.preventDefault();
        // Hiển thị thông báo hoặc thực hiện các hành động khác khi cần
        alert('Please select both City and District.');
        return false; // Ngăn form được submit nếu chưa chọn đủ giá trị
    }

    // check captcha
    if (!isRecaptchaConfirmed) {
        event.preventDefault();
        alert('Please confirm reCAPTCHA before submitting the form.');
        return false; // Ngăn form được submit nếu chưa xác nhận reCAPTCHA
    }

    // Nếu cả hai thẻ đã chọn đủ giá trị, cho phép form được submit
    return true;
}
//Trong hàm validateForm(), chúng ta kiểm tra giá trị của cả hai thẻ < select >.Nếu một trong hai thẻ chưa có giá trị được chọn, chúng ta hiển thị thông báo và trả về false, điều này sẽ ngăn form được submit.Nếu cả hai thẻ đã chọn đủ giá trị, chúng ta trả về true, cho phép form được submit.

function deleteUserAddress(Id) {
    console.log(Id)
    //const dataToSend = { "Id: Id };

    fetch('/Member/DeleteUserAddress/', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: Id 
    })
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok.');
            }
            return response.json(); // Chuyển đổi response thành JSON và trả về promise
        })
        .then(dataFromServer => {
            // Xử lý dữ liệu từ backend ở đây
            console.log('Data from server:', dataFromServer);
            // Bạn có thể sử dụng dữ liệu từ server trong hàm này
            if (dataFromServer.succcess) {
                // Nếu dữ liệu được xử lý thành công trên server, reload lại trang web
                location.reload();
            } else {
                // Xử lý trường hợp dữ liệu không thành công nếu cần
            }
        })
        .catch(error => {
            // Xử lý lỗi kết nối hoặc response lỗi
            console.error('Error:', error);
        });
}






