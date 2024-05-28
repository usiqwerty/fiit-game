using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class MessageBox : MonoBehaviour
{
    private static MessageBox instance;

    public GameObject MessageBoxPanel;
    public TextMeshProUGUI MainText;
    public TextMeshProUGUI ActionText;

    private HashSet<MessageBoxEntry> _entries;
    private Dictionary<MessageBoxEntry, State> _activeStates;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        _entries = new();
        _activeStates = new();
    }

    public MessageBoxEntry CreateEntry()
    {
        var entry = new MessageBoxEntry(this);
        _entries.Add(entry);
        return entry;
    }

    public void Activate(MessageBoxEntry entry,
        string mainText, string actionText = null, Color? actionTextColor = default)
    {
        if (!_entries.Contains(entry))
            throw new InvalidOperationException();
        var state = new State(mainText, actionText, actionTextColor ?? Color.white);
        SetState(state);
        _activeStates.Add(entry, state);
        MessageBoxPanel.SetActive(true);
    }

    public void Disable(MessageBoxEntry entry)
    {
        if (!_entries.Contains(entry))
            throw new InvalidOperationException();
        if (!_activeStates.Remove(entry))
            return;
        if (_activeStates.Any())
            SetState(_activeStates.First().Value);
        else
            MessageBoxPanel.SetActive(false);
    }

    private void SetState(State state)
    {
        MainText.text = state.MainText;
        ActionText.text = state.ActionText;
        ActionText.color = state.Color;
    }

    private readonly struct State
    {
        public string MainText { get; }
        public string ActionText { get; }
        public Color Color { get; }

        public State(string mainText, string actionText, Color color)
        {
            MainText = mainText;
            ActionText = actionText;
            Color = color;
        }
    }
}
