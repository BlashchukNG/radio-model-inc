namespace Infrastructure.Roots.Abstract
{
	public abstract class ExitParams
	{
		public string SceneName { get; }
		public EnterParams TargetSceneEnterParams { get; }


		protected ExitParams(string sceneName, EnterParams targetSceneEnterParams)
		{
			SceneName = sceneName;
			TargetSceneEnterParams = targetSceneEnterParams;
		}

		public T As<T>()
			where T : ExitParams =>
			(T)this;
	}
}