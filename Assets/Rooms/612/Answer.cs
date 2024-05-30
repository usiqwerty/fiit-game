using System;
using UnityEngine;

public class Answer : MonoBehaviour
{
    public bool isCorrect;
    public string DisplayableText;
    public Action onAnswerCallback;
    private GameObject _player;
    private Enemy _kitten;
    private bool _answered;

    private Vector2? _targetPosition;
    private const float _speed = 10f;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _kitten = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy>();
    }

    void Update()
    {
        if (_targetPosition != null)
        {
            transform.position =
                Vector2.MoveTowards(transform.position, _targetPosition.Value, _speed * Time.deltaTime);
            if (transform.position == _targetPosition)
                _targetPosition = null;
        }

        if ((_player.transform.position - transform.position).magnitude < 2)
        {
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

    private void OnGUI()
    {
        if ((_player.transform.position - transform.position).magnitude < 2)
        {
            if (DisplayableText != null)
                GUI.Label(new Rect(100, 130, 200, 30), DisplayableText);
            GUI.Label(new Rect(100, 160, 200, 30), "Нажмите X, чтобы ответить");
        }
    }

    public void Move(Vector2 targetPosition)
    {
        _targetPosition = targetPosition;
    }
}