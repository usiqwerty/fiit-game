using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour
{
    private MessageBoxEntry _messageBox;

    private DoorScript _targetDoor;
    private bool _isDoorEnabled;

    private float _prevActionTime;

    void Start()
    {
        _messageBox = GameObject.Find("MessageBox").GetComponent<MessageBox>().CreateEntry();
    }

    void Update()
    {
        if (_isDoorEnabled && Input.GetKey(KeyCode.E))
        {
            if (Time.time - _prevActionTime < 0.2)
                return;
            _prevActionTime = Time.time;

            var door = _targetDoor;
            if (door.SceneName == SceneManager.GetActiveScene().path[7..^6])
                gameObject.transform.position = door.TargetPlayerPosition;
            else
                SceneManager.LoadSceneAsync(door.SceneName).completed +=
                    _ => gameObject.transform.position = door.TargetPlayerPosition;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Door") || collision.CompareTag("DeanDoor"))
        {
            _targetDoor = collision.GetComponent<DoorScript>();
            _isDoorEnabled = ArtefactStorage.ContainsKeys(_targetDoor.RequiredKeys);
            if (_isDoorEnabled)
                _messageBox.Activate(_targetDoor.Text, "Нажмите Е, чтобы войти", Color.green);
            else
                _messageBox.Activate(_targetDoor.Text, _targetDoor.Warning, Color.red);
            return;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Door") || collision.CompareTag("DeanDoor"))
        {
            if (_targetDoor == collision.GetComponent<DoorScript>())
            {
                _targetDoor = null;
                _isDoorEnabled = false;
                _messageBox.Disable();
            }
        }
    }
}
