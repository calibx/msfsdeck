# What is this
The aim of this plugin is to use the Loupedeck Live to control MSFS
# Requirements
* Loupedeck Live : https://loupedeck.com/fr/products/loupedeck-live/
* MSFS : https://www.flightsimulator.com/
* FSUIPC : http://www.fsuipc.com/
# Upgrading
## From rc3
* Remove all older files (as now included in the plugin)
* Restart Loupedeck
## After
* Simply uninstall the plugin (replace the name !) with : "C:\Program Files (x86)\Loupedeck\Loupedeck2\LoupedeckPluginPackageInstaller.exe" uninstall -path=NAME_OF_THE_PLUGIN.lplug4
* Validate the elevation requirement
# Installation
* Install latest Loupedeck Software from Loupedeck Software - https://fr.support.loupedeck.com/loupedeck-software-download
* Install FSUIPC: http://www.fsuipc.com/ (Tested with v7.2.14/7.2.14 on Win 10/11)
* Download the latest MSFS Deck plugin: https://github.com/calibx/msfsdeck/releases
* Doubleclick the downloaded .lplug4 file to install the plugin
* Check from Loupedeck software that you can see the MSFS profile
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
