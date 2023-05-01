using UnityEngine;

namespace DefaultNamespace
{
    public class BaloonReceiver : MonoBehaviour, IReceiver
    {
        public SupplyType SupplyType => SupplyType.Baloon;
        
        public void Destroy()
        {
            DestroyImmediate(gameObject);
        }
    }
}