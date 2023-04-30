using DefaultNamespace;
using UnityEngine;

public interface IPlatform
{
    Vector3 GroundPoint { get; }
    bool TryGetSupplier(out ISupplier supplier);
}
