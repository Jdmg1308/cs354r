using Godot;
using System;

public partial class Player_controller : CharacterBody2D
{
	// Reference to the Timer node.
    private Timer dashTimer;
    // Variable to store the time when code was executed.
    private float dashCooldown = 0.0f;
	public float Speed = 300.0f;
	public const float JumpVelocity = -400.0f;
	public const float friction = .1f;
	public const float acceleration = .25f;
	private bool wallSliding = false;

	RayCast2D rray;
	RayCast2D lray;

	AnimatedSprite2D animation;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	public override void _Ready()
    {
		animation = GetNode("AnimatedSprite2D") as AnimatedSprite2D;
        dashTimer = GetNode<Timer>("dashTimer");
		lray = GetNode<RayCast2D>("raycastLeft");
		rray = GetNode<RayCast2D>("raycastRight");
	}
	public override void _PhysicsProcess(double delta) 
	{
		Vector2 velocity = Velocity;
		// Add the gravity.
		if (!IsOnFloor() && !wallSliding)
			velocity.Y += gravity * (float)delta;
		if (velocity.Y >= -100) {
			animation.Play("Fall");
		}

		if (!GetNode<pause_menu>("/root/Game/CanvasLayer/Pause menu").IsDead) {

			// JUMP
			if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
				velocity.Y = JumpVelocity;
			if (velocity.Y < 0) {
				animation.Play("Jump");
			}

			// RUN
			Vector2 direction = Input.GetVector("left", "right", "up", "down");
			if (direction != Vector2.Zero)
			{
				if ((Input.IsActionPressed("left")) || Input.IsActionPressed("right")){
					animation.Play("Run");
					animation.FlipH = direction == Vector2.Left ? true : false;
					velocity.X = direction.X * Speed;
				}
			} else if (!wallSliding) {
				if (IsOnFloor()) {
					animation.Play("Idle");
				}
				velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			}

			
			//SLIDE AND JUMP IN WALL
			if (lray.IsColliding() || rray.IsColliding()) {
				velocity.Y = gravity * (float)delta * 5f;
				if (Input.IsActionJustPressed("ui_accept")) {
					animation.Play("Jump");
					if (lray.IsColliding() && !Input.IsActionPressed("left")) {
						velocity.X = -JumpVelocity*2.6f;
						velocity.Y = JumpVelocity;
					}
					if (rray.IsColliding() && !Input.IsActionPressed("right")) {
						velocity.X = JumpVelocity*2.6f;
						velocity.Y = JumpVelocity;
					}
				}
				wallSliding = true;
			} else {
				wallSliding = false;
			}

			// DASH
			if (Input.IsActionJustPressed("dash") && dashCooldown ==  0.0f) {
				animation.Play("Attack_1");
				if (Input.IsAnythingPressed()) {
					Speed = 8000.0f;
					velocity.X = animation.FlipH ? -1*Speed : 1*Speed;
				}
				// Start the timer when your code is executed.
				dashCooldown = 0.8f;
				dashTimer.Start(dashCooldown);
			} else {
				Speed = 300.0f;
			}
		} else {
			// DEATH
			animation.Play("Death");
		}

		Velocity = velocity;
		MoveAndSlide();
	}

	public void _on_dash_timer_timeout() {
		dashCooldown = 0.0f;
	}
}
