using System;
using Godot;
using JamTemplate.Managers;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Linq;
using JamTemplate.Util;
using JamTemplate.Stores;

namespace JamTemplate;

public partial class Entry : Node
{
  public static ServiceProvider ServiceProvider;

  public override void _EnterTree()
  {
    var Services = new ServiceCollection()
    .AddSingleton(InjectNodeClass<GameManager>())
    // .AddSingleton(InjectNodeClass<SaveManager>())
    .AddSingleton<PlayerDataStore>()
    .AddSingleton<ConfigStore>()
    .AddSingleton<SettingsStore>()
    .AddSingleton(InjectInstantiatedPackedScene<SceneManager>("res://views/SceneManager.tscn"))
    ;
    ServiceProvider = Services.BuildServiceProvider();
    var gameManager = ServiceProvider.GetRequiredService<GameManager>();
    GetTree().Root.CallDeferred("add_child", gameManager);
  }
  private Func<IServiceProvider, T> InjectNodeClass<T>() where T : Node, new()
  {
    return (serviceProvider) =>
    {
      var obj = new T();

      InjectAttributedMethods(obj, serviceProvider);

      return obj;
    };
  }
  private Func<IServiceProvider, T> InjectInstantiatedPackedScene<T>(string path) where T : Node
  {
    return (serviceProvider) =>
    {
      var packed = ResourceLoader.Load<PackedScene>("res://views/SceneManager.tscn");
      var node = packed.Instantiate<T>();

      InjectAttributedMethods(node, serviceProvider);

      GetTree().Root.CallDeferred("add_child", node);
      return node;
    };
  }
  private void InjectAttributedMethods<T>(T obj, IServiceProvider provider)
  {
    var objType = obj.GetType();
    var methods = objType
      .GetMethods(BindingFlags.Instance | BindingFlags.Public)
      .Where(method =>
      {
        return method.GetCustomAttribute<FromServicesAttribute>() != null;
      });

    foreach (var method in methods)
    {
      var args = method
        .GetParameters()
        .Select(param => provider.GetService(param.ParameterType)).ToArray();
      method.Invoke(obj, args);
    }
  }
}