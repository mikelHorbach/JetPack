using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

    public GameObject prefabFireball;

    // Use this for initialization
    void Start () {
        launchFireball(toMouseAngle());
        //StartCoroutine(secondFireball());
    }

    void launchFireball(float angle)
    {
        //Створюємо копію Prefab
        GameObject obj = GameObject.Instantiate(prefabFireball);
        //Розміщуємо в просторі
        obj.transform.position = this.transform.position;
        //Запускаємо в рух
        FireBall fireBall = obj.GetComponent<FireBall>();

        fireBall.launch(angle);
    }

    float toMouseAngle()
    {
        Vector3 mousePosition = MouseController.mouse.transform.position;
        Vector3 myPosition = this.transform.position;

        return angleInGrad(Mathf.Atan2(myPosition.y - mousePosition.y, myPosition.x - mousePosition.x));
    }

    float angleInGrad(float angleInRadians)
    {
        return (angleInRadians * 180) / Mathf.PI;
    }

    IEnumerator secondFireball()
    {
        yield return new WaitForSeconds(0.7f);
        launchFireball(toMouseAngle());
    }

}
