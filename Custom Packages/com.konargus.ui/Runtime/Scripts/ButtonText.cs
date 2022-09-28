using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace com.konargus.ui
{
    public class ButtonText : Button, IButtonText
    {
        [SerializeField] private TextMeshProUGUI label;
    
        public event Action OnClick = delegate { };

        protected override void Start()
        {
            base.Start();
            onClick.AddListener(() => OnClick?.Invoke());
        }
    
        public virtual void SetLabelText(string text)
        {
            label.text = text;
        }
    }
}