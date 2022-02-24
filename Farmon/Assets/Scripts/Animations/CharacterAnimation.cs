using UnityEngine;

namespace GrassMan.Animation
{
    public class CharacterAnimation : IAnimation
    {
        Animator _animator;
       
        public CharacterAnimation(Animator animator)
        {
            _animator = animator;
            
        }
        public void MoveAnimation(float moveSpeed)
        {
            _animator.SetFloat("speed", Mathf.Abs(moveSpeed));
        }

        public void ReapAnimation()
        {
            _animator.SetTrigger("reap");
            
        }
        public void PickAnimation()
        {
            _animator.SetTrigger("pickUp");
        }
        public bool IsReap()
        {
            return this._animator.GetCurrentAnimatorStateInfo(0).IsName("Reap");
        }

        public bool IsPickUp()
        {
            return this._animator.GetCurrentAnimatorStateInfo(0).IsName("PickUp");               
        }
    }

}
