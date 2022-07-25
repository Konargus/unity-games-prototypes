using System;

public interface IButtonText
{
    event Action OnClick;
    void SetLabelText(string text);
}