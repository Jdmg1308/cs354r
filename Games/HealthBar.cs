using Godot;
using System;

public partial class HealthBar : ProgressBar
{
	[Signal]
	public delegate void deathEventHandler(bool game_paused);
	public const int MAX_HEALTH = 100;
	public int Health = MAX_HEALTH;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		this.Value = Health;
		if (this.Health <= 0) {
			//GAME OVER
			EmitSignal("death");
		}

	}

	public void damageObj() {
		Health -= 20;
	}

	public void damageObjDEATH() {
		Health -= 100;
	}
}
