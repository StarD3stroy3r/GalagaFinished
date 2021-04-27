using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class OnCollisionChangeScene : MonoBehaviour {
    public int scene;

    void OnTriggerEnter2D (Collider2D coll)
    {
        if (coll.gameObject.tag == "Bullet"){
            SceneManager.LoadScene(scene);

        }
    }
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
