using Godot;
using System;
using System.Collections.Generic;
public partial class Lemur : StaticBody2D
{
	//private PriorityQueue<Enemy, > enemyQueue = new PriorityQueue<Enemy>();
	private Dictionary<Guid, Enemy> enemyPathFollowMap = new Dictionary<Guid, Enemy>();

	private void OnLemurRangeAreaEntered(Area2D enemy)
	{
		Enemy parent = (Enemy)enemy.GetParent();
		GD.Print("Lemur sees enemy " + enemy.Name);
		GD.Print("Enemy parent is " + parent.Name);
		this.enemyPathFollowMap.Add(parent.EnemyID, parent);

		// enemyQueue.Enqueue(parent);
	}

	private void OnLemurRangeAreaExited(Area2D enemy)
	{
		Enemy parent = (Enemy)enemy.GetParent();
		GD.Print("Lemur lost sight of enemy " + enemy.Name);
		GD.Print("Enemy parent is " + parent.Name);
		this.enemyPathFollowMap.Remove(parent.EnemyID);
		// enemyQueue.Dequeue();
	}

	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
		// if (enemyQueue.Count > 0)
		// {
		// 	LookAt(enemyQueue.Peek().GlobalPosition);
		// }
		if (enemyPathFollowMap.Count > 0)
		{
			// var enemyDetails = new List<string>();
			float max_progress = 0;
			Enemy maxDistanceEnemy = null;

			foreach (var enemy in enemyPathFollowMap.Values)
			{
				// enemyDetails.Add(enemy.ToString());
				if (enemy.getProgress() > max_progress)
				{
					max_progress = enemy.getProgress();
					maxDistanceEnemy = enemy;
				}
			}

			// Join the details into a single string and print it
			// GD.Print("Enemies: [" + String.Join(", ", enemyDetails) + "]");
			// Enemy maxDistanceEnemy = enemyPathFollowMap.Values
			// .OrderByDescending(enemy => enemy.progress)
			// .FirstOrDefault();
			LookAt(maxDistanceEnemy.GlobalPosition);
		}
	}
}
