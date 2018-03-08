# UnityMobileWebAudioExample
Use Javascript and Unity to play audio, even on iOS and Android devices.

Unity is an excellent tool, but exporting to WebGL to play on mobile devices is still an incomplete feature. One of the most challenging issues with playing a WebGL game or app on mobile, especially iOS, is getting the sound to work. This example will display how to play sounds on mobile devices using browser based JavaScript while still utilizing Unity's WebGL export option.

See a playable example here (featuring fun bouncy animals): http://www.danjorquera.com/unitywebaudioexample/

![alt text](http://www.danjorquera.com/unitywebaudioexample/animal-sounds.png)

## Step 1, Prepare the Sounds

While building your application, place all of your sound files in a *StreamingAssets* folder. This will allow us to access these files later - they won't be zipped up in the usual Unity way.

## Step 2, Prepare the HTML

We need to do a couple odd things here - iOS will not let you play sounds until the user has interacted with the browser - interacting with the game doesn't count. So, we will add a simple button in out HTML (a Play or a Start button), which will hide the Unity content, and once pressed, will show the Unity content, and more importantly, play a sound. In this example, we are just playing an empty mp3. Since that audio has been "pressed" by the player, we can swap out the source of the audio tag, and play sounds that way.

```
    <script>
      function start() {
        document.getElementById("gameContainer").style.visibility= "visible";
        document.getElementById("sound").play();
      }
    </script>
    
    <audio preload="auto"
      <source id="sound" src="StreamingAssets/sfx/500-milliseconds-of-silence.mp3" type="audio/mpeg" codecs="mp3">
      Your browser does not support the audio element.
    </audio>
    
    <button onclick="start()">Start</button>
```

## Step 3, Prepare the JavaScript plugin

The "Application.ExternalCall()" trick is deprecated; the recommended way to do this now is to use a JavaScript "plugin," which just means fitting some JavaScript into a .jslib file, and putting it in a *Plugins* folder. Official documentation can be seen here:

https://docs.unity3d.com/Manual/webgl-interactingwithbrowserscripting.html

Our code looks like this:

```
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
```

Essentially, we are grabbing the "audio" tag in our HTML, and using that, we are replacing the audio source, waiting until it loads, then finally playing it.

## Step 4, Call the JavaScript from Unity

Finally, we're back in C#. First, we need to tell Unity that our "jslib" function exists. Then, we simply call it when we're ready.

```
    [DllImport("__Internal")]
    private static extern void PlayAudioFile(string str);
    
    void AnActionHasHappened()
    {
        PlaySound("crocodile");
    }
```

Our example passes over a string so that we know which MP3 to play, but anything could be passed over. For example, a boolean to tell our browser if the music should loop or not.

## WE DID IT!

We're finally playing audio on iPads and iPhones and Android devices and desktops! All the things! A friendly reminder that none of this is necessary for regular desktop usage. Mobile usage just makes things more difficult. But nothing is impossible... with the power of CODE. *roll credits*
