using System;
using System.IO;
using UnityEngine;

public static class SaveSystem
{
    private const int slotCount = 3;

    private static string _path;

    public static int CurrentSlotNumber { get; private set; } = -1;
    
    public static event Action SlotLoaded;
    public static event Action SavingSlot;

    public static bool[] GetExistingSlots()
    {
        var path = $"{Application.persistentDataPath}/slot";
        var slots = new bool[slotCount];
        for (var i = 0; i < slotCount; i++)
            if (Directory.Exists($"{path}{i + 1}"))
                slots[i] = true;
        return slots;
    }

    public static void LoadSlot(int slotNumber)
    {
        var path = $"{Application.persistentDataPath}/slot{slotNumber}/";
        Directory.CreateDirectory(path);
        _path = path;
        CurrentSlotNumber = slotNumber;
        SlotLoaded?.Invoke();
    }

    public static void SaveSlot()
    {
        SavingSlot?.Invoke();
    }

    public static void DeleteSlot(int slotNumber)
    {
        Directory.Delete($"{Application.persistentDataPath}/slot{slotNumber}/", true);
    }

    public static T LoadState<T>(string name)
    {
        var path = _path + $"{name}.json";
        if (!File.Exists(path))
            return default;
        return JsonUtility.FromJson<T>(File.ReadAllText(path));
    }

    public static void SaveState<T>(string name, T state)
        => File.WriteAllText(_path + $"{name}.json", JsonUtility.ToJson(state));
}
