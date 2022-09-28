using UnityEngine;

namespace com.konargus.vfx
{
    public static class VisualEffectPrefabs
    {
        public static SimpleDeath SimpleDeath => Resources.Load<SimpleDeath>("SimpleDeath/SimpleDeath");
        public static SimpleSmoke SimpleSmoke => Resources.Load<SimpleSmoke>("SimpleSmoke/SimpleSmoke");
        public static SimpleBreak SimpleBreak => Resources.Load<SimpleBreak>("SimpleBreak/SimpleBreak");

    }
}