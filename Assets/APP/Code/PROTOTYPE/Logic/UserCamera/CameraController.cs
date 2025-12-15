using Game.Logic.ControlPanels;
using UnityEngine;

namespace Game.Logic.UserCamera
{
	public sealed class CameraController : MonoBehaviour
	{
		[SerializeField] private Camera _controlCamera;
		[SerializeField] private LayerMask _controlMask;

		[SerializeField] private Vector3 _offset;

		private Transform _target;
		private BaseControl _control;
		private bool _busy;

		public void SetTarget(Transform target) => _target = target;

		private void Update()
		{
			if (Input.GetMouseButtonDown(0))
			{
				var ray = _controlCamera.ScreenPointToRay(Input.mousePosition);
				if (Physics.Raycast(ray, out var hit, Mathf.Infinity, _controlMask))
				{
					if (hit.collider.TryGetComponent<BaseControl>(out var control))
					{
						_control = control;
						_busy = true;
					}
				}
			}

			if (Input.GetMouseButton(0) && _busy)
			{
				var ray = _controlCamera.ScreenPointToRay(Input.mousePosition);
				if (Physics.Raycast(ray, out var hit, Mathf.Infinity, _controlMask))
				{
					if (hit.collider.TryGetComponent<BaseControl>(out var control))
					{
						_control.SetEnterParams(hit.point);
					}
				}
			}

			if (Input.GetMouseButtonUp(0) && _busy)
			{
				_control.Release();
				_control = null;
				_busy = false;
			}
		}

		private void LateUpdate()
		{
			if (_target != null)
				transform.position = Vector3.Lerp(transform.position, _target.position + _offset, Time.deltaTime * 20);
		}
	}
}