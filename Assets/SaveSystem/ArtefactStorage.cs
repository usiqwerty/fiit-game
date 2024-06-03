using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class ArtefactStorage
{
    private readonly static List<string> _keys = new();
    public readonly static List<Artefact> Artefacts = new();

    public static IReadOnlyList<string> Keys => _keys;

    public static event Action<string> ArtefactAdded;
    public static event Action<string> ArtefactRemoved;

    static ArtefactStorage()
    {
        SaveSystem.SlotLoaded += Load;
        SaveSystem.SavingSlot += Save;
        Load();
    }

    public static string[] Required = new []
    {
        "Акулье мясо",
        "CSharpPaper",
        "Дифференциал",
        "Число 5",
        "Трезубец",
    };
    
    public static void Load()
    {
        if (SaveSystem.CurrentSlotNumber == -1)
            return;
        ClearKeys();
        var state = SaveSystem.LoadState<ArrayState<string>>("artefacts");
        if (state != null)
            foreach (var key in state.Array)
                AddKey(key);
    }

    public static void Save()
    {
        SaveSystem.SaveState("artefacts", new ArrayState<string>(_keys.ToArray()));
    }

    public static void DropLastKey(float x, float y)
    {
        MonoBehaviour.print("drop last");
        var artefactKey = _keys[^1];
        var artefact = Artefacts.First(art => art.Name == artefactKey);
        RemoveKey(artefactKey);
        Artefacts.Remove(artefact);
        artefact.OnDrop(x, y);
    }

    public static void GrabArtefact(Artefact artefact)
    {
        AddKey(artefact.Name);
        Artefacts.Add(artefact);
        MonoBehaviour.print($"grabbed {artefact.Name}");
    }

    public static int Count => _keys.Count;
    public static void AddKey(string key)
    {
        if (_keys.Contains(key))
            return;
        _keys.Add(key);
        ArtefactAdded?.Invoke(key);
    }

    public static void RemoveKey(string key)
    {
        if (_keys.Remove(key))
            ArtefactRemoved?.Invoke(key);
    }

    private static void ClearKeys()
    {
        foreach (var key in _keys)
            ArtefactRemoved?.Invoke(key);
        _keys.Clear();
    }

    public static bool ContainsKey(string key) => _keys.Contains(key);
    public static bool ContainsKeys(string[] keys) => keys?.All(k => _keys.Contains(k)) ?? true;
}
