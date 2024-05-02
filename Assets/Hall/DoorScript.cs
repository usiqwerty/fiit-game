using UnityEngine;

public class DoorScript : MonoBehaviour
{
    /// <summary>Название сцены, в которую ведет дверь.</summary>
    public string SceneName;
    
    /// <summary>Текст, который отображается при подходе к двери.</summary>
    public string Text;
    
    /// <summary>Текст, который отображается дополнительно, если условия не выполнены.</summary>
    public string Warning;
    
    /// <summary>Список ключей (условий), которые нужны для возможности открытия двери.</summary>
    public string[] RequiredKeys;

    /// <summary>Координаты точки в сцене, на которой появится игрок.</summary>
    public Vector2 TargetPlayerPosition;
}
