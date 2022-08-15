using System;
using UnityEngine;

namespace com.konargus.persona
{
    public interface IPersona
    {
        void SetMovementType(IMovement movementType);
        void SetPosition(Vector3 delta);
        event Action<int> OnHealthDecrease;
        void Move(Vector3 delta, float speed);
        void Follow(Transform target, float speed);
        void Stop();
        void EquipWeapon(Transform weaponTransform);
        void TakeDamage(int damage);
        Transform Transform { get; }
        void Die();
    }
}