# Unity Visual Novel Project
An VisualNovel/RpG game project

# Text files

## Dialogue format

Name "dialogue" command(logic)

"Narrator" name hides name in the box

Name only change if you specify diffrent name, so if you have multiple lines for one character, you can write his name only once

Line breakers
{c}	= clear
{a}	= append
{wc n}	= wait clear number(time)
{wa n}	= waint append number(time)

You can show diffrent character name by using as:
Affir as ??? will use Affir customization with ??? name

## Commands
[wait] - Before function to wait for completion before executing another line/function
You can pass the values in given order, or specify parameters if you want diffrent order or you don' want to use every parameter.
All commands shoud be case insensitive. Parameters are not. If you want to pass argument consisted of two words you need to join them with ""
While passing arguments you dont use any separators in brackets. For example:
Showdb( -spd 1f -i true)


Parameters:
speed:			[-spd, -speed]
immediate:		[-i, -immediate]
enable:			[-e, -enabled]
xpos:			[-x]
ypos:			[-y]
smooth:			[-sm, -smooth]
color:			[-c, -color]
only:			[-o, -only]
sprite:			[-s, -sprite]
layer:			[-l, -layer]
sfx:			[-s, -sfx]
song:			[-s, -song]
ambience:		[-a -ambience]
volume:			[-v, -vol, -volume]
pitch:			[-p, -pitch]
loop:			[-l, -loop]
channel:		[-c, -channel]
startvolume:	[-sv, -startvolume]
panel:			[-p, -panel]
media:			[-m, -media]
blendtex:		[-b, -blend]
usevideoaudio:	[-aud, -audio]

### General
* __Wait(x) -__ Wait for x seconds specified in floats
* __ShowDB(speed: 1f immediate: true) -__ Show dialogue box (DialogueBox GameObject)
* __HideDB(speed: 1f immediate: true) -__ Hide dialogue box (DialogueBox GameObject)
* __ShowUI(speed: 1f immediate: true) -__ Show VN User Interface (Canvas Main GameObject)
* __HideUI(speed: 1f immediate: true) -__ Hide VN User Interface (Canvas Main GameObject)

### Characters
#### General characters command
* __CreateCharacter(characterName enable:false immediate: false) -__ Create a character in scene. 
		First argument wil lalways be the name of a character, passed as a string. If you want 
		to create new character with existing character config, you can use as keyword in quotes.
		For example: CreateCharacter("Mercenary as Affir") will create character named Mercenary
		with Affir specified config (font, color, sprite etc). Enable decide if character apper
		in scene visible or invisible. Immediate decide if characater fade in into a scene of is
		visible immediately
* __MoveCharacter(characterName xpos ypos speed:1f smooth: false immediate) -__ Move character to specific coordinates.
		Usually you want to use range from 0 to 1. X 0/1 will make character box stick to the edge of
		screen. Y 0 means default height for character. 1 means maximum height at which whole sprite 
		is visible without bottom part floating in air. Smooth make character slide into the position
		with lowering speed the coleser the sprite is to the desired position. Immediate sets character
		position immediately instsed of moving it.
* __Show(Characters immediate: false, speed: 1f) -__ Show/Fade in all characters passed in the function by thier name.
* __Hide(Characters immediate: false, speed: 1f) -__ Hide/Fade out all characters passed in the function by thier name.
* __Sort(characters) -__ sort passed characters on screen by their priority. Priority is set by the order of 
		which you pass them into the function. All skipped characters go to the end of priority list 
		based on their previous priority
* __Highlight(characters immediate: false only: true) -__ Lighten all passed characters up to default brightness. 
		If only is set true, function will unhiglight (darken) all characters in scene that were not passed
		into the function. Immediate set brightness immediately insted of making a transition
* __UnHighlight(characters immediate: false only: true) -__ Same as Highlight but in reversee.

#### Character added commands
Used from the character level. Example: 
After creating a character with CreateCharacter(Affir) function, you can use these commands like this:
Affir.(Move)

* __Character.Move(xpos ypos speed:1f smooth: false immediate) -__ Same as Move() function above but without specyfing
		the character name
