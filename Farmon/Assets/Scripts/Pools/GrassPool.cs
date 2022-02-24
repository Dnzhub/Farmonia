using GrassMan.Combat;
using UnityEngine;

namespace GrassMan.Pools
{
    public class GrassPool : GenericPool<Grass>
    {       
        public static GrassPool Instance { get; private set; }
                      
        protected override void SingletonObject()
        {
            if(Instance == null)
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
