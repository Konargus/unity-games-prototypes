using UnityEngine;

namespace com.konargus.vfx
{
    public class VisualEffectFactory : IVisualEffectFactory
    {
        private Transform Parent { get; }
        private VisualEffect Prefab { get; }

        public VisualEffectFactory(VisualEffect sfxPrefab, Transform parent)
        {
            Parent = parent;
            Prefab = sfxPrefab;
        }
        
        public virtual IVisualEffect CreateVisualEffect()
        {
            var vfxGo = Object.Instantiate(Prefab, Vector3.down, Quaternion.identity, Parent);
            var vfx = vfxGo.GetComponent<IVisualEffect>();
            
            return vfx;
        }
    }
}