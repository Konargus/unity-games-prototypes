using UnityEngine;

namespace com.konargus.ui
{
    public interface IMenu
    {
        IButtonText AddButtonText();
        ILabel AddLabel();
        Transform Transform { get; }
        GameObject GameObject { get; }
        void Stretch();
    }
}