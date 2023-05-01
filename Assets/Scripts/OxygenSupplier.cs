using UnityEngine;

namespace DefaultNamespace
{
    public class OxygenSupplier : MonoBehaviour, ISupplier
    {
        public SupplyType Type => SupplyType.Oxygen;
    }
}