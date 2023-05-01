namespace DefaultNamespace
{
    public interface IReceiver
    {
        bool IsDefeated { get; }
        bool IsSaved { get; }
        bool IsApplied { get; }
        SupplyType SupplyType { get; }
        void Destroy();
        void Save();
        void Apply();
    }
}