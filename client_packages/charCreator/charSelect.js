mp.events.add("charSelect", (username, password, character1, name, surname) => {
    mp.gui.cursor.show(true, true);
    var CharSelectBrowser, CharCreatorBrowser = null;
    var CharSelectBrowser = mp.browsers.new("package://charCreator/Cef/charSelect.html");

    //IF CHARACTER EXIST -LOADS WELCOME MSG + ENTER BTN (Which triggers "LoadCharacter" Event)
    if (character1 != 0) {
        CharSelectBrowser.execute(`$('#charSelectImg').attr('src','img/existUser.png');`);
        CharSelectBrowser.execute(`$('#charSelectWelcomeText').html('Welcome:<br> ${name} ${surname}')`); 
        CharSelectBrowser.execute(`$('#charSelectBtn').html('Войти').click(function(){mp.trigger("LoadCharacter");});`);
    //IF NEW CHARACTER - LOADS MSG + BTN (Which triggers "CreateNewChar" Event)
    } else {
        CharSelectBrowser.execute(`$('#charSelectImg').attr('src','img/default.png');`);
        CharSelectBrowser.execute(`$('#charSelectWelcomeText').html('У ВАС НЕТУ ПЕРСОНАЖЕЙ.')`); 
        CharSelectBrowser.execute(`$('#charSelectBtn').html('СОЗДАТЬ').click(function(){mp.trigger("CreateNewChar");});`);  
    }

    //Creats new browser for character creation 
    mp.events.add("CreateNewChar", () => {  
        mp.game.ui.displayHud(false);
        CharSelectBrowser.destroy();
        CharSelectBrowser = null;
        CharCreatorBrowser = mp.browsers.new("package://charCreator/Cef/charCreate.html");  
        //sets camera , freeze player
        creatorCamera = mp.cameras.new("creatorCamera", new mp.Vector3(402.8664, -997.5515, -98.5), new mp.Vector3(0, 0, 0), 45);
        creatorCamera.pointAtCoord(new mp.Vector3(402.8664, -996.4108, -98.5)); //Changes the rotation of the camera to point towards a location
        creatorCamera.setActive(true);
        mp.game.cam.renderScriptCams(true, false, 0, false, false);
        mp.players.local.freezePosition(true);
        //Calls server event which teleports player to character creation location
        mp.events.callRemote("charTp");
    })

    //ON FIRST CHARACTER REGISTRATION
    mp.events.add("saveCharacter", (name, surname, CharacterStyleJson) => {
        mp.events.callRemote("saveCharacter", username, name, surname, CharacterStyleJson);
    })

    //ON LOG IN (EXISTENT CHARACTER)
    mp.events.add("LoadCharacter", () => {
        mp.events.callRemote("charDataLoading" , username,password);
    })

    //ON CHARACTER LOADING
    mp.events.add("CharLoadFinish", () => {
         // destroy all browsers
        if (CharSelectBrowser != null) {
            CharSelectBrowser.destroy();
        }
        if (CharCreatorBrowser != null) {
            CharCreatorBrowser.destroy();
        }
         // delete cursor , set camera position , unfreeze player , enable chat.
        mp.gui.cursor.show(false, false);
        mp.game.cam.renderScriptCams(false, false, 0, true, true); //(render, ease, easeTime, p3, p4);
        mp.players.local.freezePosition(false);
        mp.gui.chat.show(true);
        mp.gui.chat.activate(true);
    })
    //If name is too short or empty
    mp.events.add("nameErr", () => {
        CharCreatorBrowser.execute(`$('#nameError').hide().show(); $(window).scrollTop(0);`);
    })
})

//Change Character Appearance on character creation event (front end)
mp.events.add("setParents", (mother, father) => {
    mp.players.local.setHeadBlendData(mother, father, 0, 0, 0, 0, 0, 0, 0, true);
})

mp.events.add("setSkinColor", (mother, father, skinColor) => {
    mp.players.local.setHeadBlendData(mother, father, 0, skinColor, skinColor, 0, 0, 0, 0, true);
})

mp.events.add("setFaceAppearance", (FaceAppearanceId, FaceAppearanceValue) => {
    mp.players.local.setFaceFeature(FaceAppearanceId, FaceAppearanceValue);
})

mp.events.add("setLipstick", (state) => {
    mp.players.local.setHeadOverlay(8, state, 1, 0, 0);
})

mp.events.add("setEyebrows", (state) => {
    mp.players.local.setHeadOverlay(2, state, 1, 0, 0);
})

mp.events.add("setFacialHair", (state) => {
    mp.players.local.setHeadOverlay(1, state, 1, 0, 0);
})

mp.events.add("setHairColor", (state) => {
    mp.players.local.setHeadOverlayColor(1, 1, state, state); //Face hair color
    mp.players.local.setHeadOverlayColor(2, 1, state, state); //Eyebrows color
    mp.players.local.setHairColor(state, state); // hair color
})

mp.events.add("setHair", (state) => {
    localPlayer.setComponentVariation(2, state, 0, 0);
})

mp.events.add("setEyeColor", (state) => {
    mp.players.local.setEyeColor(state);
})

mp.events.add("setGender", (state) => {
    mp.players.local.model = mp.game.joaat(state);
})
