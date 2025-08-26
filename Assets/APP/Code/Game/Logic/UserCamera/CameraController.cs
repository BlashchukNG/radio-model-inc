using UnityEngine;

namespace Game.Logic.UserCamera
{
	public sealed class CameraController : MonoBehaviour
	{
		[SerializeField] private LayerMask _controlMask;
		[SerializeField] private Vector3 _offset;
		
		private Transform _target;

		public void SetTarget(Transform target) => _target = target;

		private void Update()
		{
			//TODO: рейкасты в кнопки
		}

		private void LateUpdate()
		{
			transform.position = _target.position + _offset;
		}
	}
}