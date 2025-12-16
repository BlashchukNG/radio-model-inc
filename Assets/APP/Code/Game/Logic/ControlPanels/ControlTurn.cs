using UnityEngine;

namespace Game.Logic.ControlPanels
{
	public sealed class ControlTurn : BaseControl
	{
		[SerializeField] private float _min, _max, _speed;

		private float _angle;
		private Vector3 _target;

		private void Awake()
		{
			_target = new Vector3(0, transform.localPosition.y, transform.localPosition.z);
		}

		// public void SetEnterParams(Vector3 position)
		// {
		// 	var inverse = transform.InverseTransformPoint(position);
		// 	_target = new Vector3(inverse.x, transform.localPosition.y, transform.localPosition.z);
		// }
		
		private void OnMouseDrag()
		{
			var input = Input.GetAxis("Mouse X");
			_angle += input * 25;
		}

		public override void Release()
		{
			_angle = 0;
		}

		private void Update()
		{
			// Vector2 direction = _target - _model.localPosition;
			// float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
			// angle = Mathf.Clamp(angle - 90, _min, _max);
			// _model.localRotation = Quaternion.Euler(0, angle, 0);
			
			// Vector3 direction = _model.localPosition - _target;
			// float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
			// print($"Angle: {angle} pos: {_target}");
			_angle = Mathf.Clamp(_angle, _min, _max);
			_model.localRotation = Quaternion.Euler(90, 0, _angle);

			if (_model.localEulerAngles.y > 5 && _model.localEulerAngles.y <= 35) 
				_value = -(Mathf.Abs(_model.localEulerAngles.y) / 30);
			else if (_model.localEulerAngles.y >= 325 && _model.localEulerAngles.y <= 355) 
				_value = (360 - Mathf.Abs(_model.localEulerAngles.y)) / 30;
			else _value = 0;
		}
	}
}