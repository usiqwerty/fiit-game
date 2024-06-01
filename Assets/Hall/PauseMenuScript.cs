using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    private bool _paused;

    public SingletonScript UnloadableCanvas;
    public GameObject EventSystem;
    public GameObject PauseMenuUI;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
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
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0;
        _paused = true;
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
