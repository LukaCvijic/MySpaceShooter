using Godot;
using System;

public partial class enemy : Area2D
{
    private float _speed = 150.0f;
    private double _spendTime = 0;
    private int _direction = 1;
    private int _start = 0;
    private int _end = 1000;
    
    private Marker2D _muzzle;
	
    private PackedScene enemyLaserScene = (PackedScene)GD.Load("res://laser/enemy_laser.tscn");

    [Signal]
    public delegate void LaserShootEventHandler(PackedScene laserScene, Vector2 position);
    
    [Signal]
    public delegate void EnemyDeadEventHandler();
    
    public void Init (float speed, int start, int end)
    {
        _speed = speed;
        _start = start;
        _end = end;
    }
    
	public override void _Ready()
	{
        _muzzle =  (Marker2D)GetNode("Muzzle");

        Callable callable = new Callable(GetParent(), "_on_player_laser_shoot");
        Connect("LaserShoot", callable);
        
        callable = new Callable(GetParent(), "RemoveOneEnemy");
        Connect("EnemyDead", callable);
    }
    
	public override void _Process(double delta)
    {
        _spendTime += delta;
        
        if(_spendTime >= 1)
        {
            _spendTime = 0;
            
            EmitSignal("LaserShoot", enemyLaserScene, _muzzle.GlobalPosition);
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        Vector2 velocity = Position;
        
        if (velocity.X < _start || velocity.X > _end)
            _direction *= -1;
		
        velocity.X += _direction * _speed * (float)delta;
		
        Position = velocity;
    }

    public void _on_area_entered(Area2D laser)
    {
        EmitSignal("EnemyDead");
        
        QueueFree();
        laser.QueueFree();
    }
    
}
