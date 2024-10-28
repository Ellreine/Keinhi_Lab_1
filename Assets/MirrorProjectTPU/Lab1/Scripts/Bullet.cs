using Mirror;
using UnityEngine;

public class Bullet : NetworkBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifeTime = 3f;

    // ������������������ ����������� ����
    [SyncVar] private Vector3 direction;

    public void Initialize(Vector3 dir)
    {
        direction = dir; // ������������� ����������� �� �������

        // ��������� ������ �� �������� ����
        Invoke(nameof(DestroyBullet), lifeTime);
    }

    private void Update()
    {
        if (direction != Vector3.zero) // ���� ����������� ������
        {
            MoveBullet(); // ������� ���� �� ���� ��������
        }
    }

    private void MoveBullet()
    {
        // ���������� ���� ������ �� ��������� �����������
        transform.position += direction * (speed * Time.deltaTime);
    }

    [Server] // ��������, ��� �������� ����������� �� �������
    private void DestroyBullet()
    {
        NetworkServer.Destroy(gameObject); // ������� ���� �� ������� � �������������� ��� � ���������
    }
}
