using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Controlls : MonoBehaviour {
    Vector2 velocity = Vector2.zero;
    public float acceleration;
    public Vector4 bounds;
    public Transform bulletPrefab;
    public GameObject EnemyBullet;

	// Use this for initialization
    // Lets the ship move around using WASD
  
	void Start () {

	}
    void checkInput()
    {
        if (Input.GetKey(KeyCode.W))
        {
            velocity.y = Mathf.Clamp(velocity.y + acceleration, -10, 10);
        }

        else if (Input.GetKey(KeyCode.S))
        {
            velocity.y = Mathf.Clamp(velocity.y - acceleration, -5, 5);
        }
        else
        {
            velocity.y /= 4;
            if (Mathf.Abs(velocity.y) < 0.5f)
            {
                velocity.y = 0;
            }
        }

        if (Input.GetKey(KeyCode.A))
        {
            velocity.x = Mathf.Clamp(velocity.x - acceleration, -10, 10);
        }

        else if (Input.GetKey(KeyCode.D))
        {
            velocity.x = Mathf.Clamp(velocity.x + acceleration, -10, 10);
        }

        else
        {
            velocity.x /= 4;
            if (Mathf.Abs(velocity.x) < 0.5f)
            {
                velocity.x = 0;
            }
        }
        // Allows players to shoot with the SpaceBar
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Transform bullet = Instantiate(bulletPrefab);
            bullet.position = transform.position + Vector3.up * 0.5f;
        }

    }
	
	// Update is called once per frame
    // Stops players from moving across borderLines.
	void Update ()
    {
        checkInput();
        if (transform.position.x + velocity.x * Time.deltaTime < bounds.x ||
            transform.position.x + velocity.x * Time.deltaTime > bounds.z)
        {
            velocity.x = 0;
        }
        if (transform.position.y + velocity.y * Time.deltaTime < bounds.w ||
          transform.position.y + velocity.y * Time.deltaTime > bounds.y)
        {
            velocity.y = 0;
        }
        transform.Translate(velocity * Time.deltaTime);
        
	}
    //Creates BorderLines
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(new Vector2(bounds.x, bounds.y), new Vector2(bounds.x, bounds.w));
        Gizmos.DrawLine(new Vector2(bounds.z, bounds.y), new Vector2(bounds.z, bounds.w));
        Gizmos.DrawLine(new Vector2(bounds.x, bounds.y), new Vector2(bounds.z, bounds.y));
        Gizmos.DrawLine(new Vector2(bounds.x, bounds.w), new Vector2(bounds.z, bounds.w));

    
    }
}
