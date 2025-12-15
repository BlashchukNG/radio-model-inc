namespace services.updater
{
    public interface ILateTick : IUpdatable
    {
        void LateTick(float delta);
    }
}