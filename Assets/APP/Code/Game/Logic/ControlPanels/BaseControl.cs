using UnityEngine;

namespace Game.Logic.ControlPanels
{
	public abstract class BaseControl : MonoBehaviour
	{
		public ControlType type;

		[SerializeField] protected Transform _model;
		[SerializeField] protected float _sensetivity;
		[SerializeField] protected float _value;

		public abstract void SetEnterParams(Vector3 position);
		public float GetValue() => _value * _sensetivity;
		public abstract void Release();
	}
}