var santa = mp.peds.new(0x303638A7, new mp.Vector3(2057.905, 5004.55, 40.65411), 258.3119, 0);  
var doctor = mp.peds.new(0xD47303AC, new mp.Vector3(262.4005, -1359.777, 24.53778), 70.3119, 0);
var ag = null;

//SANTA MARKER FOR EVENT
mp.markers.new(1, new mp.Vector3(2058.905, 5004.55, 39), 1, {
    direction: new mp.Vector3(2058.905, 5004.55, 39),
    rotation: new mp.Vector3(0, 0, 0),
    color: [244, 244, 244, 255],
    visible: true,
    dimension: 0
});

//HOSPITAL DOOR MARKER FOR EVENT
mp.markers.new(0, new mp.Vector3(275.8393, -1361.423, 24.6), 1, {
    direction: new mp.Vector3(275.8393, -1361.423, 23),
    rotation: new mp.Vector3(0, 0, 0),
    color: [244, 244, 244, 255],
    visible: true,
    dimension: 0
});

//FLY MARKER FOR EVENT
mp.markers.new(0, new mp.Vector3(2062.905, 5004.55, 39.5), 1, {
    direction: new mp.Vector3(2062.905, 5004.55, 39),
    rotation: new mp.Vector3(0, 0, 0),
    color: [46, 204, 113, 255],
    visible: true,
    dimension: 0
});

//COLSHAPES VECTOR
var santaEvent = mp.colshapes.newSphere(2058.905, 5004.55, 40.65411, 1);
var flyEvent = mp.colshapes.newSphere(2062.905, 5004.55, 40.65411, 1);
var hospitalEvent = mp.colshapes.newSphere(275.8393, -1361.423, 24.5378, 1);
var prisoner = mp.colshapes.newSphere(462.7471, -993.7837, 24.914875, 1);


//ON PLAYER ENTER COLSHAPE
mp.events.add('playerEnterColshape', (shape) => {
    if (shape == santaEvent) {
        mp.gui.chat.push("!{Orange}[SANTA] !{White}ХОЧЕШЬ Я ПОКАЖУ ТЕБЕ ФОКУС?");
        santa.destroy();
        ag = mp.peds.new(0xCE5FF074, new mp.Vector3(2057.905, 5004.55, 40.65411), 258.3119, 0);
    } else if (shape == hospitalEvent) {
        mp.events.callRemote("hospitalExit");
    } else if (shape == flyEvent) {
        mp.events.callRemote("teleport");
    } 
});

//ON PLAYER EXIT COLSHAPE
function playerExitColshapeHandler(shape) {
    if (shape == santaEvent) {
        ag.destroy();
        santa = mp.peds.new(0x303638A7, new mp.Vector3(2057.905, 5004.55, 40.65411), 258.3119, 0);
    }
    else if (shape == prisoner) {
        mp.events.callRemote("isArrested");
    }
}
//Call Fun on exit from callshape
mp.events.add("playerExitColshape", playerExitColshapeHandler);