using UnityEngine;

namespace DefaultNamespace
{
    public class BaloonSupplier : MonoBehaviour, ISupplier
    {
        public SupplyType Type => SupplyType.Baloon;
    }
}