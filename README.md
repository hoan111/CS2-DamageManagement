# CS2-DamageManagement (Linux only)
This is a plugin that modifies damage done to teammates like Faceit. I tried to run this plugin on Windows but it didn't work.

Update: I currently working on a metamod plugin for Windows version. It's working but I need more work on this.
# Requirements
* `mp_friendlyfire` must be set to 1
* [CounterStrikeSharp](https://github.com/roflmuffin/CounterStrikeSharp) must be installed on your CS2 server.
* Your CS2 server must be run on Linux.
# Installation
- Extract the zip file to your `csgo` folder. The plugin will be located at `csgo/addons/CounterStrikeSharp/plugins/DamageManagement`
# Configuration
By default, this plugin will be enabled when you install it for the first time. You can turn on/off it by using configuration file or console command.
1. Using configuration file
2. Using console command
`faceit_dmg_enable`. Value `true / false`
3. Example configuraton at `game\csgo\addons\counterstrikesharp\configs\plugins\DamageManagement\DamageManagement.json`
```json
{
  "enable": true,
  "enable_inflictors": [
    "inferno",
    "hegrenade_projectile",
    "flashbang_projectile",
    "smokegrenade_projectile",
    "decoy_projectile",
    "planted_c4"
  ],
  "ConfigVersion": 1
}
```
`enable_inflictors`: This contains all designer name of the weapon class that allow to be taken damage to the teammates. Designer name can be found here https://cs2.poggu.me/dumped-data/entity-list
