﻿// Ref. http://codepen.io/andrewburgess/pen/nFgqD
// Ref. http://codepen.io/Battmatt/pen/yYLjBd
var play = false; // switch on\off
var ii = null; // interval ID
var colors = ['rgba(0,0,0,0.35)'
            , 'rgba(255,255,255,.35)'
            , 'rgba(0,0,0,0.35)'
            , 'rgba(0,0,0,0.35)'
]; // pixel colors

//  make a new renderer 
function getRender(pdx, pdy, pcolors) {
    var cvs = document.getElementById("cvs");
    var ctx = cvs.getContext('2d');
    var wt = cvs.width;
    var ht = cvs.height;
    var dx = pdx;
    var dy = pdy;
    var colors = pcolors;
    return function() {
        for (var x = 0; x < wt; x += dx)
            for (var y = 0; y < ht; y += dy) {
                var idx = Math.floor(Math.random() * colors.length);
                ctx.fillStyle = colors[idx];
                ctx.fillRect(x, y, dx, dy);
            }
    }
};

// one to go, please
var render = getRender(1, 2, colors);

// define switcher
function doSwitch(e) {
    play = !play;
    if (play)
        ii = setInterval(render, 0);
    else
        clearInterval(ii);
};
// switch ON
doSwitch();
// link switcher to keyboard
document.body.addEventListener('keydown', doSwitch);

$(function() {
    var tvStatic = $('#cvs');
    var imageAudioSource = $('#image-audio-source');

    pinHub.client.inputPinStateChange = function(index, state) {
        if (index === 1 && state === true) {
            if (imageAudioSource.length > 0) {
                imageAudioSource[0].play();
                setTimeout(function() {
                    tvStatic.show();
                }, imageAudioSource[0].seekable.end(0) * 1000);
            }

            tvStatic.hide();
        }
        else if (index === 1 && state === false) {
            if (imageAudioSource.length > 0) {
                imageAudioSource[0].pause();
                imageAudioSource[0].currentTime = 0;
            }

            tvStatic.show();
        }
    };
});