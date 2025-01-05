using System;
using System.Collections;
using System.Collections.Generic;
using NamPhuThuy;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private BoxCollider2D _boxCollider2D;
    private Rigidbody2D _rigidbody2D;
    
    void Start()
    {
        _mainCamera = Camera.main;
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 newPosi = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(newPosi.x, newPosi.y, -2);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"TNam - collide with {other.transform.name}");
        
        if (other.transform.CompareTag("Enemy"))
        {
            MessageManager.Instance.SendMessage(new Message(NamMessageType.OnHitEnemy));
            
            //Trigger enemy's die-function
            other.GetComponent<EnemyBase>().OnDeath();
            
        }
        else if (other.transform.CompareTag("Projectile"))
        {
            // AudioManager.Instance.Play(_collideBulletAudio);
            
            MessageManager.Instance.SendMessage(new Message(NamMessageType.OnGameLose));
            Destroy(gameObject);
        }
        
    }
}