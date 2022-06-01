using Godot;
using System;

public class UpgradePanel : Panel
{
	// Variables
	public Button UpgradeNumberSwords;
	public Button UpgradeSpeedSwords;
	public Button UpgradeDamageSwords;
	public Panel MainPanel;
	public Area2D Rotating1;
	public Area2D Rotating2;
	public Area2D Rotating3;
	public Area2D Rotating4;

	// Executed when the node enters the tree for the first time
	public override void _Ready()
	{
		UpgradeNumberSwords = GetNode<Button>("Panel1/Button");
		UpgradeSpeedSwords = GetNode<Button>("Panel2/Button2");
		UpgradeDamageSwords = GetNode<Button>("Panel3/Button3");
		MainPanel = GetNode<Panel>("/root/Node2D/CanvasLayer/Panel");
		Rotating1 = GetNode<Area2D>("/root/Node2D/Player/RotatingPart1");
		Rotating2 = GetNode<Area2D>("/root/Node2D/Player/RotatingPart2");
		Rotating3 = GetNode<Area2D>("/root/Node2D/Player/RotatingPart3");
		Rotating4 = GetNode<Area2D>("/root/Node2D/Player/RotatingPart4");
	}

 	private void _on_Button1_pressed() // Increases the amount of swords that you possess.
 	{
		// Checks if you have already have swords
		MainPanel.Visible = false;
		if (Rotating1.Visible == true && Rotating2.Visible == false && Rotating3.Visible == false && Rotating4.Visible == false)
		{
			Rotating2.Visible = true;
			UpgradeNumberSwords.Text = "UPGRADE - 2/4";
			GetTree().Paused = false;
		}
		
		else if (Rotating1.Visible == true && Rotating2.Visible == true && Rotating3.Visible == false && Rotating4.Visible == false)
		{
			Rotating3.Visible = true;
			UpgradeNumberSwords.Text = "UPGRADE - 3/4";
			GetTree().Paused = false;
		}
		
		else if (Rotating1.Visible == true && Rotating2.Visible == true && Rotating3.Visible == true && Rotating4.Visible == false)
		{
			Rotating4.Visible = true;
			UpgradeNumberSwords.Text = "MAX";
			GetTree().Paused = false;
			UpgradeNumberSwords.Disabled = true;
		}	
 	}
	
	private void _on_Button2_pressed() // Increases the rotation speed of the swords.
	{
		MainPanel.Visible = false;
	
		// Increases the rotation speed based on what upgrade you have already
		if (Global.RotationSpeed == 2)
		{
			UpgradeSpeedSwords.Text = "UPGRADE - 2/4";
			Global.RotationSpeed += 1;
			GetTree().Paused = false;
		}
		
		else if (Global.RotationSpeed == 3)
		{
			UpgradeSpeedSwords.Text = "UPGRADE - 3/4";
			Global.RotationSpeed += 1;
			GetTree().Paused = false;
		}
		
		else if (Global.RotationSpeed == 4)
		{
			UpgradeSpeedSwords.Text = "MAX";
			Global.RotationSpeed += 1;
			GetTree().Paused = false;
			UpgradeSpeedSwords.Disabled = true;
		}
	}
	
	// Increases the damage your swords deal to enemies
	private void _on_Button3_pressed()
	{
		MainPanel.Visible = false;
		
		// Increases the damage based on how much damage it already has
		if (Global.SwordDamage == 25)
		{
			UpgradeDamageSwords.Text = "UPGRADE - 2/4";
			Global.SwordDamage += 20;
			GetTree().Paused = false;
		}
		
		else if (Global.SwordDamage == 45)
		{
			UpgradeDamageSwords.Text = "UPGRADE - 3/4";
			Global.SwordDamage += 20;
			GetTree().Paused = false;
		}
		
		else if (Global.SwordDamage == 65)
		{
			UpgradeDamageSwords.Text = "MAX";
			Global.SwordDamage += 30;
			GetTree().Paused = false;
			UpgradeDamageSwords.Disabled = true;
			
		}
		
	}
}










