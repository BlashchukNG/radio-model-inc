namespace Game.Logic.Vehicles.Movement
{
	public interface IMovable
	{
		void Move(float power, float gear);
		void Rotate(float angle);
	}
}