using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    private bool _paused;

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
        var player = GameObject.Find("Player");
        if (!player.TryGetComponent<Controller>(out var _))
            return;
        SaveSystem.SaveSlot();
        Destroy(player);
        SceneManager.LoadScene("MainMenu/Scene");
        Time.timeScale = 1;
    }
}
