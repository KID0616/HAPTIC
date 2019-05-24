using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Miner : MonoBehaviour {
    
    public GameObject miner;

    //配列、シャッフルして順番をランダムにするため
    //[]の中身を要素
    //{}の中身が初期値
    int[] array = new int[4] { 1, 2, 3, 4 };   //上下左右をランダムに選択する
    
    //これがMazeGeneratorに呼び出される。引数はランダムな開始地点
    public void DoMining(int verNum, int horNum)
    {
        StartCoroutine(Mining(verNum, horNum));   //コルーチンを開始
    }
    
    IEnumerator Mining(int ver, int hor)  //IEnumerator:反復処理を行う
    {
        //配列の初期値の数だけ数の位置を入れ替える
        for (int i = 0; i < array.Length; i++)
        {
            //placeにarray[i]を入れ、array[i]にはランダムで選んだ
            //別の初期値を入れ、そして別の初期値の場所に最初のarray[i]の
            //数字を入れて交換を完了する
            int place = array[i];
            int random = Random.Range(i, array.Length);
            array[i] = array[random];
            array[random] = place;
        }

        //Switching関数に配列の1番目の初期値と縦横の位置の数値を引数で送る
        Switching(array[0], ver, hor);
        //0.1～1秒のランダムな数値だけ待たせる
        float random1 = Random.Range(0.1f, 1f);
        yield return new WaitForSeconds(random1);
        
        Switching(array[1], ver, hor);
        float random2 = Random.Range(0.1f, 1f);
        yield return new WaitForSeconds(random2);

        Switching(array[2], ver, hor);
        float random3 = Random.Range(0.1f, 1f);
        yield return new WaitForSeconds(random3);

        Switching(array[3], ver, hor);
        float random4 = Random.Range(0.1f, 1f);
        yield return new WaitForSeconds(random4);
        
        
        Destroy(gameObject);   //対応するCubeを破壊
    }

    void Switching(int num, int ver, int hor)
    {
        //シャッフルされた配列の初期値に対応するcaseが実行される
        switch (num)
        {
            case 4:
                    int verUp = ver + 2;
                    GameObject upObj = GameObject.Find(verUp + "-" + hor);
                    GameObject upObj2 = GameObject.Find(verUp - 1 + "-" + hor);

                if (upObj != null)
                {
                    Destroy(upObj);
                    Destroy(upObj2);

                    //MiningFormat関数を呼び出す
                    MiningFormat(verUp, hor);   //位置を更新し、もう一度実行   
                }
                break;
            case 3:
                    int verDown = ver - 2;
                    GameObject downObj = GameObject.Find(verDown + "-" + hor);
                    GameObject downObj2 = GameObject.Find(verDown + 1 + "-" + hor);

                if (downObj != null)
                {
                    Destroy(downObj);
                    Destroy(downObj2);

                    MiningFormat(verDown, hor);
                }
                break;
            case 2:
                    int horRight = hor + 2;
                    GameObject rightObj = GameObject.Find(ver + "-" + horRight);
                    GameObject rightObj2 = GameObject.Find(ver + "-" + (horRight - 1));

                if (rightObj != null)
                {
                     Destroy(rightObj);
                     Destroy(rightObj2);

                    MiningFormat(ver, horRight);
                }
                break;
            case 1:
                    int horLeft = hor - 2;
                    GameObject leftObj = GameObject.Find(ver + "-" + horLeft);
                    GameObject leftObj2 = GameObject.Find(ver + "-" + (horLeft + 1));

                if (leftObj != null)
                {
                    Destroy(leftObj);
                    Destroy(leftObj2);

                    MiningFormat(ver, horLeft);
                }
                break;
        }
    }

    //生成されたMiner2オブジェクトに与えられた引数を送る
    void MiningFormat(int ver, int hor)
    {
        GameObject minerObj = Instantiate(miner, Vector3.zero, Quaternion.identity);
        Miner minerScr = minerObj.GetComponent<Miner>();
        minerScr.DoMining(ver, hor);
    }
}