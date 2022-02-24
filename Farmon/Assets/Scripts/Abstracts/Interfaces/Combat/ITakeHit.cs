

namespace GrassMan.Combat
{
    public interface ITakeHit
    {
        void TakeHit(Reap attacker);
        bool IsGrowing { get; }
    }

}
