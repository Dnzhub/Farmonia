using UnityEngine;

namespace GrassMan.Movement
{
    public interface IMover
    {
        void Tick(Vector2 playerInput);
        
    }

}
