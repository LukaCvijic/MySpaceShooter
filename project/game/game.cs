using Godot;
using System;

public partial class game : Node2D
{
    private Node2D laserContainer;
    
    public override void _Ready()
    {
        laserContainer = GetNode<Node2D>("LaserContainer");
    }
    
    public void _on_player_laser_shoot(PackedScene laserScene, Vector2 position)
    {
        Area2D laser = laserScene.Instantiate<Area2D>();

        laser.GlobalPosition = position;
        
        laserContainer.AddChild(laser);
    }
}
