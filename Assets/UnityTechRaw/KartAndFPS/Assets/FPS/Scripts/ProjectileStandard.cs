using System.Collections.Generic;
using UnityEngine;

namespace UnityTechRaw.KartAndFPS.Assets.FPS.Scripts
{
    [RequireComponent(typeof(ProjectileBase))]
    public class ProjectileStandard : MonoBehaviour
    {
        private const QueryTriggerInteraction k_TriggerInteraction = QueryTriggerInteraction.Collide;

        [Tooltip("Area of damage. Keep empty if you don<t want area damage")]
        public DamageArea areaOfDamage;

        [Header("Damage")]
        [Tooltip("Damage of the projectile")]
        public float damage = 40f;

        [Tooltip("Downward acceleration from gravity")]
        public float gravityDownAcceleration;

        [Tooltip("Layers this projectile can collide with")]
        public LayerMask hittableLayers = -1;

        [Tooltip("Clip to play on impact")]
        public AudioClip impactSFXClip;

        [Tooltip("VFX prefab to spawn upon impact")]
        public GameObject impactVFX;

        [Tooltip("LifeTime of the VFX before being destroyed")]
        public float impactVFXLifetime = 5f;

        [Tooltip("Offset along the hit normal where the VFX will be spawned")]
        public float impactVFXSpawnOffset = 0.1f;

        [Tooltip("Determines if the projectile inherits the velocity that the weapon's muzzle had when firing")]
        public bool inheritWeaponVelocity;

        private Vector3 m_ConsumedTrajectoryCorrectionVector;
        private bool m_HasTrajectoryOverride;
        private List<Collider> m_IgnoredColliders;
        private Vector3 m_LastRootPosition;

        private ProjectileBase m_ProjectileBase;
        private float m_ShootTime;
        private Vector3 m_TrajectoryCorrectionVector;
        private Vector3 m_Velocity;

        [Tooltip("LifeTime of the projectile")]
        public float maxLifeTime = 5f;

        [Header("General")]
        [Tooltip("Radius of this projectile's collision detection")]
        public float radius = 0.01f;

        [Header("Debug")]
        [Tooltip("Color of the projectile radius debug view")]
        public Color radiusColor = Color.cyan * 0.2f;

        [Tooltip("Transform representing the root of the projectile (used for accurate collision detection)")]
        public Transform root;

        [Header("Movement")]
        [Tooltip("Speed of the projectile")]
        public float speed = 20f;

        [Tooltip("Transform representing the tip of the projectile (used for accurate collision detection)")]
        public Transform tip;

        [Tooltip(
            "Distance over which the projectile will correct its course to fit the intended trajectory (used to drift projectiles towards center of screen in First Person view). At values under 0, there is no correction"
        )]
        public float trajectoryCorrectionDistance = -1;

        private void OnEnable()
        {
            m_ProjectileBase = GetComponent<ProjectileBase>();
            DebugUtility.HandleErrorIfNullGetComponent<ProjectileBase, ProjectileStandard>(m_ProjectileBase, this, gameObject);

            m_ProjectileBase.onShoot += OnShoot;

            Destroy(gameObject, maxLifeTime);
        }

        private void OnShoot()
        {
            m_ShootTime = Time.time;
            m_LastRootPosition = root.position;
            m_Velocity = transform.forward * speed;
            m_IgnoredColliders = new List<Collider>();
            transform.position += m_ProjectileBase.inheritedMuzzleVelocity * Time.deltaTime;

            // Ignore colliders of owner
            var ownerColliders = m_ProjectileBase.owner.GetComponentsInChildren<Collider>();
            m_IgnoredColliders.AddRange(ownerColliders);

            // Handle case of player shooting (make projectiles not go through walls, and remember center-of-screen trajectory)
            var playerWeaponsManager = m_ProjectileBase.owner.GetComponent<PlayerWeaponsManager>();
            if (playerWeaponsManager)
            {
                m_HasTrajectoryOverride = true;

                var cameraToMuzzle = m_ProjectileBase.initialPosition - playerWeaponsManager.weaponCamera.transform.position;

                m_TrajectoryCorrectionVector = Vector3.ProjectOnPlane(-cameraToMuzzle, playerWeaponsManager.weaponCamera.transform.forward);
                if (trajectoryCorrectionDistance == 0)
                {
                    transform.position += m_TrajectoryCorrectionVector;
                    m_ConsumedTrajectoryCorrectionVector = m_TrajectoryCorrectionVector;
                }
                else if (trajectoryCorrectionDistance < 0) m_HasTrajectoryOverride = false;

                if (Physics.Raycast(
                    playerWeaponsManager.weaponCamera.transform.position,
                    cameraToMuzzle.normalized,
                    out var hit,
                    cameraToMuzzle.magnitude,
                    hittableLayers,
                    k_TriggerInteraction
                ))
                    if (IsHitValid(hit))
                        OnHit(hit.point, hit.normal, hit.collider);
            }
        }

