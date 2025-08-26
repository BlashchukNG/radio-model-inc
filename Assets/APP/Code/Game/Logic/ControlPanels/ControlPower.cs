using UnityEngine;

namespace Game.Logic.ControlPanels
{
	public sealed class ControlPower : BaseControl
	{
		[SerializeField] private Transform _min, _max;
		
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
				newPos.x = Mathf.Clamp(newPos.x, _min.localPosition.x, _max.localPosition.x);
				_model.localPosition = newPos;
				
				_value = Vector3.Distance(new Vector3(_model.localPosition.x,0,0), _min.localPosition) / 4f;
			}
		}
	}
}