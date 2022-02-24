using UnityEngine;

namespace GrassMan.Pools
{
    public class BlockPool : GenericPool<Block>
    {
        public static BlockPool Instance { get; private set; }
        protected override void SingletonObject()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }

}
