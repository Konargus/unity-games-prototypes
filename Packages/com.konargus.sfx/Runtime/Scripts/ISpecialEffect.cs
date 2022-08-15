using System;
using UnityEngine;

namespace com.konargus.sfx
{
    public interface ISpecialEffect
    {
        void SetPosition(Vector3 position);
        void SetChild(Transform goTransform);
        void PlayAnimation(Action onComplete);
    }
}