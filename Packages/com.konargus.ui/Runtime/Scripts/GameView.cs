namespace com.konargus.ui
{
    public class GameView : Menu, IGameView
    {
        public IJoystick Joystick { get; private set; }

        internal void SetJoystick (IJoystick joystick)
        {
            Joystick = joystick;
        }
    }
}