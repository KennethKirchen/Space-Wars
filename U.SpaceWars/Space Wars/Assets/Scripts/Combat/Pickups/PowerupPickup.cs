using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupPickup : MonoBehaviour, IPickup
{
    [SerializeField] float effectLifeTime;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] GameObject floatingPE;
    Collider2D collision;
    
    public void BeginEffect(Collider2D collision)
    {
        this.collision = collision;
        ToggleAssets();
        this.collision.GetComponent<PlayerFire>().ChangeActiveWeapon(WeaponType.Spreadshot);

        Invoke("CancelEffect", effectLifeTime);
    }

    public void CancelEffect()
    {
        collision.GetComponent<PlayerFire>().ChangeActiveWeapon(WeaponType.Blaster);
        Destroy(gameObject);
    }

    void ToggleAssets()
    {
        floatingPE.SetActive(false);
        spriteRenderer.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            BeginEffect(collision);
        }
    }
}
