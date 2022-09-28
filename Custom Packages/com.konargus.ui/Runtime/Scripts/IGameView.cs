namespace com.konargus.ui
{
    public interface IGameView : IMenu
    {
        IJoystick Joystick { get; }
    }
}