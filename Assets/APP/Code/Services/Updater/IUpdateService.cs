namespace services.updater
{
    public interface IUpdateService
    {
        void Add(IUpdatable updatable);
        void Remove(IUpdatable updatable);
        
        void Clear();
    }
}