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

        void Start()
        {
            _mainCamera = Camera.main;
            spriteTransform = GetComponentInChildren<SpriteRenderer>().transform;
        }
        
        void Update()
        {
            // Rotate the gun to face the mouse cursor
            mousePosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector3 direction = mousePosition - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle);

            spriteTransform.LookAt(mousePosition);
            
            // Check if left mouse button is held and if cooldown has elapsed
            if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
            {
                nextFireTime = Time.time + fireRate;
                Shoot();
            }
        }

        void Shoot()
        {
            // Create a new bullet instance at the firePoint position
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            BulletScript bulletScript = bullet.GetComponent<BulletScript>();
            bulletScript.damage = Damage;
            bulletScript.speed = bulletSpeed;
            bulletScript.target = mousePosition;
            bulletScript.lifeTime = 5f;
        }
    }
}