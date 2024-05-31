using Godot;
using System;

public partial class Needle : RigidBody2D
{

    [Export]
    public float Speed = 500f;
    public Vector2 Direction { get; set; }
    public Node2D playerDirection;
    private Timer _lifetimeTimer;
    public AudioStreamPlayer2D audio;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        audio = GetNode<AudioStreamPlayer2D>("AudioStreamPlayer2D");
        audio.Play(0);
        //Rotation = Direction.Angle();
        _lifetimeTimer = new Timer();
        _lifetimeTimer.WaitTime = 2.0f; // 2 seconds
        _lifetimeTimer.OneShot = true; // Timer should only run once
        _lifetimeTimer.Timeout += onTimeFinished;
        AddChild(_lifetimeTimer);
        _lifetimeTimer.Start();
    }
    private void onTimeFinished()
    {
        QueueFree();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _PhysicsProcess(double delta)
    {

        LinearVelocity = Direction * Speed;
    }
}
