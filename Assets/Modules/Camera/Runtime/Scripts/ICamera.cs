using UnityEngine;

public interface ICamera
{
    void LookAt(Transform target);
    void Follow(Transform target, Vector3 offset);
}
