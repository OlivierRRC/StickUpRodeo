# STICK UP RODEO v2.0!

This was a jam game made with a few friends for the Winnipeg Game Collective's 2025 pegjam this past february.

The theme was "Unconventional Weapons."

And so I ask you, what is more unconventional than a horse spitting out benadryl?

Thus, Stick Up Rodeo was born. A first person shooter game where you and your horse "Chicken Nugget" break into a glue factory to save your other horse named Princess.

Version 1 was created in three feverish days with the full power of our entire team backing it, and was released to a room full of laughter when it was presented to the other participants.
It took place in a combat arena and the main goal was to survive a horde of glue factory employees as they chased you forever.

Version 2 is a complete remaster sticking with the spirit of the original. This time around you must fight your way through a level, hunting down the evil factory workers (with improved AI) before reaching the end and rescuing your darling Princess.

## DOWNLOAD INSTRUCTIONS
- To get the Unity Project, just download the repository and import the files into the engine.
- To Download the game, you can grab it from the [**itch.io**](https://heliturtleop.itch.io/stickup-rodeo) page

## TO REVIEW THE CODE

All of the relevant project code is annotated and exists inside of the [_scripts_](Assets/Scripts) folder.

The base of the code was largely written by Olivier, with additions to function and annotations made by Jake.
If you'd like to review the key parts, feel free to poke around in there, but if you'd like some direction to the important stuff, here are some recommendations:

- [**OlivierPlayerMove**](Assets/Scripts/OlivierPlayerMove.cs):
  - While the project was starting, Olivier and Jake worked in seperate movement scripts to prevent conflicts. Once Jake started to work on sound, Olivier's script became the main one.
  - Handles all player activity.
- [**VoiceLineManager**](Assets/VoiceLineManager.cs):
  - Handles the selection and playing of randomized voice lines for getting hit, killing an enemy or reloading.
- [**EnemyBase**](Assets/Scripts/EnemyBase.cs):
  - Handles the fundamentals of all enemy behaviour, acts as a base for the other enemy scripts.
- [**Rat**](Assets/Scripts/Rat.cs), [**Ox**](Assets/Scripts/Ox.cs) and [**Rooster**](Assets/Scripts/Rooster.cs):
  - Each one is an individual enemy type built off of the EnemyBase script.


## CREDITS
- Olivier Proulx - Director and Lead Programmer
- Jake Szmon - Sound Designer and Programmer
- Anna Froese - Artist
- Rain Porayko - Artist
- Jordan Erikson - Musician
