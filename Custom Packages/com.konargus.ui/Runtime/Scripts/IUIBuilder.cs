using UnityEngine;

namespace com.konargus.ui
{
    public interface IUIBuilder
    {
        (Canvas, IMenuSimple) BuildSimpleMenu();
        (Canvas, IGameView) BuildGameView();
    }
}