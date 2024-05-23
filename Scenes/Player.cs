using Godot;
using System;

public partial class Player : CharacterBody2D
{
	public const float Speed = 300.0f;
	public const float JumpVelocity = -400.0f;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
    [Export]
    public PackedScene NeedleScene;
    [Export]
    public Node2D SpawnPoint;

    public override void _Ready()
    {
        SpawnPoint = GetNode<Node2D>("spawnPoint");
        base._Ready();
    }

    public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
			velocity.Y += gravity * (float)delta;

		// Handle Jump.
		if (Input.IsActionJustPressed("player2_up") && IsOnFloor())
			velocity.Y = JumpVelocity;

        // Get the input direction and handle the movement/deceleration.
        // As good practice, you should replace UI actions with custom gameplay actions.
        Vector2 direction2 = Input.GetVector("player2_left", "player2_right", "player2_up", "player2_down");
        if (direction2 != Vector2.Zero)
        {
            velocity.X = direction2.X * Speed;
        }
        else
        {
            velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
        }


        //fires a needle
        if (Input.IsActionJustPressed("player2_fire"))
        {
            LaunchBullet();
        }


        Velocity = velocity;
		MoveAndSlide();
	}


    private void LaunchBullet()
    {
        Needle needleInstance = (Needle)NeedleScene.Instantiate();
        AddChild(needleInstance);

        // Set the bullet's position to the player's position or the firing point.
        needleInstance.Position = GlobalPosition + new Vector2(-200, 0);

        // Set the bullet's direction based on the player's current rotation.
        needleInstance.Direction = new Vector2(Mathf.Cos(Rotation), Mathf.Sin(Rotation)).Normalized();
    }
}
