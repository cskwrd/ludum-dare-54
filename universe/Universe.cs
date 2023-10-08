using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class Universe : Node2D
{
	[Export]
	private PackedScene _pawnTemplate;

	private Board _board;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// This sets the min window size (as you would expect).
		// However just setting this will cause the window to load
		// at the size set in Project Settings > Display > Window > Size > Viewport Width/Height.
		// When this happens the screen will no longer be centered.
		// To fix it, I toggled advanced settings to ON.
		// Then set Project Settings > Display > Window > Size > Window Width/Height Override
		// to the desired min size.
		// That causes the window to load at the min size (and centered) even if the viewport size is smaller.
		DisplayServer.WindowSetMinSize(new Vector2I(1280, 720));

		_board = GetNode<Board>("Board");

		NewGame();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void NewGame()
	{
		var teamSize = Board.TEAM_SIZE;
		
		_board.AddTeam(Teams.Yellow, FormTeam(Teams.Yellow, teamSize).ToList());
		_board.AddTeam(Teams.Red, FormTeam(Teams.Red, teamSize).ToList());
		_board.AddTeam(Teams.Green, FormTeam(Teams.Green, teamSize).ToList());
		_board.AddTeam(Teams.Blue, FormTeam(Teams.Blue, teamSize).ToList());
	}

	private IEnumerable<Pawn> FormTeam(Teams teamColor, int memberCount)
	{
		for (int i = 0; i < memberCount; i++)
		{
			var pawn = _pawnTemplate.Instantiate<Pawn>();
			pawn.Team = teamColor;
			yield return pawn;
		}
	}

	private void OnMovePawnPressed(string selectedTeamColor, int selectedPawnIndex, int dieOne, int dieTwo)
	{
		var teamColor = Enum.Parse<Teams>(selectedTeamColor);

		_board.TryMovePawn(teamColor, selectedPawnIndex, dieOne, dieTwo);
	}

}
