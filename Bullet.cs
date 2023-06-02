using Godot;
using System;
using System.Diagnostics;

public class Bullet : Node2D
{
    public float Range = 300;
    private float distanceTravelled = 0;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        var area = GetNode<Area2D>("Area2D");

        // Connect signal for collision detected

        // area_entered: emitted when an Area2D enters in contact with another Area2D (but not a PhysicsBody2D).
        area.Connect("area_entered", this, "OnCollision");

        // body_entered: emitted when a PhysicsBody2D enters in contact with another PhysicsBody2D.
        area.Connect("body_entered", this, "OnCollision");
    }

    private void OnCollision(Node body)
    {
        QueueFree();
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        float speed = 200;
        float moveAmount = speed * delta;
        Position += Transform.x.Normalized() * moveAmount;

        distanceTravelled += moveAmount;

        if(distanceTravelled > Range)
            QueueFree();
    }
}