* __Character.Show(immediate: false, speed: 1f) -__ Show/Fade in a character
* __Character.Hide(immediate: false, speed: 1f) -__ Hide/Fade out a character
* __Character.SetPriority(x) -__ Manually set character priority. The lower priority the more "on top" character is
		in the scene.
* __Character.SetColor(color speed: 1f immediate: true) -__ Set character color (sprite/model/live2D) to provided color name with
		defined transition speed or immediate effect. Currently implemented colors: [red, gree, ble, yellow, white,
		black, gray, cyan, magenta, orange]
* __Character.Highlight(immediate: false only: true) -__ Same as Highlight() function above but without specyfing
		the character name
* __Character.UnHighlight(immediate: false only: true) -__ Same as UnHighlight() function above but without specyfing
		the character name
### Specific Character Type Commands
Commands that can be used only by specific character type Sprite/Text/Live2D/3D
#### Sprite Characters
* __SetSprite(spriteName layer: 0 speed: 0.1f immediate: true<sup>*</sup>) -__ Set character sprite on given layer with
		given transition speed or immediate effect.
		<sup>*</sup> Immediate is checked and set to true only if transision speed is not passed into function.
		
On single layer characters you can skip the layer indication on expression and just type [Happy] for example	
### Audio Commands
There are 4 main audio mixers. SFX, Voice, Ambience, Music. Music and Ambience have channel managment systems. You can
set track to specific channel. Only one track can play on specific channel. Music and ambience do not share channels. 

* __PlaySFX(filepath volume: 1f pitch: 1f loop: false) -__ Used to play short sound effects. In filepath you need to
		specify only filename you want to play. SFX file need to be located in Main/Resources/Auido/SFX
* __PlayVoice(filepath volume: 1f pitch: 1f loop: false) -__ Used to play characters voicelines. In filepath you need to
		specify only filename you want to play. Voice file need to be located in Main/Resources/Auido/Voices
* __PlaySong(filepath channel:1 volume: 1f startvolume: 0f pitch: 1f loop: true)__ - Used to play music. In filepath 
		you need to specify only filename you want to play. Music file need to be located in Main/Resources/Auido/Music
		Music start fading in from  startvolume to volume value
* __PlayAmbience(filepath channel:0 volume: 1f startvolume: 0f pitch: 1f loop: true) - Used to play Ambience. In filepath
		 you need to specify only filename you want to play. Ambience file need to be located in Main/Resources/Auido/Ambience
		 Rest of the behaviour identical to PlaySong()
* __StopSFX(filepath) -__ Immediately stop sound effect specifed by name
* __StopVoice(filepath) -__ Immediately stop voice line specifed by name
* __StopSong(filepath/channel) -__ Fade out music specified by filepath or by channel on which is playing. Invoking without
		any parameter will try to fade out music on default music channel
* __StopAmbience(filepath/channel) -__ Same behaviour as StopSong() but working on ambience

### Graphic Panel Commands
* __SetLayerMedia(panelName layer: 0 media immediate: false speed: 1f blendtex: "" usevideoaudio: false ) -__ Load graphic
		image as a background on one of the avalible panels: Background, Cineamtic, Foreground. More panels can be added
		via Graphic Panel Manager GameObject. Filepath for media is: Main/Resources/Graphics/BG_Images for still images and
		Main/Resources/Graphics/BG_Videos for videos. You only need to specify file name. Audio played from video backgrounds 
		currently are not connected to any game Mixer. Path for Blend textures is: Main/Resources/Graphics/TransitionEffects
		If blendtex in unspecified transistion will be simple fading in/out. Only one background can be set on specific 
		panel and layer.
* __ClearLayerMedia(panelName layer: -1 immediate: false speed: 1f blendtex: "") - __ Clear background from specified panel
		if layer is not set, or is set to -1 function will clear bakcgrounds from all layers in panel. ath for Blend textures
		is: Main/Resources/Graphics/TransitionEffects If blendtex in unspecified transistion will be simple fading in/out.


