using Godot;
using System;


public partial class pause_menu : Control
{
	private HealthBar Health;
	private Player_controller player;

	private Next_0 WON;
	private game_manager Game;

	private bool isdead;

	public bool IsDead {
        get { return isdead; }
        set
        {
			isdead = value;
        }
    }
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Hide();

		Game = GetNode<game_manager>("/root/Game");
		var callable = new Callable(this, "_OnGamePaused");
		Game.Connect("toggle_game_paused", callable);

		var callable2 = new Callable(this, "_return_from_death");
		Game.Connect("restart", callable2);

		Health = GetNode<HealthBar>("/root/Game/CanvasLayer/HealthBar");
		var callable3 = new Callable(this, "_OnGameDeath");
		Health.Connect("death", callable3);

		WON = GetParent().GetParent().GetNode<Next_0>("Level_0/Next_0");
		var wincall = new Callable(this, "_OnGameWin");
		WON.Connect("win", wincall);

		player = GetNode<Player_controller>("/root/Game/Level_0/Player");
    }

	private void _OnGameDeath()
    {
		GetNode<Button>("Panel/VBoxContainer/Resume").Hide();
		GetNode<Button>("Panel/VBoxContainer/Quit").Hide();
		GetNode<Button>("Panel/VBoxContainer/Restart").Hide();
		GetNode<Label>("Panel/VBoxContainer/Label").Text = "DEATH";
		GetNode<Panel>("Panel").Modulate = new Color(0.788f, 0, 0.318f, 0.945f);
		GetNode<Panel>("Panel2").Modulate = new Color(0.788f, 0, 0.318f, 0.945f);
		Show();
		isdead = true;
    }

	private void _OnGameWin()
    {
		GetNode<Button>("Panel/VBoxContainer/Resume").Hide();
		GetNode<Button>("Panel/VBoxContainer/Quit").Hide();
		GetNode<Button>("Panel/VBoxContainer/Restart").Hide();
		GetNode<Label>("Panel/VBoxContainer/Label").Text = "YOU WON, CONGRATS";
		GetNode<Panel>("Panel").Modulate = new Color(0.11f, 1, 0.154f);
		GetNode<Panel>("Panel2").Modulate = new Color(0.11f, 1, 0.154f);
		Show();
		isdead = true;
    }

    // Define the method that will be called when the signal is received
    private void _OnGamePaused(bool game_paused)
    {
		if (game_paused) {
			Hide();
		} else  {
			Show();
		}
        // Do something in response to the signal
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void _on_resume_pressed()
	{
		Game.GamePaused = !Game.GamePaused;
	}
	public void _on_quit_pressed()
	{
		GetTree().Quit();
	}

	public void _on_restart_pressed()
	{
		GetTree().ReloadCurrentScene();
		Game.GamePaused = !Game.GamePaused;
	}

	public void _return_from_death() {
		isdead = false;
	}
}
