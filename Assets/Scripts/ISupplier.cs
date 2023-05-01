namespace DefaultNamespace
{
    public enum SupplyType
    {
        Baloon,
        Oxygen
    }
    
    public interface ISupplier
    {
        SupplyType Type { get; }
    }
}