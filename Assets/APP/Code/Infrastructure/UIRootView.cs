using UnityEngine;

namespace Infrastructure
{
	public sealed class UIRootView : MonoBehaviour
	{
		[SerializeField] private LoadingScreen _loadingScreen;
		[SerializeField] private Transform _uiSceneContainer;

		private void Awake() => HideLoadingScreen();

		public void ShowLoadingScreen()
		{
			_loadingScreen.Show();
		}

		public void HideLoadingScreen() => _loadingScreen.Hide();

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