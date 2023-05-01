using UnityEngine;

namespace DefaultNamespace
{
    public class OxygenReceiver : MonoBehaviour, IReceiver
    {
        public SupplyType SupplyType => SupplyType.Oxygen;
        
        public void Destroy()
        {
            DestroyImmediate(gameObject);
        }
    }
}