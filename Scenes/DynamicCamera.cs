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
    public float MinZoom = 0.5f;
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
        // Calculate the midpoint between the two players
        Vector2 midpoint = (player1.GlobalPosition + player2.GlobalPosition) / 2;
        GlobalPosition = midpoint;

        // Calculate the distance between the two players
        float distance = player1.GlobalPosition.DistanceTo(player2.GlobalPosition);
        GD.Print("distance together: " + distance);
        // Calculate the desired zoom level
        float desiredZoom = Mathf.Clamp(BoundaryPadding / distance , MinZoom, MaxZoom);
        GD.Print("zoom level: " +  desiredZoom);    
        // Smoothly adjust the zoom level
        Zoom = Zoom.Lerp(new Vector2(desiredZoom, desiredZoom), ZoomSpeed * (float)delta);

        // Ensure the players are within the view boundaries
        Rect2 screenRect = GetViewportRect();
        Vector2 screenMin = screenRect.Position;
        Vector2 screenMax = screenRect.End;
        GD.Print("screenRect: ", screenRect);
        Vector2 camMin = ToLocal(player1.GlobalPosition - screenRect.Size / 2);
        Vector2 camMax = ToLocal(player2.GlobalPosition + screenRect.Size / 2);

        Vector2 newCamPos = GlobalPosition;

        if (camMin.X < screenMin.X)
            newCamPos.X = screenMin.X;
        if (camMin.Y < screenMin.Y)
            newCamPos.Y = screenMin.Y;
        if (camMax.X > screenMax.X)
            newCamPos.X = screenMax.X;
        if (camMax.Y > screenMax.Y)
            newCamPos.Y = screenMax.Y;

        GD.Print("newcampos: ", newCamPos);

        GlobalPosition = newCamPos;
    }
}
