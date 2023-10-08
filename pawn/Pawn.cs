using Godot;
using System;

public partial class Pawn : Area2D
{
	[Export]
	public Teams Team { get; set; }
	
	public int TrackPosition { get; set; } = -1;

	private AnimatedSprite2D _skin;
	private string _idleAnimationName = "yellow_idle";
	private int _idleAnimationCountdown = 0;

	private Timer _skinAnimationTimer;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_idleAnimationName = Team switch
		{
			Teams.Yellow => @"yellow_idle",
			Teams.Red => @"red_idle",
			Teams.Green => @"green_idle",
			Teams.Blue => @"blue_idle",
			_ => @"yellow_idle",
		};

		_skin = GetNode<AnimatedSprite2D>("Skin");
		_skin.Animation = _idleAnimationName;

		_skinAnimationTimer = GetNode<Timer>("AnimationIdler");
		_skinAnimationTimer.WaitTime = GD.RandRange(0.5, 3.5);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (_skinAnimationTimer.IsStopped())
		{
			_skinAnimationTimer.Start();
		}
	}

	private void OnAnimationIdlerTimeout()
	{
		_skinAnimationTimer.WaitTime = GD.RandRange(4.5, 10.5);
		_idleAnimationCountdown = GD.RandRange(1, 3);
		_skin.Play();
	}

	private void OnSkinAnimationLooped()
	{
		if ((--_idleAnimationCountdown) < 1)
		{
			_skin.Stop();
			_idleAnimationCountdown = 0;
		}
	}
}
