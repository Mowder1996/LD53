namespace DefaultNamespace
{
    public interface IReceiver
    {
        SupplyType SupplyType { get; }
        void Destroy();
    }
}