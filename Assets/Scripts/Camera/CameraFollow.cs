using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraFollow : MonoBehaviour
{
    public GameObject hedef;
    public Vector3 mesafe;

    void LateUpdate()
    {
        this.transform.position = Vector3.Lerp(this.transform.position, hedef.transform.position + mesafe, Time.fixedDeltaTime);
    }
}