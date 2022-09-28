using System.Collections.Generic;
using UnityEngine;

namespace com.konargus.persona
{
    public class PersonaFactory<T> : IPersonaFactory where T : Movement
    {
        private Transform Parent { get; }
        private Persona Prefab { get; }

        public PersonaFactory(Persona trapPrefab, Transform parent)
        {
            Prefab = trapPrefab;
            Parent = parent;
        }
        
        public virtual IPersona CreatePersona()
        {
            var personaGo = Object.Instantiate(Prefab, Vector3.down, Quaternion.identity, Parent);
            var movementType = personaGo.gameObject.AddComponent<T>();
            var persona = personaGo.GetComponent<IPersona>();
            persona.SetMovementType(movementType);
            
            return persona;
        }
    }
}