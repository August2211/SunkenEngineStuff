# SunkenEngineStuff

## Basic feature overview
Basic features to speed up progress or help test the game

F1 opens the menu with most the features

V Enables/Disables flying with no collision

## Installation
Install [MelonLoader Installer](https://github.com/LavaGang/MelonLoader.Installer/blob/master/README.md#how-to-install-re-install-or-update-melonloader)

Install the latest [release](https://github.com/August2211/SunkenEngineStuff/releases)

Select the game in the list or add a game manually

Enable Nightly builds

Pick 0.7.2-ci.2367 higher versions will probably work too, but idk

Click install

Drag the dll file and optionally the pdb file to `<Game Dir>\Mods`

And that's about it, first startup might take a little while, while il2cppdumper does its thing

## Building
First run the MelonLoader Installer and run the game at least once after installing MelonLoader as shown in the installation steps above

Replace `<GameDir>` in [SunkenEngineStuff/SunkenEngineStuff.csproj](https://github.com/August2211/SunkenEngineStuff/blob/master/SunkenEngineStuff/SunkenEngineStuff.csproj#L18)

Run `dotnet build -c Release -r win-x64`
