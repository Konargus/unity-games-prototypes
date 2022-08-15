using UnityEngine;
using Object = UnityEngine.Object;

namespace com.konargus.traps
{
    public abstract class TrapFactory : ITrapFactory
    {
        protected virtual TrapPrototype TrapPrototype { get; set; }
        private TrapPrototype[] _trapPrototypes;
        private protected Trap Prefab { get; set; }
        private protected Transform Parent { get; set; }
        
        internal TrapFactory(Trap trapPrefab, Transform parent)
        {
            Prefab = trapPrefab;
            Parent = parent;
        }
        
        public virtual ITrap CreateTrap()
        {
            var trapGo = Object.Instantiate(Prefab, Vector3.down, Quaternion.identity, Parent);
            var trap = trapGo.GetComponent<Trap>();
            trap.Damage = TrapPrototype.Damage;
            
            return trap;
        }
    }
}