using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class MyScript : MonoBehaviour {
  // 当たった時に呼ばれる関数
    void OnCollisionStay(Collision collision)
    {
        Debug.Log("Hit"); // ログを表示する
    }
}