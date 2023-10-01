using Godot;
using System;

public partial class DiceTray : PanelContainer
{
	private OptionButton _teamSelector;
	private OptionButton _pawnSelector;
	private SpinBox _dieOneRoller;
	private SpinBox _dieTwoRoller;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_teamSelector = GetNode<OptionButton>("VBoxContainer/TeamControls/TeamPicker");
		_pawnSelector = GetNode<OptionButton>("VBoxContainer/TeamControls/PawnPicker");
		_dieOneRoller = GetNode<SpinBox>("VBoxContainer/OtherControls/DieOne");
		_dieTwoRoller = GetNode<SpinBox>("VBoxContainer/OtherControls/DieTwo");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	[Signal]
	public delegate void MovePawnEventHandler(string teamColor, int selectedPawn, int dieOne, int dieTwo);

	private void OnActionatorButtonPressed()
	{
		Teams selectedTeam = GetSelectedTeam();
		int selectedPawn = GetSelectedPawn();
		(int dieOne, int dieTwo) = GetDiceRoll();
		EmitSignal(SignalName.MovePawn, selectedTeam.ToString(), selectedPawn, dieOne, dieTwo);
	}

    private (int dieOne, int dieTwo) GetDiceRoll()
    {
		var dieOne = (int)_dieOneRoller.Value;
		var dieTwo = (int)_dieTwoRoller.Value;
		return (dieOne, dieTwo);
    }


    private int GetSelectedPawn()
    {
		var pawn = _pawnSelector.Selected;
		return pawn;
    }


    private Teams GetSelectedTeam()
    {
		var team = (Teams)_teamSelector.Selected;
		return team;
    }

}
