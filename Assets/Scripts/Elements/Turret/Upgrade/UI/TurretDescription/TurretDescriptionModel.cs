using EventManagement;
using UnityEngine;

namespace Elements.Turret.Upgrade.UI.TurretDescription
{
    public interface ITurretDescriptionModel
    {
        string Description { get; set; }
    }

    public class TurretDescriptionModel : ScriptableObject, ITurretDescriptionModel
    {
        [SerializeField] private IEventAggregator eventAggregator;
        private string _description;

        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                eventAggregator.Publish(new TurretDescriptionTextUpdateEvent(this));
            }
        }
    }
}