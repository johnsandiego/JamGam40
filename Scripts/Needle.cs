using Godot;
using System;

public partial class Needle : RigidBody2D
{

    [Export]
    public float Speed = 500f;
    public Vector2 Direction { get; set; }
    public Node2D playerDirection;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        LinearVelocity = Direction * Speed;
    }
}
