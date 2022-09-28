using System;
using UnityEngine;

namespace TrapThem
{
    public struct DogAnimatorData
    {
        public struct BoolHashes
        {
            // public static int Dead => Animator.StringToHash ("Dead");
            public static int Run => Animator.StringToHash("Run");
        }
        
        public struct TriggerHashes
        {
            public static int Dead => Animator.StringToHash ("Dead");
            public static int Run => Animator.StringToHash("Run");
        }
        
        public struct StateHashes
        {
            public static int Idle => Animator.StringToHash("Idle");
        }
    }
}