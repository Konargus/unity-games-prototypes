using System;
using System.Collections.Generic;
using com.konargus.persona;
using UnityEngine;

namespace com.konargus.persona_equipment
{
    public interface IMeleeWeapon : IEquipment
    {
        event Action<Collider, int> OnCollision;
        IList<IPersona> Targets { get; set; }
    }
}
