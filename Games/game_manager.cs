using Godot;
using System;
using System.Dynamic;

public partial class game_manager : Node
{

	[Signal]
	public delegate void toggle_game_pausedEventHandler(bool game_paused);

	[Signal]
	public delegate void restartEventHandler();

	private bool game_paused = false;

	public pause_menu pause;

    public bool GamePaused {
        get { return game_paused; }
        set
        {
			game_paused = value;
			GetTree().Paused = !game_paused;
			EmitSignal("toggle_game_paused", game_paused);
        }
    }


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		pause = GetNode<pause_menu>("CanvasLayer/Pause menu");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("ui_cancel")) {
			GamePaused = !GamePaused;
		}
		if (Input.IsActionJustPressed("right")){
			if (pause.IsDead){
				GetNode<Node>("Level_0").GetTree().ReloadCurrentScene();
			}
			EmitSignal("restart");
		}
	}
}
