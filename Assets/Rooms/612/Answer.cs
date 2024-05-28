using UnityEngine;

public class Answer : MonoBehaviour
{
    public bool isCorrect;
    private GameObject _player;
    private MessageBoxEntry _messageBox;
    private Enemy _kitten;
    private bool _answered;

    private bool _isShow = false;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _kitten = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy>();
        _messageBox = GameObject.Find("MessageBox").GetComponent<MessageBox>().CreateEntry();
    }

    private void OnDestroy()
    {
        _messageBox.Disable();
    }

    void Update()
    {
        if (_isShow != ((_player.transform.position - transform.position).sqrMagnitude < 2))
        {
            _isShow = !_isShow;
            if (_isShow)
                _messageBox.Activate("", "Нажмите E, чтобы ответить", Color.green);
            else
                _messageBox.Disable();
        }

        if (_isShow && Input.GetKey(KeyCode.E) && !_answered)
        {
            _answered = true;
            if (isCorrect)
                _kitten.DropAllAwards();
            else
                _kitten.FollowPlayer = true;
        }
    }
}