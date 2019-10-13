using UnityEngine;

namespace Units.UnitCommon
{
    public abstract class Unit : MonoBehaviour
    {
        [SerializeField] private UnitData unitData;
        public Transform Transform => transform;

        protected void Awake()
        {
            unitData = Instantiate(unitData);
        }

        protected void TakeDamage(int damage)
        {
            unitData.Health -= damage;
            Debug.Log(unitData.Health);
        }
    }
}