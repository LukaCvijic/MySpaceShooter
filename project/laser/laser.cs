using Godot;
using System;

public partial class laser : Area2D
{
	
	public const float speed = -300.0f;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
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
