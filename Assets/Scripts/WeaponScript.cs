using UnityEngine;

namespace DefaultNamespace
{
    public class WeaponScript : MonoBehaviour
    {
        public float Damage { get; set; } = 10f;
        private Camera _mainCamera;
        
        public GameObject bulletPrefab;
        public float bulletSpeed = 10f;
        public float fireRate = 0.5f;
        
        private float nextFireTime = 0f;

        private Vector3 mousePosition;
        private Transform spriteTransform;

        private float _damage;
        private float _fireRate;
        
        private PlayerStats _playerStats;

        public AudioSource AudioSource;

        void Start()
        {
            _mainCamera = Camera.main;
            spriteTransform = GetComponentInChildren<SpriteRenderer>().transform;
            _playerStats = Singleton<PlayerStats>.Instance;
        }
        
        void Update()
        {
            Vector3 mousePos = Input.mousePosition;
            Vector3 gunPos = Camera.main.WorldToScreenPoint(transform.position);
            mousePos.x = mousePos.x - gunPos.x;
            mousePos.y = mousePos.y - gunPos.y;

            float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(new Vector3(180f, 0f, -angle));
            mousePosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);

            // Check if left mouse button is held and if cooldown has elapsed
            if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
            {
                nextFireTime = Time.time + fireRate * (1f / _playerStats.FireRateMultiplier);
                Shoot();
            }
        }

        void Shoot()
        {
            // Create a new bullet instance at the firePoint position
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            BulletScript bulletScript = bullet.GetComponent<BulletScript>();
            bulletScript.damage = Damage * _playerStats.DamageMultiplier;
            bulletScript.speed = bulletSpeed * _playerStats.BulletSpeedMultiplier;
            bulletScript.target = mousePosition;
            bulletScript.lifeTime = 5f;
            Instantiate(AudioSource, transform);
        }
    }
}