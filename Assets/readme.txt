MAIN ROADMAP
add more objects!
 have a list of objects on a toolbar, with buttons, do it settings
auto-select the slot that contains the asset that is already assigned to the component you're selecting for
upload new assets to cloudinary on building json
TweenTo component
make studiocanvas clamp to actual canvas and not mouse pos on screen
ignore escape key if file selector winow is open
message that indicates that sprite already exists. replace sprite?
refactor out the asset deletion window to be a generic confirmation window that uses a delegate system. so you can use it for deleting assets AND for confirming replacement of assets, among other things in the future
refactor assetselector and assetgallery. assetgallery creates the slots. simplify that. we shouldn't two dictionaries and a list for this shit.
fix that bug!!

use new input system for escape key close panel and shift modifier

NICE-TO-HAVES
add tab feature to position controls later on. all fields add themselves to a static tab list that is queued through
get rid of red question mark... throw error if it is going to be generated
show a bit of handles if you mouse over but only show fully handles if you "select" an object
(punt this until later when we've expanded the scope of this to selecting multiple objects)
upload multiple files at once

POLISH
after changing size, clamp position to screen (will possibly be fixed by having toolbar)
after changing asset, make sure it's on screen (can be off screen from resizing/positioning) (also might be unecessary with toolbar)
add a hint bar on bottom of screen that tells you controls
add opening animation/closing animation for selector
find higher def handle sprites
add DOTween classes so hovering over things can do some stuff

CONTINUAL
improve naming of prefabs and hierarchy objects, class names, etc