using Root;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
	public sealed class GameUI : MonoBehaviour
	{
		[SerializeField] public Button bQuit;
		private AppEntryPoint _entryPoint;

		public void Init(AppEntryPoint entryPoint)
		{
			_entryPoint = entryPoint;
			bQuit.onClick.AddListener(ToLobby);
		}

		private void ToLobby()
		{
			_entryPoint.ToLobby();
		}
	}
}