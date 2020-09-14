using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    [SerializeField]
    public float _speed = 3.5f;
    [SerializeField]
    public GameObject _Laser;
    [SerializeField]
    private bool _IstrippleShotActive = false;
    [SerializeField]
    private bool _tripleShotActive = false;
    [SerializeField]
    private float _fireRate = 0.5f;
    [SerializeField]
    private float _canFire = -1f;
    [SerializeField]
    private int _numOf_lives = 3;
    private SpawnManager _spawnManager;
    [SerializeField]
    public GameObject _TrippleShot;
    [SerializeField]
    private GameObject _Tripple_Shot_Powerup;


    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.FindGameObjectWithTag("SpawnManager").GetComponent<SpawnManager>();

        if (_spawnManager == null)
        {
            Debug.LogError("The SpawnManager is NULL!");
        }

    }
    void Update()
    {
        CalculateMovement();

        if (Input.GetKey(KeyCode.Mouse0) && Time.time > _canFire)
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
        GameObject.Instantiate(_Laser, transform.position + new Vector3(0, 0.82f, 0), Quaternion.identity);

        if (_IstrippleShotActive == true)
        {
            GameObject.Instantiate(_TrippleShot, transform.position + new Vector3(0, 0.12f, 0), Quaternion.identity);
        }
        else
        {
            GameObject.Instantiate(_Laser, transform.position + new Vector3(0, 0.718f, 0), Quaternion.identity);
        }

    }

    public void Damage()
    {
        _numOf_lives--;

        if (_numOf_lives < 1)
        {
            _spawnManager.OnplayerDeath();
            Destroy(this.gameObject);
        }
    }

    public void TrippleShotActive()
    {
        //tripleShotActive becomes true
        //start the power down coroutine for triple shot
        _IstrippleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());

        // IEnumerator TripleShotPowerDownRoutine
        // Wait 5 seconds
        // Set the triple shot to false
        IEnumerator TripleShotPowerDownRoutine()
        {
            yield return new WaitForSeconds(0.7f);
            _IstrippleShotActive = false;
        }
    }
}
