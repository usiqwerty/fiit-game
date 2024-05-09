using System;
using UnityEngine;

[Serializable]
public class SlotInfo
{
    /// <summary>Название слота сохранения.</summary>
    public string SlotName;
    /// <summary>Время последнего сохранения</summary>
    public DateTime LastSaveTime;
    /// <summary>Сцена, на которой игрок находится.</summary>
    public string PlayerScene;
    /// <summary>Позиция в сцене, на которой игрок находится.</summary>
    public Vector2 PlayerPosition;
}
