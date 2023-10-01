using Godot;
using System;

public partial class Pawn : Area2D
{
	[Export]
	public Teams Team { get; set; }
	
	public int TrackPosition { get; set; } = -1;

    private Texture2D _yellow = ResourceLoader.Load("res://pawn/dino-yellow.png") as Texture2D;
	private Texture2D _red = ResourceLoader.Load("res://pawn/dino-red.png") as Texture2D;
	private Texture2D _green = ResourceLoader.Load("res://pawn/dino-green.png") as Texture2D;
	private Texture2D _blue = ResourceLoader.Load("res://pawn/dino-blue.png") as Texture2D;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var skinTexture = Team switch
		{
			Teams.Yellow => _yellow,
			Teams.Red => _red,
			Teams.Green => _green,
			Teams.Blue => _blue,
			_ => _yellow
		};

		var skin = GetNode<Sprite2D>("Skin");
		skin.Texture = skinTexture;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

}
