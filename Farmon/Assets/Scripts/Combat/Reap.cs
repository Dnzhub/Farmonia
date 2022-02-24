using UnityEngine;

namespace GrassMan.Combat
{
    public class Reap : MonoBehaviour
    {
        [SerializeField] AttackScriptableObject playerAttackAttributes;
        [SerializeField] Transform attackDirection;
        [SerializeField] ParticleSystem hitEffect;

        public int Damage => playerAttackAttributes.Damage;
        Collider[] _attackResults;
        int maxColliders = 5;

        private void Awake()
        {
            _attackResults = new Collider[maxColliders];
        }
        private void OnEnable()
        {
            GetComponentInChildren<AnimationReapEvent>().OnHit += HandleHit;
        }
        private void OnDisable()
        {
            GetComponentInChildren<AnimationReapEvent>().OnHit -= HandleHit;
        }

        public void Attack(ITakeHit takeHit)
        {
            takeHit.TakeHit(this);
        }
        private void HandleHit()
        {
            int hitCount = Physics.OverlapSphereNonAlloc(attackDirection.position + attackDirection.forward,
                playerAttackAttributes.attackRadius, _attackResults);
            for (int i = 0; i < hitCount; i++)
            {
                ITakeHit takeHit = _attackResults[i].GetComponent<ITakeHit>();
                if (takeHit != null && !takeHit.IsGrowing)
                {
                    Attack(takeHit);
                    hitEffect.Play();
                }
            }
        }

        private void OnDrawGizmos()
        {
            OnDrawGizmosSelected();
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackDirection.position + attackDirection.forward, playerAttackAttributes.attackRadius);
        }
    }

}
