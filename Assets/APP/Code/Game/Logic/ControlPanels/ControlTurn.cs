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

		public override void SetEnterParams(Vector3 position)
		{
			var inverse = transform.InverseTransformPoint(position);
			_target = new Vector3(inverse.x, transform.localPosition.y, transform.localPosition.z);
		}

		public override void Release()
		{
			_target = new Vector3(0, transform.localPosition.y, transform.localPosition.z);
		}

		private void Update()
		{
			Vector2 direction = _target - _model.localPosition;
			float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
			angle = Mathf.Clamp(angle - 90, _min, _max);
			_model.localRotation = Quaternion.Euler(0, angle, 0);

			if (_model.localEulerAngles.y > 0 && _model.localEulerAngles.y <= 35) 
				_value = -(Mathf.Abs(_model.localEulerAngles.y) / 30);
			else if (_model.localEulerAngles.y >= 330 && _model.localEulerAngles.y <= 360) 
				_value = (360 - Mathf.Abs(_model.localEulerAngles.y)) / 30;
			else _value = 0;
		}
	}
}