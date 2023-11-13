using Godot;
using System;

public partial class player : CharacterBody2D
{
	private const float Speed = 300.0f;

	private Marker2D _muzzle;
	
	private PackedScene _laserScene = (PackedScene)GD.Load("res://laser/laser.tscn");

	[Signal]
	public delegate void LaserShootEventHandler(PackedScene laserScene, Vector2 position);
    [Signal]
    public delegate void PlayerShootEventHandler();
	
	public override void _Ready()
	{
		_muzzle =  (Marker2D)GetNode("Muzzle");
	}
	
	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		int direction = 0;
		
		if (Input.IsActionPressed("move_left"))
			direction = -1;
		if (Input.IsActionPressed("move_right"))
			direction = 1;
		if (Input.IsActionJustPressed("shoot"))
			Shoot();
		
		velocity.X = direction * Speed;

		if ((Position.X <= 10 && direction < 0)|| (Position.X >= 1050 && direction > 0))
			velocity.X = 0;
		
		Velocity = velocity;
		MoveAndSlide();
	}
    
	private void Shoot()
	{
		EmitSignal("LaserShoot", _laserScene, _muzzle.GlobalPosition);
	}
    
    public void _on_area_entered(Area2D area)
    {
        EmitSignal("PlayerShoot");
        area.QueueFree();
    }
}
