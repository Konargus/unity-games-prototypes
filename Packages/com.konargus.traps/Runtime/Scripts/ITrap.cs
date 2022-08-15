using System;
using UnityEngine;

namespace com.konargus.traps
{
    public interface ITrap
    {
        Vector3 Position { get; set; }
        event Action<Collider, int> OnCollision;
        void Destroy();
    }
}
