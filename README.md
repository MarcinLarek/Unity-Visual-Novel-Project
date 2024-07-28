# Unity Visual Novel Project
//Todo 

# Using text files

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

On single layer characters you can skip the layer indication on expression and just type [Happy] for example

## Commands
[wait] - Before function to wait for completion before executing another line/function
You can pass the values in given order, or specify parameters if you want diffrent order or you don' want to use every parameter.
All commands shoud be case insensitive. Parameters are not.
While passing arguments you dont use any separators in brackets. For example:
Showdb( -spd 1f -i true)


Parameters:
speed:		[-spd, -speed]
immediate:	[-i, -immediate]
enable:		[-e, -enabled]
xpos:		[-x]
ypos:		[-y]
smooth:		[-sm, -smooth]
color:		[-c, -color]
only:		[-o, -only]
sprite:		[-s, -sprite]
layer:		[-l -layer]

### General
Wait(x) - Wait for x seconds specified in floats
ShowDB(speed: 1f immediate: true) - Show dialogue box (DialogueBox GameObject)
HideDB(speed: 1f immediate: true) - Hide dialogue box (DialogueBox GameObject)
ShowUI(speed: 1f immediate: true) - Show VN User Interface (Canvas Main GameObject)
HideUI(speed: 1f immediate: true) - Hide VN User Interface (Canvas Main GameObject)

### Characters
#### General characters command
CreateCharacter(characterName enable:false immediate: false) - Create a character in scene. 
		First argument wil lalways be the name of a character, passed as a string. If you want 
		to create new character with existing character config, you can use as keyword in quotes.
		For example: CreateCharacter("Mercenary as Affir") will create character named Mercenary
		with Affir specified config (font, color, sprite etc). Enable decide if character apper
		in scene visible or invisible. Immediate decide if characater fade in into a scene of is
		visible immediately
MoveCharacter(characterName xpos ypos speed:1f smooth: false immediate) - Move character to specific coordinates.
		Usually you want to use range from 0 to 1. X 0/1 will make character box stick to the edge of
		screen. Y 0 means default height for character. 1 means maximum height at which whole sprite 
		is visible without bottom part floating in air. Smooth make character slide into the position
		with lowering speed the coleser the sprite is to the desired position. Immediate sets character
		position immediately instsed of moving it.
Show(Characters immediate: false, speed: 1f) - Show/Fade in all characters passed in the function by thier name.
Hide(Characters immediate: false, speed: 1f) - Hide/Fade out all characters passed in the function by thier name.
Sort(characters) - sort passed characters on screen by their priority. Priority is set by the order of 
		which you pass them into the function. All skipped characters go to the end of priority list 
		based on their previous priority
Highlight(characters immediate: false only: true) - Lighten all passed characters up to default brightness. 
		If only is set true, function will unhiglight (darken) all characters in scene that were not passed
		into the function. Immediate set brightness immediately insted of making a transition
UnHighlight(characters immediate: false only: true) - Same as Highlight but in reversee.

#### Character added commands
Used from the character level. Example: 
After creating a character with CreateCharacter(Affir) function, you can use these commands like this:
Affir.(Move)

Character.Move(xpos ypos speed:1f smooth: false immediate) - Same as Move() function above but without specyfing
		the character name
Character.Show(immediate: false, speed: 1f) - Show/Fade in a character
Character.Hide(immediate: false, speed: 1f) - Hide/Fade out a character
Character.SetPriority() - Manually set character priority. The lower priority the more "on top" character is
		in the scene.
Character.SetColor(color speed: 1f immediate: true) - Set character color (sprite/model/live2D) to provided color name with
		defined transition speed or immediate effect. Currently implemented colors: [red, gree, ble, yellow, white,
		black, gray, cyan, magenta, orange]
Character.Highlight(immediate: false only: true) - Same as Highlight() function above but without specyfing
		the character name
Character.UnHighlight(immediate: false only: true) - Same as UnHighlight() function above but without specyfing
		the character name
### Specific Character Type Commands
Commands that can be used only by specific character type Sprite/Text/Live2D/3D
#### Sprite Characters
SetSprite(spriteName layer: 0 speed: 0.1f immediate: true<sup>*</sup>) - Set character sprite on given layer with
		given transition speed or immediate effect.
		<sup>*</sup> Immediate is checked and set to true only if transision speed is not passed into function. 