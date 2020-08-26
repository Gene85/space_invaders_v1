using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    public float _speed = 3.5f;
    [SerializeField]
    public GameObject _Laser;

    [SerializeField]
    private float _fireRate = 0.5f;
    [SerializeField]
    private float _canFire = -1f;
    [SerializeField]
    private int _numOf_lives = 3;
    private SpawnManager _spawnManager;


    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        //find the object > get the component.
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        //_spawnManager.onPlayerDeath

        if( _spawnManager == null )
        {
            Debug.LogError("The SpawnManager is NULL!");
        }
    }
    void Update()
    {
        CalculateMovement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }

    }
    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        transform.Translate(direction * _speed * Time.deltaTime);

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -15.0f, 0), 0);


        if (transform.position.x > 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }
        else if (transform.position.x <= -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }
    }

    void FireLaser()
    {
        _canFire = Time.time + _fireRate;
        GameObject.Instantiate(_Laser, transform.position + new Vector3(0, 0.9f, 0), Quaternion.identity);
    }

    public void Damage()
    {
        _numOf_lives--;

        if (_numOf_lives < 1)
        {
            //communicate with Spawn Manager
            //Let them know to stop Spawning
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }
}
