using Godot;
using System;

public class Slime : KinematicBody2D
{
	// Variables
	public int runSpeed = 50;
	public int maxHealth = 120;
	public int damage = 20;
	public Vector2 velocity = new Vector2();
	public int PointsGiven = 15;
	public Label ScoreLabel;

	
	public KinematicBody2D Player;
	public TextureProgress HpBar;
	public AnimatedSprite animatedSlime;
	public CollisionShape2D SlimeCollision;

	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Player = GetNode<KinematicBody2D>("/root/Node2D/Player");
		HpBar = GetNode<TextureProgress>("HpBar");
		animatedSlime = GetNode<AnimatedSprite>("AnimatedSprite");
		SlimeCollision = GetNode<CollisionShape2D>("CollisionShape2D");
		ScoreLabel = GetNode<Label>("/root/Node2D/CanvasLayer/ScoreLabel");

		HpBar.MaxValue = maxHealth;
	}

	// Runs the code inside every frame
	public override void _PhysicsProcess(float delta)
	{
	velocity = (Player.Position - Position).Normalized() * runSpeed;
	velocity = MoveAndSlide(velocity);
	
		if (Player.Position < Position)
		{
			animatedSlime.FlipH = true;
			SlimeCollision.RotationDegrees = 90;
		}
	
		else
		{
			SlimeCollision.RotationDegrees = -90;
			animatedSlime.FlipH = false;
		}
	}
	
	// When the hp value is changed, execute this code
	private void _on_HpBar_value_changed(float Health)
	{
		GD.Print("slime hit");
		if (Health == 0)
		{
			Global.Score += PointsGiven;
			if (Global.Score > Global.PointsTillLvlUp)
			{
				Global.Score = Global.PointsTillLvlUp;
			}
			QueueFree();
		}
	}
	
}








