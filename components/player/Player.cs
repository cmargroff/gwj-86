using Godot;
using JamTemplate.Player.States;
using JamTemplate.Util.FSM;
using Microsoft.Extensions.DependencyInjection;

namespace JamTemplate.Components.Player;

public partial class Player : CharacterBody2D
{
	public float FrictionCoefficient { get; set; } = 0.15f; // TODO: move to stats
	public Vector2 MoveVelocity = Vector2.Zero;
	[Export]
	public AnimationPlayer AnimationPlayer;
	private StatsManager _statsManager;
	private AnimatedFiniteStateManager _fsm;
	private uint _jumpsLeft = 0;

	public override void _EnterTree()
	{
		_statsManager = Globals.ServiceProvider.GetRequiredService<StatsManager>();
		ResetJumps();
		BuildFSM();
	}
	public void BuildFSM()
	{
		_fsm = new();
		var states = new ServiceCollection();
		states.AddSingleton(this);
		states.AddSingleton(AnimationPlayer);
		states.AddSingleton(_statsManager);
		states.Scan(scan => scan
			.FromAssemblyOf<EntryState>()
			.AddClasses(classes =>
				classes
				.InNamespaceOf<EntryState>()
				.AssignableTo<AnimatedState>()
			)
			.As<AnimatedState>()
		);
		_fsm.AnimationPlayer = AnimationPlayer;
		_fsm.AddStates(states.BuildServiceProvider());
	}
	public override void _Ready()
	{
		_fsm.InitialStateName = "entry";
		_fsm.Start();
	}

	public Vector2 GetMoveVector()
	{
		return Input.GetVector("move_left", "move_right", "move_up", "move_down");
	}
	public void Move(double delta, Vector2 moveVector, float speed)
	{
		MoveVelocity.X = moveVector.X * speed;
	}

	public void ResetJumps()
	{
		_jumpsLeft = _statsManager.MaxJumps;
	}
	public void DecrementJumps()
	{
		_jumpsLeft--;
	}

	public bool CanJump()
	{
		return _jumpsLeft > 0;
	}
	public void ResetVelocity()
	{
		MoveVelocity = Vector2.Zero;
	}

	public void ApplyGravity(double delta)
	{
		var gravity = GetGravity();
		MoveVelocity.Y += gravity.Y * (float)delta;
	}
	public void ApplyFriction(double delta)
	{
		if (IsOnFloor())
		{
			MoveVelocity.X = MoveVelocity.X * (1f - FrictionCoefficient);
		}
	}

	public override void _Process(double delta)
	{
		_fsm.Process(delta);
	}

	public override void _PhysicsProcess(double delta)
	{
		_fsm.PhysicsProcess(delta);
		Velocity = MoveVelocity;
		ApplyFriction(delta);
		ApplyGravity(delta);
		MoveAndSlide();
	}
}
