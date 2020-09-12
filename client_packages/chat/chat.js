mp.events.add('Send_ToChat', (player, message) =>{
    mp.gui.chat.push(`${player.name}[${player.id}]: ${message}`);
});
