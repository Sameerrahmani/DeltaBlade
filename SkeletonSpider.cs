using Godot;
using System;

public class SkeletonSpider : KinematicBody2D
{
	// Variables 
	public int runSpeed = 125;
	public int maxHealth = 100;
	public int damage = 25;
	public int PointsGiven = 25;
	public Vector2 velocity = new Vector2();
	public Vector2 Knockback = new Vector2();
	
	public KinematicBody2D Player;
	public TextureProgress HpBar;
	public AnimatedSprite animatedSkele;
	public CollisionShape2D SkeleCollision;
	

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Player = GetNode<KinematicBody2D>("/root/Node2D/Player");
		HpBar = GetNode<TextureProgress>("HpBar");
		animatedSkele = GetNode<AnimatedSprite>("AnimatedSprite");
		SkeleCollision = GetNode<CollisionShape2D>("CollisionShape2D");
		
		HpBar.MaxValue = maxHealth;
	}

	// Executes this code every frame 
	public override void _PhysicsProcess(float delta)
	{
		velocity = (Player.Position - Position).Normalized() * runSpeed;
		velocity = MoveAndSlide(velocity);
		
		if (Player.Position < Position)
		{
			animatedSkele.FlipH = true;
			SkeleCollision.RotationDegrees = 90;
		}
		else
		{
			animatedSkele.FlipH = false;
			SkeleCollision.RotationDegrees = -90;
		}
	}
		
	// Every time the hp value is changed, the code below is executed
	private void _on_HpBar_value_changed(float Health)
	{
		GD.Print("skeleton spider hit");
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



