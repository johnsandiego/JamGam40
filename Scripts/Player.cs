using Godot;
using System;

public partial class Player : BasePlayer
{
    public override void _Ready()
    {
        base.playerIndex = 1;
        base._Ready();
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
	}
    //private void UpdateSpawnPointPosition(bool isMovingLeft)
    //{
    //    // Update the spawn point position to be on the opposite side of the player
    //    if (isMovingLeft)
    //    {
    //        SpawnPoint.Position = new Vector2(-Math.Abs(SpawnPoint.Position.X), SpawnPoint.Position.X);
    //    }
    //    else
    //    {
    //        SpawnPoint.Position = new Vector2(Math.Abs(SpawnPoint.Position.X), SpawnPoint.Position.X);
    //    }
    //}

    //public void OnBodyEntered(Node2D body)
    //{
        
    //}

    //private void Teleport(Vector2 direction)
    //{

    //    // Normalize the direction to prevent faster diagonal movement
    //    direction = direction.Normalized();

    //    // Calculate the new position
    //    Vector2 newPosition = Position + direction * TeleportDistance;

    //    // Optional: Check for collision or valid teleport location here

    //    // Set the new position
    //    Position = newPosition;
    //}

    //private void LaunchBullet()
    //{
    //    Needle needleInstance = (Needle)NeedleScene.Instantiate();
    //    needleInstance.AddToGroup("player1");
    //    SpawnPoint.AddChild(needleInstance);

    //    // Set the bullet's position to the player's position or the firing point.
    //    needleInstance.Position = GlobalPosition + new Vector2(500, 0);

    //    // Set the bullet's direction based on the player's current rotation.
    //    needleInstance.Direction = new Vector2(Mathf.Cos(Rotation), Mathf.Sin(Rotation)).Normalized();
    //}

    public override void OnBodyEntered(Node2D body)
    {
        if (body != null && body.IsInGroup("player2"))
        {
            hitFlash.Play("juice");
            playerMultiplier += 5;
        }
    }

    public override void OnBodyEntered2(Node2D body)
    {
        throw new NotImplementedException();
    }
}
