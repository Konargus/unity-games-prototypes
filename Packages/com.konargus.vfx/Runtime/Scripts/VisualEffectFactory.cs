using UnityEngine;

namespace com.konargus.vfx
{
    public class VisualEffectFactory : IVisualEffectFactory
    {
        private protected Transform Parent { get; }
        private protected VisualEffect Prefab { get; }

        public VisualEffectFactory(VisualEffect sfxPrefab, Transform parent)
        {
            Parent = parent;
            Prefab = sfxPrefab;
        }
        
        public virtual IVisualEffect CreateSpecialEffect()
        {
            var sfxGo = Object.Instantiate(Prefab, Vector3.down, Quaternion.identity, Parent);
            var sfx = sfxGo.GetComponent<IVisualEffect>();
            
            return sfx;
        }
    }
}