using System;

namespace com.konargus.ui
{
    public interface IButtonText
    {
        event Action OnClick;
        void SetLabelText(string text);
    }
}