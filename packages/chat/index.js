mp.events.add("playerChat", (player,message) =>{
    player.call('SendToChat',[player,message]);
});