        private void Update()
        {
            // Move
            transform.position += m_Velocity * Time.deltaTime;
            if (inheritWeaponVelocity) transform.position += m_ProjectileBase.inheritedMuzzleVelocity * Time.deltaTime;

            // Drift towards trajectory override (this is so that projectiles can be centered 
            // with the camera center even though the actual weapon is offset)
            if (m_HasTrajectoryOverride && m_ConsumedTrajectoryCorrectionVector.sqrMagnitude < m_TrajectoryCorrectionVector.sqrMagnitude)
            {
                var correctionLeft = m_TrajectoryCorrectionVector - m_ConsumedTrajectoryCorrectionVector;
                var distanceThisFrame = (root.position - m_LastRootPosition).magnitude;
                var correctionThisFrame = distanceThisFrame / trajectoryCorrectionDistance * m_TrajectoryCorrectionVector;
                correctionThisFrame = Vector3.ClampMagnitude(correctionThisFrame, correctionLeft.magnitude);
                m_ConsumedTrajectoryCorrectionVector += correctionThisFrame;

                // Detect end of correction
                if (m_ConsumedTrajectoryCorrectionVector.sqrMagnitude == m_TrajectoryCorrectionVector.sqrMagnitude) m_HasTrajectoryOverride = false;

                transform.position += correctionThisFrame;
            }

            // Orient towards velocity
            transform.forward = m_Velocity.normalized;

            // Gravity
            if (gravityDownAcceleration > 0)
            {
                // add gravity to the projectile velocity for ballistic effect
                m_Velocity += Vector3.down * gravityDownAcceleration * Time.deltaTime;
            }

            // Hit detection
            {
                var closestHit = new RaycastHit();
                closestHit.distance = Mathf.Infinity;
                var foundHit = false;

                // Sphere cast
                var displacementSinceLastFrame = tip.position - m_LastRootPosition;
                var hits = Physics.SphereCastAll(
                    m_LastRootPosition,
                    radius,
                    displacementSinceLastFrame.normalized,
                    displacementSinceLastFrame.magnitude,
                    hittableLayers,
                    k_TriggerInteraction
                );
                foreach (var hit in hits)
                    if (IsHitValid(hit) && hit.distance < closestHit.distance)
                    {
                        foundHit = true;
                        closestHit = hit;
                    }

                if (foundHit)
                {
                    // Handle case of casting while already inside a collider
                    if (closestHit.distance <= 0f)
                    {
                        closestHit.point = root.position;
                        closestHit.normal = -transform.forward;
                    }

                    OnHit(closestHit.point, closestHit.normal, closestHit.collider);
                }
            }

            m_LastRootPosition = root.position;
        }

        private bool IsHitValid(RaycastHit hit)
        {
            // ignore hits with an ignore component
            if (hit.collider.GetComponent<IgnoreHitDetection>()) return false;

            // ignore hits with triggers that don't have a Damageable component
            if (hit.collider.isTrigger && hit.collider.GetComponent<Damageable>() == null) return false;

            // ignore hits with specific ignored colliders (self colliders, by default)
            if (m_IgnoredColliders.Contains(hit.collider)) return false;

            return true;
        }

        private void OnHit(Vector3 point, Vector3 normal, Collider collider)
        {
            // damage
            if (areaOfDamage)
            {
                // area damage
                areaOfDamage.InflictDamageInArea(damage, point, hittableLayers, k_TriggerInteraction, m_ProjectileBase.owner);
            }
            else
            {
                // point damage
                var damageable = collider.GetComponent<Damageable>();
                if (damageable) damageable.InflictDamage(damage, false, m_ProjectileBase.owner);
            }

            // impact vfx
            if (impactVFX)
            {
                var impactVFXInstance = Instantiate(impactVFX, point + normal * impactVFXSpawnOffset, Quaternion.LookRotation(normal));
                if (impactVFXLifetime > 0) Destroy(impactVFXInstance.gameObject, impactVFXLifetime);
            }

            // impact sfx
            if (impactSFXClip) AudioUtility.CreateSFX(impactSFXClip, point, AudioUtility.AudioGroups.Impact, 1f, 3f);

            // Self Destruct
            Destroy(gameObject);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = radiusColor;
            Gizmos.DrawSphere(transform.position, radius);
        }
    }
}