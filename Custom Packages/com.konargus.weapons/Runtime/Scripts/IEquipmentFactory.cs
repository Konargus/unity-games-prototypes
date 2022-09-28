namespace com.konargus.persona_equipment
{
    public interface IEquipmentFactory<out T> where T : IEquipment
    {
        T CreateEquipment();
    }
}