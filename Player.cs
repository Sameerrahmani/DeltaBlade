using Godot;
using System;

public class Player : KinematicBody2D
{

	// Variables
	public bool Attackable = true;
	public int MaxHealth = 100;
	public TextureProgress EnemyHp;
	public Area2D Rotating1;
	public Area2D Rotating2;
	public Area2D Rotating3;
	public Area2D Rotating4;
	public AnimatedSprite animatedSprite;
	public CollisionShape2D PlayerCollision;
	public Vector2 velocity = new Vector2();
	public TextureProgress Health;
	public Timer InvTimer;
	public Panel UpgradePanel;
	public CollisionShape2D PlayerCollision2;
	public AudioStreamPlayer2D HitSound;
	

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Rotating1 = GetNode<Area2D>("/root/Node2D/Player/RotatingPart1");
		Rotating2 = GetNode<Area2D>("/root/Node2D/Player/RotatingPart2");
		Rotating3 = GetNode<Area2D>("/root/Node2D/Player/RotatingPart3");
		Rotating4 = GetNode<Area2D>("/root/Node2D/Player/RotatingPart4");
		animatedSprite = GetNode<AnimatedSprite>("/root/Node2D/Player/AnimatedSprite");
		PlayerCollision = GetNode<CollisionShape2D>("/root/Node2D/Player/Area2D/PlayerCollider");
		Health = GetNode<TextureProgress>("Health");
		InvTimer = GetNode<Timer>("/root/Node2D/InvTimer");
		UpgradePanel = GetNode<Panel>("/root/Node2D/CanvasLayer/Panel");
		PlayerCollision2 = GetNode<CollisionShape2D>("PlayerCollider2");
		HitSound = GetNode<AudioStreamPlayer2D>("/root/Node2D/HitSound");
		
		Health.MaxValue = MaxHealth;
		
	}
	
	// When a body enters rotating part #1:
	private void _on_RotatingPart1_body_entered(KinematicBody2D body)
	{
		if(Rotating1.Visible == true && body.IsInGroup("Enemies"))
		{
			GD.Print("Hit");
			body.GetNode<TextureProgress>("HpBar").Value -= Global.SwordDamage;
			HitSound.Play();
			
		}
	}
	
	// When a body enters rotating part #2:
	private void _on_RotatingPart2_body_entered(KinematicBody2D body)
	{
		if (Rotating2.Visible == true && body.IsInGroup("Enemies"))
		{
			body.GetNode<TextureProgress>("HpBar").Value -= Global.SwordDamage;
		}
	}
		
	// When a body enters rotating part #3:
	private void _on_RotatingPart3_body_entered(KinematicBody2D body)
	{
		if (Rotating3.Visible == true && body.IsInGroup("Enemies"))
		{
			body.GetNode<TextureProgress>("HpBar").Value -= Global.SwordDamage;
		}
	}
	
	// WHen a body enters rotating part #4:
	private void _on_RotatingPart4_body_entered(KinematicBody2D body)
	{
		if (Rotating4.Visible == true && body.IsInGroup("Enemies"))
		{
			body.GetNode<TextureProgress>("HpBar").Value -= Global.SwordDamage;
		}
	}
	
	// Gets the input of the user. ex, when user presses right button, velocity increases and flip the character
	public void GetInput()
	{
		velocity = new Vector2();

		if (Input.IsActionPressed("right"))
		{
			velocity.x += 1;
			animatedSprite.FlipH = false;
			PlayerCollision.RotationDegrees = -90;
			PlayerCollision2.RotationDegrees = -90;
		}

		if (Input.IsActionPressed("left"))
		{
			velocity.x -= 1;
			animatedSprite.FlipH = true;
			PlayerCollision.RotationDegrees = 90;
			PlayerCollision2.RotationDegrees = 90;
		}
			

		if (Input.IsActionPressed("down"))
		{
			velocity.y += 1;
		}

		if (Input.IsActionPressed("up"))
		{
			velocity.y -= 1;
		}
		

		velocity = velocity.Normalized() * Global.speed;
	}

	// runs every frame
	public override void _PhysicsProcess(float delta)
	{
		GetInput();
		velocity = MoveAndSlide(velocity);	
		
		// rotates the moving parts, swords.
		Rotating1.Rotation += Global.RotationSpeed * delta;
		Rotating2.Rotation += Global.RotationSpeed * delta;
		Rotating3.Rotation += Global.RotationSpeed * delta;
		Rotating4.Rotation += Global.RotationSpeed * delta;
	}
	
	// When a body (enemy) enters the player, take damage
	private void _on_Area2D_body_entered(KinematicBody2D body)
	{
		if (body.IsInGroup("Enemies") && Attackable == true)
		{
			GD.Print(body + " has damaged the character");
			Health.Value -= 20;
			Attackable = false;
			InvTimer.Start();
			animatedSprite.Modulate = new Color("80ffffff");
		}
	}	
	
	// Invincibility timer, player goes back to normal transparency when timer runs out
	private void _on_InvTimer_timeout()
	{
		Attackable = true;
		animatedSprite.Modulate = new Color("ffffff");
	}
}


















