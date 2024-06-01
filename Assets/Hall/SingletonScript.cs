using UnityEngine;

public class SingletonScript : MonoBehaviour
{
    private static SingletonScript instance;

    private void Start()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void DestroySingleton()
    {
        Destroy(instance.gameObject);
        instance = null;
    }
}
