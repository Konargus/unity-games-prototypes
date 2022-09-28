using System;
using UnityEngine;

namespace com.konargus.persona
{
    public interface IPersona
    {
        void SetMaxHealth(int health);
        void SetRemainingHealth(int health);
        void SetMovementType(IMovement movementType);
        void SetPosition(Vector3 delta);
        event Action<int> OnHealthDecrease;
        event Action<PersonaState> OnStateChange;
        PersonaState State { get; }
        void Move(Vector3 delta, float speed);
        void Follow(Transform target, float speed);
        void Stop();
        void TakeDamage(int damage);
        Transform Transform { get; }
        Animator Animator { get; }
        void Die();
        void Reset();
    }
}