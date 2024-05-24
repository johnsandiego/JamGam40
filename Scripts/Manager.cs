using Godot;
using System;

public partial class Manager : Node
{
	[Export]
	public Panel helpMenu;

	public Player player;
	public Player2 player2;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		player = GetNode<Player>("Necromancer");
        player2 = GetNode<Player2>("Necromancer2");

    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}

    public override void _Input(InputEvent @event)
    {
        if (Input.IsKeyPressed(Key.H))
        {
            helpMenu.Visible = !helpMenu.Visible;

        }
    }
}
