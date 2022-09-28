using UnityEngine;

namespace com.konargus.camera
{
    public interface ICustomCamera
    {
        void LookAt(Transform target, Vector3 offset);
        void Follow(Transform target, Vector3 offset);
        void FollowFromBehind(Transform target, Vector3 offset);
    }
}
