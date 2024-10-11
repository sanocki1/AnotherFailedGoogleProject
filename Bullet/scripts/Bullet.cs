using Godot;

public partial class Bullet : Area2D
{
	[Export]
	private float _speed = 10f;
	public Vector2 Direction = new();

	public override void _PhysicsProcess(double delta)
	{
		Position += Direction * _speed;

		if (Position.X < 0 || Position.X > GetViewportRect().Size.X || Position.Y < 0 || Position.Y > GetViewportRect().Size.Y)
		{
			QueueFree(); // Remove the bullet when it goes off screen
		}
	}
}
