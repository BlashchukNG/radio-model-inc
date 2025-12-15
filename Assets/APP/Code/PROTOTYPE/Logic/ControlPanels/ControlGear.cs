using UnityEngine;

namespace Game.Logic.ControlPanels
{
	public sealed class ControlGear : BaseControl
	{
		[SerializeField] private float _min, _max;
		
		private Vector3 _newPosition;

		private void Awake()
		{
			_newPosition = _model.localPosition;
		}

		public override void SetEnterParams(Vector3 position)
		{
			_newPosition.x = transform.InverseTransformPoint(position).x;
		}

		public override void Release() { }

		private void Update()
		{
			if (_model.position != _newPosition)
			{
				var newPos = Vector3.Lerp(_model.localPosition, _newPosition, Time.deltaTime * 50f);
				newPos.x = Mathf.Clamp(newPos.x, _min, _max);
				_model.localPosition = newPos;

				if (_model.localPosition.x < -0.25f) _value = -1;
				else if (_model.localPosition.x > 0.25f) _value = 1;
				else _value = 0;
			}
		}
	}
}