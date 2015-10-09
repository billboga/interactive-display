// Ref. http://codepen.io/jmw/pen/azVZZY

$(function() {
    var soundEffectcAudioSource = $('#sound-effect-audio-source');
    var strobeTimeout = null;

    $.connection.hub.start().done(function() {
        console.log('hub connection open');

        pinHub.server.setOutputPinState(2, true);
        pinHub.server.setOutputPinState(3, true);
    });

    pinHub.client.inputPinStateChange = function(index, state) {
        if (index === 1 && state === true) {
            if (soundEffectcAudioSource.length > 0) {
                soundEffectcAudioSource[0].play();
                setTimeout(function() {
                    clearTimeout(strobeTimeout);
                    pinHub.server.setOutputPinState(0, false);
                    pinHub.server.setOutputPinState(1, false);

                }, soundEffectcAudioSource[0].seekable.end(0) * 1000);
            }

            strobe(500, true);
        }
        else if (index === 1 && state === false) {
            if (soundEffectcAudioSource.length > 0) {
                soundEffectcAudioSource[0].pause();
                soundEffectcAudioSource[0].currentTime = 0;
            }

            clearTimeout(strobeTimeout);

            pinHub.server.setOutputPinState(0, false);
            pinHub.server.setOutputPinState(1, false);
        }
    };

    function strobe(interval, initialPinState) {
        pinHub.server.setOutputPinState(0, initialPinState);
        pinHub.server.setOutputPinState(1, !initialPinState);

        strobeTimeout = setTimeout(function() {
            strobe(Math.floor(Math.random() * 500), !initialPinState);
        }, interval);
    }
});