using System;
using System.Collections;
using UnityEngine;

namespace DefaultNamespace
{
    [Serializable]
    public class WaterPlanData
    {
        public int Timer;
        public float Level;
    }
    
    public class WaterLevel : MonoBehaviour
    {
        [SerializeField]
        private WaterPlanData[] _planData;
        [SerializeField]
        private Transform _water;

        public float WaterLevelValue => _water.position.y;

        private int _timer;
        private int _timerSize;

        public int TimerSeconds => _timer;
        public int TimerFull => _timerSize;
        
        private void Start()
        {
            var startLevel = _water.localPosition;
            startLevel.y = 0;
            _water.localPosition = startLevel;
            
            StartCoroutine(WaterRoutine());
        }

        private IEnumerator WaterRoutine()
        {
            for (var i = 0; i < _planData.Length; i++)
            {
                yield return Timer(_planData[i]);
            }
        }

        private IEnumerator Timer(WaterPlanData planData)
        {
            _timerSize = planData.Timer;
            _timer = planData.Timer;

            for (var i = 0; i < planData.Timer; i++)
            {
                yield return new WaitForSeconds(1);

                _timer--;
            }

            yield return null;

            yield return WaterLevelUp(planData.Level);
        }

        private IEnumerator WaterLevelUp(float level)
        {
            while (_water.localPosition.y < level)
            {
                yield return new WaitForSeconds(0.1f);

                _water.localPosition += Vector3.up * 0.01f;
            }
        }
    }
}