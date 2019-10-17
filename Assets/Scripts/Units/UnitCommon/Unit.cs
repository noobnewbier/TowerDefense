using UnityEngine;

namespace Units.UnitCommon
{
    public abstract class Unit : MonoBehaviour
    {
        [SerializeField] protected UnitData unitData;
        public Transform Transform => transform;

        protected void Awake()
        {
            unitData = Instantiate(unitData);
        }

        protected virtual void TakeDamage(int damage)
        {
            unitData.Health -= damage;
            Debug.Log(unitData.Health);
        }
    }
}