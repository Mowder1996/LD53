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

        private MeshRenderer _meshRenderer;
        private Material _normalMaterial;
        
        public bool TryGetSupplier(out ISupplier supplier)
        {
            supplier = GetComponentInChildren<ISupplier>();

            return supplier != null;
        }

        public bool TryGetReceiver(out IReceiver receiver)
        {
            receiver = GetComponentInChildren<IReceiver>();

            return receiver != null;
        }

        public void Highlight(Material material)
        {
            if (_meshRenderer == null)
            {
                _meshRenderer = GetComponent<MeshRenderer>();
            }

            _normalMaterial = _meshRenderer.material;
            _meshRenderer.material = material;
        }

        public void HideHighlight()
        {
            if (_normalMaterial == null)
            {
                return;
            }
            
            if (_meshRenderer == null)
            {
                _meshRenderer = GetComponent<MeshRenderer>();
            }

            _meshRenderer.material = _normalMaterial;
        }

        public bool IsObstacle()
        {
            return GetComponent<Obstacle>();
        }

        public Vector3 GetGroundPoint()
        {
            var bounds = GetComponent<Collider>().bounds;

            return bounds.center + Vector3.up * bounds.size.y / 2f;
        }
    }
}