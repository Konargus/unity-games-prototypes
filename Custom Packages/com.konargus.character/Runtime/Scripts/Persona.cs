using System;
using UnityEngine;

namespace com.konargus.persona
{
    [RequireComponent(typeof(Rigidbody), typeof(Animator), typeof(Collider))]
    public class Persona : MonoBehaviour, IPersona
    {
        [SerializeField] private int maxHealth = 1;
        
        private int _remainingHealth;
        private IMovement _movement;

        private PersonaState _state = PersonaState.Idle;
        public PersonaState State
        {
            get => _state;
            private set
            {
                if (value == _state)
                {
                    return;
                }
                _state = value;
                OnStateChange.Invoke(_state);
            }
        }

        public event Action<int> OnHealthDecrease = delegate { };
        public event Action<PersonaState> OnStateChange = delegate { };
    
        public Transform Transform => transform;
        
        private Animator _animator;

        public Animator Animator
        {
            get
            {
                if (_animator == null)
                {
                    _animator = GetComponentInChildren<Animator>();
                }
                return _animator;
            }
        }

        private void Start()
        {
            _remainingHealth = maxHealth;
        }

        public virtual void SetMovementType(IMovement movementType)
        {
            _movement = movementType;
        }
        
        public virtual void SetMaxHealth(int health)
        {
            maxHealth = health;
            _remainingHealth = health;
        }
        
        public virtual void SetRemainingHealth(int health)
        {
            _remainingHealth = health;
        }
    
        public virtual void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        public virtual void Move(Vector3 delta, float speed)
        {
            if (State == PersonaState.Dead)
            {
                if (_movement.IsMoving)
                {
                    _movement.Stop();
                }
                return;
            }

            _movement.Move(delta, speed);
            State = PersonaState.IsMoving;
        }

        public virtual void Follow(Transform target, float speed)
        {
            if (State == PersonaState.Dead)
            {
                return;
            }
            _movement.Follow(target, speed);
            State = PersonaState.IsMoving;
        }

        public virtual void Stop()
        {
            _movement.Stop();
            if (State == PersonaState.Dead)
            {
                return;
            }
            State = PersonaState.Idle;
        }

        public virtual void TakeDamage(int damage)
        {
            _remainingHealth -= damage;
            OnHealthDecrease.Invoke(_remainingHealth);
        }

        public virtual void Die()
        {
            State = PersonaState.Dead;
        }

        public void Reset()
        {
            SetRemainingHealth(maxHealth);
            State = PersonaState.Idle;
        }
    }
}