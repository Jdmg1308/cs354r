using Godot;
using System;

public partial class Next_0 : Area2D
{

	[Signal]
	public delegate void winEventHandler();
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void _on_body_entered(Node body) {
		if (body is Player_controller playerController)
        {
			EmitSignal("win");
        }
	}
}
