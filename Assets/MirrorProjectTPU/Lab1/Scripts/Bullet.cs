using Mirror;
using UnityEngine;

public class Bullet : NetworkBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifeTime = 3f;

    // Синхронизированное направление пули
    [SyncVar] private Vector3 direction;

    public void Initialize(Vector3 dir)
    {
        direction = dir; // Устанавливаем направление на сервере

        // Запускаем таймер на удаление пули
        Invoke(nameof(DestroyBullet), lifeTime);
    }

    private void Update()
    {
        if (direction != Vector3.zero) // Если направление задано
        {
            MoveBullet(); // Двигаем пулю на всех клиентах
        }
    }

    private void MoveBullet()
    {
        // Перемещаем пулю вперед по заданному направлению
        transform.position += direction * (speed * Time.deltaTime);
    }

    [Server] // Убедимся, что удаление выполняется на сервере
    private void DestroyBullet()
    {
        NetworkServer.Destroy(gameObject); // Удаляем пулю на сервере и синхронизируем это с клиентами
    }
}
