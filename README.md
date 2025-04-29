# Godot 4.4 C# Jam Template Project

This will always be a work in progress but it should be enough to jumpstart your project.

## Features
- Save data manager
- Scene manager with loading scene
- Doesn't include other libraries

## Planned features
- Scene manager preloading assets
- Audio manager for handling your bgm from one place
- Flexible code driven FSM for easy state logic
- Create project from `dotnet new`

## Folder Structure
- **scripts**, each folder in here should mirror the structure of where the file is used
  - **managers**, the main singleton pattern objects that run the game
  - **stores**, the scripts that control persisted data files
- **components**, Contains composition component scenes
- **views**, scene files that are the main composed views of the game, think unity scenes
- **assets**, the main files that hold textures, materials, models, sounds, etc

## Code structure
### Managers
Most Managers are made to run permanently. They are added to the project in the Globals setting so that they are added to the SceneTree before the game starts

### SceneManager
This is a class that handles switching scene files at a root level as is added as a Global so that it is loaded before the start of the game.<br>
It will emit scene loading scene events when a new scene is requested so that you may listen to it in your code.

### GameManager
This is the main entry point of the project. Its job is to start the first scene of your project and as such it is the default scene of the project.<br>
By default the initial scene is set to `res://views/boot.tscn` but you can change it to whatever you need your initial scene to be. <br>
This class may be expanded to include your main game state.

### SaveManager
This is a singleton that is used to persist nodes as .scn files to the user directory.
The _EnterTree method calls loading on any objects in the SceneTree that are in the `savedata` node group.

### Data stores
These are globals in the project that are used as the persistent files. The node name dictates the filename that is used to save. These files are automatically loaded when the game starts if they are available.

The current procedure here for saving and loading is naive but works.