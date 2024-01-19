# NutriLearnVR
Master Thesis Repository of NutriLearnVR Application in Unity 

## Build Instructions:

- Download Unity Version 2021.3.30f1 with Android Build Support, SDK and OpenJDK
- Clone Repository
- Open Project (not Repository!) Folder in Unity
- Wait for Unity to import packages and install plugins
- (Select Scene to display)
- Go to build settings and select Android, switch build platform if necessary
  - Make sure that "Use Player Settings" is enabled for Texture Compression and both scenes are selected for building.
  - Within Player Settings, make sure that no custom KeyStore is selected to sign the application as this information is not easy to store within Unity commits
- Build the application
- Move .apk to desired VR headset and start the application

## Important Notes:

- Within the Repository (called NutriLearnVR), there is another folder called *NutriLearnVR*. This is the actual folder of the Unity Project to open in the Unity Hub.
- Other Supplementary material, e.g. regarding the analysis of the user test, can be found in *Supplmentary_Material*
  - (UPDATE 19.01.2024) The Defense presentation file *without* video can also be found in this directory.
  - (UPDATE 19.01.2024) The Video is uploaded [here](https://drive.google.com/file/d/18fR94f952Fl-n0-POX0PGFWNha96rWbb/view?usp=sharing) due to GitHub file size limitations. (*Note*:  Some parts of the video were sped up to introduce the application visually without taking too much time)
</br>

- No custom KeyStore should be selected for building the application, else Unity will ask for a KeyStore passphrase for signing the application
  - If the .apk should be published to Meta, e.g. using the Meta Quest Developer Hub, the application *has* to be signed with a consistent KeyStore across all builds!
    
- The current version of the database only works correctly on devices with a German language setting, else the values from the database may be interpreted incorrectly!
  - For English language settings, the entries have to be adjusted accordingly (decimal numbers using . instead of , as a delimiter)
