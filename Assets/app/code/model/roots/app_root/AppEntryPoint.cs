// using Constants;
// using Infrastructure.DI;
// using Services.AssetInstantiate;
// using Services.GameState;
// using Services.SceneLoader;
// using Services.Settings;

using model.di;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure.Roots.AppRoot
{
	public sealed class AppEntryPoint
	{
		private static AppEntryPoint _instance;

		private readonly DIContainer _diContainer = new();

		// private ISettingsProvider _settingsProvider;
		// private ISceneLoaderService _sceneLoaderService;

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		public static void LoadApp()
		{
			//TODO: add global settings service
			Application.targetFrameRate = 60;
			Screen.sleepTimeout = SleepTimeout.NeverSleep;

			_instance = new AppEntryPoint();
			_instance.RunApp();
		}

		private AppEntryPoint()
		{
			InitServices();
		}

		private async void RunApp()
		{
			//await _settingsProvider.LoadGameSettingsAsync();

		#if UNITY_EDITOR
			var sceneName = SceneManager.GetActiveScene().name;

			// switch (sceneName)
			// {
			// 	case Scenes.LOBBY:
			// 		_sceneLoaderService.LoadLobby();
			// 		break;
			// 	case Scenes.GAMEPLAY:
			// 		_sceneLoaderService.LoadGameplay();
			// 		break;
			// }
			//
			// if (sceneName != Scenes.BOOT)
			// 	return;
		#endif

			//_sceneLoaderService.LoadLobby();
		}

		private void InitServices()
		{
			//_settingsProvider = new SettingsProvider();
			//_diContainer.RegisterInstance(_settingsProvider);

			// var sceneObjectFactory = new SceneObjectFactory();
			// _diContainer.RegisterInstance<ISceneObjectFactory>(sceneObjectFactory);
			//
			// _diContainer.RegisterInstance(sceneObjectFactory.CreateCoroutineRunner());
			// _diContainer.RegisterInstance(sceneObjectFactory.CreateUIRootView());
			//
			// _diContainer.RegisterInstance<IGameStateProvider>(new PlayerPrefsGameStateProvider(_diContainer));
			//
			// _sceneLoaderService = new SceneLoaderService(_diContainer);
			// _diContainer.RegisterInstance(_sceneLoaderService);
		}
	}
}