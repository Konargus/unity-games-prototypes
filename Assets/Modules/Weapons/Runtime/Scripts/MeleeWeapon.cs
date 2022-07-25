using System;
using UnityEngine;

public class MeleeWeapon : Weapon, IMeleeWeapon
{
    protected override int Damage { get; } = 1000;
    
    public event Action<int> OnCollidedWithPlayer = delegate {  };
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnCollidedWithPlayer.Invoke(Damage);
        }
    }
}