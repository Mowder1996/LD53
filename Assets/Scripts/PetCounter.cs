using System;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class PetCounter : MonoBehaviour
    {
        [SerializeField]
        private OxygenReceiver[] _oxygenReceivers;
        [SerializeField]
        private Image[] _catIcons;
        [SerializeField]
        private BaloonReceiver[] _baloonReceivers;
        [SerializeField]
        private Image[] _dogIcons;
        [SerializeField]
        private Sprite _defeatedCat;
        [SerializeField]
        private Sprite _defeatedDog;
        [SerializeField]
        private WaterLevel _waterLevel;
        [SerializeField]
        private Text _timerText;
        [SerializeField]
        private Image _timerImage;

        private void Update()
        {
            var minutes = _waterLevel.TimerSeconds / 60;
            var seconds = _waterLevel.TimerSeconds % 60;
            
            _timerText.gameObject.SetActive(minutes > 0 || seconds > 0);
            
            _timerText.text = $"{minutes}:{seconds}";
            _timerImage.fillAmount = _waterLevel.TimerSeconds / (float) _waterLevel.TimerFull;
            
            for (var i = 0; i < _oxygenReceivers.Length; i++)
            {
                if (_oxygenReceivers[i].IsApplied)
                {
                    continue;
                }
                
                if (_oxygenReceivers[i].IsDefeated || _oxygenReceivers[i].IsSaved)
                {
                    if (_oxygenReceivers[i].IsDefeated)
                    {
                        _catIcons[i].sprite = _defeatedCat;
                    }
                    else
                    {
                        _catIcons[i].gameObject.SetActive(false);
                    }
                    
                    _oxygenReceivers[i].Apply();
                }
            }
            
            for (var i = 0; i < _baloonReceivers.Length; i++)
            {
                if (_baloonReceivers[i].IsApplied)
                {
                    continue;
                }
                
                if (_baloonReceivers[i].IsDefeated || _baloonReceivers[i].IsSaved)
                {
                    if (_baloonReceivers[i].IsDefeated)
                    {
                        _dogIcons[i].sprite = _defeatedDog;
                    }
                    else
                    {
                        _dogIcons[i].gameObject.SetActive(false);
                    }
                    
                    _baloonReceivers[i].Apply();
                }
            }
        }
    }
}