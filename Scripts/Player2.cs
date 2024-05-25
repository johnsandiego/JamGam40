using Godot;
using System;

public partial class Player2 : BasePlayer, IBasePlayer
{
    public override void _Ready()
    {
        base.playerIndex = 2;
        base._Ready();
    }
    public override void _PhysicsProcess(double delta)
	{
        base._PhysicsProcess(delta);
	}

    //public void OnBodyEntered(Node2D body)
    //{
       
    //}

    public override void OnBodyEntered2(Node2D body)
    {
        if (body != null && body.IsInGroup("player1"))
        {
            hitFlash.Play("juice");
            playerMultiplier += 5;
        }
    }

    public override void OnBodyEntered(Node2D body)
    {
        throw new NotImplementedException();
    }
}
