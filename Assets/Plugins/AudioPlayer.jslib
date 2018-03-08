mergeInto(LibraryManager.library, {

  PlayAudioFile: function (str) {
    var audio = document.getElementById("sound");
    audio.src = "StreamingAssets/sfx/" + Pointer_stringify(str) + ".mp3"

    audio.addEventListener('loadeddata', function() {
      if(audio.readyState >= 4) {
        audio.play();
      }
    });
  },
});
