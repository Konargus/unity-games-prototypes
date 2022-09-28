using System;
using UnityEngine;

namespace com.konargus.persona
{
    public interface IPersonaWithEquipment : IPersona
    {
        void EquipWeapon(Transform weaponTransform);
    }
}