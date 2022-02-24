using UnityEngine;

namespace GrassMan.Combat
{
    public class AnimationReapEvent : MonoBehaviour
    {
        public event System.Action OnHit;

        public void Hit()
        {
            OnHit?.Invoke();
        }

    }

}
