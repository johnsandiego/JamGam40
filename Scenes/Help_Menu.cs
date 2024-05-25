using Godot;
using System;

public partial class Help_Menu : CanvasLayer
{
	[Export]
	public Panel optionMenu;
	public void OnReturn()
	{
        optionMenu.Visible = false;
	}

	public void OnQuit()
	{
        GetTree().Quit();
    }
}
