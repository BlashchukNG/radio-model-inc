using Game.Logic.ControlPanels;
using Game.Logic.Vehicles.Movement;
using UnityEngine;

namespace Game.Logic.Vehicles
{
	public sealed class VehicleBulldozer :
		BaseVehicle,
		IMovable
	{
		private ControlPanelBulldozer _controlPanel;


		public void SetControl(ControlPanelBulldozer controlPanel)
		{
			_controlPanel = controlPanel;
		}

		private void Start()
		{
			_rb.maxLinearVelocity = _maxVelocity;
			_rb.maxAngularVelocity = _maxTurnSpeed;
		}

		private void FixedUpdate()
		{
			if (!_inGame) return;

			Move(_controlPanel.power.GetValue(), _controlPanel.gear.GetValue());
			Rotate(_controlPanel.turn.GetValue());
		}

		public void Move(float power, float gear)
		{
			power *= gear;
			_rb.position += transform.forward * _velocity * power;

			//_rb.AddForce(transform.forward * (_velocity * (power * gear)), ForceMode.Acceleration);
		}

		public void Rotate(float angle)
		{
			transform.Rotate(transform.up, angle * _turnSpeed * Time.deltaTime);

			// Vector3 torque = Vector3.up * angle * _turnSpeed;
			// _rb.AddTorque(torque, ForceMode.Acceleration);
		}
	}
}