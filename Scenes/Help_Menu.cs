using Godot;
using System;

public partial class Help_Menu : CanvasLayer
{
	[Export]
	public Panel optionMenu;
	public void OnReturn()
	{
        optionMenu.Visible = false;
        // Get the current scene
        PackedScene currentScene = (PackedScene)ResourceLoader.Load(GetTree().CurrentScene.SceneFilePath);

        // Load the current scene again
        GetTree().ChangeSceneToPacked(currentScene);
    }

	public void OnQuit()
	{
        GetTree().Quit();
    }
}
