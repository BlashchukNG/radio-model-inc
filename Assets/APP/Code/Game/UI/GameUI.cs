using Infrastructure;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
	public sealed class GameUI : MonoBehaviour
	{
		[SerializeField] private Button _bQuit;
		
		private AppEntryPoint _appEntryPoint;

		
		private void Awake()
		{
			_bQuit.onClick.AddListener(ButtonQuitClicked);
		}

		private void ButtonQuitClicked()
		{
			print("ButtonQuitClicked");
			_appEntryPoint.ToLobby();
		}
		
		public void Init(AppEntryPoint appEntryPoint)
		{
			_appEntryPoint = appEntryPoint;
		}
	}
}