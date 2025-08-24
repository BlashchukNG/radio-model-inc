using System.Collections;
using System.Threading.Tasks;
using Constants;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure
{
	public sealed class Boot
	{
		private static Boot _instance;


		private CoroutineRunner _coroutiner;
		private WaitForSeconds _delayBeforeLoadScene = new WaitForSeconds(0.5f);
		private WaitForSeconds _delayBetweenScenes = new WaitForSeconds(2f);


		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		public static void BootApp()
		{
			//TODO: add global settings service
			Application.targetFrameRate = 60;
			Screen.sleepTimeout = SleepTimeout.NeverSleep;

			_instance = new Boot();
			_instance.RunApp();
		}

		private Boot()
		{
			InitServices();
		}

		private async void RunApp()
		{
			var result = await InitSDK();

		#if UNITY_EDITOR
			var sceneName = SceneManager.GetActiveScene().name;

			switch (sceneName)
			{
				case SceneNames.LOBBY:
					_coroutiner.StartCoroutine(LoadLobby());
					break;
				// case SceneNames.GAMEPLAY:
				// 	//_sceneLoaderService.LoadGameplay();
				// 	break;
			}

			if (sceneName != SceneNames.INIT)
				return;
		#endif

			_coroutiner.StartCoroutine(LoadLobby());
		}

		#region LOAD

		private IEnumerator LoadLobby()
		{
			yield return _delayBeforeLoadScene;
			yield return LoadScene(SceneNames.INIT);
			yield return LoadScene(SceneNames.LOBBY);
			yield return _delayBetweenScenes;
			yield return LoadGameState();

			//TODO: init lobby

			yield return _delayBeforeLoadScene;

			//_uiRootView.HideLoadingScreen();
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