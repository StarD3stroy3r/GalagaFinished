using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public float speed;
    void OnCollisionEnter2D(Collision2D col)
    {
        Destroy(col.gameObject);
        GameObject.Find("Score").GetComponent<Score>().score++;
    }
	void Start () {
  	
	}
	
	void Update () {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }
}
