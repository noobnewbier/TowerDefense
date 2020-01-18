using UnityEngine;
using UnityTechRaw.KartAndFPS.Assets.Scripts.Core.Health;

namespace UnityTechRaw.KartAndFPS.Assets.Scripts.ActionGameFramework.Spawning
{
	/// <summary>
	/// A hit object is a special type of GameObject that consumes hit info
	/// e.g. using Damage to scale the size
	/// </summary>
	public abstract class HitObject : MonoBehaviour
	{
		public abstract void SetHitInfo(HitInfo hitInfo);
	}
}