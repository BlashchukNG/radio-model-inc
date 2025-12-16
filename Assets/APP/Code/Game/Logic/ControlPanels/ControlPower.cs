using UnityEngine;

namespace Game.Logic.ControlPanels
{
	public sealed class ControlPower : BaseControl
	{
		[SerializeField] private float _min, _max;

		private Vector3 _newPosition;

		private void Awake()
		{
			_newPosition = _model.localPosition;
		}

		private void OnMouseDrag()
		{
			var input = Input.GetAxis("Mouse Y");
			_newPosition.y += input * 0.15f;
		}

		public override void Release()
		{
		}

		private void Update()
		{
			if (_model.position != _newPosition)
			{
				var newPos = Vector3.Lerp(_model.localPosition, _newPosition, Time.deltaTime * 50f);
				newPos.y = Mathf.Clamp(newPos.y, _min, _max);
				_model.localPosition = newPos;

				_value = 1 + Mathf.MoveTowards(_min, _max, _model.localPosition.y); 
			}
		}
	}
}