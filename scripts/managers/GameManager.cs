using Godot;
using JamTemplate.Util;

namespace JamTemplate.Managers;

public partial class GameManager : Node
{
  private ConfigFile _config;
  private SceneManager _sceneManager;
  [FromServices]
  public void Inject(SceneManager sceneManager)
  {
    GD.Print(GetType().Name, " Constructed");
    _config = new ConfigFile();
    _config.Load("res://config.ini");
    _sceneManager = sceneManager;
  }
  public override void _Ready()
  {
    GD.Print(GetType().Name, " Started");
    var InitialSceneName = (string)_config.GetValue("game", "INITIAL_SCENE_NAME");
    var currentScene = GetTree().CurrentScene;
    if (currentScene.Name != "Entry")
    {
      InitialSceneName = currentScene.Name;
      currentScene.QueueFree();
    }
    if (InitialSceneName != "")
    {
      _sceneManager.ChangeScene(InitialSceneName);
    }
  }
}
