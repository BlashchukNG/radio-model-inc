namespace services.updater
{
    public interface IFixedTick : IUpdatable
    {
        void FixedTick(float delta);
    }
}