using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour {

    public float speed = 4;
    public float angle = 0;

    void Start()
    {
        StartCoroutine(destroyLater());
    }

    IEnumerator destroyLater()
    {
        yield return new WaitForSeconds(4.0f);
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

    public void launch(float angle2)
    {
        angle = angle2;
        this.transform.rotation = new UnityEngine.Quaternion(0,0,angle,90);
        GetComponent<Rigidbody2D>().velocity = new Vector2( -speed*Mathf.Cos(Mathf.PI*angle/180), -speed * Mathf.Sin(Mathf.PI * angle / 180));
        GetComponent<SpriteRenderer>().flipX = true;
    }


}
