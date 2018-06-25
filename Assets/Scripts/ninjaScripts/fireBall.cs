using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireBall : MonoBehaviour {

    public float speed = 4;

    void Start()
    {
        StartCoroutine(destroyLater());
    }

    IEnumerator destroyLater()
    {
        yield return new WaitForSeconds(5.0f);
        Destroy(this.gameObject);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        MouseController mm = collider.GetComponent<MouseController>();
        if (mm != null)
        {
            mm.Die();
            Destroy(this.gameObject);
        }
           
    }


    public void launch()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2( -speed, 0);
        GetComponent<SpriteRenderer>().flipX = true;
    }


}
