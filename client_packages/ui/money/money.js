require("admin/noclip/index.js");
require("VehManager/vehicleManager.js");
require("login/login.js");
require("colshapes/index.js");
require("blips/index.js");

//require("charcreator/index.js");

mp.gui.chat.show(true);
mp.gui.chat.activate(false);


// 0x75 is the F6 key code
mp.keys.bind(0x75, true, function () { 
    mp.gui.chat.push('F6 key is pressed.');
});