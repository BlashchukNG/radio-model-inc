using System.Collections;
using System.Threading.Tasks;
using Constants;
using Game.UI;
using Lobby.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure
{
	public sealed class AppEntryPoint
	{
		private static AppEntryPoint _instance;

		private CoroutineRunner _coroutiner;
		private readonly WaitForSeconds _delayBeforeLoadScene = new(0.5f);
		private readonly WaitForSeconds _delayBetweenScenes = new(2f);

		private readonly UIRootView _uiRootView;


		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		public static void BootApp()
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

		#if UNITY_EDITOR
			var sceneName = SceneManager.GetActiveScene().name;

			switch (sceneName)
			{
				case Scenes.LOBBY:
					_coroutiner.StartCoroutine(LoadLobby());
					break;
				case Scenes.GAME:
					//_sceneLoaderService.LoadGameplay();
					break;
			}

			if (sceneName != Scenes.INIT)
				return;
		#endif

			_coroutiner.StartCoroutine(LoadLobby());
		}

		public UIRootView CreateUIRootView()
		{
			var prefab = Resources.Load<UIRootView>(Prefabs.UI_ROOT);
			var obj = Object.Instantiate(prefab);
			Object.DontDestroyOnLoad(obj.gameObject);
			return obj;
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

			yield return _delayBeforeLoadScene;
			yield return LoadScene(Scenes.INIT);
			yield return LoadScene(Scenes.LOBBY);
			yield return _delayBetweenScenes;
			yield return LoadGameState();

			var ui = Object.Instantiate(Resources.Load<LobbyUI>(Prefabs.UI_LOBBY));
			ui.Init(this);
			_uiRootView.AttachSceneUI(ui.gameObject);

			yield return _delayBeforeLoadScene;

			_uiRootView.HideLoadingScreen();
		}

		private IEnumerator LoadGame()
		{
			_uiRootView.ShowLoadingScreen();

			yield return _delayBeforeLoadScene;
			yield return LoadScene(Scenes.INIT);
			yield return LoadScene(Scenes.GAME);
			yield return _delayBetweenScenes;
			yield return LoadGameState();

			var ui = Object.Instantiate(Resources.Load<GameUI>(Prefabs.UI_GAME));
			ui.Init(this);
			_uiRootView.AttachSceneUI(ui.gameObject);

			yield return _delayBeforeLoadScene;

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

		private Task<bool> InitSDK()
		{
			return Task.FromResult(true);
		}

		private void InitServices()
		{
			_coroutiner = new GameObject("COROUTINER")
				.AddComponent<CoroutineRunner>();
			Object.DontDestroyOnLoad(_coroutiner);
		}
	}
}