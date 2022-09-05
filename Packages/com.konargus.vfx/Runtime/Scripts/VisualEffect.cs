using System;
using System.Collections;
using UnityEngine;

namespace com.konargus.vfx
{
    [RequireComponent(typeof(Animator))]
    public class VisualEffect : MonoBehaviour, IVisualEffect
    {
        [SerializeField] private AnimationClip animationClip;
        [SerializeField] private Transform animationTransform;
        
        public virtual void SetPosition(Vector3 position)
        {
            transform.position = position;
        }
        
        public virtual void SetChild(Transform goTransform)
        {
            goTransform.SetParent(animationTransform);
        }
        
        public virtual void PlayAnimation(Action onComplete)
        {
            StartCoroutine(PlayAnimationCoroutine(onComplete.Invoke));
        }

        private IEnumerator PlayAnimationCoroutine(Action onComplete)
        {
            var waitStep = animationClip.length / 10;
            for (var i = 0; i < animationClip.length * 10; i++)
            {
                yield return new WaitForSeconds(waitStep);
            }
            Destroy(gameObject);
            onComplete.Invoke();
        }
    }
}