let player = mp.players.local;
let cruisetoggle = 0
// Char controlls 
//Player Data Menu (F4)
mp.keys.bind(115, false, () => {
    if (mp.gui.cursor.visible) return;
    mp.events.callRemote("PlayerData");
});

//action key (E)
mp.keys.bind(69, false, () => {
    if (mp.gui.cursor.visible) return;
    mp.events.callRemote("actionkeyE");
});

//inv (I)
mp.keys.bind(73, false, () => {
    if (mp.gui.cursor.visible) return;
    mp.events.callRemote("OpenInventory");
});

//Pick up item (O)
mp.keys.bind(79, false, () => {
    if (mp.gui.cursor.visible) return;
    mp.events.callRemote("pickupItem");
});

//Cursor Toggle (F2)
mp.keys.bind(113, false, () => {
    if(mp.gui.cursor.visible)
    {
        mp.gui.cursor.show(false, false)
    }
    else{
        mp.gui.cursor.show(true, true)
    }
});

// Vehicle Controlls 
//Lock car (K)
mp.keys.bind(75, false, () => {
    if (mp.gui.cursor.visible) return;
   // if(mp.players.local.vehicle == null){
        mp.events.callRemote("toggleLockCar");
   // }
});

//enter car driver (F)
mp.keys.bind(70, false, () => {
    if(mp.players.local.vehicle == null){
        if (mp.gui.cursor.visible) return;
         mp.events.callRemote("checkPlayerEventKey");
    }
 });
 
//engine OFF (Arrow Down)
 mp.keys.bind(40, false, () => {
    if (mp.gui.cursor.visible) return;
        if (player.vehicle && player.vehicle.getPedInSeat(-1) === player.handle)
        {
            
            mp.events.callRemote("stopPlayerCar");
        }
 });

 //engine On (Arrow Up)
 mp.keys.bind(38, false, () => {
    if (mp.gui.cursor.visible) return;
        if (player.vehicle && player.vehicle.getPedInSeat(-1) === player.handle)
        {   
            if(player.vehicle.getEngineHealth() > 9){

               
                mp.events.callRemote("engineOnEventKey");

            }
            else if (player.vehicle.getEngineHealth() < 10){

                
                mp.events.callRemote("engineFinished");

            }
        }
 });

// cruise toggle (L)
//  mp.keys.bind(76, false, () => {
//     if (mp.gui.cursor.visible) return;
//     if (player.vehicle && player.vehicle.getPedInSeat(-1) === player.handle)
//     {   
//         let vel1 = player.vehicle.getSpeed() * 2.236936;
//         let vel = (vel1).toFixed(0);

//         mp.gui.chat.push("cruise pushed");
//         if(cruisetoggle == 0){
//             mp.gui.chat.push("cruise on");
//             mp.events.CallRemote("toggleCC", 1, vel)
//             cruisetoggle == 1;
//         }    

//         if(cruisetoggle == 1){
//             mp.gui.chat.push("cruise off");
//             mp.events.CallRemote("toggleCC", 0, vel)
//             cruisetoggle == 0;
//         }  
//     }
//  });


