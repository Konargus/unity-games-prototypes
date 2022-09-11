using System;
using UnityEngine;

namespace BallPusher
{
    public interface IBall
    {
        float Speed { get; set; }
        Transform Transform { get; }
        event Action<Collision> OnCollision;
        public event Action OnBeginDrag;
        public event Action OnEndDrag;
        event Action<Vector3> OnMouseDragging;
        void SetPosition(Vector3 vector3);
        void SetDirection(Vector3 direction);
        void ScaleOnImpact(Vector3 impactPos, Action changeDirection);
    }
}