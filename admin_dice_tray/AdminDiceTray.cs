using Godot;
using System;

public partial class AdminDiceTray : PanelContainer
{
	private OptionButton _teamSelector;
	private OptionButton _pawnSelector;
	private SpinBox _dieOneRoller;
	private SpinBox _dieTwoRoller;
	private RandomNumberGenerator _dieSimulator = new();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_teamSelector = GetNode<OptionButton>("VBoxContainer/TeamControls/TeamPicker");
		_pawnSelector = GetNode<OptionButton>("VBoxContainer/TeamControls/PawnPicker");
		_dieOneRoller = GetNode<SpinBox>("VBoxContainer/OtherControls/DieOne");
		_dieOneRoller.ValueChanged += GetOnDieSpinBoxValueChanged(_dieOneRoller);
		_dieTwoRoller = GetNode<SpinBox>("VBoxContainer/OtherControls/DieTwo");
		_dieTwoRoller.ValueChanged += GetOnDieSpinBoxValueChanged(_dieTwoRoller);
		_dieSimulator.Randomize();
	}

    private Godot.Range.ValueChangedEventHandler GetOnDieSpinBoxValueChanged(SpinBox dieSpinBox)
    {
        return (double newValue) =>
		{
			var suffix = "pips";
			if (newValue >= 1.0 && newValue < 2.0)
			{
				suffix = "pip";
			}
			dieSpinBox.Suffix = suffix;
		};
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

	private void OnDiceCasterPressed()
	{
		_dieOneRoller.Value = _dieSimulator.RandiRange(1, 6);
		_dieTwoRoller.Value = _dieSimulator.RandiRange(1, 6);
	}

}
