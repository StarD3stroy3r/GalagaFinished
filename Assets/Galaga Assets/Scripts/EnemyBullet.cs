using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EnemyBullet : MonoBehaviour {
    public float speed;
    public bool enemyBullet;
	// Use this for initialization
    // destroys a gameobject
	void Start () {

        
	}

    // when object is destroyed, add points
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(collision.gameObject);
        SceneManager.LoadScene("Game Over");
    }

    // Update is called once per frame
    // Adjusts bullets speed acording to developer modification
    void Update () {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
		
	}
}
