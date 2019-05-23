using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Miner : MonoBehaviour {

    //MinerのPrefabを入れる
    public GameObject miner;

    //MazeGeneratorから引数を受け取り実行
    public void DoMining(int verNum, int horNum)
    {
        //コルーチンに引数を送って実行させる
        //MazeGneratorから実行させることもできるが重くなる
        StartCoroutine(Mining(verNum, horNum));
    }

    //DoMining関数から引数を2つ受け取る
    IEnumerator Mining(int ver, int hor)
    {
        //一度に全てのMinerを動かすとエラー出るのでランダムで時間差つける
        float random1 = Random.Range(0.1f, 3f);
        yield return new WaitForSeconds(random1);

        //前に消した2つ上のオブジェクトが欲しいため2を足す
        int verUp = ver + 2;

        //2つ上のを検索
        GameObject upObj = GameObject.Find(verUp + "-" + hor);
        //1つ上のを検索
        GameObject upObj2 = GameObject.Find(verUp - 1 + "-" + hor);

        //2つ上のオブジェクトがあるかどうか判定
        if (upObj != null)
        {
            //2つ上のと1つ上のを消す
            Destroy(upObj);
            Destroy(upObj2);

            //新たなMinerを生成
            GameObject minerObj = Instantiate(miner, Vector3.zero, Quaternion.identity);
            //新たなMinerのスクリプトを取得
            Miner minerScr = minerObj.GetComponent<Miner>();
            //2つ上のオブジェクトの縦軸と横軸の番号を送る
            minerScr.DoMining(verUp, hor);
        }

        float random2 = Random.Range(0.1f, 3f);
        yield return new WaitForSeconds(random2);

        //ここから下のオブジェクトの判定と消去
        int verDown = ver - 2;

        GameObject downObj = GameObject.Find(verDown + "-" + hor);
        GameObject downObj2 = GameObject.Find(verDown + 1 + "-" + hor);

        if (downObj != null)
        {
            Destroy(downObj);
            Destroy(downObj2);

            GameObject minerObj = Instantiate(miner, Vector3.zero, Quaternion.identity);
            Miner minerScr = minerObj.GetComponent<Miner>();
            minerScr.DoMining(verDown, hor);
        }

        float random3 = Random.Range(0.1f, 3f);
        yield return new WaitForSeconds(random3);

        //ここから右のオブジェクトの判定と消去
        int horRight = hor + 2;

        GameObject rightObj = GameObject.Find(ver + "-" + horRight);
        GameObject rightObj2 = GameObject.Find(ver + "-" + (horRight - 1));

        if (rightObj != null)
        {
            Destroy(rightObj);
            Destroy(rightObj2);

            GameObject minerObj = Instantiate(miner, Vector3.zero, Quaternion.identity);
            Miner minerScr = minerObj.GetComponent<Miner>();
            minerScr.DoMining(ver, horRight);
        }

        float random4 = Random.Range(0.1f, 3f);
        yield return new WaitForSeconds(random4);

        //ここから左のオブジェクトの判定と消去
        int horLeft = hor - 2;

        GameObject leftObj = GameObject.Find(ver + "-" + horLeft);
        GameObject leftObj2 = GameObject.Find(ver + "-" + (horLeft + 1));

        if (leftObj != null)
        {
            Destroy(leftObj);
            Destroy(leftObj2);

            GameObject minerObj = Instantiate(miner, Vector3.zero, Quaternion.identity);
            Miner minerScr = minerObj.GetComponent<Miner>();
            minerScr.DoMining(ver, horLeft);
        }

        //このオブジェクトを消す
        Destroy(gameObject);
    }
}