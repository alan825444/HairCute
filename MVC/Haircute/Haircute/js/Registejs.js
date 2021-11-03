document.getElementById("fEmail").addEventListener("blur", check_email);
function check_email() {
    var email = document.getElementById("fEmail").value;
    var emailmsg = document.getElementById("emailaddress_msg");
    var regemail = /^\w+\@\w+((\.|-)\w+)*\.[A-Za-z]+$/;
    if (email == "" || email.trim() == "") {
        emailmsg.innerHTML = "<span class='Errorstatus'>請輸入信箱<i class='fas fa-times-circle'></i></span>";
    } else if (!regemail.test(email)) {
        emailmsg.innerHTML = "<span class='Errorstatus'>信箱不正確<i class='fas fa-times-circle'></i></span>";
    } else {
        emailmsg.innerHTML = "<span class='Okstatus'><i class='fas fa-check-circle'></i></span>";
    }
}

    document.getElementById("fPwd").addEventListener("blur", check_pwd);
    function check_pwd() {
            var pwd = document.getElementById("fPwd").value;
    var pwdmsg = document.getElementById("pwd_msg");
    var regpwd = /^(?=.*\d)(?=.*[a-zA-Z]).{6, 15}$/;
    if (pwd == "" || pwd.trim() == "") {
        pwdmsg.innerHTML = "<span class='Errorstatus'>請輸入密碼<i class='fas fa-times-circle'></i></span>";
            } else if (!regpwd.test(pwd)) {
        pwdmsg.innerHTML = "<span class='Errorstatus'>格式不正確<i class='fas fa-times-circle'></i></span>";
            } else {
        pwdmsg.innerHTML = "<span class='Okstatus'><i class='fas fa-check-circle'></i></span>";
            }
        }
    document.getElementById("fUsername").addEventListener("blur", check_name);
    function check_name() {
            var name = document.getElementById("fUsername").value;
    var namemsg = document.getElementById("username_msg");
    var regname = /^[\u4e00-\u9fa5]+$/;
    if (name == "" || name.trim() == "") {
        namemsg.innerHTML = "<span class='Errorstatus'>請輸入姓名<i class='fas fa-times-circle'></i></span>";
            } else if (!regname.test(name)) {
        namemsg.innerHTML = "<span class='Errorstatus'>姓名須為中文<i class='fas fa-times-circle'></i></span>";
            } else {
        namemsg.innerHTML = "<span class='Okstatus'><i class='fas fa-check-circle'></i></span>";
            }
        }
    document.getElementById("fBirth").addEventListener("blur", check_birthday);
    function check_birthday() {
            var birthday = document.getElementById("fBirth").value;
    var birthdaymsg = document.getElementById("date_msg");
    var regbirthday = /^((0?[1-9]|[12][0-9]|3[01])[/.](0?[1-9]|1[012])[/.]((?:19|20)[0-9]{2}))*$/;
    if (birthday == "" || birthday.trim() == "") {
        birthdaymsg.innerHTML = "<span class='Errorstatus'>請輸入生日<i class='fas fa-times-circle'></i></span>";
            } else if (!regbirthday.test(birthday)) {
        birthdaymsg.innerHTML = "<span class='Errorstatus'>格式不正確<i class='fas fa-times-circle'></i></span>";
            } else {
        birthdaymsg.innerHTML = "<span class='Okstatus'><i class='fas fa-check-circle'></i></span>";
            }
        }
    document.getElementById("fCity").addEventListener("blur", check_address);
    document.getElementById("fArea").addEventListener("blur", check_address);
    function check_address() {
            var city = document.getElementById("fCity").value;
    var region = document.getElementById("fArea").value;
    var addressmsg = document.getElementById("address_msg");
    if (city == "-縣市-" || region == "--------") {
        addressmsg.innerHTML = "<span class='Errorstatus'>請選擇地址<i class='fas fa-times-circle'></i></span>";
            } else {
        addressmsg.innerHTML = "<span class='Okstatus'><i class='fas fa-check-circle'></i></span>";
            }
        }
    document.getElementById("fPhone").addEventListener("blur", check_phone);
    function check_phone() {
            var phone = document.getElementById("fPhone").value;
    var phonemsg = document.getElementById("phonenumber_msg");
    var regphone = /^09\d{8}$/;
    if (phone == "" || phone.trim() == "") {
        phonemsg.innerHTML = "<span class='Errorstatus'>請輸入手機號碼<i class='fas fa-times-circle'></i></span>";
            } else if (!regphone.test(phone)) {
        phonemsg.innerHTML = "<span class='Errorstatus'>格式不正確<i class='fas fa-times-circle'></i></span>";
            } else {
        phonemsg.innerHTML = "<span class='Okstatus'><i class='fas fa-check-circle'></i></span>";
            }
        }

$(function () {
    $(".datepicker").datepicker();
    $(".datepicker").datepicker('setDate', 'today');
});

