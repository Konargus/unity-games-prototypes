using System;
using System.Collections.Generic;
using UnityEngine;

namespace com.konargus.persona
{
    public class PersonaWithToxicSpots : Persona, IPersonaWithToxicSpots
    {
        private PersonaToxicSpot[] _toxicSpots;
        
        public IList<IPersona> VulnerableTargets { get; set; }
        public event Action<Collider, int> OnToxicSpotCollision = delegate {  };

        private void Start()
        {
            foreach (var toxicSpot in GetComponentsInChildren<PersonaToxicSpot>())
            {
                toxicSpot.OnCollision += (col, damage) =>
                {
                    OnToxicSpotCollision.Invoke(col, damage);
                };
            }
        }
    }
}