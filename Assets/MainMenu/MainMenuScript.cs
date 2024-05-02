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
        var slots = SaveSystem.GetSlotInfos();
        for (var i = 0; i < slots.Length; i++)
            if (slots[i] != null)
                EditSlot(i, slots[i].SlotName, true);
    }

    public void NewGame(int number)
    {
        var info = SaveSystem.CreateAndLoadSlot(number, $"Слот {number}");
        StartGame(info);
    }

    public void LoadSlot(int number)
    {
        var info = SaveSystem.LoadSlot(number);
        StartGame(info);
    }

    private void StartGame(SlotInfo info)
    {
        SceneManager.LoadScene(info.PlayerScene);
        var player = Instantiate(PlayerPrefab);
        player.GetComponent<Controller>().Initialize(info.PlayerScene, info.PlayerPosition);
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
