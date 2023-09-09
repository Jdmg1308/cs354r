using Godot;
using System;

public partial class damage_block : Area2D
{
	private ProgressBar HealthBar;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        AnimationPlayer ap = GetNode("DamagePlayer") as AnimationPlayer;
        ap.Play("new_animation");
		ap.SpeedScale = 20.0f;
        HealthBar = GetNodeOrNull<ProgressBar>("/root/Game/CanvasLayer/HealthBar");
        if (HealthBar == null ) {
            GetTree().Quit();
        }
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void _on_body_entered(Node body)
    {
        if (body is Player_controller playerController)
        {
            HealthBar.Call("damageObjDEATH");
        }
    }
}
