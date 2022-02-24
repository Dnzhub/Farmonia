using GrassMan.Pools;
using DG.Tweening;
using UnityEngine;


namespace GrassMan.Combat
{
    public class Grass : MonoBehaviour, ITakeHit
    {      
        public static event System.Action OnTakeHit;
        [SerializeField] GrassScriptableOBJ grassSO;              

        int _currentHealth;
        public bool IsDead => _currentHealth < 1;
        public bool _isGrowing = false;
        public bool IsGrowing => _isGrowing;

        private void Start()
        {
            _currentHealth = grassSO.MaxHealth;          
        }      
        private void OnEnable()
        {
            _isGrowing = true;
            transform.DOScale(5, 10).SetEase(Ease.InOutBounce).OnComplete(() => _isGrowing = false);
        }
        private void OnDisable()
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        public void TakeHit(Reap attacker)
        {
            if (_isGrowing) return;
            OnTakeHit?.Invoke();
            _currentHealth = Mathf.Max(0, _currentHealth -= attacker.Damage);
            if (IsDead)
            {
                GrassPool.Instance.SetBack(this);
                Block block = BlockPool.Instance.Get();
                block.transform.position = transform.position;
                block.transform.rotation = Quaternion.identity;
                block.gameObject.SetActive(true);
                this.enabled = false;

            }
        }    
    }
}

