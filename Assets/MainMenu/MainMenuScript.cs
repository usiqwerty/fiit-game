using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public GameObject[] Slots;

    public GameObject PlayerPrefab;

    void Start()
    {
        LoadSlots();
    }

    public void QuitClick()
    {
        Application.Quit();
    }

    private void LoadSlots()
    {
        var slots = SaveSystem.GetExistingSlots();
        for (var i = 0; i < slots.Length; i++)
            if (slots[i])
                EditSlot(i, $"Слот {i + 1}", true);
    }

    public void NewGame(int number) => StartGame(number);

    public void LoadSlot(int number) => StartGame(number);

    private void StartGame(int slotNumber)
    {
        SaveSystem.LoadSlot(slotNumber);
        SceneManager.LoadSceneAsync("Hall/Scene").completed += CreatePlayer;
    }

    private void CreatePlayer(AsyncOperation _)
    {
        var player = Instantiate(PlayerPrefab);
        player.name = "Player";
        DontDestroyOnLoad(player);
    }

    public void DeleteSlot(int number)
    {
        SaveSystem.DeleteSlot(number);
        EditSlot(number - 1, "Пусто", false);
    }

    private void EditSlot(int id, string text, bool isExist)
    {
        Slots[id].transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = text;
        Slots[id].transform.GetChild(1).gameObject.SetActive(!isExist);
        Slots[id].transform.GetChild(2).gameObject.SetActive(isExist);
        Slots[id].transform.GetChild(3).gameObject.SetActive(isExist);
    }
}
