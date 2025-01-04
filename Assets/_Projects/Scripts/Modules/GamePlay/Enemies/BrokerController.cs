using System;
using System.Collections;
using System.Threading.Tasks;
using NamPhuThuy;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class BrokerController : EnemyBase
{
    [SerializeField] private GameObject _projectile;
    [SerializeField] private Transform _transform;
    [SerializeField] private Collider2D _collider2D;

    [Header("Stats")] 
    [SerializeField] private float _speed = 2f;
    [SerializeField] private int _projectileNums = 12;
    [SerializeField] private float _spreadAngle;
    [SerializeField] private float _projectileSpeed = 4.8f;

    private void Start()
    {
        _transform = GetComponent<Transform>();
        _collider2D = GetComponent<Collider2D>();
        _spreadAngle = 360f / _projectileNums;
    }

    private void OnEnable()
    {
        GamePlayManager.Instance.AddEnemyToList(gameObject);
        
    }

    private void OnDisable()
    {
        GamePlayManager.Instance.RemoveEnemyFromList(gameObject);
    }
    
    public async override Task OnDeath()
    {
        //exclude player's layer from collision
        _collider2D.excludeLayers = LayerMaskHelper.OnlyIncluding(LayerMask.NameToLayer("Player"));
        await DraggedAway();
        await FireProjectiles();
        await Die();
    }
    
    private async Task DraggedAway()
    {
        float minDistance = 3.5f;
        float maxDistance = 6.5f;
        float moveDuration = 1f; // Time in seconds for the movement
    
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        float randomDistance = Random.Range(minDistance, maxDistance);

        Vector2 startPosition = _transform.position;
        Vector2 targetPosition = (Vector2)_transform.position + randomDirection * randomDistance;
    
        float elapsedTime = 0f;
    
        while (elapsedTime < moveDuration)
        {
            elapsedTime += Time.deltaTime;
            float percentageComplete = Mathf.Clamp01(elapsedTime / moveDuration);
            _transform.position = Vector2.Lerp(startPosition, targetPosition, percentageComplete);
            await Task.Yield();
        }
    
        _transform.position = targetPosition; // Ensure final position is exact
    }

    private async Task FireProjectiles()
    {
        for (int i = 0; i < _projectileNums; i++)
        {
            // Calculate the rotation for the bullet
            Quaternion rotation = Quaternion.Euler(0, 0, _spreadAngle * i);

            // Calculate the bullet's position
            Vector3 position = transform.position;

            // Instantiate the bullet
            GameObject bullet = Instantiate(_projectile, position, rotation);

            // bullet.transform.parent = transform;
            // // Set the bullet's velocity
            bullet.GetComponent<Rigidbody2D>().velocity = rotation * Vector3.up * _projectileSpeed;
        }
    }

    private async Task Die()
    {
        MessageManager.Instance.SendMessage(new Message(NamMessageType.OnEnemyDie));
        //cập nhật data
        //cập nhật lại UI 
        //phát đoạn nhạc
        Destroy(gameObject);
    }
}
