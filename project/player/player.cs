using Godot;
using System;
using System.Runtime.CompilerServices;

public partial class player : CharacterBody2D
{
	private const float Speed = 300.0f;
	private float Lives = 5.0f;

	private Marker2D Muzzle;
	
	private PackedScene LaserScene = (PackedScene)GD.Load("res://laser/laser.tscn");

	[Signal]
	public delegate void LaserShootEventHandler(PackedScene laserScene, Vector2 position);
	
	public override void _Ready()
	{
		Muzzle =  (Marker2D)GetNode("Muzzle");
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
		EmitSignal("LaserShoot", LaserScene, Muzzle.GlobalPosition);
	}
	
	public void gotHit()
	{
		Lives--;
	}
}
