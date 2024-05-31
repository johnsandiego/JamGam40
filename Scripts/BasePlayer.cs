using Godot;
using System;

public abstract partial class BasePlayer : CharacterBody2D
{
    public const float Speed = 300.0f;
    public const float JumpVelocity = -400.0f;

    public int playerMultiplier = 0;

    public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
    public int TeleportDistance = 100; // Teleport distance in pixels
    private int tripleJump = 0;

    [Export]
    public PackedScene NeedleScene;
    [Export]
    public Node2D SpawnPoint;

    private Sprite2D _sprite;
    private Node2D _spawnPoint;
    public int playerIndex = 0;
    [Export]
    public Node2D parent;
    public override void _Ready()
    {
        _sprite = GetNode<Sprite2D>("Sprite2D");
        SpawnPoint = GetNode<Node2D>("spawnPoint");
    }

    public override void _PhysicsProcess(double delta)
    {
        Vector2 velocity2 = Velocity;
        switch (playerIndex)
        {
            case 1:

                if (!IsOnFloor())
                {
                    velocity2.Y += gravity * (float)delta;
                }
                else
                {
                    tripleJump = 0;
                }

                if (Input.IsActionJustPressed("player2_up"))
                {
                    if (IsOnFloor() || tripleJump <= 3)
                    {
                        velocity2.Y = JumpVelocity;
                        tripleJump++;
                    }
                }

                Vector2 direction = Input.GetVector("player2_left", "player2_right", "player2_up", "player2_down");
                if (direction != Vector2.Zero)
                {
                    velocity2.X = direction.X * Speed;
                }
                else
                {
                    velocity2.X = Mathf.MoveToward(Velocity.X, 0, Speed);
                }

                if (Input.IsActionJustPressed("teleport"))
                {
                    Teleport(direction);
                }

                if (Input.IsActionJustPressed("player2_fire"))
                {
                    LaunchBullet("player1", false);
                }

                if (velocity2.X != 0)
                {
                    bool isMovingLeft = velocity2.X < 0;
                    _sprite.FlipH = isMovingLeft;
                    UpdateSpawnPointPosition(isMovingLeft);
                }

                Velocity = velocity2;
                MoveAndSlide();
                break;
            case 2:

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
                    }
                }

                Vector2 direction2 = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
                if (direction2 != Vector2.Zero)
                {
                    velocity.X = direction2.X * Speed;
                }
                else
                {
                    velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
                }

                //fires a needle
                if (Input.IsActionJustPressed("fire"))
                {
                    LaunchBullet("player2", true);
                }

                if (Input.IsActionJustPressed("player2teleport"))
                {
                    Teleport(direction2);
                }

                if (velocity.X != 0)
                {
                    bool isMovingLeft = velocity.X < 0;
                    _sprite.FlipH = !isMovingLeft;
                    UpdateSpawnPointPosition(!isMovingLeft);
                }

                Velocity = velocity;
                MoveAndSlide();
                break;
        }
    }

    private void UpdateSpawnPointPosition(bool isMovingLeft)
    {
        if (isMovingLeft)
        {
            SpawnPoint.Position = new Vector2(-Math.Abs(SpawnPoint.Position.X), SpawnPoint.Position.Y);
        }
        else
        {
            SpawnPoint.Position = new Vector2(Math.Abs(SpawnPoint.Position.X), SpawnPoint.Position.Y);
        }
    }

    private void Teleport(Vector2 direction)
    {
        direction = direction.Normalized();
        Vector2 newPosition = Position + direction * TeleportDistance;
        Position = newPosition;
    }

    private void LaunchBullet(string groupName, bool rotateNeedle180)
    {
        Needle needleInstance = (Needle)NeedleScene.Instantiate();

        var position = GlobalPosition + new Vector2(100, 0);
        var direction = new Vector2(Mathf.Cos(Rotation), Mathf.Sin(-Rotation)).Normalized();

        if (_sprite.FlipH)
        {
            needleInstance.Direction = -direction;
            //needleInstance.Position = -position;
 
        }
        else
        {
            needleInstance.Direction = direction;
            //needleInstance.Position = position;
            if (rotateNeedle180)
            {
                needleInstance.Rotate(Mathf.DegToRad(180));
            }
        }


        needleInstance.GlobalPosition = SpawnPoint.GlobalPosition;
        needleInstance.AddToGroup(groupName);
        GetParent().AddChild(needleInstance);
    }
}
