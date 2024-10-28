using Mirror;
using UnityEngine;

namespace MirrorProjectTPU.Lab1.Scripts
{
    public class PlayerController : NetworkBehaviour
    {
        [SerializeField] private float speed = 0.5f;
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private Transform bulletSpawnPoint; // ����� ������ ����
        [SerializeField] private float rotationSpeed = 10f; // �������� �������� ������

        private Vector3 _movedVector;

        void Update()
        {
            if (!isLocalPlayer) return;

            // �������� ������
            HandleMovement();

            // ������� ������ � ������� �����
            RotateTowardsMouse();

            // �������� ��� ������� ����� ������ ����
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 direction = GetMouseDirection();
                CmdFireBullet(direction);
            }
        }

        // �������� ������
        private void HandleMovement()
        {
            _movedVector.x = Input.GetAxis("Horizontal");
            _movedVector.z = Input.GetAxis("Vertical");

            Vector3 move = new Vector3(_movedVector.x, 0, _movedVector.z) * (speed * Time.deltaTime);
            transform.Translate(move, Space.World);
        }

        // ������� ������ � ������� �����, ��������� ���������
        private void RotateTowardsMouse()
        {
            Plane playerPlane = new Plane(Vector3.up, transform.position); // ��������� �� ������ ������
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (playerPlane.Raycast(ray, out float enter))
            {
                Vector3 hitPoint = ray.GetPoint(enter); // �������� ����� ����������� ���� � ����������
                Vector3 targetDirection = hitPoint - transform.position;
                targetDirection.y = 0; // ���������� ��� Y

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

        // ��������� ����������� ��� ������ ����
        private Vector3 GetMouseDirection()
        {
            Plane playerPlane = new Plane(Vector3.up, transform.position); // ��������� �� ������ ������
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (playerPlane.Raycast(ray, out float enter))
            {
                Vector3 hitPoint = ray.GetPoint(enter);
                return (hitPoint - bulletSpawnPoint.position).normalized;
            }
            return transform.forward; // ���� ��� �� ������� ���������, �������� ������
        }

        // ������� ��� �������� ���� �� �������
        [Command]
        private void CmdFireBullet(Vector3 direction)
        {
            var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
            NetworkServer.Spawn(bullet);

            // ������������� ����������� ������ ����
            bullet.GetComponent<Bullet>().Initialize(direction);
        }
    }
}
