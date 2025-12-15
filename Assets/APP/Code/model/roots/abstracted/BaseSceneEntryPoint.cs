using model.di;
using UnityEngine;

namespace model.roots.abstracted
{
	public abstract class BaseSceneEntryPoint : MonoBehaviour
	{
		protected DIContainer _di_container;
		protected DIContainer _views_DI_container;
		protected EnterParams _enterParams;

		// public Observable<ExitParams> Run(DIContainer diContainer, EnterParams enterParams)
		// {
		// 	_di_container = diContainer;
		// 	_views_DI_container = new DIContainer(_di_container);
		// 	_enterParams = enterParams;
		//
		// 	OnRun();
		// 	InitWorld();
		// 	InitUI();
		// 	return CreateExitSignal();
		// }

		protected abstract void OnRun();
		// protected abstract Observable<ExitParams> CreateExitSignal();
		protected abstract void InitWorld();
		protected abstract void InitUI();
	}
}