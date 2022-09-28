using UnityEngine;

namespace com.konargus.persona_equipment
{
    public class EquipmentFactory<T> : IEquipmentFactory<T> where T : IEquipment
    {
        private protected Equipment Prefab { get; }

        public EquipmentFactory(Equipment trapPrefab)
        {
            Prefab = trapPrefab;
        }
        
        public virtual T CreateEquipment()
        {
            var equipmentGo = Object.Instantiate(Prefab, Vector3.down, Quaternion.identity);
            var trap = equipmentGo.GetComponent<T>();
            
            return trap;
        }
    }
}