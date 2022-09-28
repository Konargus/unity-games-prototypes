using System;
using System.Collections.Generic;
using UnityEngine;

namespace com.konargus.persona
{
    public interface IPersonaWithToxicSpots : IPersona
    {
        public IList<IPersona> VulnerableTargets { get; set; }
        event Action<Collider, int> OnToxicSpotCollision;
    }
}