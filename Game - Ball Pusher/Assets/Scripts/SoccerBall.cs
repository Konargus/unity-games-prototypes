using System;
using System.Collections;
using UnityEngine;

namespace BallPusher
{
    public class SoccerBall : Ball
    {
        protected override float Friction => 1f;
    }
}