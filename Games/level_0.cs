using Godot;
using System;

public partial class level_0 : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		CollisionShape2D wall =this.GetNode("Area2D/Damage_walls") as CollisionShape2D;
		wall.Disabled = false;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void _on_area_2d_body_exited(Node body) {
		if (body is Player_controller playerController)
        {
			GetTree().ReloadCurrentScene();
        }
	}
}
