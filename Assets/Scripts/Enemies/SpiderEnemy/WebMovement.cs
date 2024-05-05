using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebMovement : MonoBehaviour
{
    public delegate void WebHitPlayer();
    public static event WebHitPlayer PlayerTakeDamage; 
    public float speed = 5f;
    void Start() 
    {
        Destroy(gameObject, 3f);
    }
    void Update()
    {
        // transform.position += new Vector3(Vector2.right.x, 0, 0) * speed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider collision)
    {
        if(collision.isTrigger){return;}
        if(collision.gameObject.tag == "Player")
        {
            PlayerTakeDamage.Invoke();
        }
        Destroy(gameObject);
    }
}
