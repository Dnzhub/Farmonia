using UnityEngine;
using TMPro;
using UnityEngine.UI;
using GrassMan.Inventory;
using DG.Tweening;

namespace GrassMan.Manager
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance { get; private set; }
        public  TextMeshProUGUI _scoreText = null;
        
        [SerializeField] Transform coinPanel;
        [SerializeField] GameObject coinPrefab;
        [SerializeField] Canvas canvas;
        [SerializeField] GameObject pauseMenu;
      

        int _perCrop = 15;

        private void Awake()
        {
            CreateSingleton();
        }

        private void CreateSingleton()
        {
            if (Instance == null)
            {
                Instance = this;
           
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        private void OnEnable()
        {           
            Bag.OnDeliver += SetScoreText;
        }
        private void OnDisable()
        {
            Bag.OnDeliver -= SetScoreText;
        }
        public void PauseMenu()
        {
            if (pauseMenu.activeInHierarchy) pauseMenu.SetActive(false);
            else { pauseMenu.SetActive(true); }

        }
        public void CoinAnimation()
        {
            GameObject coin = Instantiate(coinPrefab, canvas.transform);
            coin.transform.DOMove(coinPanel.position, 2).SetEase(Ease.InOutBack).OnComplete(() => ShakeCounter()).OnStepComplete(() => Destroy(coin));
        }
        private void ShakeCounter()
        {
            _scoreText.transform.DOShakePosition(0.5f, 0.5f);
            _scoreText.transform.DOShakeScale(0.5f, 0.5f);        
        }
     
        public void SetScoreText(int total,int current) => _scoreText.text = (_perCrop * (total += current)).ToString();
    }

}
