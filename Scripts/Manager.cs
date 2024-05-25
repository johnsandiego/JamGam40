using Godot;
using System;

public partial class Manager : Node
{
	[Export]
	public Panel helpMenu;

	public Player player;
	public Player2 player2;
    [Export]
    public Panel optionMenu;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		player = GetNode<Player>("Necromancer");
        player2 = GetNode<Player2>("Necromancer2");

    }



    public override void _Input(InputEvent @event)
    {
        if (Input.IsKeyPressed(Key.H))
        {
            helpMenu.Visible = !helpMenu.Visible;

        }

        if(Input.IsKeyPressed(Key.Escape))
        {
            optionMenu.Visible = !optionMenu.Visible;
        }
    }

	private void _on_out_of_bounce_area_body_entered(Node2D body)
	{
		if (body != null)
		{
			ReloadScene();
        }
	}

	private void _on_out_of_bounce_area_2_body_entered(Node2D body)
	{
		if(body != null)
		{
			ReloadScene();

        }
	}
    private void _on_out_of_bounce_area_3_body_entered(Node2D body)
    {
        if (body != null)
        {
            ReloadScene();

        }
    }
    

    private void ReloadScene()
	{
        // Get the current scene
        PackedScene currentScene = (PackedScene)ResourceLoader.Load(GetTree().CurrentScene.SceneFilePath);

        // Remove the current scene
        GetTree().CurrentScene.Free();

        // Load the current scene again
        GetTree().ChangeSceneToPacked(currentScene);
    }
}
