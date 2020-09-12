const updateInterval = 500;


mp.events.add("LOBBYCAM1", (player) => {

    mp.game.controls.disableAllControlActions(0);
    mp.game.controls.disableAllControlActions(1);

    let sceneryCamera = mp.cameras.new('default', new mp.Vector3(-1981.1108, -1013.1947, 14.0164995), new mp.Vector3(0, 0, 0), 40);

    sceneryCamera.pointAtCoord(-2043.9028, -1031.4694, 11.980707); //Changes the rotation of the camera to point towards a location
    sceneryCamera.setActive(true);
    mp.game.cam.renderScriptCams(true, false, 0, true, false);
});

mp.events.add("LOBBYCAM2", (player) => {

    mp.game.controls.disableAllControlActions(0);
    mp.game.controls.disableAllControlActions(1);
    let lobbyCam = mp.cameras.new('default', new mp.Vector3(-2087.083, -1017.4535, 9.571129), new mp.Vector3(0, 0, 0), 60);

    lobbyCam.pointAtCoord(-2089.6309, -1016.70245, 8.971192); //Changes the rotation of the camera to point towards a location
    lobbyCam.setActive(true);
    mp.game.cam.renderScriptCams(true, false, 0, true, false);
});

mp.events.add("ENDLOBBY", (player) => {

    mp.game.controls.disableAllControlActions(0);
    mp.game.controls.disableAllControlActions(1);

    mp.game.cam.renderScriptCams(false, false, 0, true, false);
});
