using System;
using UnityEngine;

namespace com.konargus.persona
{
    public class PersonaToxicSpot : MonoBehaviour
    {
        [SerializeField] private int damage;

        public event Action<Collider, int> OnCollision = delegate {  };

        private void OnTriggerEnter(Collider col)
        {
            OnCollision.Invoke(col, damage);
        }
    }
}