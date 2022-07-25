using System;
using UnityEngine;

namespace JoystickMobile
{
    public interface IJoystick
    {
        void SetRadius(float radius);
        void SetSpriteBackground(Sprite sprite);
        void SetSpriteKnob(Sprite sprite);
        event Action<Vector2> OnJoystickDrag;
        event Action OnJoystickDragEnd;
    }
}
