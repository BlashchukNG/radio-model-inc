using System;
using Infrastructure;
using UnityEngine;
using UnityEngine.UI;

namespace Lobby.UI
{
	public sealed class LobbyUI : MonoBehaviour
	{
		[SerializeField] private Button _bPlay;
		[SerializeField] private Button _bLeaderboard;

		private AppEntryPoint _appEntryPoint;

		private void Awake()
		{
			_bPlay.onClick.AddListener(ButtonPlayClicked);
		}

		private void ButtonPlayClicked()
		{
			print("ButtonPlayClicked");
			_appEntryPoint.ToGame();
		}

		public void Init(AppEntryPoint appEntryPoint)
		{
			_appEntryPoint = appEntryPoint;
		}
	}
}