using UnityEngine;

namespace Game.Logic.ControlPanels
{
	public abstract class BaseControl : MonoBehaviour
	{
		public ControlType type;

		public abstract void SetEnterParams(Vector3 position);

		public abstract float GetValue();
	}
}