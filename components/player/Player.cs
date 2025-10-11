using Godot;
using JamTemplate;
using Microsoft.Extensions.DependencyInjection;
using System;

public partial class Player : CharacterBody2D
{
	private StatsManager _statsManager;

    public override void _EnterTree()
    {
		_statsManager = Globals.ServiceProvider.GetRequiredService<StatsManager>();
    }

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)delta;
		}

		// Handle Jump.
		if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
		{
			velocity.Y = _statsManager.JumpVelocity;
		}

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * _statsManager.Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, _statsManager.Speed);
		}

		Velocity = velocity;
		MoveAndSlide();
	}
}
