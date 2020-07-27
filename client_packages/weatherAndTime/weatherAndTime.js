var time = 10;
var i = 0;
var weather = ["EXTRASUNNY", "CLEAR", "CLOUDS", "SMOG", "OVERCAST", "RAIN", "THUNDER", "RAIN", "OVERCAST",  "CLEARING", "SNOW", "BLIZZARD", "SNOWLIGHT", "XMAS"];
setInterval(function () {
    time = time + 1;
    i = i + 1;
    if (time == 25) {
        time = 0;
    }
    if (i == 14) {
        i = 0;
    }
    mp.events.callRemote("serverTimeAndWeather", time, weather[i]);
}, 60000);


