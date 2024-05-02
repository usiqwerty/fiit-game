using System;
using UnityEngine;

[Serializable]
public class SlotInfo
{
    /// <summary>Название слота сохранения.</summary>
    public string SlotName { get; set; }
    /// <summary>Время последнего сохранения</summary>
    public DateTime LastSaveTime { get; set; }
    /// <summary>Сцена, на которой игрок находится.</summary>
    public string PlayerScene { get; set; }
    /// <summary>Позиция в сцене, на которой игрок находится.</summary>
    public Vector2 PlayerPosition { get; set; }
}
