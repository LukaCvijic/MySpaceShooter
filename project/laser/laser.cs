using Godot;
using System;

public partial class laser : Area2D
{
	
	public const float speed = -300.0f;
	
	public override void _Ready()
	{
		
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 position = GlobalPosition;

		position.Y += speed * (float)delta;

		GlobalPosition = position;
	}
	
	public void _on_visible_on_screen_notifier_2d_screen_exited(){
		QueueFree();
	}
}
