using Godot;
using System;

public partial class DynamicCamera : Camera2D
{
    [Export]
    public NodePath Player1Path;
    [Export]
    public NodePath Player2Path;

    private Node2D player1;
    private Node2D player2;

    [Export]
    public float ZoomSpeed = 2f;
    [Export]
    public float MinZoom = 1f;
    [Export]
    public float MaxZoom = 2f;
    [Export]
    public float BoundaryPadding = 100f;

    public override void _Ready()
    {
        player1 = GetNode<Node2D>(Player1Path);
        player2 = GetNode<Node2D>(Player2Path);
    }

    public override void _PhysicsProcess(double delta)
    {
        FollowPlayers(delta);
    }

    private void FollowPlayers(double delta)
    {
        //midpoint
        GlobalPosition = (player1.GlobalPosition + player2.GlobalPosition) / 2;
        float distance = player1.GlobalPosition.DistanceTo(player2.GlobalPosition);
        // Calculate the desired zoom level
        float desiredZoom = Mathf.Clamp(BoundaryPadding / distance, MinZoom, MaxZoom);
        // Smoothly adjust the zoom level
        Zoom = Zoom.Lerp(new Vector2(desiredZoom, desiredZoom), ZoomSpeed * (float)delta);
    }
}
