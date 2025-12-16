using System;
using Game.Logic.ControlPanels;
using UnityEngine;

namespace Game.Logic.UserCamera
{
	public sealed class CameraController : MonoBehaviour
	{
		[SerializeField] private Camera _controlCamera;
		[SerializeField] private Transform _controlSpawnPoint;

		[SerializeField] private Vector3 _offset;
		[SerializeField] private float _smoothTime = 0.3f;

		private Transform _target;
		private BaseControlPanel _controlPanel;
		private Vector3 _velocity;
		private bool _inGame;

		public BaseControlPanel ControlPanel => _controlPanel;

		public bool InGame
		{
			get => _inGame;
			set => _inGame = value;
		}

		private void Start()
		{
			_controlCamera.targetDisplay = 0;
			_controlCamera.farClipPlane = 30;
			_controlCamera.depth = 1;
		}

		public void SetTarget(Transform target)
		{
			_target = target;
			transform.position = _target.position + _offset;
		}

		public void SetControl(BaseControlPanel prefab)
		{
			_controlPanel = Instantiate(prefab, _controlSpawnPoint);
		}

		private void Update()
		{
		}

		private void LateUpdate()
		{
			if (!_inGame) return;

			var desiredPosition = _target.position + _offset;
			var smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref _velocity, _smoothTime);
			transform.position = smoothedPosition;
			
			//transform.position = Vector3.Lerp(transform.position, _target.position + _offset, Time.deltaTime * 20);
		}
	}
}