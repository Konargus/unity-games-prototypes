using System.Collections.Generic;

namespace com.konargus.persona
{
    public interface IPersonaFactory
    {
        IPersona CreatePersona();
    }
}