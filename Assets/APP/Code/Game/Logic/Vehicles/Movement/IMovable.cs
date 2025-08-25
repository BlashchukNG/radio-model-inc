namespace Game.Logic.Vehicles.Movement
{
	public interface IMovable
	{
		void Move(float power);
		void Rotate(float angle);
	}
}