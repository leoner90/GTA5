function sendAccountInfo(state) {
    $('.errors').hide();
    //Login State
    if(state === 0){   
        let login = loginName.value;
        let Psw = loginPass.value;
        $("#loginBtn").hide();
        mp.trigger("loginDataToServer", state, login, "", Psw, "");
    //Register State
    } else {    
        let registerName = document.getElementById("registerName");
        let Email = document.getElementById("email");
        let Psw = document.getElementById("regPsw");
        let PswRepeat = document.getElementById("regPsw-repeat"); 
        $("#registerBtn").hide();
        mp.trigger("loginDataToServer", state, registerName.value, Email.value, Psw.value, PswRepeat.value);
    }
}
