require("admin/noclip/noclip.js");
require("blips/blips.js");
require("vehManager/vehicleManager.js");
require("login/login.js");
require("colshapes/colshapes.js");
require("weatherAndTime/weatherAndTime.js");
require("speedo/speedo.js");
require("charCreator/charSelect.js");
require("phone/phone.js");
require("money/money.js");
require("voice/voice.js");

// 0x75 is the F4 key code
var cursor = true;
mp.keys.bind(0x73, true, function () {
    if (cursor == true) {
        mp.gui.cursor.show(false, false);
        cursor = false;
    } else {
        mp.gui.cursor.show(true, true);
        cursor = true;
    }
});

mp.gui.chat.show(false);
mp.gui.chat.activate(false);
