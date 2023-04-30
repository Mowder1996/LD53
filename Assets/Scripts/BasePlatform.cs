using UnityEngine;

namespace DefaultNamespace
{
    public abstract class BasePlatform : MonoBehaviour, IPlatform
    {
        [SerializeField]
        private Transform _platform;

        protected Transform Platform => _platform;

        private Vector3 _groundPoint;

        public Vector3 GroundPoint => GetGroundPoint();
        
        public bool TryGetSupplier(out ISupplier supplier)
        {
            supplier = GetComponentInChildren<ISupplier>();

            return supplier != null;
        }

        public Vector3 GetGroundPoint()
        {
            var bounds = GetComponent<Collider>().bounds;

            return bounds.center + Vector3.up * bounds.size.y / 2f;
        }
    }
}