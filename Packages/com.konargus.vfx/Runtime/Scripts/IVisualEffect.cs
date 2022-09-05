using System;
using UnityEngine;

namespace com.konargus.vfx
{
    public interface IVisualEffect
    {
        void SetPosition(Vector3 position);
        void SetChild(Transform goTransform);
        void PlayAnimation(Action onComplete);
    }
}