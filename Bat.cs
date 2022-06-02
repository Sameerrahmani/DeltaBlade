using Godot;
using System;

public class Bat : KinematicBody2D
{
	// Variables
	public int runSpeed = 75;
	public int maxHealth = 50;
	public int damage = 10;
	public Vector2 velocity = new Vector2();
	public int PointsGiven = 5;
	public Label ScoreLabel;

	
	public KinematicBody2D Player;
	public TextureProgress HpBar;
	public AnimatedSprite animatedBat;
	public CollisionShape2D BatCollision;

	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Player = GetNode<KinematicBody2D>("/root/Node2D/Player");
		HpBar = GetNode<TextureProgress>("HpBar");
		animatedBat = GetNode<AnimatedSprite>("AnimatedSprite");
		BatCollision = GetNode<CollisionShape2D>("CollisionShape2D");
		ScoreLabel = GetNode<Label>("/root/Node2D/CanvasLayer/ScoreLabel");

		HpBar.MaxValue = maxHealth;
	}

	// Runs the code inside every frame
	public override void _PhysicsProcess(float delta)
	{
		
	// Gets the path to the player by subtracting the position of the player by itself and then moving towards it
	velocity = (Player.Position - Position).Normalized() * runSpeed;
	velocity = MoveAndSlide(velocity);
	
		if (Player.Position < Position)
		{
			animatedBat.FlipH = true;
			BatCollision.RotationDegrees = 90;
		}
	
		else
		{
			BatCollision.RotationDegrees = -90;
			animatedBat.FlipH = false;
		}
	}
	

	// When the bat loses value from it's health
	public void _on_HpBar_value_changed(float Health)
	{
		GD.Print("bat lost 25hp");
		
		// When the bat has no more hp, then add to the score and play the death sound.
		if(Health == 0)
		{
			Global.Score += PointsGiven;
			if (Global.Score > Global.PointsTillLvlUp)
			{
				Global.Score = Global.PointsTillLvlUp;
			}
			GD.Print(Global.Score);
			DeathSound.Play();
			QueueFree();
			
		}
	}

}





