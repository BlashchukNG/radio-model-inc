using System;
using System.Collections;
using System.Threading.Tasks;
using constants;
using Game;
using Game.Logic.ControlPanels;
using Game.Logic.UserCamera;
using Game.Logic.Vehicles;
using Lobby;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;
using Object = UnityEngine.Object;

namespace Root
{
	public sealed class AppEntryPoint
	{
		private CoroutineRunner _coroutioner;
		private UIRootView _uiRootView;
		private static AppEntryPoint _instance;

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		public static void LoadApp()
		{
			//TODO: add global settings service
			Application.targetFrameRate = 60;
			Screen.sleepTimeout = SleepTimeout.NeverSleep;

			_instance = new AppEntryPoint();
			_instance.RunApp();
		}

		private async void RunApp()
		{
			var result = await InitSDK();

			_coroutioner = new GameObject("[COROUTINE RUNNER]")
				.AddComponent<CoroutineRunner>();
			Object.DontDestroyOnLoad(_coroutioner.gameObject);

			_uiRootView = Object.Instantiate(Resources.Load<UIRootView>("root/ui root view"));
			Object.DontDestroyOnLoad(_uiRootView.gameObject);

			InitServices();

			_coroutioner.StartCoroutine(LoadLobby());
		}

		private async Task<bool> InitSDK()
		{
			return true;
		}

		private void InitServices()
		{
		}

		private IEnumerator LoadLobby()
		{
			_uiRootView.ShowLoadingScreen();

			yield return new WaitForSeconds(ModelConstants.DELAY_BEFORE_LOAD_SCENE);
			yield return LoadScene(SceneConstants.BOOT);
			yield return LoadScene(SceneConstants.LOBBY);
			yield return new WaitForSeconds(ModelConstants.DELAY_AFTER_LOAD_SCENE);

			var sceneUI = Object.Instantiate(Resources.Load<LobbyUI>("ui/ui lobby"));
			sceneUI.Init(this);
			_uiRootView.AttachSceneUI(sceneUI.gameObject);

			var world = Object.FindFirstObjectByType<LobbyWorld>();
			var bulldozer = Object.Instantiate(Resources.Load<VehicleBulldozer>("vehicles/bulldozer/bulldozer"), world.spawnPoint);
			bulldozer.Rigidbody.isKinematic = true;
			bulldozer.InGame = false;

			_uiRootView.HideLoadingScreen();
		}

		private IEnumerator LoadGame()
		{
			_uiRootView.ShowLoadingScreen();

			yield return new WaitForSeconds(ModelConstants.DELAY_BEFORE_LOAD_SCENE);
			yield return LoadScene(SceneConstants.BOOT);
			yield return LoadScene(SceneConstants.GAME);
			yield return new WaitForSeconds(ModelConstants.DELAY_AFTER_LOAD_SCENE);

			var sceneUI = Object.Instantiate(Resources.Load<GameUI>("ui/ui game"));
			sceneUI.Init(this);
			_uiRootView.AttachSceneUI(sceneUI.gameObject);

			var world = Object.FindFirstObjectByType<GameWorld>();
			var spawnPoints = Object.FindObjectsByType<VehicleSpawnPoint>(FindObjectsInactive.Include, FindObjectsSortMode.None);

			foreach (var spawnPoint in spawnPoints)
			{
				switch (spawnPoint.vehicleType)
				{
					case VehicleType.None:
						break;
					case VehicleType.Bulldozer:
						var bulldozer = Object.Instantiate(Resources.Load<VehicleBulldozer>("vehicles/bulldozer/bulldozer"), spawnPoint.transform.position,
							spawnPoint.transform.rotation, world.rootPlayer);
						bulldozer.Rigidbody.isKinematic = false;
						bulldozer.InGame = true;
						var camera = Object.FindFirstObjectByType<CameraController>();
						camera.SetTarget(bulldozer.transform);
						camera.SetControl(Resources.Load<BaseControlPanel>("controls/control panel bulldozer"));
						camera.InGame = true;

						bulldozer.SetControl(camera.ControlPanel as ControlPanelBulldozer);
						break;
					case VehicleType.Crane:
						break;
					default:
						throw new ArgumentOutOfRangeException();
				}
			}


			_uiRootView.HideLoadingScreen();
		}

		private IEnumerator LoadScene(string sceneName)
		{
			yield return SceneManager.LoadSceneAsync(sceneName);
		}

		public void ToLobby()
		{
			_coroutioner.StartCoroutine(LoadLobby());
		}

		public void ToGame()
		{
			_coroutioner.StartCoroutine(LoadGame());
		}
	}
}