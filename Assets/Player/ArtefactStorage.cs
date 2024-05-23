using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public static class ArtefactStorage
{
    private readonly static List<string> _keys = new();
    public readonly static List<Artefact> Artefacts = new();
    
    
    public static void Load(string[] keys)
    {
        _keys.Clear();
        _keys.AddRange(keys);
    }

    public static void DropLastKey(float x, float y)
    {
        MonoBehaviour.print("drop last");
        var artefactKey = _keys[^1];
        var artefact = Artefacts.First(art => art.Name == artefactKey);
        _keys.Remove(artefactKey);
        Artefacts.Remove(artefact);
        artefact.OnDrop(x, y);
    }

    public static void GrabArtefact(Artefact artefact)
    {
        AddKey(artefact.Name);
        Artefacts.Add(artefact);
        MonoBehaviour.print($"grabbed {artefact.Name}");
    }
    public static string[] Save() => _keys.ToArray();
    public static int Count => _keys.Count; 
    public static void AddKey(string key) => _keys.Add(key);

    public static bool ContainsKey(string key) => _keys.Contains(key);
    public static bool ContainsKeys(string[] keys) => keys?.All(k => _keys.Contains(k)) ?? true;
}
