using UnityEngine;

public interface IMovement
{
    Vector3 Position { get; }
    Transform Transform { get; }
    void Follow(Transform target, float speed);
    void Move(Vector2 delta, float speed);
    void Stop();
}