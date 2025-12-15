# Instructions:
- Download the repo <br/>
- Open it using the correct Unity version (6000.0.59f2) <br/>
- Rename the Assets/{MyMod} folder to something unique <br/>
- Keep your files inside your unique mod folder <br/>

Renaming the folder is important so that there are no conflicting asset paths with other mods




# Creating arenas:
- Name the arena: "map_{yourArenaName}" <br/>
- Add a Gameobject named "SpawnPoints"<br/>
	- Add child objects inside (these children will be the spawn points for the players)<br/>
- Put your colliders on layer "16:MapPart"<br/>
- If you want there to be collision sounds, add "SoundMaterial" script to colliders or their parent objects<br/>
- If you want to set your own music for the arena, add a gameObject with SceneMusicOverride script and select the music you want to include<br/>
- Add your arena to AssetBundle "maps"<br/>
- Select Tools->Build AssetBundles<br/>
- Navigate to folder "AssetBundleOutput" at the root of the project<br/>
- File "maps" contains your map<br/>



# Creating weapons:
- I recommend that you create a copy of a example weapon and edit the copy. Creating a copy ensures that the weapon has unique Identifiers and does not conflict with other mods.
- Rename your prefab
- Add your own 3D model to the prefab
- Delete the old 3D model from the prefab
- Do not rotate the HandleCollider or HoldPosition. Instead rotate your 3D model so that the rotation matches the HandleCollider.
- Resize the HandleCollider so that it fits you handle
- Never scale any of the colliders or their parents. Instead modify the collider components.
- Move the HoldPosition on the HandleCollider to where the default hold should be
- Resize and move the rest of the colliders and triggers to fit your weapon
- Create new colliders/triggers or remove some if you weapon needs it
- Add your weapon to "equipment" asset bundle and remove other example equipment from this bundle (CandyCane, ChristamsHat, SmallBillhook)

## Weapon parts:
- BladeColliders: Colliders that collide with bones and players bodyparts if the edge is not aligned to make a cut. 
  - These should be added into "Blade Colliders" list in weapon script so that their collisions can be disabled for cutting.  
  - Layer: weapons
- BladeTrigger/BladeTriggerChild: Triggers that cause damage when they hit organs.
  - You should have only one BladeTrigger. If your weapons shape requires it you can add any number of child triggers
  - Layer: Blade
-LargeBladeTrigger/LargeBladeTriggerChild: Disables collisions between bladeCollider and players bodyparts. 
  - Should be oversized so that the collisions are disabled before they happen.
  - If you want to make sure that cutting is disabled from some direction, you can add a LargeBladeTriggerChild with "Blunt side" to that side. This will make sure that collisions are not disabled when hitting with that side.
  - Layer: Blade
- CenterOfMass: Weapons Rigidbodys center of mass will be placed here
- HoldPosition: Default position for holding the weapon.
  - Should be on the weapons handle
- EquipmentToEquipmentColliders: Colliders that collide with maps and other equipment (weapons and armour)
  - These should be slightly larger than the basic colliders(bladeColliders, handleCollider, GuardCollider) 
  - Layer: EquipmentToEquipmentColliders
- Painter: Defines a line where blood should be painted when cutting
  - Should be along the weapons edge
  - You can have multiple of these and they should be added to BladeTriggers.BladePainters list so that they will be activated by the correct BladeTrigger/BladeTriggerChild
- WeaponEdgeSections: Define a line for the cutting edge
  - Used by the Ai to detect danger and dismemberment system to detect when a bodypart has been cut through
  - You can define multiple lines and add them to Weapon scripts "Weapon Edge Sections"
- WeaponCenterOfMassLine: Should be a line with just two points spanning the weapons length
  - Used in blunt damage calculations
  - Should be added to Weapons scripts "Blunt Damage Dealer" -> "Center Of Mass Line"
- BladeTip: Used in detecting stabs on bones in certain situations. Should be at the end of the weapon

## Scripts:

### Weapon:
- Handle: Reference to the handle script in HandleCollider. Handle will register itself. Can Be left empty.
- Center Of Mass: Reference to a transform that will be set as the Center Of Mass for the Rigidbody
- Blunt Damage Dealer:
  - Equipment: Leave empty
  - Rb: Leave empty
  - Blunt Damage Type: Slightly changes the weapons blunt damage capabilities. Default is fine for most weapons. If weapon should have more armour penetration, use Mace.
  - Center Of Mass Line: Should be two points at both ends of the weapon
  - Override mass and Override Mass To Use: Enable and use these, if the rigidbody weight differs from weight that should be used for blunt damage calculations. 
   - For example: Weapons should handle light, but should have some weight behind the hits
