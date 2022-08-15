using UnityEngine;

namespace com.konargus.traps
{
    public class KillTrapFactory : TrapFactory
    {
        public KillTrapFactory(Trap trapPrefab, Transform parent) : base(trapPrefab, parent)
        {
            Prefab = trapPrefab;
            Parent = parent;
        }

        public override ITrap CreateTrap()
        {
            TrapPrototype = new TrapPrototype(1000);
            return base.CreateTrap();
        }
    }
}