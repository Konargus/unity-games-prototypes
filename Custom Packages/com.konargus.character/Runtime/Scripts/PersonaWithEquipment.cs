using System;
using UnityEngine;

namespace com.konargus.persona
{
    public class PersonaWithEquipment : Persona, IPersonaWithEquipment
    {
        [SerializeField] private Transform weaponHolder;

        public virtual void EquipWeapon(Transform weaponTransform)
        {
            weaponTransform.position = weaponHolder.position;
            weaponTransform.SetParent(weaponHolder);
        }
    }
}