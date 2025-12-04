# Instructions:
- Download the repo <br/>
- Open it using the correct Unity version (6000.0.59f2) <br/>
- Rename the Assets/{MyMod} folder to something unique <br/>

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

To test your map:<br/>
- Create a folder at the root of your GladioMori installation called "Mods/{yourModName}"<br/>
- Copy "maps" file into this folder<br/>
- Start gladio mori<br/>
- You should see your map in the map list<br/>
