using Godot;
using System;

public partial class Die : Node2D
{
	public int? SelectedFace { get; set; }

	private AnimatedSprite2D _rollingAnimation;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_rollingAnimation = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		
		// SelectedFace = 5;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (SelectedFace < 1 || SelectedFace > 6)
		{
			SelectedFace = null;
		}

		if (SelectedFace is null)
		{
			_rollingAnimation.Play();
		}
		else
		{
			_rollingAnimation.Stop();
			_rollingAnimation.SetFrameAndProgress(SelectedFace.Value - 1, 0.0f);
		}
	}
}
