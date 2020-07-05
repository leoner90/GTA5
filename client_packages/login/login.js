var cam = mp.cameras.new('default', new mp.Vector3(-95, 19, 1182), new mp.Vector3(0, 0, 0), 70);
cam.pointAtCoord(-95, 19, 0);
cam.setActive(true);
mp.game.cam.renderScriptCams(true, false, 0, true, false);

var browser1 = mp.browsers.new("package://login/Cef/index.html");
mp.gui.cursor.show(true, true);

mp.events.add("loginDataToServer", (state ,user,email, pass,passRepeat ) => {
    mp.events.callRemote("loginDataToServer", state, user, email, pass, passRepeat);
});

mp.events.add("loginHandler", (handle) => {
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
                browser1.execute(`$(".incorrect-info").show().html(handle); $("#loginBtn").show();`);
            break;
        }
        case "takeninfo":
        {
            browser1.execute(`$(".taken-info").show(); $("#registerBtn").show();`);
            break;
        }
        case "tooshort":
        {
            browser1.execute(`$(".short-info").show(); $("#registerBtn").show();`);
            break;
        }
        default:
        {
            break;
        }
    }
});