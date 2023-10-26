using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public enum CollidesWithTag
    {
        Player,
        Enemy
    }

    [SerializeField] int damage;
    [SerializeField] float moveSpeed;
    [SerializeField] float lifeTime;
    [SerializeField] float angleOffset;
    [SerializeField] CollidesWithTag collidesWithTag;
    [SerializeField] GameObject bulletImpact;
    [SerializeField] int originalDamage = 20; // Must be Serialized.

    Rigidbody2D rig;
    float spawnTime;

    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();

        spawnTime = Time.time;
    }

    private void Start()
    {
        Move();
    }

    private void Update()
    {
        CheckLifeTime();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" || collision.tag == "Enemy")
        {
            if (collidesWithTag.ToString() != collision.tag) return;

            PlayImpactParticleEffect(collision);

            collision.GetComponent<Health>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }

    public void SetAngleOffset(float angleOffset)
    {
        this.angleOffset = angleOffset;
    }
    public void UpdateDamage(int damage)
    {
        this.damage += damage;
    }

    public void ResetDamage()
    {
        damage = originalDamage;
    }

    void Move()
    {
        rig.AddForce(Quaternion.AngleAxis(angleOffset, Vector3.forward) * 
            transform.right * moveSpeed);
    }

    void CheckLifeTime()
    {
        if (lifeTime + spawnTime <= Time.time)
            Destroy(gameObject);
    }

    void PlayImpactParticleEffect(Collider2D collision)
    {
        // Using a Vector3 because Instantiate() will not take a Vector2
        // as a parameter for this particular use case.

        Vector3 position = new Vector3(
             collision.gameObject.transform.position.x,
              collision.gameObject.transform.position.y, 0);

        Instantiate(bulletImpact, position, Quaternion.identity);
    }
}

