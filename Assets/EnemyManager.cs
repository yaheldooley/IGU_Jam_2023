using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager
{
	private static List<Enemy> EnemiesHostile = new List<Enemy>();
	public static void AddToHostileList(Enemy enemy)
	{
		if (!EnemiesHostile.Contains(enemy)) EnemiesHostile.Add(enemy);
		if (MusicPlayer.Instance) MusicPlayer.Instance.SetThreatLevel(1);
	}

	public static void RemoveFromHostileList(Enemy enemy)
	{
		if (EnemiesHostile.Contains(enemy)) EnemiesHostile.Remove(enemy);
		if (EnemiesHostile.Count < 1 && MusicPlayer.Instance) MusicPlayer.Instance.SetThreatLevel(0);
	}

	public static void Update()
	{

	}
	
}
