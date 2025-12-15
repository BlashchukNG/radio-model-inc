using UnityEngine;

namespace Game.Logic.Vehicles
{
	public abstract class BaseVehicle : MonoBehaviour
	{
		[SerializeField] protected Rigidbody _rb;
		[SerializeField] protected float _velocity;
		[SerializeField] protected float _maxVelocity;
		[SerializeField] protected float _turnSpeed;
		[SerializeField] protected float _maxTurnSpeed;
	}
}