namespace services.updater
{
	public interface ITick : IUpdatable
	{
		void Tick(float delta);
	}
}