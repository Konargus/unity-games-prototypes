using UnityEngine;

namespace com.konargus.traps
{
    public class InstantKillTrapFactory : TrapFactory
    {
        public InstantKillTrapFactory(Trap trapPrefab, Transform parent) : base(trapPrefab, parent)
        {
            Prefab = trapPrefab;
            Parent = parent;
        }

        public override ITrap CreateTrap()
        {
            TrapPrototype = new TrapPrototype(Prefab.damage);
            return base.CreateTrap();
        }
    }
}