using Godot;
using System;

public partial class dangerous_obstacle : Area2D
{
    private ProgressBar HealthBar;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        AnimationPlayer ap = GetNode("AnimationPlayer") as AnimationPlayer;
        ap.Play("Idle");

        Node levelNode = GetTree().Root.GetNodeOrNull("Game/Level_0");
        if (levelNode != null)
        {
            HealthBar = levelNode.GetNodeOrNull<ProgressBar>("/root/Game/CanvasLayer/HealthBar");
            if (HealthBar == null)
            {
                GD.Print("HealthBar not found.");
            }
        }
        else
        {
            GD.Print("Level_0 not found.");
        }
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        if (HealthBar != null)
        {
            AnimationPlayer ap = GetNode("AnimationPlayer") as AnimationPlayer;
            CollisionShape2D dmg = this.GetNode("Damage") as CollisionShape2D;
            if (ap.CurrentAnimationPosition > 0.4 && ap.CurrentAnimationPosition < 1.5)
            {
                dmg.Disabled = false;
            }
            else
            {
                dmg.Disabled = true;
            }
        }
    }

    public void _on_body_entered(Node body)
    {
        CollisionShape2D dmg = this.GetNode("Damage") as CollisionShape2D;
        if (body is Player_controller playerController)
        {
            HealthBar.Call("damageObj");
            GD.Print("");
            // Handle player collision here.
        }
    }
}
