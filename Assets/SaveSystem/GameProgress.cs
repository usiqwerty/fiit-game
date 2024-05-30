using System.Collections.Generic;

public static class GameProgress
{
    public static List<string> EnemiesKilled = new();

    static GameProgress()
    {
        SaveSystem.SlotLoaded += Load;
        SaveSystem.SavingSlot += Save;
        Load();
    }

    public static void Load()
    {
        if (SaveSystem.CurrentSlotNumber == -1)
            return;
        EnemiesKilled.Clear();
        var state = SaveSystem.LoadState<ArrayState<string>>("enemiesKilled");
        if (state != null)
            EnemiesKilled.AddRange(state.Array);
    }

    public static void Save()
    {
        SaveSystem.SaveState("enemiesKilled", new ArrayState<string>(EnemiesKilled.ToArray()));
    }

}