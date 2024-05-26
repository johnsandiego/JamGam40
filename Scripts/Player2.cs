using Godot;
using System;

public partial class Player2 : BasePlayer
{
    public AnimationPlayer hitFlash;
    public bool isHit = false;
    public AudioStreamPlayer2D audio;
    public override void _Ready()
    {
        base.playerIndex = 2;
        hitFlash = GetNode<AnimationPlayer>("AnimationPlayer");
        audio = GetNode<AudioStreamPlayer2D>("AudioStreamPlayer2D");
        base._Ready();
    }
    public override void _PhysicsProcess(double delta)
	{
        base._PhysicsProcess(delta);
    }

    public void OnBodyEntered2(Node2D body)
    {
        if (body != null && body.IsInGroup("player1"))
        {
            audio.Play(0);
            hitFlash.Play("juice");
            isHit = true;
            playerMultiplier += new Random().Next(5, 26); ;
            GD.Print("player1 hit");
            Velocity *= new Vector2(playerMultiplier, -playerMultiplier * (float).2);

        }
    }
}
