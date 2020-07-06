var cam = mp.cameras.new('default', new mp.Vector3(-95, 19, 1182), new mp.Vector3(0, 0, 0), 70);
cam.pointAtCoord(-95, 19, 0);
cam.setActive(true);
mp.game.cam.renderScriptCams(true, false, 0, true, false);
 

var browser1 = mp.browsers.new("package://login/Cef/index.html");
mp.gui.cursor.show(true, true);

mp.events.add("loginDataToServer", (state ,user,email, pass,passRepeat ) => {
    mp.events.callRemote("loginDataToServer", state, user, email, pass, passRepeat);
});

mp.events.add("loginHandler", (handle , error) => {
    switch(handle){
        case "success":
        {
            browser1.destroy();
            mp.gui.cursor.show(false, false);
            mp.gui.chat.push("Login successful");
            mp.gui.chat.activate(true);
            mp.game.cam.renderScriptCams(false, true, 3000, true, true);
            break;
        }
        case "registered":
        {
            browser1.destroy();
            mp.gui.cursor.show(false, false);
            mp.gui.chat.push("Registration successful");
            mp.gui.chat.activate(true);
            mp.game.cam.renderScriptCams(false, true, 3000, true, true);
            break;
        }
        case "incorrectinfo":
            {
                browser1.execute(`$("#loginBtn, #registerBtn").prop('disabled', true);`);
                browser1.execute(`$("#loginBtn, #registerBtn").html("Disabled");`);

                var timer = 5;
                var setTimeOut = setInterval(setTimeOut, 1000);
                function setTimeOut() {
                    timer--;
                    browser1.execute(`$("#loginBtn, #registerBtn").html("Wait: "+${timer});`);
                    if (timer <= 0) {
                        browser1.execute(`$("#loginBtn").prop('disabled', false).html("Login");`);  
                        browser1.execute(`$("#registerBtn").prop('disabled', false).html("Зарегистрироваться");`);  
                        clearInterval(setTimeOut);
                    }
                }
             
                browser1.execute(`$(".errors, #loginBtn, #registerBtn").show();$(".errors").empty(); `);
                for (var i = 0; i < error.length; i++) {
                    if (error[i] != null) {
                        browser1.execute(`$(".errors").append("<div class='alert alert-danger'>${error[i]}</div>") `);
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