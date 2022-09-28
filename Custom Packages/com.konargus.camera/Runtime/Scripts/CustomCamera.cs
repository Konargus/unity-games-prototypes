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
            DontDestroyOnLoad(this);
        }

        private void Update()
        {
            if (_target == null)
            {
                return;
            }
            
            var targetPos = _target.position;
            transform.position = new Vector3(targetPos.x + _offset.x, targetPos.y + _offset.y, targetPos.z + _offset.z);
        }
    
        public virtual void LookAt(Transform target, Vector3 offset)
        {
            _target = target;
            _offset = offset;
            var targetPos = _target.position;
            transform.position = new Vector3(targetPos.x + _offset.x, targetPos.y + _offset.y, targetPos.z + _offset.z);
            transform.LookAt(_target);
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
