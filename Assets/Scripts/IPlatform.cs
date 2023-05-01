using DefaultNamespace;
using UnityEngine;

public interface IPlatform
{
    Vector3 GroundPoint { get; }
    bool TryGetSupplier(out ISupplier supplier);
    bool TryGetReceiver(out IReceiver receiver);
    void Highlight(Material material);
    void HideHighlight();
    bool IsObstacle();
}
