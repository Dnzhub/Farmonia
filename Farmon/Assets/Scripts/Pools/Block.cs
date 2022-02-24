using GrassMan.Combat;
using UnityEngine;

namespace GrassMan.Pools
{
    public class Block : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            Grass grass = other.GetComponent<Grass>();

            if (grass != null)
            {                
                BlockPool.Instance.SetBack(this);
            }
        }
    }
}
