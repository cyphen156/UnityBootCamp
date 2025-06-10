using _25_06_04_AR_First.Database;
using _25_06_04_AR_First.Services;
using UnityEngine;
using UnityEngine.UI;

namespace _25_06_04_AR_First.UIs
{
    /// <summary>
    /// UI component for tracking monsters in the game.
    /// </summary>
    /// <remarks>
    /// This class is responsible for displaying and managing the UI related to monsters.
    /// </remarks>
    public class UI_MonsterTracker : MonoBehaviour
    {
        [SerializeField] MonsterService monsterService;
        [SerializeField] Sprite[] _pawIcons;
        [SerializeField] Image _paw;


        private void Update()
        {
            RefreshPaw();    
        }

        void RefreshPaw()
        {

            int minFootStep = 4;
            
            foreach (Monster monster in monsterService.monsters)
            {
                minFootStep = Mathf.Min(monster.footstepRange, minFootStep);
            }

            switch (minFootStep)
            {
                case 0:
                    _paw.sprite = _pawIcons[0];
                    break;
                case 1:
                    _paw.sprite = _pawIcons[1];
                    break;
                case 2:
                    _paw.sprite = _pawIcons[2];
                    break;

                default:
                    _paw.sprite = null;
                    break;
                
            }

            _paw.enabled = minFootStep >= 1 && minFootStep <= 3;
        }
    }
}