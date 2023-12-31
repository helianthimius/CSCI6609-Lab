# Lab Assignments - CSCI6609
Helia Homami - B00975927

## Repository
* [Client GitHub](https://github.com/helianthimius/CSCI6609-Lab)
* [Server GitHub](https://github.com/helianthimius/CSCI6609-3dServer)

## Project Overview
### Theme
Space

### Main Character
White Spaceship (Cutter)

### Sensors
* **Touch**: Swipe up/down to change the altitude
* **Gyroscope**: Rotate device with yourself to change the direction
* **Accelerator**: Shake to change color
* **Step Counter**: Step to move forward

### Other Objects
* Floor (Terrain)
* Light
* 2 Buildings (4 Cubes)
* 2 Other Spaceships (Red and Green)
* 4 Dead Trees (2 Different Rampikes)

### How it works
Client:
* **Input Field**: Add IP Address:33417
* **Connect Button**: Connects client to the server
* **B1, B2, B3 Buttons**: By pressing them, their name will be shown on server screen
* **Play Button**: Starts the game

Server:
* Shows pressed button name on the client
* Shows client online sensors values

_NOTE_: Try to add the correct IP address, and then press the "Connect" button.

## Citations and References
### Assignment 1
* [Space sky](https://assetstore.unity.com/packages/3d/environments/landscapes/lunar-landscape-3d-132614)
* [Space assets](https://assetstore.unity.com/packages/3d/vehicles/space/voxel-space-ships-109876)
* [Setup VSCode for unity](https://code.visualstudio.com/docs/other/unity)
* [Force app to run in landscape mode](https://discussions.unity.com/t/landscape-mode-only/114273)
* [Changing color of child game object](https://discussions.unity.com/t/renderer-material-color-not-changing-color-of-prefab/246469)
* [Touch docs (mobile input)](https://docs.unity3d.com/2022.1/Documentation/Manual/MobileInput.html)
* [Touch docs](https://docs.unity3d.com/2022.1/Documentation/ScriptReference/Touch.html)
* [Get permission in android](https://docs.unity3d.com/Manual/android-RequestingPermissions.html)
* [Override android manifest for adding permission](https://docs.unity3d.com/Manual/overriding-android-manifest.html#creating-a-template-android-manifest-file)
* [Add tree](https://docs.unity3d.com/Manual/tree-FirstTree.html)
* [Find game object](https://docs.unity3d.com/ScriptReference/GameObject.Find.html)
* [Gyroscope docs](https://docs.unity3d.com/ScriptReference/Input-gyro.html)
* [Change color of material](https://docs.unity3d.com/ScriptReference/Material.SetColor.html)
* [Generate random value](https://docs.unity3d.com/ScriptReference/Random-value.html)
* [Transform object direction](https://docs.unity3d.com/ScriptReference/Transform-eulerAngles.html)
* [Transform position](https://docs.unity3d.com/ScriptReference/Transform-position.html)
* [Move object in direction](https://docs.unity3d.com/ScriptReference/Transform.Translate.html)
* [How to use `var` keyword](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/implicitly-typed-local-variables)
* [Add script component to game object](https://sharpcoderblog.com/blog/unity-3d-how-to-attach-a-script-or-a-component-to-a-game-object)
* [Use steps counter](https://www.reddit.com/r/Unity3D/comments/wjrg5q/how_to_use_the_pedometerstepcounter_sensor/)
* [Exact post for steps counter](https://www.reddit.com/r/Unity3D/comments/wjrg5q/how_to_use_the_pedometerstepcounter_sensor/jcesp2h/?utm_source=share&utm_medium=mweb3x&utm_name=mweb3xcss&utm_term=1&utm_content=share_button)

### Assignment 2
* [Menu Manager](https://www.youtube.com/watch?v=pcyiub1hz20)
* [FixedUpdate](https://docs.unity3d.com/ScriptReference/MonoBehaviour.FixedUpdate.html)
* [Finding an object by its type](https://gamedev.stackexchange.com/questions/132569/how-do-i-find-an-object-by-type-and-name-in-unity-using-c)
* [Match canvas to main camera](https://stackoverflow.com/questions/33086715/match-canvas-with-main-camera-unity)
* [Singleton](https://en.wikipedia.org/wiki/Singleton_pattern)
* [Avoid Destroying Network Manager](https://stackoverflow.com/questions/33787803/share-gameobjects-between-scenes)
* [Avoid Destroying Network Manager](https://docs.unity3d.com/ScriptReference/Object.DontDestroyOnLoad.html)