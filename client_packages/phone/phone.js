var phoneBrowser = null;

// 0x76 is the F7 key code
mp.keys.bind(0x76, true, function () {
    if (phoneBrowser != null) {
        phoneBrowser.destroy();
        phoneBrowser = null;
        mp.gui.cursor.show(false, false);
    } else {
        phoneBrowser = mp.browsers.new("package://phone/Cef/index.html");
        mp.gui.cursor.show(true, true);
    }
});