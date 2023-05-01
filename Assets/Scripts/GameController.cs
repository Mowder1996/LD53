using System.Collections;
using UnityEngine;

namespace DefaultNamespace
{
    public class GameController : MonoBehaviour
    {
        [SerializeField]
        private Transform _avatar;
        [SerializeField]
        private WaterLevel _waterLevel;
        [SerializeField]
        private GameObject _gameOverPanel;

        private bool _isGameOver;
        
        private void LateUpdate()
        {
            if (_isGameOver)
            {
                return;
            }

            if (_avatar.transform.position.y < _waterLevel.WaterLevelValue)
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