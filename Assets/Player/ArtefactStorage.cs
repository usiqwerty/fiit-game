using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class ArtefactStorage
{
    private readonly static List<string> _keys = new();
    public readonly static List<Artefact> Artefacts = new();
    
    static ArtefactStorage()
    {
        SaveSystem.SlotLoaded += Load;
        SaveSystem.SavingSlot += Save;
        Load();
    }
    
    public static void Load()
    {
        if (SaveSystem.CurrentSlotNumber == -1)
            return;
        _keys.Clear();
        var state = SaveSystem.LoadState<ArrayState<string>>("artefacts");
        if (state != null)
            _keys.AddRange(state.Array);
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

    public static int Count => _keys.Count; 
    public static void AddKey(string key) => _keys.Add(key);

    public static bool ContainsKey(string key) => _keys.Contains(key);
    public static bool ContainsKeys(string[] keys) => keys?.All(k => _keys.Contains(k)) ?? true;
}
