using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace DefaultNamespace
{
    public class GameController : MonoBehaviour
    {
        [SerializeField]
        private BaloonReceiver[] _baloonReceivers;
        [SerializeField]
        private OxygenReceiver[] _oxygenReceivers;
        [SerializeField]
        private Transform _avatar;
        [SerializeField]
        private WaterLevel _waterLevel;
        [SerializeField]
        private GameObject _gameOverPanel;
        [FormerlySerializedAs("_victorypanel")] [SerializeField]
        private GameObject _victoryPanel;

        private bool _isGameOver;
        private bool _isVictory;
        
        private void LateUpdate()
        {
            if (_isGameOver || _isVictory)
            {
                return;
            }

            if (_oxygenReceivers.All(item => item.IsSaved)
                && _baloonReceivers.All(item => item.IsSaved))
            {
                Time.timeScale = 0;

                _isVictory = true;
                _victoryPanel.SetActive(true);
                
                StartCoroutine(DelayAndQuit());
            }
            
            if (_avatar.transform.position.y < _waterLevel.WaterLevelValue
                || _baloonReceivers.Any(item => item.IsDefeated)
                || _oxygenReceivers.Any(item => item.IsDefeated))
            {
                Time.timeScale = 0;
                
                _isGameOver = true;
                _gameOverPanel.SetActive(true);

                StartCoroutine(DelayAndQuit());
            }
        }

        private IEnumerator DelayAndQuit()
        {
            yield return new WaitForSeconds(3);
            
            Application.Quit();
        }
    }
}