

namespace GrassMan.Animation
{
    public interface IAnimation 
    {
        void MoveAnimation(float speed);
        void ReapAnimation();
        void PickAnimation();
        bool IsReap();

        bool IsPickUp();
    }

}
