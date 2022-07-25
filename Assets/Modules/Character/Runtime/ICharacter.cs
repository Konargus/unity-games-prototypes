using System;
using UnityEngine;

public interface ICharacter
{
    void SetMovementType(IMovement movementType);
    void SetPosition(Vector3 delta);
    event Action<int> OnHealthDecrease;
    void Move(Vector3 delta, float speed);
    void Follow(Transform target, float speed);
    void Stop();
    void EquipWeapon(Weapon weapon);
    void TakeDamage(int damage);
    Vector3 Position { get; }
    Transform Transform { get; }
    void Die();
}