# TripledotTechArtAssignment
Unity UI Assessment Project 
Simple overview of use/purpose.

## Description

An in-depth paragraph about your project and overview of use.

## Getting Started

### Dependencies

* Unity Version 6000.3.6f1
* DOTWeen Tweening Library
* Safe Area Helper
* Localization 

## Details

### Home Screen & Bottom Bar

Setup for portrait only using Safe Area Helper package from the Asset store to group items so that UI elems get shifted while backgrounds can still overflow.
One noticable area with this was the bottom bar background. Here, as a band aid solution, I had one instance im the overflow group and another in the SafeArea zone.
This prevents bar from ever floating but isn't ideal, even though it is setup as a prefab it does make updates a little messy.

My initial approach with the bottom bar was to set these up as a ToggleGroup, treating them like Radio buttons and to leverage as much of Unity's components with as little script as possible. Although I was able to get it somewhat there in functionality, the performance in some instances was abysmal. That led me to search for the promised Tweening library but all I got was DOTween... :D
This improved performance and quality of the animations but as this was a new tool for me, I am sure I wasn't using it to it's full potential. For instance I would definitely utilise sequences far more and do much better validation and clean up of tweens. However that being said, everrything I tested was very performant and smooth.

In terms of the bottom bar code, the idea here was to have a base button class, "SimpleButtonBaseClass.cs", that extended the following -> IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler, IPointerDownHandler.
This would handle any base button functionality such as playing a generic sound, fallback behaviour and the like.
You could then add to this buy writing some button modifiers that could do little bits of something via overrides and custom logic.
For the bottom bar for example I have a "BottomBarButtonModifier.cs" file which handles the bottom bar buttons. This is taken in by the "BottomBarView.cs" and together they achieve thhe desired functionality. 
The poorly named "SimpleButtonTranslationModifier.cs" in turn handles the tab behind the 
This could be refactored a fair bit though with less coupling and much more robust validation but for the sake of is required it makes its case.

There were some things I was not 100% sure about so I made some rash assumptions... hopefully they were close enough :D
The first being that nothing would be selected on first entry and that after a selection is made something is always selected.
For the locked buttons, I treated these as their own thing so that they could be tasked with doing what they needed to do regardless of the active available buttons.


### Top Bar UI

Setup on own canvas using layout elements and anchoring to keep central on all devices as well as reflowing if anything is removed.
All items are prefab variants with clear purposed naming. I went with naming items using uppercase prefixes, so GFX_, BTN_, TXT_, followed by another prefix for where that item might be found, _BB, _LC


### Settings Popup

Once again using DOTWeen to animate.
There is a script called "PopUpManager.cs" which handles the showing and hiding as well as the sequencing of the animations.
For languages I used the built in Unity localization package to setup string tables and an asset one for the level complete text.
For the blurring of the overlay, I looked at 2 options, both were insufficient and ineffienct so I left them out. My next approach was going to take a screen shot of the game and apply some sampling to that image to make it appear as it does in the PSD. It seemed to make sense to leave this out though as it is mainly a just a visual flourish but not functionally integral.
The original design was to have a base pop up class with a show() and a hide() method which could then be extended upon for both Settings and LevelComplete.
However I decided to keep those separate but ideally I would still try refactor to achieve something closer to that.
The way the panel is set up is so that as options are added or removed, it should expand or contract vertically if needs be.
Using the button modifier approach I added "SimpleButtonScaleModifier.cs" to indicate button focus.


### Level Completed Screen

Once again using DOTWeen to animate with some animator animations for the video icon pulse and the star dancing around.
Added a shimmer material to some elements to add some razz matazz to the whole situation and a offset to the background tiles.
Initially had a sparkle waterfall but this looked a bit too loud for my taste so kept it rather subdued.
Some particles are activated on entry, by just setting the GameObject containing them to active on transition.
This includes some confetti canons and a looping light trails from behind the star using thhe trails properties in the particle emitter.
This is where I could have been better with the use of DOTween Sequences, but for the sake of delivery I forsook it this time.
I also added a simple countup script and had plans to setup a pot collection as it countup but again just a nice to have flourish. 


### Optimization Notes

I setup 2 sprite atlases, one with the base items including settings and another for the level completed screen.
This is all using Sprite Atlas V2, but I may need to dig in and see if things are running correctly.
Some drawcalls seem high but could do with improvements.
Regarding the atlas setup, I did this brazenly according to my folder structure. However it might be beneficial to have these set up by grouping things a bit better. icons, buttons and so forth. For this exercise though that seems excessive.
I did want to slice some sprites in half, the bottom bar and the settings panel, and mirror these to also save space on the atlas textures. The intention was there but I forgot.
For compression I enabled Crunch compression on packed items.
Each section, Top Bar, Bottom Bar, Settings, and Level Complete are housed in their own canvases and turned off raycast targets where applicable. 


### Closing arguments

This was a fun exercise and getting to know Unity 6.3 has been very fruitful on a personal level.
Thank you for your time and I look forward to any feedback that you might have.

