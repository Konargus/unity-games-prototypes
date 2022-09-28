using System;
using UnityEngine;

namespace TrapThem
{
    public struct CatAnimatorData
    {
        public struct BoolHashes
        {
            public static int Moving => Animator.StringToHash("Moving");
            public static int Dead => Animator.StringToHash("Dead");
        }

        public struct TriggerHashes
        {
            public static int Hit => Animator.StringToHash("Hit");
        }
        
        public struct StateHashes
        {
            public static int RunLoop => Animator.StringToHash("runLoop");
            public static int IdleHappy => Animator.StringToHash("IdleHappy");
        }
    }
}