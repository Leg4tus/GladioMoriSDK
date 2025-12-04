Instructions:
-Download the repo
-Open it using the correct Unity version (6000.0.59f2)
-Rename the Assets/{MyMod} folder to something unique

Renaming the folder is important so that there are no conflicting asset paths with other mods




Creating arenas:
-name the arena: "map_{yourArenaName}"
-Add a Gameobject named "SpawnPoints"
	-Add child objects inside (these children will be the spawn points for the players)
-Put your colliders on layer "16:MapPart"
-If you want there to be collision sounds, add "SoundMaterial" script to colliders or their parent objects
-If you want to set your own music for the arena, add a gameObject with SceneMusicOverride script and select the music you want to include
-Add your arena to AssetBundle "maps"
-Select Tools->Build AssetBundles
-Navigate to folder "AssetBundleOutput" at the root of the project
-File "maps" contains your map

To test your map:
-Create a folder at the root of your GladioMori installation called "Mods/{yourModName}"
-Copy "maps" file into this folder
-Start gladio mori
-You should see your map in the map list
