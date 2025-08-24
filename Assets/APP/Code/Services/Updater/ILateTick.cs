namespace Services.Updater
{
    public interface ILateTick : IUpdatable
    {
        void LateTick(float delta);
    }
}