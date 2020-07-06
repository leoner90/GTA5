//DISCONECT
mp.events.add("disconnect", () => {
    var disconnectBrowser = mp.browsers.new("package://login/Cef/index.html");
    mp.gui.cursor.show(true, true);
});

