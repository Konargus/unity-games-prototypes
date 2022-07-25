using System;
using UnityEngine;

public interface ITrap
{
    event Action<Collider, int> OnCollidedWithCharacter;
    void Destroy();
}
