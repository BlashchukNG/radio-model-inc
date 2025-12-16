using Game.Logic.ControlPanels;
using Game.Logic.Vehicles.Movement;
using Game.Logic.World;
using UnityEngine;

namespace Game.Logic.Vehicles
{
	public sealed class VehicleBulldozer :
		BaseVehicle,
		IMovable
	{
		[SerializeField] private Rigidbody[] _rigidbodies;
		[SerializeField] private Collider[] _colliders;

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
			Rotate(_controlPanel.power.GetValue(), _controlPanel.turn.GetValue());
		}

		public void Move(float power, float gear)
		{
			power *= gear;
			_rb.position += transform.forward * _velocity * power;

			//_rb.AddForce(transform.forward * (_velocity * (power * gear)), ForceMode.Acceleration);
		}

		public void Rotate(float power, float angle)
		{
			if (power > 0)
				transform.Rotate(transform.up, angle * _turnSpeed);

			// Vector3 torque = Vector3.up * angle * _turnSpeed;
			// _rb.AddTorque(torque, ForceMode.Acceleration);
		}

		private void OnCollisionEnter(Collision other)
		{
			if (other.collider.TryGetComponent<Ground>(out var ground))
			{
				print("lose");
				_rb.isKinematic = true;
				_collider.enabled = false;

				_inGame = false;

				foreach (var rb in _rigidbodies)
				{
					rb.transform.parent = null;
					rb.isKinematic = false;
					rb.AddForce(new Vector3(Random.Range(-1f, 1f), Random.Range(0.2f, 1f), Random.Range(-1f, 1f)) * 5, ForceMode.Impulse);
				}

				foreach (var c in _colliders)
				{
					c.enabled = true;
				}
			}
		}
	}
}