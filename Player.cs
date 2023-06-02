using Godot;
using System;

public class Player : Node2D
{
    PackedScene bulletScene;
    private float _health = 10;
    public float Health
    {
        get { return _health; }
        set 
        { 
            _health = value;
            if(_health <= 0)
                QueueFree();
        }
    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        bulletScene = GD.Load<PackedScene>("res://Bullet.tscn");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        // Move player with arrow keys
        var speed = 150f;
        var moveAmount = speed * delta;

        var moveVector = new Vector2(0,0);

        if(Input.IsKeyPressed((int)KeyList.Up))
            moveVector.y = -1;

        if (Input.IsKeyPressed((int)KeyList.Down))
            moveVector.y = 1;

        if (Input.IsKeyPressed((int)KeyList.Left))
            moveVector.x = -1;

        if (Input.IsKeyPressed((int)KeyList.Right))
            moveVector.x = 1;

        Position += moveVector.Normalized() * moveAmount;

        // Rotate player with mouse
        Rotation = (GetGlobalMousePosition() - GlobalPosition).Angle();
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        var isLeftMouseButtonPressed = 
            @event is InputEventMouseButton mouseEvent &&
            mouseEvent.ButtonIndex is (int)ButtonList.Left &&
            mouseEvent.Pressed;

        if (isLeftMouseButtonPressed)
        {
            var bullet = (Bullet)bulletScene.Instance();
            bullet.Position = Position;
            bullet.Rotation = Rotation;
            GetParent().AddChild(bullet);
            GetTree().SetInputAsHandled();
        }
    }
}
