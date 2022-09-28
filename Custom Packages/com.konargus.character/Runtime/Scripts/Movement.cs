using UnityEngine;

namespace com.konargus.persona
{
    public abstract class Movement : MonoBehaviour, IMovement
    {
        protected Vector2 MoveDelta { get; private set; }
        protected float Speed { get; private set; }
        public bool IsMoving { get; private set; }
        Vector3 IMovement.Position => transform.position;
        public Transform Transform => transform;
        protected Transform Target { get; private set; }

        public virtual void Move(Vector2 delta, float speed)
        {
            IsMoving = true;
            MoveDelta = delta;
            Speed = speed;
        }

        public virtual void Follow(Transform target, float speed)
        {
            IsMoving = true;
            Target = target;
            Speed = speed;
        }

        public virtual void Stop()
        {
            IsMoving = false;
            Target = null;
            MoveDelta = Vector3.zero;
            Speed = 0;
        }
    }
}
