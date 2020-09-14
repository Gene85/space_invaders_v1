using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    public float _speed = 3.0f;

    void Update()
    {
        //Move down at a speed of 3 (adjust speed inspector)
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -4.5f)
        {
            Destroy(this.gameObject);
        }
    }

    //On Collison destroy
    //OnTriggerCollision
    //Only be collectable by the Player (use tags)

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
           //communicate with the player script
           //handle to the component I wante
           //assign the handle to the component
            Player player = other.transform.GetComponent<Player>();
            if(player != null)
            {
                player.TrippleShotActive();
            }

            Destroy(this.gameObject);
        }

    }

}