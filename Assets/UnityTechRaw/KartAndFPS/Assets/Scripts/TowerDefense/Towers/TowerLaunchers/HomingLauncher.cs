﻿using UnityEngine;
using UnityTechRaw.KartAndFPS.Assets.Scripts.ActionGameFramework.Health;
using UnityTechRaw.KartAndFPS.Assets.Scripts.ActionGameFramework.Helpers;
using UnityTechRaw.KartAndFPS.Assets.Scripts.ActionGameFramework.Projectiles;

namespace UnityTechRaw.KartAndFPS.Assets.Scripts.TowerDefense.Towers.TowerLaunchers
{
	/// <summary>
	/// An implementation of ILauncher that firest homing missiles
	/// </summary>
	public class HomingLauncher : Launcher
	{
		public ParticleSystem fireParticleSystem;

		/// <summary>
		/// Launches homing missile at a target from a starting position
		/// </summary>
		/// <param name="enemy">
		/// The enemy to attack
		/// </param>
		/// <param name="attack">
		/// The projectile used to attack
		/// </param>
		/// <param name="firingPoint">
		/// The point the projectile is being fired from
		/// </param>
		public override void Launch(Targetable enemy, GameObject attack, Transform firingPoint)
		{
			var homingMissile = attack.GetComponent<HomingLinearProjectile>();
			if (homingMissile == null)
			{
				Debug.LogError("No HomingLinearProjectile attached to attack object");
				return;
			}
			Vector3 startingPoint = firingPoint.position;
			Vector3 targetPoint = Ballistics.CalculateLinearLeadingTargetPoint(
				startingPoint, enemy.position,
				enemy.velocity, homingMissile.startSpeed,
				homingMissile.acceleration);

			homingMissile.SetHomingTarget(enemy);
			homingMissile.FireAtPoint(startingPoint, targetPoint);
			PlayParticles(fireParticleSystem, startingPoint, targetPoint);
		}
	}
}