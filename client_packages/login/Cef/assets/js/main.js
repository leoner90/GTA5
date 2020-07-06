function sendAccountInfo(state) {
    $('.errors').hide();
    if(state === 0){    //Login State
        let login = document.getElementById("loginName");
        let Psw = document.getElementById("loginPass");
        $("#loginBtn").hide();
    
        mp.trigger("loginDataToServer", state,  loginName.value, "", loginPass.value, "" );
    } else {    //Register State
        let registerName = document.getElementById("registerName");
        let Email = document.getElementById("email");
        let Psw = document.getElementById("regPsw");
        let PswRepeat = document.getElementById("regPsw-repeat");
       
        $("#registerBtn").hide();

        mp.trigger("loginDataToServer", state, registerName.value, Email.value, Psw.value, PswRepeat.value);
    }
}