using System.Collections;
using System.Collections.Generic;
using Rooms.Dean;
using UnityEngine;


public class Dean : MonoBehaviour
{
    public GameObject answerPrefab;

    private Queue<Question> _questions;

    // private Enemy _enemy;
    // private GameObject _player;
    void Start()
    {
        // _enemy = GetComponent<Enemy>();
        // _player = GameObject.FindGameObjectWithTag("Player");
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
            Answers = new[] {("Нет", false), ("Да", true) }
        });
        NextQuestion();
    }

    public void NextQuestion()
    {
        if (_questions.Count == 0)
            return;
        var question = _questions.Dequeue();
        var i = 1;
        foreach (var (text, correct) in question.Answers)
            GenerateAnswer(i++, text, correct);
    }

    private void GenerateAnswer(int number, string text, bool isCorrect)
    {
        if (number < 1 || number > 3) return;
        var answer = Instantiate(answerPrefab);
        var ansConfig = answer.GetComponent<Answer>();
        ansConfig.DisplayableText = text;
        ansConfig.isCorrect = isCorrect;
        ansConfig.onAnswerCallback = NextQuestion;

        answer.transform.position = transform.position;
        var spriteRenderer = answer.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = Resources.Load<Sprite>($"Sprite Assets/ans{number}");
    }
}