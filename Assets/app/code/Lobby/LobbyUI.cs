using Root;
using UnityEngine;
using UnityEngine.UI;

namespace Lobby
{
	public sealed class LobbyUI : MonoBehaviour
	{
		[SerializeField] private Button bPlay, bLeaderboard;
		private AppEntryPoint _entryPoint;

		public void Init(AppEntryPoint entryPoint)
		{
			_entryPoint = entryPoint;
			bPlay.onClick.AddListener(ToGame);
		}

		private void ToGame()
		{
			_entryPoint.ToGame();
		}
	}
}