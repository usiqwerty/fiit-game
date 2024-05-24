using System;
using System.IO;
using UnityEngine;

public static class SaveSystem
{
    private const int slotCount = 3;
    private const string infoFileName = "info.json";
    private const string keysFileName = "keys.json";

    private static string _path;
    
    public static int CurrentSlotNumber { get; private set; }
    public static string CurrentSlotName { get; private set; }

    public static SlotInfo[] GetSlotInfos()
    {
        var path = $"{Application.persistentDataPath}/slot";
        var slots = new SlotInfo[slotCount];
        for (var i = 0; i < slotCount; i++)
        {
            if (File.Exists($"{path}{i + 1}/{infoFileName}"))
                slots[i] = GetSlotInfo(i + 1);
        }
        return slots;
    }

    public static SlotInfo CreateAndLoadSlot(int slotNumber, string name)
    {
        var info = new SlotInfo
        {
            SlotName = name,
            LastSaveTime = DateTime.Now,
            PlayerScene = "Hall/Scene",
            PlayerPosition = Vector2.zero
        };
        var path = $"{Application.persistentDataPath}/slot{slotNumber}/";
        Directory.CreateDirectory(path);
        File.WriteAllText(path + infoFileName, JsonUtility.ToJson(info));
        _path = path;
        CurrentSlotNumber = slotNumber;
        CurrentSlotName = name;
        SaveKeys(Array.Empty<string>());
        return info;
    }

    public static SlotInfo LoadSlot(int slotNumber)
    {
        var info = GetSlotInfo(slotNumber);
        _path = $"{Application.persistentDataPath}/slot{slotNumber}/";
        CurrentSlotNumber = slotNumber;
        CurrentSlotName = info.SlotName;
        ArtefactStorage.Load(LoadKeys());
        return info;
    }

    public static void DeleteSlot(int slotNumber)
    {
        Directory.Delete($"{Application.persistentDataPath}/slot{slotNumber}/", true);
    }

    private static SlotInfo GetSlotInfo(int slotNumber)
    {
        var path = $"{Application.persistentDataPath}/slot{slotNumber}/{infoFileName}";
        if (!File.Exists(path))
            throw new ArgumentException("Slot save not exist.", nameof(slotNumber));
        return JsonUtility.FromJson<SlotInfo>(File.ReadAllText(path));
    }

    public static void SaveToCurrentSlot(string playerScene, Vector2 playerPosition)
    {
        var info = new SlotInfo
        {
            SlotName = CurrentSlotName,
            LastSaveTime = DateTime.Now,
            PlayerScene = playerScene,
            PlayerPosition = playerPosition
        };
        File.WriteAllText(_path + infoFileName, JsonUtility.ToJson(info));
        SaveKeys(ArtefactStorage.Save());
    }

    public static T LoadLevelState<T>(string levelName)
        => JsonUtility.FromJson<T>(File.ReadAllText(_path + $"{levelName}.json"));

    public static void SaveLevelState<T>(string levelName, T state)
        => File.WriteAllText(_path + $"{levelName}.json", JsonUtility.ToJson(state));

    private static string[] LoadKeys()
        => JsonUtility.FromJson<KeysInfo>(File.ReadAllText(_path + keysFileName)).Keys;

    private static void SaveKeys(string[] keys)
        => File.WriteAllText(_path + keysFileName, JsonUtility.ToJson(new KeysInfo { Keys = keys }));
}
