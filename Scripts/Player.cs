using Godot;
using System;

public partial class Player : BasePlayer
{
    public AnimationPlayer hitFlash;
    public AudioStreamPlayer2D audio;

    public override void _Ready()
    {
        playerIndex = 1;
        hitFlash = GetNode<AnimationPlayer>("AnimationPlayer");
        audio = GetNode<AudioStreamPlayer2D>("AudioStreamPlayer2D");
        base._Ready();
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        //hitFlash.Play("idle");

    }

    public void OnBodyEntered(Node2D body)
    {
        if (body != null && body.IsInGroup("player2"))
        {
            audio.Play(0);
            hitFlash.Play("juice");
            playerMultiplier += new Random().Next(5,26);
            // Calculate the knockback direction based on the impact point
            Vector2 knockbackDirection = (GlobalPosition - body.GlobalPosition).Normalized();
            knockbackDirection.Y -= .5f;
            knockbackDirection.X += 1f;
            // Apply the knockback force with the multiplier
            Velocity += knockbackDirection * playerMultiplier * 10;

            // Ensure the player moves with the updated velocity
            MoveAndSlide();

            body.QueueFree();
        }
    }
}
