using System;
using UnityEngine;

namespace BallPusher
{
    public interface ICoin
    {
        event Action OnTrigger;
        Transform Transform { get; }
        void Hide();
        void Destroy();
    }
}