var time = 10;
var i = 0;
var weather = ["EXTRASUNNY", "CLEAR", "CLOUDS", "SMOG", "FOGGY", "OVERCAST", "RAIN", "THUNDER", "CLEARING", "NEUTRAL", "SNOW", "BLIZZARD", "SNOWLIGHT", "XMAS","HALLOWEEN"];
setInterval(function () {
    time = time + 1;
    i = i + 1;
    if (time == 25) {
        time = 0;
    }
    if (i == 15) {
        i = 0;
    }
    mp.events.callRemote("serverTimeAndWeather", time, weather[i]);
}, 30000);