using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreenScript : MonoBehaviour
{
    private static DeathScreenScript instance;

    public SingletonScript UnloadableCanvas;
    public GameObject EventSystem;
    public GameObject DeathScreenPanel;

    private void Start()
    {
        instance = this;
    }

    public static void Die()
    {
        PauseMenuScript.Enabled = false;
        instance.DeathScreenPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void DeleteAndQuit()
    {
        SaveSystem.DeleteSlot(SaveSystem.CurrentSlotNumber);
        Destroy(EventSystem);
        SceneManager.LoadSceneAsync("MainMenu/Scene").completed += _ =>
        {
            var player = GameObject.Find("Player");
            Destroy(player);
            UnloadableCanvas.DestroySingleton();
            Time.timeScale = 1;
        };
    }
}
