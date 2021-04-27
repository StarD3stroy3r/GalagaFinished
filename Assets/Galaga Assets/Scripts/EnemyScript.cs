using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {
    public float speed;
    int Direction = 1;
    public float vSpeed;
    public Transform bulletPreFab;
    Vector4 Bounds;
    Vector2 Velocity;
    public GameObject Bullet;

    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject == Bullet)
        {
            Destroy(col.gameObject);
        }

    }

    // Use this for initialization
    void Start () {
        Bounds = GameObject.FindObjectOfType<Controlls>().bounds;
        InvokeRepeating("shoot",Random.Range(1,4),Random.Range(1,4));
        
	}
	
	// Update is called once per frame
	void Update () {
       
        if (Velocity.y < 0)
        {
            Velocity.y += Time.deltaTime * vSpeed * 2;
        }
        if (Direction == 1 && transform.position.x >= Bounds.z)
        {
            Direction = -1;
            Velocity.y = -vSpeed;
        }
        if (Direction == -1 && transform.position.x <= Bounds.x)
        {
            Direction = 1;
            Velocity.y = -vSpeed;
        }
        Velocity.x = Direction * speed;
        transform.Translate(Velocity * Time.deltaTime);
	}
    void shoot()
    {
        Transform bullet = Instantiate(bulletPreFab);
        bullet.position = transform.position + -Vector3.up * 0.2f;
    }
}
