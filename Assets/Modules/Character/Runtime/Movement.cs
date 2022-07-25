using UnityEngine;

public abstract class Movement : MonoBehaviour, IMovement
{
    protected Vector2 MoveDelta { get; private set; }
    protected float Speed { get; private set; }
    Vector3 IMovement.Position => transform.position;
    public Transform Transform => transform;
    protected Transform Target { get; private set; }

    public virtual void Move(Vector2 delta, float speed)
    {
        MoveDelta = delta;
        Speed = speed;
    }

    public virtual void Follow(Transform target, float speed)
    {
        Target = target;
        Speed = speed;
    }

    public virtual void Stop()
    {
        Target = null;
        MoveDelta = Vector2.zero;
        Speed = 0;
    }
}
