using System;
using System.Collections;
using UnityEngine;

namespace com.konargus.vfx
{
    [RequireComponent(typeof(Animator))]
    public class VisualEffect : MonoBehaviour, IVisualEffect
    {
        [SerializeField] private Transform animationTransform;

        private Animator _animator;
        
        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        public virtual void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        public virtual void SetRotation(Quaternion rotation)
        {
            animationTransform.rotation = rotation;
        }

        public virtual void SetChild(Transform goTransform)
        {
            goTransform.SetParent(animationTransform);
        }
        
        public virtual void Destroy()
        {
            Destroy(gameObject);
        }
        
        public virtual void PlayAnimation(Action onComplete)
        {
            _animator.Play("Play");
            StartCoroutine(PlayAnimationCoroutine(onComplete.Invoke));
        }

        private IEnumerator PlayAnimationCoroutine(Action onComplete)
        {
            yield return new WaitWhile(() => !_animator.IsInTransition(0));
            onComplete.Invoke();
        }
    }
}