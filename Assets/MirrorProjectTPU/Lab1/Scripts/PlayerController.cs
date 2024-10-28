using Mirror;
using UnityEngine;

namespace MirrorProjectTPU.Lab1.Scripts
{
    public class PlayerController : NetworkBehaviour
    {
        [SerializeField] private float speed = 0.5f;
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private Transform bulletSpawnPoint; // Точка спавна пули
        [SerializeField] private float rotationSpeed = 10f; // Скорость вращения игрока

        private Vector3 _movedVector;

        void Update()
        {
            if (!isLocalPlayer) return;

            // Движение игрока
            HandleMovement();

            // Поворот игрока в сторону мышки
            RotateTowardsMouse();

            // Стрельба при нажатии левой кнопки мыши
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 direction = GetMouseDirection();
                CmdFireBullet(direction);
            }
        }

        // Движение игрока
        private void HandleMovement()
        {
            _movedVector.x = Input.GetAxis("Horizontal");
            _movedVector.z = Input.GetAxis("Vertical");

            Vector3 move = new Vector3(_movedVector.x, 0, _movedVector.z) * (speed * Time.deltaTime);
            transform.Translate(move, Space.World);
        }

        // Поворот игрока в сторону мышки, используя плоскость
        private void RotateTowardsMouse()
        {
            Plane playerPlane = new Plane(Vector3.up, transform.position); // Плоскость на уровне игрока
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (playerPlane.Raycast(ray, out float enter))
            {
                Vector3 hitPoint = ray.GetPoint(enter); // Получаем точку пересечения луча с плоскостью
                Vector3 targetDirection = hitPoint - transform.position;
                targetDirection.y = 0; // Игнорируем ось Y

                if (targetDirection != Vector3.zero)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
                    transform.rotation = Quaternion.Slerp(
                        transform.rotation,
                        targetRotation,
                        Time.deltaTime * rotationSpeed
                    );
                }
            }
        }

        // Получение направления для полета пули
        private Vector3 GetMouseDirection()
        {
            Plane playerPlane = new Plane(Vector3.up, transform.position); // Плоскость на уровне игрока
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (playerPlane.Raycast(ray, out float enter))
            {
                Vector3 hitPoint = ray.GetPoint(enter);
                return (hitPoint - bulletSpawnPoint.position).normalized;
            }
            return transform.forward; // Если луч не пересек плоскость, стреляем вперед
        }

        // Команда для создания пули на сервере
        [Command]
        private void CmdFireBullet(Vector3 direction)
        {
            var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
            NetworkServer.Spawn(bullet);

            // Инициализация направления полета пули
            bullet.GetComponent<Bullet>().Initialize(direction);
        }
    }
}
