using System.Linq;
using Common.Class;
using EventManagement;
using Manager;
using TrainingSpecific;
using UnityEngine;

namespace ProjectSpecificUtils
{
    public class ForceKillAllEnemyAgent: MonoBehaviour
    {
        [SerializeField] private SurvivingEnemyAgentTracker agentTracker;
        private IEventAggregator _eventAggregator;
        private void OnEnable()
        {
            _eventAggregator = EventAggregatorHolder.Instance;
        }

        private void OnGUI()
        {
            if (GUI.Button(new Rect(5, 5, 150, 20), "Kill EnemyAgents"))
            {
                foreach (var enemy in agentTracker.EnemiesInField.ToList())
                {
                    _eventAggregator.Publish(new ForceResetEvent(enemy));
                }
            }
        }
    }
}