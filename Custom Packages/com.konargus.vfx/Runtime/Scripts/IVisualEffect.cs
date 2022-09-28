using System;
using UnityEngine;

namespace com.konargus.vfx
{
    public interface IVisualEffect
    {
        void Destroy();
        void SetPosition(Vector3 position);
        void SetRotation(Quaternion rotation);
        void SetChild(Transform goTransform);
        void PlayAnimation(Action onComplete);
    }
}