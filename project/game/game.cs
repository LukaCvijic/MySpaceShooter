using Godot;
using System;

public partial class game : Node2D
{
    private Node2D _laserContainer;
    private float _lives = 5.0f;
    private int _enemyCounter = 2;
    
    public Callable callable;
    
    private PackedScene _enemyScene = (PackedScene)GD.Load("res://enemy/enemy.tscn");
    
    public override void _Ready()
    {
        _laserContainer = GetNode<Node2D>("LaserContainer");
        
        int[][] positions = new int[][]
        {
            // speed, startPositionX, min, max
            new int[]{ 150, 100, 0, 290}, 
            new int[]{ 200, 500, 310, 1000}
        };

        foreach (int[] position in positions)
        {
            enemy enemyToAdd = (enemy)_enemyScene.Instantiate<Area2D>();

            enemyToAdd.Init(position[0], position[2], position[3]);
            
            enemyToAdd.GlobalPosition = new Vector2(position[1], 70);
            
            AddChild(enemyToAdd);
        }
    }
    
    public void RemoveOneEnemy()
    {
        _enemyCounter--;
        
        if(_enemyCounter == 0)
        {
            GetTree().ChangeSceneToFile("res://home/home_screen.tscn");
        }
    }
    
    public void _on_player_laser_shoot(PackedScene laserScene, Vector2 position)
    {
        Area2D laser = laserScene.Instantiate<Area2D>();

        laser.GlobalPosition = position;
        
        _laserContainer.AddChild(laser);
    }
    
    public void _on_playerr_shoot()
    {
        _lives--;

        Label liveLabel = GetNode<Label>("Lives");

        liveLabel.Text = "Lives: " + _lives;
        
        if(_lives == 0)
        {
            GetTree().ChangeSceneToFile("res://home/home_screen.tscn");
        }
    }
}
