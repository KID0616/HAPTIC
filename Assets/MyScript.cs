using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 


public class MyScript : MonoBehaviour {
    Vector3 hitPos;
    void OnCollisionEnter(Collision other)
    {
        foreach (ContactPoint point in other.contacts)
        {
            hitPos = point.point;
            Debug.Log(hitPos); // ログを表示する
        }

    }
  // 当たった時に呼ばれる関数
    void OnCollisionStay(Collision collision)
    {
        Debug.Log("Hit"); // ログを表示する
    }
}