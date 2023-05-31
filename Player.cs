using Godot;
using System;

public class Player : Node2D
{
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        // Move player with arrow keys
        var speed = 150f;
        var moveAmount = speed * delta;

        var moveVector = new Vector2(0,0);

        if(Input.IsKeyPressed((int)KeyList.Up))
            moveVector.y = -moveAmount;

        if (Input.IsKeyPressed((int)KeyList.Down))
            moveVector.y = moveAmount;

        if (Input.IsKeyPressed((int)KeyList.Left))
            moveVector.x = -moveAmount;

        if (Input.IsKeyPressed((int)KeyList.Right))
            moveVector.x = moveAmount;

        Position += moveVector;

        // Rotate player with mouse
        Rotation = (GetGlobalMousePosition() - GlobalPosition).Angle();
    }
}
