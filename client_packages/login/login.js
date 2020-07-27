//To Show cursor will not work if it's not a function
mp.events.add("cursorShow", () => {
    mp.gui.cursor.show(true, true);
})
mp.gui.cursor.show(true, true);
//SET CAMERA POSITION
var cam = mp.cameras.new('default', new mp.Vector3(-95, 19, 1182), new mp.Vector3(0, 0, 0), 70);
cam.pointAtCoord(-95, 19, 0);
cam.setActive(true);
mp.game.cam.renderScriptCams(true, false, 0, true, false);

//CREATE BROWSER LOAD HTML AND SHOW CURSOR
var loginBrowser = mp.browsers.new("package://login/Cef/index.html");
loginBrowser.execute(`mp.events.call("cursorShow");`);

//EVENT OCCURS WHEN PLAYER PRESS LOGIN OR REGISTRATION BTN , AND SEND DATA FROM FIeLDS TO SERVER (var state= reg or login 0 or 1)
mp.events.add("loginDataToServer", (state ,user,email, pass,passRepeat ) => {
    mp.events.callRemote("loginDataToServer", state, user, email, pass, passRepeat);
});

//SERVER CHECKS DATA AND EXECUTES loginhandler with succes or registred or error(shows errors also disable buttons for 5 sec)
//In case of success or registration calls function charSelect and sends information there
mp.events.add("loginHandler", (handle, error, userName, password, character1 , name ,surname) => {
    switch(handle){
        case "success":
        {
            loginBrowser.destroy();
            mp.events.call("charSelect", userName, password, character1, name, surname); 
            mp.gui.chat.push("Login successful");
            break;
        }
        case "registered":
        {
            loginBrowser.destroy();
            mp.gui.chat.push("Registration successful");
                mp.events.call("charSelect", userName, password, character1, name, surname); 
            break;

        }
        case "incorrectinfo":
            {
                loginBrowser.execute(`$("#loginBtn, #registerBtn").prop('disabled', true);`);
                loginBrowser.execute(`$("#loginBtn, #registerBtn").html("Disabled");`);

                var timer = 5;
                var setTimeOut = setInterval(timeOutForRegAndLoginBtn, 1000);
                function timeOutForRegAndLoginBtn() {
                    timer--;
                    loginBrowser.execute(`$("#loginBtn, #registerBtn").html("Wait: "+${timer});`);
                    if (timer <= 0) {
                        loginBrowser.execute(`$("#loginBtn").prop('disabled', false).html("Login");`);  
                        loginBrowser.execute(`$("#registerBtn").prop('disabled', false).html("Зарегистрироваться");`);  
                        clearInterval(setTimeOut);
                    }
                }
             
                loginBrowser.execute(`$(".errors, #loginBtn, #registerBtn").show();$(".errors").empty(); `);
                for (var i = 0; i < error.length; i++) {
                    if (error[i] != null) {
                        loginBrowser.execute(`$(".errors").append("<div class='alert alert-danger'>${error[i]}</div>") `);
                    }
                } 
            break;
        }
        default:
        {
            break;
        }
    }
});
