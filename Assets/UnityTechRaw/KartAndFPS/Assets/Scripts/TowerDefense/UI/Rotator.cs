﻿using UnityEngine;

namespace UnityTechRaw.KartAndFPS.Assets.Scripts.TowerDefense.UI
{
	/// <summary>
	/// A simple component that applies a constant rotation to a transform
	/// </summary>
	public class Rotator : MonoBehaviour
	{
		public Vector3 rotationSpeed;
	
		void Update ()
		{
			transform.localEulerAngles += rotationSpeed;
		}
	}
}
