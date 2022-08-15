using System;
using System.Collections.Generic;
using com.konargus.persona;
using UnityEngine;

namespace com.konargus.persona_equipment
{
    [RequireComponent(typeof(Collider))]
    public abstract class MeleeWeapon : Weapon, IMeleeWeapon
    {
        protected override int Damage { get; } = 1000;
        public IList<IPersona> Targets { get; set; }

        public event Action<Collider, int> OnCollision = delegate {  };
    
        private void OnTriggerEnter(Collider col)
        {
            OnCollision.Invoke(col, Damage);
        }
    }
}