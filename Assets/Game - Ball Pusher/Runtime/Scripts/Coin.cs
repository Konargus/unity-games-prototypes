using System;
using UnityEngine;

namespace BallPusher
{
    public abstract class Coin : MonoBehaviour, ICoin
    {
        public event Action OnTrigger = delegate {  };
        public Transform Transform => transform;

        public virtual void Hide()
        {
            gameObject.SetActive(false);
        }

        public virtual void Destroy()
        {
            Destroy(gameObject);
        }
        
        private void OnTriggerEnter(Collider other)
        {
            OnTrigger.Invoke();
        }
    }
}