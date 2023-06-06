using Godot;
using System;

public class Enemy : Node2D
{
    private Timer timer;
    private int health = 5;

    public void Damage(int amount)
    {
        health -= amount;

        if (health <= 0)
            QueueFree();
    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        timer = GetNode<Timer>("Timer");
        var area = GetNode<Area2D>("Area2D");
        area.Connect("area_entered", this, nameof(OnCollision));
        area.Connect("area_exited", this, nameof(ExitCollission));

        // Create timer to handle damage. When the enemy collides with the player,
        // there is no immediate damage dealt. Instead, on collision, a timer is started.
        // After 1 second has passed, the timer naturally throws a 'timeout' signal.
        // When the timeout signal is thrown, we call our OnTimerTimeout method that
        // will handle the damage.
        timer.Connect("timeout", this, nameof(OnTimerTimeout));
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        // follow player
        try
        {
            // TODO: try implementing GetTree().Root.HasNode("targetPath")
            // to check for existence. Otherwise, godot will throw error when
            // trying to get the player. 
            var player = GetParent().GetNode<Player>("Player");

            // pixels per second
            var speed = 80f;
            var moveAmount = speed * delta;
            var moveDirection = (player.Position - Position).Normalized();
            Position += moveDirection * moveAmount;
        }
        catch (Exception ex)
        {
            GD.Print("Player not found, muhahah");
        }
    }

    private void OnCollision(Area2D body)
    {
        if(body.GetParent() is Player player)
            timer.Start(1);
    }

    private void ExitCollission(Area2D body)
    {
        if(body.GetParent() is Player player)
            timer.Stop();
    }

    private void OnTimerTimeout()
    {
        var player = GetParent().GetNode<Player>("Player");
        if(player != null)
        {
            player.Health -= 2;
            GD.Print("Damage take. Health at: " + player.Health);
        }
    }
}
