using UnityEngine;

public class Secretary : MonoBehaviour
{
    private bool _allowIn;
    private DoorScript _deanDoor;
    private GameObject _player;

    private bool _isShow = false;
    private MessageBoxEntry _messageBox;

    void Start()
    {
        _allowIn = ArtefactStorage.ContainsKeys(ArtefactStorage.Required);
        _deanDoor = GameObject.FindGameObjectWithTag("DeanDoor").GetComponent<DoorScript>();
        _deanDoor.RequiredKeys = ArtefactStorage.Required;
        _player = GameObject.FindGameObjectWithTag("Player");
        _messageBox = GameObject.Find("MessageBox").GetComponent<MessageBox>().CreateEntry();
    }

    private void Update()
    {
        if (_isShow != (_player.transform.position - transform.position).sqrMagnitude < 9)
        {
            _isShow = !_isShow;
            if (!_allowIn)
            {
                if (_isShow)
                    _messageBox.Activate("Вы же не собрали всё, что нужно! Приходите, когда будете готовы к пересдаче");
                else
                    _messageBox.Disable();
            }
        }
    }
}