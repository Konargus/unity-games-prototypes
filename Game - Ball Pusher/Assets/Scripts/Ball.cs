using System;
using System.Collections;
using UnityEngine;

namespace BallPusher
{
    public abstract class Ball : MonoBehaviour, IBall
    {
        [SerializeField] private Transform ballModel;
        [SerializeField] private Transform squishy;

        public Transform Transform => ballModel;
        public event Action<Collision> OnCollision = delegate {  };
        public event Action OnBeginDrag = delegate {  };
        public event Action OnEndDrag = delegate {  };
        public event Action<Vector3> OnMouseDragging = delegate {  };
        
        public float Speed { get; set; }
        protected abstract float Friction { get; }

        private float _speedDecline;
        private Vector3 _direction;
        private bool _canRotate = true;
        private Vector3 _mouseDownPos;
        private Vector3 _mouseUpPos;
        private Plane _plane;
        private Quaternion _rot;

        private void Update()
        {
            Speed -= _speedDecline;
            if (Speed <= 0)
            {
                return;
            }

            if (!_canRotate)
            {
                transform.position = Vector3.MoveTowards(transform.position, _mouseUpPos, Time.deltaTime * Speed / 25);
                var step = Speed / 100;
                if (squishy.localScale.z < 0.5f)
                {
                    return;
                }
                squishy.localScale -= new Vector3(-step, -step, step);
                return;
            }
            
            transform.position = Vector3.MoveTowards(transform.position, _mouseUpPos, Time.deltaTime * Speed);
            ballModel.Rotate(_rot.x * Speed * 2.5f, _rot.y * Speed * 2.5f, _rot.z * Speed * 2.5f, Space.World);
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
            _speedDecline = Friction / 100f;
        }
        
        public void SetDirection(Vector3 direction)
        {
            _direction = new Vector3(direction.x, 0, direction.z);
            _mouseUpPos = _direction * 1000;
            _rot.SetFromToRotation(Vector3.up, _mouseUpPos);
        }
        
        private void OnMouseDown()
        {
            _plane = new Plane(Vector3.up, 0);
            var worldPosition = GetMouseWorldPosition();
            _mouseDownPos = worldPosition;
            OnBeginDrag.Invoke();
        }

        private void OnMouseDrag()
        {
            var worldPosition = GetMouseWorldPosition();
            OnMouseDragging.Invoke(worldPosition);
        }

        private void OnMouseUp()
        {
            var worldPosition = GetMouseWorldPosition();
            Speed += (worldPosition - _mouseDownPos).magnitude;
            SetDirection((worldPosition - _mouseDownPos).normalized);
            OnEndDrag.Invoke();
        }

        private Vector3 GetMouseWorldPosition()
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (!_plane.Raycast(ray, out var distance))
            {
                return Vector3.zero;
            }
            var worldPosition = ray.GetPoint(distance);
            return new Vector3(worldPosition.x, 0, worldPosition.z);
        }

        private void OnCollisionEnter(Collision other)
        {
            OnCollision.Invoke(other);
        }

        public void ScaleOnImpact(Vector3 impactPos, Action changeDirection)
        {
            StartCoroutine(ScaleOnImpactCoroutine(impactPos, changeDirection));
        }

        private IEnumerator ScaleOnImpactCoroutine(Vector3 impactPos, Action changeDirection)
        {
            _canRotate = false;
            var angle = Vector3.Angle(Vector3.forward, new Vector3(impactPos.x, 0, impactPos.z));
            squishy.rotation = Quaternion.Euler(0, impactPos.x < 0 ? -angle : angle, 0);
            ballModel.SetParent(squishy, true);

            for (var i = 0; i < 5; i++)
            {
                yield return new WaitForEndOfFrame();
            }
            Speed /= 1.5f;
            changeDirection.Invoke();
            squishy.localScale = Vector3.one;
            ballModel.SetParent(transform, true);
            ballModel.transform.localScale = Vector3.one;
            _canRotate = true;
        }
    }
}