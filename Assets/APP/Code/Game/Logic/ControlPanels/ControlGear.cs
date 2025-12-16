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

		public override void Release()
		{
		}

		private void OnMouseDrag()
		{
			var input = Input.GetAxis("Mouse Y");
			_newPosition.y += input * 0.15f;
		}


		private void Update()
		{
			if (_model.position != _newPosition)
			{
				var newPos = Vector3.Lerp(_model.localPosition, _newPosition, Time.deltaTime * 50f);
				newPos.y = Mathf.Clamp(newPos.y, _min, _max);
				_model.localPosition = newPos;

				if (_model.localPosition.y < -0.025f) _value = -1;
				else if (_model.localPosition.y > 0.025f) _value = 1;
				else _value = 0;
			}
		}
	}
}