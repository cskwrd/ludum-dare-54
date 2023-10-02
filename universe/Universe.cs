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
