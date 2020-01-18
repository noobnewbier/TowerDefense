using System;
using UnityEngine;
using UnityTechRaw.KartAndFPS.Assets.Scripts.TowerDefense.Agents.Data;
using UnityTechRaw.KartAndFPS.Assets.Scripts.TowerDefense.Nodes;

namespace UnityTechRaw.KartAndFPS.Assets.Scripts.TowerDefense.Level
{
	/// <summary>
	/// Serializable class for specifying properties of spawning an agent
	/// </summary>
	[Serializable]
	public class SpawnInstruction
	{
		/// <summary>
		/// The agent to spawn - i.e. the monster for the wave
		/// </summary>
		public AgentConfiguration agentConfiguration;

		/// <summary>
		/// The delay from the previous spawn until when this agent is spawned
		/// </summary>
		[Tooltip("The delay from the previous spawn until when this agent is spawned")]
		public float delayToSpawn;

		/// <summary>
		/// The starting node, where the agent is spawned
		/// </summary>
		public Node startingNode;
	}
}