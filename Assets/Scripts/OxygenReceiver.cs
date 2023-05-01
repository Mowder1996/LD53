using UnityEngine;

namespace DefaultNamespace
{
    public class OxygenReceiver : MonoBehaviour, IReceiver
    {
        public bool IsDefeated => _isDefeated;
        public bool IsSaved => _isSaved;
        public bool IsApplied => _isApplied;
        public SupplyType SupplyType => SupplyType.Oxygen;
        private bool _isDefeated;
        private bool _isSaved;
        private bool _isApplied;
        
        public void Destroy()
        {
            _isDefeated = true;
            // DestroyImmediate(gameObject);
        }

        public void Save()
        {
            _isSaved = true;
        }

        public void Apply()
        {
            _isApplied = true;
        }
    }
}