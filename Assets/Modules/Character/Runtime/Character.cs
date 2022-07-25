using System;
using UnityEngine;

public class Character : MonoBehaviour, ICharacter
{
    [SerializeField] private Transform weaponHolder;
    private int _health = 1;
    private IMovement _movement;

    public event Action<int> OnHealthDecrease = delegate { };
    
    public Vector3 Position { get; private set; }
    public Transform Transform => transform;

    public virtual void SetMovementType(IMovement movementType)
    {
        _movement = movementType;
    }
    
    public virtual void SetHealth(int health)
    {
        _health = health;
    }
    
    public virtual void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    public virtual void Move(Vector3 delta, float speed)
    {
        _movement.Move(delta, speed);
    }

    public virtual void Follow(Transform target, float speed)
    {
        _movement.Follow(target, speed);
    }

    public virtual void Stop()
    {
        _movement.Stop();
    }

    public virtual void EquipWeapon(Weapon weapon)
    {
        weapon.transform.position = weaponHolder.position;
        weapon.transform.SetParent(weaponHolder);
    }
    
    public virtual void TakeDamage(int damage)
    {
        _health -= damage;
        OnHealthDecrease.Invoke(_health);
    }

    public virtual void Die()
    {
        Destroy(gameObject);
    }
}