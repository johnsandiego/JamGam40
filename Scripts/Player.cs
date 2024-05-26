using Godot;
using System;

public partial class Player : BasePlayer
{
    public AnimationPlayer hitFlash;
    public AudioStreamPlayer2D audio;

    public override void _Ready()
    {
        base.playerIndex = 1;
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
            GD.Print("player2 hit");
            Velocity += new Vector2(playerMultiplier, -playerMultiplier * (float).2);
        }
    }
}
