using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class Board : TileMap
{
	public const int TEAM_SIZE = 4;

	private Dictionary<Teams, IList<Pawn>> _teams = new Dictionary<Teams, IList<Pawn>>();

	private Dictionary<Teams, TeamTileMapInfo> _teamStartingPositions = new()
	{
		
	};
	private static Vector2I _pawnPositionOffset = new Vector2I(0, 16);

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_teamStartingPositions = new()
		{
			[Teams.Yellow] = new()
			{
				Team = Teams.Yellow,
				StartingLocations = new Vector2I[] { new(7, 26), new(8, 24), new(8, 28), new(9, 26) }.Select(MapToLocal).ToArray(),
				TrackInfo = new Vector2I[]
				{
					new(5, 24), new(5, 23), new(6, 22), new(6, 21), new(7, 20), new(7, 19), new(8, 18), new(8, 19), new(9, 20), new(9, 21), new(10, 22), new(10, 23), new(11, 24), new(11, 23),
					new(12, 22), new(11, 21), new(11, 20), new(10, 19), new(10, 18), new(9, 17), new(9, 16), new(9, 15), new(10, 14), new(10, 13), new(11, 12), new(11, 11), new(12, 10), new(11, 9),
					new(11, 8), new(10, 9), new(10, 10), new(9, 11), new(9, 12), new(8, 13), new(8, 14), new(7, 13), new(7, 12), new(6, 11), new(6, 10), new(5, 9), new(5, 8), new(4, 9),
					new(4, 10), new(4, 11), new(5, 12), new(5, 13), new(6, 14), new(6, 15), new(7, 16), new(6, 17), new(6, 18), new(5, 19), new(5, 20), new(4, 21), new(4, 22), new(4, 23),
					new(5, 22), new(5, 21), new(6, 20), new(6, 19), new(7, 18), new(7, 17),
				}.Select(MapToLocal).ToArray(),
			},
			[Teams.Red] = new()
			{
				Team = Teams.Red,
				StartingLocations = new Vector2I[] { new(3, 14), new(4, 16), new(2, 16), new(3, 18) }.Select(MapToLocal).ToArray(),
				TrackInfo = new Vector2I[]
				{
					new(4, 10), new(4, 11), new(5, 12), new(5, 13), new(6, 14), new(6, 15), new(7, 16), new(6, 17), new(6, 18), new(5, 19), new(5, 20), new(4, 21), new(4, 22), new(4, 23),
					new(5, 24), new(5, 23), new(6, 22), new(6, 21), new(7, 20), new(7, 19), new(8, 18), new(8, 19), new(9, 20), new(9, 21), new(10, 22), new(10, 23), new(11, 24), new(11, 23),
					new(12, 22), new(11, 21), new(11, 20), new(10, 19), new(10, 18), new(9, 17), new(9, 16), new(9, 15), new(10, 14), new(10, 13), new(11, 12), new(11, 11), new(12, 10), new(11, 9),
					new(11, 8), new(10, 9), new(10, 10), new(9, 11), new(9, 12), new(8, 13), new(8, 14), new(7, 13), new(7, 12), new(6, 11), new(6, 10), new(5, 9), new(5, 8), new(4, 9),
					new(5, 10), new(5, 11), new(6, 12), new(6, 13), new(7, 14), new(7, 15),
				}.Select(MapToLocal).ToArray(),
			},
			[Teams.Green] = new()
			{
				Team = Teams.Green,
				StartingLocations = new Vector2I[] { new(13, 18), new(12, 16), new(14, 16), new(13, 14) }.Select(MapToLocal).ToArray(),
				TrackInfo = new Vector2I[]
				{
					new(12, 22), new(11, 21), new(11, 20), new(10, 19), new(10, 18), new(9, 17), new(9, 16), new(9, 15), new(10, 14), new(10, 13), new(11, 12), new(11, 11), new(12, 10), new(11, 9),
					new(11, 8), new(10, 9), new(10, 10), new(9, 11), new(9, 12), new(8, 13), new(8, 14), new(7, 13), new(7, 12), new(6, 11), new(6, 10), new(5, 9), new(5, 8), new(4, 9),
					new(4, 10), new(4, 11), new(5, 12), new(5, 13), new(6, 14), new(6, 15), new(7, 16), new(6, 17), new(6, 18), new(5, 19), new(5, 20), new(4, 21), new(4, 22), new(4, 23),
					new(5, 24), new(5, 23), new(6, 22), new(6, 21), new(7, 20), new(7, 19), new(8, 18), new(8, 19), new(9, 20), new(9, 21), new(10, 22), new(10, 23), new(11, 24), new(11, 23),
					new(11, 22), new(10, 21), new(10, 20), new(9, 19), new(9, 18), new(8, 17),
				}.Select(MapToLocal).ToArray(),
			},
			[Teams.Blue] = new()
			{
				Team = Teams.Blue,
				StartingLocations = new Vector2I[] { new(9, 6), new(8, 8), new(8, 4), new(7, 6) }.Select(MapToLocal).ToArray(),
				TrackInfo = new Vector2I[]
				{
					new(11, 8), new(10, 9), new(10, 10), new(9, 11), new(9, 12), new(8, 13), new(8, 14), new(7, 13), new(7, 12), new(6, 11), new(6, 10), new(5, 9), new(5, 8), new(4, 9),
					new(4, 10), new(4, 11), new(5, 12), new(5, 13), new(6, 14), new(6, 15), new(7, 16), new(6, 17), new(6, 18), new(5, 19), new(5, 20), new(4, 21), new(4, 22), new(4, 23),
					new(5, 24), new(5, 23), new(6, 22), new(6, 21), new(7, 20), new(7, 19), new(8, 18), new(8, 19), new(9, 20), new(9, 21), new(10, 22), new(10, 23), new(11, 24), new(11, 23),
					new(12, 22), new(11, 21), new(11, 20), new(10, 19), new(10, 18), new(9, 17), new(9, 16), new(9, 15), new(10, 14), new(10, 13), new(11, 12), new(11, 11), new(12, 10), new(11, 9),
					new(11, 10), new(10, 11), new(10, 12), new(9, 13), new(9, 14), new(8, 15),
				}.Select(MapToLocal).ToArray(),
			},
		};
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void AddTeam(Teams teamColor, IList<Pawn> teamsters)
	{
		if (_teams.ContainsKey(teamColor))
		{
			throw new ArgumentException("Team already added!", nameof(teamColor));
		}

		if (teamsters.Count != TEAM_SIZE)
		{
			throw new ArgumentException($"Team size must be {TEAM_SIZE}!", nameof(teamsters));
		}

		var startingPositions = teamColor switch 
		{
			Teams.Yellow => _teamStartingPositions[Teams.Yellow].StartingLocations,
			Teams.Red => _teamStartingPositions[Teams.Red].StartingLocations,
			Teams.Green => _teamStartingPositions[Teams.Green].StartingLocations,
			Teams.Blue => _teamStartingPositions[Teams.Blue].StartingLocations,
			_ => throw new ArgumentException("Cannot not add unknown team!", nameof(teamColor)),
		};

		if (_teams.TryGetValue(teamColor, out var team) == false)
		{
			team = new List<Pawn>();
			_teams[teamColor] = team;
		}

		foreach (var teamsterAndStartingPosition in teamsters.Zip(startingPositions))
		{
			var teamster = teamsterAndStartingPosition.First;
			var startingPosition = teamsterAndStartingPosition.Second;
			teamster.GlobalPosition = startingPosition - _pawnPositionOffset;
			teamster.ZAsRelative = true;
			teamster.ZIndex = 1;
			team.Add(teamster);
			AddChild(teamster);
		}
	}

    internal bool TryMovePawn(Teams teamColor, int pawnIndex, int dieOne, int dieTwo)
    {
		if (_teams.TryGetValue(teamColor, out var team) == false)
		{
			// throw new InvalidOperationException("Selected team unavailable!");
			return false;
		}

		if (pawnIndex < 0 || pawnIndex >= team.Count)
		{
			// throw new ArgumentOutOfRangeException("Selected pawn unavailable!");
			return false;
		}

		var pawn = team[pawnIndex];

		var teamBoardInfo = _teamStartingPositions[teamColor];
		var trackInfo = teamBoardInfo.TrackInfo;
		int diceRoll = CalculateDiceRoll(dieOne, dieTwo);
		if (IsOnTrack(pawn))
		{
			if (IsInStable(pawn, trackInfo))
			{
				if (RollIsDoubles(dieOne, dieTwo))
				{
					diceRoll = 1;
				}
				else
				{
	#if DEBUG
					GD.PrintErr("Must roll doubles to advance in stables");
	#endif
					return false;
				}
			}
			// pawn can move with any roll, but once the pawn is in the safe zone can only advance one space at a time by rolling doubles
			if ((pawn.TrackPosition + diceRoll) > (trackInfo.Length - 1)) // ensure the pawn doesn't move off the track
			{
#if DEBUG
				GD.PrintErr("Track not long enough");
#endif
				return false;
			}
			// check no pawn is in the way, except final space, that will be checked in a moment
			Vector2 trackTarget;
			for (int i = 1; i < diceRoll; i++)
			{
				trackTarget = trackInfo[pawn.TrackPosition + i];
				if (IsTargetOccupied(trackTarget) is not null)
				{
#if DEBUG
					GD.PrintErr("Another pawn is blocking the path");
#endif
					return false;
				}
			}
			trackTarget = trackInfo[pawn.TrackPosition + diceRoll];
			if (IsTargetOccupied(trackTarget) is Pawn otherPawn)
			{
				if (otherPawn.Team == pawn.Team)
				{
#if DEBUG
					GD.PrintErr("Two friendly pawns cannot occupy the same space");
#endif
					return false;
				}
				MoveEnemyPawnHome(otherPawn);
			}
			UpdatePawnPosition(pawn, pawn.TrackPosition + diceRoll, trackTarget);
			return true;
		}
		else if (RollIsDoubles(dieOne, dieTwo) || RollIsSpecialCombo(dieOne, dieTwo))
		{
			var trackTarget = trackInfo[0];
			if (IsTargetOccupied(trackTarget) is null)
			{
				UpdatePawnPosition(pawn, 0, trackTarget);
				return true;
			}
#if DEBUG
			else
			{
				GD.PrintErr("Target is occupied");
			}
#endif
		}
#if DEBUG
		else
		{
			GD.PrintErr("Doubles or special combo required to move onto track");
		}
#endif
		return false;
    }

    private int CalculateDiceRoll(int dieOne, int dieTwo)
    {
		if (RollIsDoubles(dieOne, dieTwo) && dieOne != 1 && dieOne != 6)
		{
			return dieOne;
		}
		return dieOne + dieTwo;
    }


    private bool IsInStable(Pawn pawn, Vector2[] trackInfo)
    {
		var stableLength = 6;
		return pawn.TrackPosition > (trackInfo.Length - stableLength - 1);
    }


    private void MoveEnemyPawnHome(Pawn pawn)
    {
		var startingPositions = _teamStartingPositions[pawn.Team].StartingLocations;
		var team = _teams[pawn.Team];
		foreach (var location in startingPositions)
		{
			if (team.All(p => p.GlobalPosition != location))
			{
				UpdatePawnPosition(pawn, -1, location);
				return;
			}
		}
		throw new InvalidOperationException("Could not find free home space for enemy pawn!");
    }


    private Pawn IsTargetOccupied(Vector2 trackTarget)
    {
		foreach (var teamKvp in _teams)
		{
			var teamColor = teamKvp.Key;
			var team = teamKvp.Value;
			var teamTrackInfo = _teamStartingPositions[teamColor].TrackInfo;
			foreach (var pawn in team)
			{
				if (pawn.TrackPosition > -1 && teamTrackInfo[pawn.TrackPosition].Equals(trackTarget))
				{
					return pawn;
				}
			}
		}
		return null;
    }

	private void UpdatePawnPosition(Pawn pawn, int index, Vector2 newPosition)
	{
		GD.Print($"{pawn.TrackPosition}:{LocalToMap(pawn.GlobalPosition)} -> {index}:{LocalToMap(newPosition)}");
		pawn.TrackPosition = index;
		pawn.GlobalPosition = newPosition;
	}

    private bool IsOnTrack(Pawn pawn) => pawn.TrackPosition > -1;


    private bool RollIsSpecialCombo(int dieOne, int dieTwo) => (dieOne == 1 && dieTwo == 6) || (dieOne == 6 && dieTwo == 1);


    private bool RollIsDoubles(int dieOne, int dieTwo) => dieOne == dieTwo;

}
