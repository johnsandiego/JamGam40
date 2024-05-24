using Godot;
using System;

public partial class Player2 : CharacterBody2D
{
	public const float Speed = 300.0f;
	public const float JumpVelocity = -400.0f;

    public int playerMultiplier = 0;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
    [Export]
    public PackedScene NeedleScene;

    [Export]
    public Node2D SpawnPoint;
    public AnimationPlayer hitFlash;
    private int tripleJump = 0;
    public override void _Ready()
    {
        SpawnPoint = GetNode<Node2D>("spawnPoint");
        hitFlash = GetNode<AnimationPlayer>("AnimationPlayer");
    }
    public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

        // Add the gravity.
        if (!IsOnFloor())
        {
            velocity.Y += gravity * (float)delta;
        }
        else
        {
            tripleJump = 0;
        }

        // Handle Jump.
        if (Input.IsActionJustPressed("ui_up"))
        {

            if (IsOnFloor() || tripleJump <= 3)
            {
                velocity.Y = JumpVelocity;
                tripleJump++;
                GD.Print(tripleJump);

            }


        }


        Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
        if (direction != Vector2.Zero)
        {
            velocity.X = direction.X * Speed;
        }
        else
        {
            velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
        }

        //fires a needle
        if (Input.IsActionJustPressed("fire"))
        {
            LaunchBullet();
        }

        Velocity = velocity;
		MoveAndSlide();
	}

    public void OnBodyEntered(Node2D body)
    {
        if (body != null && body.IsInGroup("player1"))
        {
            hitFlash.Play("juice");
            playerMultiplier += 5;
        }
    }

    private void LaunchBullet()
    {
        Needle needleInstance = (Needle)NeedleScene.Instantiate();
        needleInstance.AddToGroup("player2");
        AddChild(needleInstance);

        // Set the bullet's position to the player's position or the firing point.
        needleInstance.Position = GlobalPosition + new Vector2(500, 0);

        // Set the bullet's direction based on the player's current rotation.
        needleInstance.Direction = new Vector2(Mathf.Cos(Rotation), Mathf.Sin(Rotation)).Normalized();
    }
}
