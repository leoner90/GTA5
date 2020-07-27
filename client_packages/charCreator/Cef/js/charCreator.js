//Function to call and , slider value
function updateAppearance(fun, slideAmount) {
    var slideAmount = Number(slideAmount);
    mp.trigger(fun, slideAmount);
}
//Gender Updater  , if male - hide lipsticks, if women hide face hair and it's labels , also update both button value for male or female value for future use
//mp_m_freemode_01 = 1885233650 =  male , mp_f_freemode_01 = -1667301416 = female
function updateGender(fun, gender) {
    if (gender == "mp_m_freemode_01") {
        $(".gender").val(1885233650); 
        $("#lipstickColor").hide();
        $("#lipstickColor").prev().hide();
        $("#lipstickColor").val(255);
        $("#faceHair").show();
        $("#faceHair").prev().show();
        $("#faceHair").val(0);
    } else {
        $(".gender").val(-1667301416);
        $("#lipstickColor").show();
        $("#lipstickColor").prev().show()
        $("#lipstickColor").val(0);
        $("#faceHair").hide();
        $("#faceHair").prev().hide();
        $("#faceHair").val(29);
    }
    mp.trigger(fun, gender);
}
//Change parent , change both as they depend on each other
function updateParents() {
    var father = Number($("#father").val());
    var mother = Number($("#mother").val());
    mp.trigger("setParents", mother, father);
}
//Change Face parameters eg. nose length , eye size etc. 20 parameters.
function setFaceAppearance(FaceAppearanceId,FaceAppearanceValue) {
    FaceAppearanceId = Number(FaceAppearanceId);
    FaceAppearanceValue = Number(FaceAppearanceValue);
    mp.trigger("setFaceAppearance", FaceAppearanceId, FaceAppearanceValue);
}
//Change Skin color needs father and mother , as it's same function as for parents
function setSkinColor(skinColor) {
    var father = Number($("#father").val());
    var mother = Number($("#mother").val());
    skinColor =  Number(skinColor);
    mp.trigger("setSkinColor", mother, father, skinColor);
}
//Save all variables  as json and send to front end js and then to server
function save() {
    var name = $("#name").val();
    var surname = $("#surname").val();
    var FaceAppearance = new Array;
    for (var i = 0; i < 20; i++) { 
        FaceAppearance[i] = Number($(".face-change-input").eq(i).val());
    }
    var CharacterObject = {
        gender: Number($(".gender").val()),
        father: Number($("#father").val()),
        mother : Number($("#mother").val()),
        hair : Number($("#hair").val()),
        eyeBrows : Number($("#eyeBrows").val()),
        faceHair : Number($("#faceHair").val()),
        eyeColor : Number($("#eyeColor").val()),
        lipstickColor: Number($("#lipstickColor").val()),
        skinColor: Number($("#skinColor").val()),
        hairColor: Number($("#hairColor").val()),
        FaceAppearance: FaceAppearance
    };
    var CharacterStyleJson = JSON.stringify(CharacterObject);
    mp.trigger("saveCharacter", name, surname, CharacterStyleJson );
}