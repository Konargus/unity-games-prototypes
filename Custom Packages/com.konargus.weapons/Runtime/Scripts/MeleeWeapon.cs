using System;
using System.Collections.Generic;
using com.konargus.persona;
using UnityEngine;

namespace com.konargus.persona_equipment
{
    [RequireComponent(typeof(Collider))]
    public abstract class MeleeWeapon : Weapon, IMeleeWeapon
    {
        public IList<IPersona> VulnerableTargets { get; set; }

        public event Action<Collider, int> OnCollision = delegate {  };
    
        private void OnTriggerEnter(Collider col)
        {
            OnCollision.Invoke(col, Damage);
        }
    }
}