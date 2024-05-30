using System;
using UnityEngine;

public class Answer : MonoBehaviour
{
    public bool isCorrect;
    public Action onAnswerCallback;
    private GameObject _player;
    private MessageBoxEntry _messageBox;
    private Enemy _kitten;
    private bool _answered;

    private Vector2? _targetPosition;
    private const float _speed = 10f;

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
        // 
        if (_targetPosition != null)
        {
            transform.position =
                Vector2.MoveTowards(transform.position, _targetPosition.Value, _speed * Time.deltaTime);
            if (transform.position == _targetPosition)
                _targetPosition = null;
        }
        if (_isShow != ((_player.transform.position - transform.position).sqrMagnitude < 2))
        // if ((_player.transform.position - transform.position).magnitude < 2)
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
            if (Input.GetKey(KeyCode.X) && !_answered)
            {
                _answered = true;
                if (isCorrect)
                {
                    _kitten.DropAllAwards();
                    onAnswerCallback?.Invoke();
                }
                else
                    _kitten.FollowPlayer = true;
            }
        }
    }
}