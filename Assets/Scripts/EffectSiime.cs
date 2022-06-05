using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSiime : MonoBehaviour
{
    public float lifeTime = 10f;

    private void Update()
    {
        if (lifeTime > 0)
        {
            lifeTime -= Time.deltaTime;
            if (lifeTime <= 0)
            {
                Destruction();
            }
        }
        if (this.transform.position.y <= -20)
        {
            Destruction();
        }
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.name == "destroyer")
        {
            Destruction();
        }
    }
    void Destruction()
    {
        Destroy(this.gameObject);
    }
}
