using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public static class KeySystem
{
    private readonly static List<string> _keys = new();
    private readonly static List<ArtefactScript> _artefacts = new();
    
    
    public static void Load(string[] keys)
    {
        _keys.Clear();
        _keys.AddRange(keys);
    }

    public static void DropLastKey(float x, float y)
    {
        MonoBehaviour.print("drop last");
        var artefactKey = _keys[^1];
        var artefact = _artefacts.First(art => art.KeyName == artefactKey);
        _keys.Remove(artefactKey);
        _artefacts.Remove(artefact);
        artefact.OnDrop(x, y);
    }

    public static void GrabArtefact(ArtefactScript artefact)
    {
        AddKey(artefact.KeyName);
        _artefacts.Add(artefact);
        MonoBehaviour.print($"grabbed {artefact.KeyName}");
    }
    public static string[] Save() => _keys.ToArray();
    public static int Count => _keys.Count; 
    public static void AddKey(string key) => _keys.Add(key);

    public static bool ContainsKey(string key) => _keys.Contains(key);
    public static bool ContainsKeys(string[] keys) => keys?.All(k => _keys.Contains(k)) ?? true;
}
