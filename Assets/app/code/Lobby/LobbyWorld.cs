using UnityEngine;

namespace Lobby
{
	public sealed class LobbyWorld : MonoBehaviour
	{
		public Transform podium;
		public Transform spawnPoint;

		private void Start()
		{
		}

		private void Update()
		{
			podium.Rotate(Vector3.up, Time.deltaTime * 90f);
		}
	}
}