- Blade Trigger: Should have the BladeTrigger script from a child object
- Blade Colliders: Should contain the colliders that will be deactivated when cutting through something
- Disable Local Logic: Leave off
- Weapons Max/Min Distance: Used by Ai to determine the distance where it can attack.
- Weapon Edge Sections: Define a line for the cutting edge
  - Used by the Ai to detect danger and dismemberment system to detect when a bodypart has been cut through
  - You can define multiple lines and add them to Weapon scripts "Weapon Edge Sections" 

### NetworkIdentity:
- AssetId: Unique id for networking. Needs to be unique. If you created a copy of a example, the value should have been generated automatically.

### SoundMaterial:
- Should be in a gameObject that has a collider or its parent object
- SoundMaterialType: Defines the material for collision sounds

### Blade:
- Joint: Leave empty
- Bone Joint: Leave empty
- Weapon Rigidbody: Set the rigidbody of the weapon here
- Drag point: Can be any transform inside the weapon. Will be the basis for the dragging when cutting into something. Works best when set near the tip of the weapons edge.
- Blade Trigger Collider: Set the trigger for the blade here
- Weapon: Set the weapon script from parent object here
- Stab/Slash Multiplier: How easily the blade moves while cutting. Above 3.3 = razor sharp. 1 = quite dull. Value can be negative if you want to stop the weapon in its tracks.
- Stab/Slash Bone Multiplier: Multiplier for how easy it is to cut through bones. Value of 1 works fine most of the time.
- Blade Tip: Should reference a transform at the tip of the weapon. 
- Blade Painters: Painters that will be activated when this blade is cutting something

### BladeChild:
- Blade: Should reference a Blade script
- Blade Painters: Painters that will be activated when this blade is cutting something

### LargeBladeTrigger:
- Disables collisions between bodyparts and BladeColliders when this trigger hits bodyparts
- Weapon: You can leave this empty. Contains a reference to the weapon script.
- Blade Penetrating: Leave off
- Disable Local Logic: Leave off
- Blunt Side: Reverses the functionality so that collision do not get disabled.

### LargeBladeTriggerChild:
- Large Blade Trigger: Should reference a LargeBladeTrigger script
- Blunt Side: Reverses the functionality so that collision do not get disabled.

### BladePainter:
- Drawer width: Width of the "blade" that is doing the painting
- Position 0/1: Will paint between these two points

### Handle:
- Weapon: Can be left empty
- Equipment: Can be left empty
- Weapon Rigidbody: Should reference the weapons Rigidbody
- Hold Position: Default hold position
- Physical Hold Rotation: Determines which way the weapon is held. Strongly recommended to keep values from examples.
- Handle Colliders: When weapon is held collisions will be disabled between these colliders and armour colliders on players arms. These collisions can often cause issues with movesets and physics glitches.
- Non Grabbable Transform: If you have positions on your handle that should not be grabbable, define them here
- Non Grabbable Handle Positions: Leave empty


# Testing:<br/>
- Create a folder at the root of your GladioMori installation called "Mods/{yourModName}"<br/>
- Copy your asset bundle (maps/equipment) file into this folder<br/>
- Start Gladio Mori<br/>
- Your map/equipment should be loaded into use<br/>

# Uploading content to mod.io:
- Add your files to a zip with no subfolders
- Create the mod in mod.io
- Select relevant tags:
- Moveset:
  - Should contain one or more moveset files with ".json" extension
- Player skin:
  - Should contain one or more custom texture files with ".jpg", ".jpeg" or ".png" extension
- Local mod:
  - Should contain a dll mod that is not required by the clients in a multiplayer game. Server side mods even when altering multiplayer experience should be in this category
- Multiplayer mod:
  - Mods that are required by the clients and should be automatically downloaded.
- Arena:
  - Should contain a asset bundle called "maps". These are automatically loaded by clients when joining a server.
- Equipment:
  - Should contain a asset bundle called "equipment". These are automatically loaded by clients when joining a server.
 
# The mod load order is:
- Load modio subscribed maps
- Load modio subscribed equipment
- Load all mods from local mods folder in alphabetical order
- Load modio subscribed dll mods
