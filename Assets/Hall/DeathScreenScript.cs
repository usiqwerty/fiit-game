using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreenScript : MonoBehaviour
{
    private static DeathScreenScript instance;

    public SingletonScript UnloadableCanvas;
    public TextMeshProUGUI Text;
    public GameObject EventSystem;
    public GameObject DeathScreenPanel;

    private void Start()
    {
        instance = this;
    }

    public static void Win()
    {
        PauseMenuScript.Enabled = false;
        instance.Text.text = "Вы победили!";
        instance.Text.color = Color.green;
        instance.DeathScreenPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public static void Die()
    {
        PauseMenuScript.Enabled = false;
        instance.Text.text = "Отчислен!";
        instance.Text.color = Color.red;
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
