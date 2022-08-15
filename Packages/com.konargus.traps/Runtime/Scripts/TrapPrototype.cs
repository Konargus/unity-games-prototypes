using System;

namespace com.konargus.traps
{
    public class TrapPrototype
    {
        protected internal int Damage { get; }
        private CreationInfo CreationInfo { get; set; }

        public TrapPrototype(int damage)
        {
            Damage = damage;
            CreationInfo = new CreationInfo(DateTime.Now);
        }
        
        public TrapPrototype ShallowCopy()
        {
            return (TrapPrototype) MemberwiseClone();
        }

        public TrapPrototype DeepCopy()
        {
            var clone = (TrapPrototype) MemberwiseClone();
            clone.CreationInfo = new CreationInfo(CreationInfo.Time);
            return clone;
        }
    }
    
    public class CreationInfo
    {
        public DateTime Time { get; }

        public CreationInfo(DateTime creationTime)
        {
            Time = creationTime;
        }
    }
}