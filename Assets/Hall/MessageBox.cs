using TMPro;
using UnityEngine;

public class MessageBox : MonoBehaviour
{
    private static MessageBox instance;

    public GameObject MessageBoxPanel;
    public TextMeshProUGUI MainText;
    public TextMeshProUGUI ActionText;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void Active(string mainText, string actionText)
    {
        MainText.text = mainText;
        ActionText.text = actionText;
        MessageBoxPanel.SetActive(true);
    }

    public void Disable()
    {
        MessageBoxPanel.SetActive(false);
    }
}
