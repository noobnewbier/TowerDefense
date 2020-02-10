using UnityEngine;

namespace Elements.Turret
{
    //TODO: provider of repository.... something gone wrong here?

    //note this need to be placed in the root of the prefab
    public class TurretProvider : MonoBehaviour
    {
        [SerializeField] private TurretData turretData;
        [SerializeField] private Turret turret;
        [SerializeField] private BulletShooterRepositoryProvider bulletShooterRepositoryProvider;
        [SerializeField] private Collider mCollider;

        private ITurretRepository _turretRepository;

        private ITurretRepository TurretRepository =>
            _turretRepository ?? (_turretRepository = new TurretRepository(bulletShooterRepositoryProvider.ProvideRepository(), turretData));

        public Turret Turret => turret;

        public ITurretRepository GetRepository() => TurretRepository;

        //assuming this is at the root
        public GameObject GetTurretPrefab() => Instantiate(gameObject);
        public Vector3 HalfSize => mCollider.bounds.extents;
    }
}