using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DefaultNamespace
{
    public class CityManager : MonoBehaviour
    {
        private List<IPlatform> _platforms;
        private Supplier[] _treasureHubs;

        private void Awake()
        {
            _platforms = new List<IPlatform>();
            
            var childCount = transform.childCount;

            for (var i = 0; i < childCount; i++)
            {
                _platforms.Add(transform.GetChild(i).gameObject.AddComponent<StaticPlatform>());
            }
 
            _treasureHubs = GetComponentsInChildren<Supplier>();
        }

        public IPlatform GetNearestPlatform(Vector3 position)
        {
            IPlatform nearestPlatform = null;

            var minDeltaDistance = float.MaxValue;
            
            for (var i = 0; i < _platforms.Count; i++)
            {
                var distance = Vector2.Distance(
                    new Vector2(position.x, position.z), 
                    new Vector2(_platforms[i].GroundPoint.x, _platforms[i].GroundPoint.z));

                if (distance <= minDeltaDistance)
                {
                    minDeltaDistance = distance;
                    nearestPlatform = _platforms[i];
                    
                    continue;
                }
            }

            return nearestPlatform;
        }
        
        public IPlatform[] GetNextPlatforms(IPlatform sourcePlatform, Vector3 limits)
        {
            var nextPlatforms = _platforms
                .Where(item => !item.Equals(sourcePlatform)
                        && Math.Abs(sourcePlatform.GroundPoint.x - item.GroundPoint.x) <= limits.x
                        && item.GroundPoint.y - sourcePlatform.GroundPoint.y <= limits.y
                        && Math.Abs(sourcePlatform.GroundPoint.z - item.GroundPoint.z) <= limits.z);

            return nextPlatforms.ToArray();
        }

        public void OnDrawGizmos()
        {
            if (!Application.isPlaying)
            {
                return;
            }
            
            for (var i = 0; i < _platforms.Count; i++)
            {
                Gizmos.DrawSphere(_platforms[i].GroundPoint, 0.1f);
            }
        }
    }
}