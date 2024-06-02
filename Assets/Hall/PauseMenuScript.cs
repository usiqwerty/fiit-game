using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    private bool _paused;

    public SingletonScript UnloadableCanvas;
    public GameObject EventSystem;
    public GameObject PauseMenuUI;
    public GameObject ExitButton;

    public static bool Enabled { get; set; }

    private void Start()
    {
        Enabled = true;
    }

    private void Update()
    {
        if (Enabled && Input.GetKeyDown(KeyCode.Escape))
        {
            if (_paused)
                Resume();
            else
                Pause();
        }
    }

    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1;
        _paused = false;
    }

    public void Pause()
    {
        _paused = true;
        Time.timeScale = 0;
        PauseMenuUI.SetActive(true);
        ExitButton.SetActive(SceneManager.GetActiveScene().path.Contains("Hall"));
    }

    public void SaveAndQuit()
    {
        SaveSystem.SaveSlot();
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
