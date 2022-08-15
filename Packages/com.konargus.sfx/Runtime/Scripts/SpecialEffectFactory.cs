using UnityEngine;

namespace com.konargus.sfx
{
    public class SpecialEffectFactory : ISpecialEffectFactory
    {
        private protected Transform Parent { get; }
        private protected SpecialEffect Prefab { get; }

        public SpecialEffectFactory(SpecialEffect sfxPrefab, Transform parent)
        {
            Parent = parent;
            Prefab = sfxPrefab;
        }
        
        public virtual ISpecialEffect CreateSpecialEffect()
        {
            var sfxGo = Object.Instantiate(Prefab, Vector3.down, Quaternion.identity, Parent);
            var sfx = sfxGo.GetComponent<ISpecialEffect>();
            
            return sfx;
        }
    }
}