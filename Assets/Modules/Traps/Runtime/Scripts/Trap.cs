using System;
using UnityEngine;

public class Trap : MonoBehaviour, ITrap
{
    protected virtual int Damage { get; } = 1000;
    public event Action<Collider, int> OnCollidedWithCharacter = delegate { };

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Player"))
        {
            OnCollidedWithCharacter(other, Damage);
        }
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}