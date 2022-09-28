using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace com.konargus.ui
{
    public class Joystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler, IJoystick
    {
        [SerializeField] private Image background;
        [SerializeField] private Image knob;
        private Vector2 _knobInitialLocalPos;
        private Vector2 _knobInitialPos;
        private float _radius;
        private Canvas _canvas;

        public event Action<Vector2> OnJoystickDrag = delegate {  };
        public event Action OnJoystickDragEnd = delegate {  };

        private void Start()
        {
            _knobInitialLocalPos = knob.transform.localPosition;
            _canvas = GetComponentInParent<Canvas>();
        }

        public virtual void SetRadius(float radius)
        {
            _radius = radius;
            var rt = GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(radius * 2, radius * 2);
        }
        
        public virtual void SetSpriteBackground(Sprite sprite)
        {
            background.sprite = sprite;
        }

        public virtual void SetSpriteKnob(Sprite sprite)
        {
            knob.sprite = sprite;
        }

        public void OnDrag(PointerEventData eventData)
        {
            var length = eventData.position -_knobInitialPos;
            var tr = knob.transform;
            var radialDistance = _radius * _canvas.scaleFactor;
            if (length.magnitude > radialDistance)
            {
                length *= radialDistance / length.magnitude;
                tr.position = _knobInitialPos + length;
            }
            else
            {
                tr.position = eventData.position;
            }
            OnJoystickDrag.Invoke(_knobInitialPos - eventData.position);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            var tr = knob.transform;
            _knobInitialPos = tr.position;
            tr.position = eventData.position;
            OnJoystickDrag.Invoke(_knobInitialPos - eventData.position);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            knob.transform.localPosition = _knobInitialLocalPos;
            OnJoystickDragEnd.Invoke();
        }
    }
}