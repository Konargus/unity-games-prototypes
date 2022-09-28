using UnityEngine;

namespace com.konargus.traps
{
    public class FireTrapFactory : TrapFactory
    {
        public FireTrapFactory(Trap trapPrefab, Transform parent) : base(trapPrefab, parent)
        {
            Prefab = trapPrefab;
            Parent = parent;
        }
        
        public override ITrap CreateTrap()
        {
            TrapPrototype = new TrapPrototype(0);
            return base.CreateTrap();
        }
    }
}