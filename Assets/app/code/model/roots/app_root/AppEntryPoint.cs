using System.Collections;
using System.Threading.Tasks;
using constants;
using model.di;
using services.updater;
using UnityEngine;
using UnityEngine.SceneManagement;
using utils.coroutines;

namespace model.roots.app_root
{
	public sealed class AppEntryPoint
	{
		private static AppEntryPoint _instance;

		private readonly DIContainer _di_сontainer = new();
		private readonly UIRootView _uiRootView;

		private CoroutineRunner _coroutiner;
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
			_uiRootView = CreateUIRootView();
		}

		private async void RunApp()
		{
			var result = await InitSDK();

			//await _settingsProvider.LoadGameSettingsAsync();

		#if UNITY_EDITOR
			var sceneName = SceneManager.GetActiveScene().name;

			switch (sceneName)
			{
				case SceneConstants.LOBBY:
					_coroutiner.StartCoroutine(LoadLobby());
					break;
				case SceneConstants.GAME:
					//_sceneLoaderService.LoadGameplay();
					break;
			}

			if (sceneName != SceneConstants.INIT)
				return;
		#endif

			_coroutiner.StartCoroutine(LoadLobby());
		}

		private void InitServices()
		{
			_coroutiner = new GameObject("COROUTINER")
				.AddComponent<CoroutineRunner>();
			Object.DontDestroyOnLoad(_coroutiner);
			_di_сontainer.RegisterInstance(_coroutiner);

			var updater = new GameObject("UPDATER")
				.AddComponent<UpdateService>();
			Object.DontDestroyOnLoad(updater);
			_di_сontainer.RegisterInstance<IUpdateService>(updater);

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

		public UIRootView CreateUIRootView()
		{
			var prefab = Resources.Load<UIRootView>(PrefabConstants.UI_ROOT);
			var obj = Object.Instantiate(prefab);
			Object.DontDestroyOnLoad(obj.gameObject);
			return obj;
		}

		private Task<bool> InitSDK()
		{
			return Task.FromResult(true);
		}

		#region LOAD

		public void ToLobby()
		{
			Debug.Log("LOBBY");
			_coroutiner.StartCoroutine(LoadLobby());
		}

		public void ToGame()
		{
			Debug.Log("Game");
			_coroutiner.StartCoroutine(LoadGame());
		}

		private IEnumerator LoadLobby()
		{
			_uiRootView.ShowLoadingScreen();

			yield return ModelConstants.DELAY_BEFORE_LOAD_SCENE;
			yield return LoadScene(SceneConstants.INIT);
			yield return LoadScene(SceneConstants.LOBBY);
			yield return ModelConstants.DELAY_BTW_SCENES;
			;
			yield return LoadGameState();

			// var ui = Object.Instantiate(Resources.Load<LobbyUI>(PrefabConstants.UI_LOBBY));
			// ui.Init(this);
			//_uiRootView.AttachSceneUI(ui.gameObject);

			yield return ModelConstants.DELAY_AFTER_LOAD_SCENE;

			_uiRootView.HideLoadingScreen();
		}

		private IEnumerator LoadGame()
		{
			_uiRootView.ShowLoadingScreen();

			yield return ModelConstants.DELAY_BEFORE_LOAD_SCENE;
			yield return LoadScene(SceneConstants.INIT);
			yield return LoadScene(SceneConstants.GAME);
			yield return ModelConstants.DELAY_BTW_SCENES;
			;
			yield return LoadGameState();

			// var ui = Object.Instantiate(Resources.Load<GameUI>(PrefabConstants.UI_GAME));
			// ui.Init(this);
			// _uiRootView.AttachSceneUI(ui.gameObject);

			yield return ModelConstants.DELAY_AFTER_LOAD_SCENE;

			_uiRootView.HideLoadingScreen();
		}

		private IEnumerator LoadGameState()
		{
			var isSettingsLoaded = true;
			//_gameStateProvider.LoadSettings().Subscribe(_ => isSettingsLoaded = true);
			var isDataLoaded = true;
			//_gameStateProvider.LoadData().Subscribe(_ => isDataLoaded = true);

			yield return new WaitUntil(() => isDataLoaded && isSettingsLoaded);
		}

		private IEnumerator LoadScene(string sceneName)
		{
			yield return SceneManager.LoadSceneAsync(sceneName);
		}

		#endregion
	}
}