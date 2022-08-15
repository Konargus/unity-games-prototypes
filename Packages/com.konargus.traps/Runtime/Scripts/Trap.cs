using System;
using UnityEngine;

namespace com.konargus.traps
{
    [RequireComponent(typeof(Collider))]
    public abstract class Trap : MonoBehaviour, ITrap
    {
        protected internal virtual int Damage { get; set; }

        public virtual Vector3 Position
        {
            get => transform.position;
            set => transform.position = value;
        }

        public event Action<Collider, int> OnCollision = delegate { };

        private void OnTriggerEnter(Collider other)
        {
            OnCollision(other, Damage);
        }

        public virtual void Destroy()
        {
            Destroy(gameObject);
        }
    }
}