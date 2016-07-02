# T.A.C.O. #
###An intel channel monitor with an OpenGL based map display.###

[![TACO Main Window](http://i.imgur.com/ykbKBWY.png)](http://i.imgur.com/wQV5nOI.png "TACO Main Window - Click to Enlarge") [![TACO Channels](http://i.imgur.com/3uFU1oH.png)](http://i.imgur.com/ZR3XmK0.png "Channels - Click to Enlarge") [![TACO Alerts](http://i.imgur.com/NkIaVZp.png)](http://i.imgur.com/7TUHwru.png "Alerts - Click to Enlarge") [![TACO Lists](http://i.imgur.com/OKJW1Kp.png)](http://i.imgur.com/QKAn4eL.png "Lists - Click to Enlarge") [![TACO Misc](http://i.imgur.com/PFvtCqP.png)](http://i.imgur.com/dvfK9Zc.png "Misc - Click to Enlarge")

* **Still in development**

####To Do:####
* General code tidying and refactoring
* Jump bridge routing
* Recent intel display
* Hostile tracking
* zKillboard hostile links
* Path animations

####Known Bugs:####
* Monitors going to sleep may cause OpenGL to bug out when monitors wake up (rare)
* When upgrading from v0.4.x to v0.5.x, configs are not imported, sorry. I stuffed something up, but further config upgrades shouldn't be an issue.

####Change Log:####
v0.7.0b:

- Added: Player selection and linking to Combined Intel pane - highlight a player name and hit 'Enter' or 'Z' to open their zKillboard
- Added: Linked players are stored across sessions
- Added: Autolinking of systems in Combined Intel
- Added: Ability to mute sound - available from the map context menu
- Added: Intel buffering when CombinedIntel has focus
- Added: Ability to edit existing alerts
- Changed: CombinedIntel is now implemented using an extended richtext box, let me know if there are any issues
- Fixed: Invalid window position on startup - this will fix application starting and but is not being visible (due to it being at screen coordinates -32000, -32000)

v0.6.5b:

- Added: Alert expiration - alerts can be set to expire after a set about of time
- Added: The number of alerts to display can now be set
- Added: OpenGL debug diagnostic dumping - create a text file called "taco-gldiag.txt" in your taco directory to cause OpenGL diagnostics to be written to the file.
- Added: v5 config
- Change: Rearranged "Misc Settings" tab
- Change: Refactored loading procedure
- Change: Numerous internal refactorings
- Change: Refactored config saving and app closing to be more robust
- Change: Refactored LogParsing to raise start and stop combat events for Anomaly Monitor
- Change: Refactored some of the config code to be more robust
- Fixed: Bug in range display for 0 jumps
- Fixed: Small bug in zoom procedure
- Fixed: Small system highlight bug
- Fixed: Failure to detach combat event when closing logs

v0.6.0b:

- Added: "Anomaly Monitor" - alerts you when no game log activity detected for 30 secs, enable/disable per character
- Added: "Any Character" alerts
- Added: Right click empty space on the map to bring up new menu - quick control and anomaly monitor controlled through this
- Added: Automatic config upgrade v3 to v4 and v2 to v4 (please let me know if you have any issues)
- Change: Fine tuned ranged alert creation UI
- Change: Numerous code refactorings
- Fixed: Several potential crash bugs
- Fixed: Several potential config corrupting bugs

v0.5.5b:

- Added: Ability to follow a character on map (camera moves with character)
- Added: Ability to display character names on map
- Added: v3 config and new config options
- Added: Automatic config upgrade v2 to v3 (please let me know if you have any issues)
- Added: Ability to select map jump range reference point (Home system or specific character)
- Change: Strip out unnecessary unicode character from log lines
- Change: Refactored code in some locations (TacoConfig, Crosshair drawing, Log reading, and a few other spots)
- Change: Channel tabs now hide and show based on channel selection in settings
- Change: Character "following" moved from seperate button to misc settings options
- Change: Updated screenshots
- Fixed: System change alignment incorrect
- Fixed: Incorrect boolean operator in InputFocused() - Q hotkey now works as intended
- Fixed: Wormhole crash bug (I hope...)
- Fixed: Numerous small bugs

v0.5.0a:

- Added: New intel channels: Fade, Pure Blind, Tribute, Vale, Providence, Delve
- Added: Ranged alerts based on character location
- Added: Lower range limit to ranged alerts
- Added: Store known characters in config file (used for character alerting)
- Added: A few more Mario themed sounds
- Added: Character identification from chat logs
- Added: Several alerts to default config (just for demo really)
- Added: Corrupt config file handling (just generates a new default)
- Changed: Alert ordering now matters - further alerts are not triggered once one alert fires
- Changed: Refactored log parsing system
- Changed: Alert display in alert list
- Changed: Ranged alert creation section to be less ambiguous
- Changed: Moved all configuration options to sub-tabs in new Config tab
- Changed: Several small internal refactorings
- Changed: Screenshots updated
- Fixed: Hotkeys firing when input is focused
- Fixed: Range alerts using "<=" not working correctly
- Fixed: Some alerts being missed due to logic bug
- Fixed: Other small bugs

v0.4.2a:

- Added: "Follow My Chars" - If activated, yellow crosshairs represent systems containing your chars.  You may need to change systems for a char to show up
- Fixed: Unintended display of game log lines if they contain ignored text
- Fixed: Unintended exit when typing "Q" into an input field, E.g. "QCWA-Z"

v0.4.0b:

- Added: Ability to reorder alerts in alert list (cosmetic only at this stage)
- Changed: Rewrote log reading system
- Fixed: Multiple characters running causes issues with reading log files

v0.3.1b:

- Fixed: Reading log files when running multiple chars

v0.3.0b:

- Added: New configuration system
- Added: Persistent system labels
- Added: Ability to select custom sounds for alerts
- Added: Ability to limit how often custom alerts trigger
- Added: System and custom text ignore lists
- Added: Ability to disable file events from displaying
- Change: Rewrote alerting system
- Change: All OpenGL is now ModernGL - no immediate mode
- Change: Updated font library
- Change: Changed the display font
- Fixed: Duplicate intel reporting after stopping and restarting log watching
- Fixed: Venal intel not parsing (i think...)
- Fixed: Numerous small bugs
- Fixed: Crash bug on load

v0.2.2b:

- Added: Custom jump range on all alerts (except home system)
- Added: Form maximise enabled again
- Change: Refactored chat log parsing substantially
- Change: Jump ranges are now calculated in seperate background thread
- Change: Numerous internal code refactorings
- Change: Changed "red" crosshair to a new test style
- Fixed: Jump ranges now update when change the home system
- Fixed: Formatting issues in intel text output
- Fixed: Log reader not starting at end of file

v0.2.0b:

- Added: Sound alers for +5, +3, +1, and same system intel
- Added: Settings
- Added: Tabbed intel
- Added: Combat log parsing for custom alert triggers eg. "Dread Guristas"
- Change: Quite few internal changes
- Fixed: Several crash bugs
- Fixed: Numerous small bugs

v0.1.2a:

* Added: Regex name parsing
* Added: Ability to clear home system
* Added: Basic system search
* Change: Main window resizable
* Fixed: Right click allowed setting of more than one home system
* Fixed: Player names containing systems names triggering alerts 

NB: clearing the home system will disable jump range calulation.