using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Answer : MonoBehaviour
{
    public bool isCorrect;
    public string DisplayableText;
    public Action onAnswerCallback;
    private GameObject _player;
    private Enemy _kitten;
    private bool _answered;
    
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _kitten = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy>();
    }

    void Update()
    {
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
            if (DisplayableText!=null)
                GUI.Label(new Rect(100, 130, 200, 30), DisplayableText);
            GUI.Label(new Rect(100, 160, 200, 30), "Нажмите X, чтобы ответить");
        }
    }
}