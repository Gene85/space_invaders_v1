using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    public float _enemySpeed = 4.0f;

    void Start()
    {
        transform.position = new Vector3(0, 5, 0);
        //gameObject.tag = "Enemy";
        //gameObject.tag = "Laser";
        // gameObject.tag = "Player";
        //currentHealth = max_health;
    }

    // Update is called once per frame
    void Update()
    {
        // Move enemy down 4 meters per second
        transform.Translate(Vector3.down * _enemySpeed * Time.deltaTime);

        // if enemy reaches the bottom of the screen
        // Respawn at top with a new random x position 

        if (transform.position.y < -5f)
        {
            float randomX = Random.Range(-8f, 8f);
            transform.position = new Vector3(randomX, 7, 0);
        }
    }

    /*public void TakeDamage(float ammount)
    {
        currentHealth -= ammount;
    }*/
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();

            if (player != null)
            {
                player.Damage();
            }
        }

        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
