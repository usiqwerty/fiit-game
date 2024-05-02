using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;

public static class KeySystem
{
    private readonly static HashSet<string> _keys = new();

    public static void Load(string[] keys)
    {
        _keys.Clear();
        _keys.AddRange(keys);
    }

    public static string[] Save() => _keys.ToArray();

    public static void AddKey(string key) => _keys.Add(key);

    public static bool ContainsKey(string key) => _keys.Contains(key);
    public static bool ContainsKeys(string[] keys) => keys?.All(k => _keys.Contains(k)) ?? true;
}
