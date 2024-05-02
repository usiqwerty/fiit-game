using UnityEngine;

public class ArtefactScript : MonoBehaviour
{
    /// <summary>Ключ, соответствующий этому артефакту.</summary>
    public string KeyName;

    void Start()
    {
        if (KeySystem.ContainsKey(KeyName))
            Destroy(gameObject);
    }
}
