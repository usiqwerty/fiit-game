using System;
using System.Collections.Generic;
using Rooms.Dean;
using UnityEngine;


public class Dean : MonoBehaviour
{
    public GameObject answerPrefab;
    private MessageBoxEntry _messageBox;
    private Queue<Question> _questions;

    private readonly Vector2 _answerPos1 = new Vector2(-10, 8);
    private readonly Vector2 _answerPosShift = new Vector2(3, 0);
    private GameObject _player;
    private bool _showText;
    private Question _currentQuestion;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _messageBox = GameObject.Find("MessageBox").GetComponent<MessageBox>().CreateEntry();
        _questions = new Queue<Question>();
        _questions.Enqueue(new Question
        {
            Text = "Как зовут препода?",
            Answers = new[] { ("Василий", true), ("Николай", false), ("Vfhbz", false) }
        });
        _questions.Enqueue(new Question
        {
            Text = "Как дела?",
            Answers = new[] { ("Василий", false), ("Николай", false), ("Vfhbz", true) }
        });
        _questions.Enqueue(new Question
        {
            Text = "Понравилось учиться на матмехе?",
            Answers = new[] { ("Нет", false), ("Да", true) }
        });
        NextQuestion();
    }

    private void Update()
    {
        if (_currentQuestion != null)
        {
            if (_showText != (transform.position - _player.transform.position).magnitude < 3)
            {
                _showText = !_showText;
                if (_showText)
                    _messageBox.Activate(_currentQuestion.Text, "", Color.green);
                else
                    _messageBox.Disable();
            }
                
        }

    }

    private void NextQuestion()
    {
        foreach (var answer in GameObject.FindGameObjectsWithTag("Answer"))
            Destroy(answer);
        if (_questions.Count == 0)
            GameOver();

        var question = _questions.Dequeue();
        _currentQuestion = question;
        var i = 1;

        foreach (var (text, correct) in question.Answers)
            GenerateAnswer(i++, text, correct);
    }

    private void GameOver()
    {
        throw new System.NotImplementedException();
    }

    private void GenerateAnswer(int number, string text, bool isCorrect)
    {
        var position = _answerPos1 + (number - 1) * _answerPosShift;
        print(position);
        if (number < 1 || number > 3) return;
        var answer = Instantiate(answerPrefab);
        var ansConfig = answer.GetComponent<Answer>();
        answer.transform.position = transform.position;
        ansConfig.DisplayableText = text;
        ansConfig.isCorrect = isCorrect;
        ansConfig.OnAnswerCallback = NextQuestion;
        ansConfig.Move(position);


        var spriteRenderer = answer.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = Resources.Load<Sprite>($"Sprite Assets/ans{number}");
    }
}