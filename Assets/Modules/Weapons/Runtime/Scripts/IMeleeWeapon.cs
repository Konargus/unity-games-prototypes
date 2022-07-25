using System;

public interface IMeleeWeapon
{
    event Action<int> OnCollidedWithPlayer;
}
