var moneyBrowser = null;

mp.events.add("LoadMoney", (amount) => {
    moneyBrowser = mp.browsers.new("package://money/Cef/money.html");
    moneyBrowser.execute(`$("#money").html("${(amount)}");`);
    mp.players.local.setMoney(amount);
})
