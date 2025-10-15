using System;
using System.Collections.Generic;
using Godot;

namespace JamTemplate.Util;

public static class NodeExtensions
{
  public static IEnumerable<T> FindChildren<T>(this Node node, Type type, bool recursive = false) where T : Node
  {
    var children = new List<T>();
    foreach (var n in node.GetChildren())
    {
      if (n is Node childNode)
      {
        if (childNode is T)
        {
          children.Add((T)childNode);
        }

        if (recursive)
        {
          children.AddRange(childNode.FindChildren<T>(type, true));
        }
      }
    }
    return children;
  }
}