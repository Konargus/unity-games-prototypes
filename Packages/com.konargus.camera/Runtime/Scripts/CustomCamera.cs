using System;
using UnityEngine;

namespace com.konargus.camera
{
    public class CustomCamera : MonoBehaviour, ICustomCamera
    {
        private static ICustomCamera _instance;
        private Transform _target;
        private Vector3 _offset;

        public static ICustomCamera Instance
        {
            get { return
                _instance ??= Instantiate(Resources.Load<CustomCamera>("CustomCamera")).GetComponent<ICustomCamera>();
                
            }
        }

        private void Start()
        {
            tag = "MainCamera";
            DontDestroyOnLoad(this);
        }

        private void Update()
        {
            if (_target == null)
                return;
        
            var tr = transform;
            tr.LookAt(_target);
            var targetPos = _target.position;
            tr.position = new Vector3(targetPos.x - _offset.x, _offset.y, targetPos.z);
        }
    
        public virtual void LookAt(Transform target)
        {
            _target = target;
        }
    
        public virtual void Follow(Transform target, Vector3 offset)
        {
            _target = target;
            _offset = offset;
        }
        
        public virtual void FollowFromBehind(Transform target, Vector3 offset)
        {
            throw new NotImplementedException();
        }
    }
}
