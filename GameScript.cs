using Godot;
using System;
using System.Collections.Generic; 

public class GameScript : Node
{
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
	
	public override void _Process(float delta)
	{ 
		ScoreLabel.Text = "Score: " + Global.Score.ToString();
		UpgradeLabel.Text = "Points until next level: " + (Global.PointsTillLvlUp - Global.Score);
		
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
	
	private void _on_SpawnTimer_timeout()
	{
		KinematicBody2D BatMob = (KinematicBody2D)Enemy1.Instance();
		KinematicBody2D BatMob1 = (KinematicBody2D)Enemy1.Instance();
		KinematicBody2D SkeleMob = (KinematicBody2D)Enemy2.Instance();
		KinematicBody2D SkeleMob1 = (KinematicBody2D)Enemy2.Instance();
		KinematicBody2D SlimeMob = (KinematicBody2D)Enemy3.Instance();
		KinematicBody2D SlimeMob1 = (KinematicBody2D)Enemy3.Instance();
		
		
		if (Global.Score <= 45)
		{
			
			AddChild(BatMob);
			BatMob.Position = new Vector2(pos.Next(-1776, 2350), 80);
			AddChild(BatMob1);
			BatMob1.Position = new Vector2(pos.Next(-1776, 2350), 1000);
		}
		
		else if (Global.Score >= 45 && Global.Score < 150)
		{
			SpawnTimer.WaitTime = 5;
			AddChild(SlimeMob);
			SlimeMob.Position = new Vector2(pos.Next(-1776, 2350), 1000);
			AddChild(SlimeMob1);
			SlimeMob1.Position = new Vector2(pos.Next(-1776, 2350), 80);
			AddChild(BatMob);
			BatMob.Position = new Vector2(pos.Next(-1776, 2350), 1000);
		}
		
		else if (Global.Score >= 150)
		{
			SpawnTimer.WaitTime = 6;
			AddChild(SkeleMob);
			SkeleMob.Position = new Vector2(pos.Next(-1776, 2350), 1000);
			AddChild(SkeleMob1);
			SkeleMob1.Position = new Vector2(pos.Next(-1776, 2350), 80);
			
		}
		
	}
	
	private void _on_RegenTimer_timeout()
	{
		if (Health.Value < 100)
		{
			Health.Value += 5;
			GD.Print("Max HP: " + Health.MaxValue + "Current HP: "  + Health.Value);
		}
	}
	
	public void GameOver()
	{
		
		GetTree().Paused = true;
		GameOverScreen.Visible = true;
		
	}
}









