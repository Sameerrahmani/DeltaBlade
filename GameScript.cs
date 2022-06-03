using Godot;
using System;
using System.Collections.Generic; 

public class GameScript : Node
{
	// Variables
	public int Score;
	public KinematicBody2D BatMob;
	public KinematicBody2D SkeleMob;
	public Label ScoreLabel;
	public Timer SpawnTimer;
	public Panel UpgradePanel;
	public RichTextLabel UpgradeLabel;
	public TextureProgress Health;
	public KinematicBody2D Player;
	public Label GameOverScreen;
	Random pos = new Random();
	
// -1776 - 2350
	
	public PackedScene Enemy1 = ResourceLoader.Load("res://Scenes/BatEnemy.tscn") as PackedScene;
	public PackedScene Enemy2 = ResourceLoader.Load("res://Scenes/SkeleEnemy.tscn") as PackedScene;	
	public PackedScene Enemy3 = ResourceLoader.Load("res://Scenes/SlimeEnemy.tscn") as PackedScene;

	// Called when the node enters the tree for the first time
	public override void _Ready()
	{
		ScoreLabel = GetNode<Label>("/root/Node2D/CanvasLayer/ScoreLabel");
		UpgradeLabel = GetNode<RichTextLabel>("/root/Node2D/CanvasLayer/UpgradeLabel");
		SpawnTimer = GetNode<Timer>("SpawnTimer");
		UpgradePanel = GetNode<Panel>("CanvasLayer/Panel");
		Health = GetNode<TextureProgress>("Player/Health");
		Player = GetNode<KinematicBody2D>("Player");
		GameOverScreen = GetNode<Label>("CanvasLayer/GameOverScreen");
	}
	
	// Runs the code inside every frame
	public override void _Process(float delta)
	{ 
		// Score labels
		ScoreLabel.Text = "Score: " + Global.Score.ToString();
		UpgradeLabel.Text = "Points until next level: " + (Global.PointsTillLvlUp - Global.Score);
		
		// if the score is greater or equal to points till level up, then level up
		if (Global.Score >= Global.PointsTillLvlUp)
		{
			UpgradePanel.Visible = true;
			Global.PointsTillLvlUp += (150 * Global.Level);
			GetTree().Paused = true;	
			Global.Level++;
		}
		
		if (Health.Value <= 0)
		{
			GameOver();
		}
	}
	
	// When the spawn timer runs out, spawn more enemies
	private void _on_SpawnTimer_timeout()
	{
		KinematicBody2D BatMob = (KinematicBody2D)Enemy1.Instance();
		KinematicBody2D BatMob1 = (KinematicBody2D)Enemy1.Instance();
		KinematicBody2D SkeleMob = (KinematicBody2D)Enemy2.Instance();
		KinematicBody2D SkeleMob1 = (KinematicBody2D)Enemy2.Instance();
		KinematicBody2D SkeleMob2 = (KinematicBody2D)Enemy2.Instance();
		KinematicBody2D SlimeMob = (KinematicBody2D)Enemy3.Instance();
		KinematicBody2D SlimeMob1 = (KinematicBody2D)Enemy3.Instance();
		
		
		if (Global.Level == 1)
		{
			
			AddChild(BatMob);
			BatMob.Position = new Vector2(pos.Next(-1776, 2350), 80);
			AddChild(BatMob1);
			BatMob1.Position = new Vector2(pos.Next(-1776, 2350), 1000);
		}
		
		else if (Global.Level == 2)
		{
			SpawnTimer.WaitTime = 5;
			AddChild(SlimeMob);
			SlimeMob.Position = new Vector2(pos.Next(-1776, 2350), 1000);
			AddChild(SlimeMob1);
			SlimeMob1.Position = new Vector2(pos.Next(-1776, 2350), 80);
			AddChild(BatMob);
			BatMob.Position = new Vector2(pos.Next(-1776, 2350), 1000);
		}
		
		else if (Global.Level == 3)
		{
			SpawnTimer.WaitTime = 6;
			AddChild(SkeleMob);
			SkeleMob.Position = new Vector2(pos.Next(-1776, 2350), 1000);
			AddChild(SkeleMob1);
			SkeleMob1.Position = new Vector2(pos.Next(-1776, 2350), 80);
			
		}
		
		else if (Global.Level == 4)
		{
			Global.speed = 275;
			SpawnTimer.WaitTime = 4;
			AddChild(SkeleMob);
			SkeleMob.Position = new Vector2(pos.Next(-1776, 2350), 1000);
			AddChild(SkeleMob1);
			SkeleMob1.Position = new Vector2(pos.Next(-1776, 2350), 80);
			AddChild(SkeleMob2);
			SkeleMob2.Position = new Vector2(pos.Next(-1777, 2350), 1000);
			
		}
		
	}
	
	// Every 5 seconds, if the player's hp is lower than 100, add 5 hp to Player's health
	private void _on_RegenTimer_timeout()
	{
		if (Health.Value < 100)
		{
			Health.Value += 5;
			GD.Print("Max HP: " + Health.MaxValue + "Current HP: "  + Health.Value);
		}
	}
	
	// code for when game is over
	public void GameOver()
	{
		
		GetTree().Paused = true;
		GameOverScreen.Visible = true;
		
	}
	
	private void _on_PlayAgain_pressed()
	{
		GameOverScreen.Visible = false;
		Global.Score = 0;
		GetTree().ReloadCurrentScene();
		GetTree().Paused = false;
	}
	
	private void _on_Exit_pressed()
	{
		GetTree().Paused = false;
		GameOverScreen.Visible = false;
		GetTree().ChangeScene("res://Scenes/MainMenu.tscn");
		
	}
}















