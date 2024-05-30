using UnityEngine;

public class MessageBoxEntry
{
    private readonly MessageBox _messageBox;

    public MessageBoxEntry(MessageBox messageBox)
    {
        _messageBox = messageBox;
    }

    public void Activate(string mainText, string actionText = null, Color? actionTextColor = default)
        => _messageBox.Activate(this, mainText, actionText, actionTextColor);

    public void Disable()
        => _messageBox.Disable(this);
}
