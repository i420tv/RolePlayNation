mp.events.add('playerJoin', player => {

    mp.players.forEach((_player) => {
        player.setVariable('voiceStatus', true);
        player.setVariable('voiceChannel', 0);
        console.log('Your are now on CHANNEL 0');

        if (player == _player) {
           
            return false;
            player.disableVoiceTo(_player);        
        }
    });

});
mp.events.add('playerStartTalking', player => {
    console.log('Player', player);

    player.setVariable('voiceChannel', 0);
    console.log('Player voice (should be true)? ', player.getVariable('voiceChannel'));
    //player.setVariable('voice', false);
    console.log('Player voice (should be false)? ', player.getVariable('voiceStatus'));
 
  
    if (player.getVariable('voiceChannel')  == 0) {
        console.log('voice found 1');

        mp.players.forEachInRange(player.position, 10, (_player) => {

            if (player == _player) return false;

            console.log('Player: ', _player)


            //if(player == _player) return false; for testing.
           // if (_player.getVariable('voiceChannel') != 0) return false;
            //if (_player.getVariable('voiceStatus') != true) return false;
            player.enableVoiceTo(_player);
            console.log('voice found');
        }
        );
    }
    else if (player.getVariable('voiceChannel') == 1) // walkie talkie in the future
    {
        mp.players.forEach((_player) => {
            if (player == _player) return false;
            if (_player.getVariable('voiceChannel') != 1) return false;
            if (_player.getVariable('voiceStatus') != true) return false;
            player.enableVoiceTo(_player);
        });
    }
});

mp.events.add('playerStopTalking', player => {
    if (player.loggedIn == true) {
        player.voiceListeners.forEach((listener) => {
            player.disableVoiceTo(listener);
            console.log('Did it work?');
        });
    }
});