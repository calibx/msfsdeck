# What is this
The aim of this plugin is to use the Loupedeck Live to control MSFS
# Requirements
* Loupedeck Live : https://loupedeck.com/fr/products/loupedeck-live/
* MSFS : https://www.flightsimulator.com/
* FSUIPC : http://www.fsuipc.com/
* FSUIPC Client DLL from Paul Henty (included) : http://fsuipc.paulhenty.com/#home
# Upgrading
## From rc3
* Remove all older files (as now included in the plugin)
* Restart Loupedeck
## After
* Simply uninstall the plugin (replace the name !) with : "C:\Program Files (x86)\Loupedeck\Loupedeck2\LoupedeckPluginPackageInstaller.exe" uninstall -path=NAME_OF_THE_PLUGIN.lplug4
* Validate the elevation requirement
# Installation
* Install FSUIPC
* Install the plugin (replace the name !) run in cmd : "C:\Program Files (x86)\Loupedeck\Loupedeck2\LoupedeckPluginPackageInstaller.exe" install -path=NAME_OF_THE_PLUGIN.lplug4
* Validate the elevation requirement
* For the first time you can load the profile msfs.lp4 in LoupeDeck UI : don't forget to active dynamic mode
* You can then configure your own profile (don't forget to backup it)
# Usage
* Launch MSFS
* FSUIPC should autostart
* The Loupedesk should switch to MSFS profile
* Have Fun
# Controls description
https://github.com/calibx/msfsdeck/wiki
# Troubleshooting
## N/A is displayed on each input
The DLL isn't install correctly, verify logs, reinstall the plugin
## No value on the inputs
Verify the connection with FSUIPC adding the "Connection" input in your profil.
If it doesn't display "Connect" but "Trying to connect", reinstall FSUIPC
