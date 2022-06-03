using Godot;
using System;

public class MainMenu : Control
{

	// When play button is pressed:
	private void _on_PlayButton_pressed()
	{
		// moves to the main scene (game)
		GetTree().ChangeScene("res://Scenes/MainScene.tscn");
		Global.Score = 0;
	}
	
	// When how to play is pressed:
	private void _on_InfoButton_pressed()
	{
		OS.ShellOpen("https://sameerr.itch.io/deltablade");
	}
	
	// When exit button is pressed:
	private void _on_ExitButton_pressed()
	{
		GetTree().Quit(); // Quits to the desktop
	}
}








