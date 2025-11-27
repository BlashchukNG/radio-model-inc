//using Infrastructure.Coroutiner;
using UnityEngine;

namespace Infrastructure.Roots.AppRoot
{
	public sealed class UIRootView : MonoBehaviour
	{
		//[SerializeField] private CanvasShowHideController _loadingScreen;
		[SerializeField] private Transform _uiSceneContainer;

		//private void Awake() => HideLoadingScreen();

		public void ShowLoadingScreen()
		{
			// print(1);
			// _loadingScreen.Show();
		}

		//public void HideLoadingScreen() => _loadingScreen.Hide();

		public void AttachSceneUI(GameObject sceneUI)
		{
			ClearSceneUI();
			sceneUI.transform.SetParent(_uiSceneContainer, false);
		}

		public void ClearSceneUI()
		{
			foreach (Transform child in _uiSceneContainer)
				Destroy(child.gameObject);
		}
	}
}