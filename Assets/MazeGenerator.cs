using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour {


    //上から見て縦、Z軸のオブジェクトの量
    public int vertical = 11;
    //上から見て横、X軸のオブジェクトの量
    public int horizontal = 11;

    //Prefabを入れる欄を作る
    public GameObject cube;

    //for文でオブジェクトを縦横に並べるための変数
    float vi;
    float hi;

    //MinerのPrefabを入れるための変数
    public GameObject miner;

    void Start () {
        //Cubeを並べるための基準になる位置
        Vector3 pos = new Vector3((float)-0.055, (float)-0.08, (float)0.10);

         //Z軸にverticalの数だけ並べる
        for (vi = 0; vi < vertical; vi++)
        {
            //X軸にhorizontalの数だけ並べる
            for (hi = 0; hi < horizontal; hi++)
            {
                //PrefabのCubeを生成する
                GameObject copy = Instantiate(cube,
                    //生成したものを配置する位置
                    new Vector3(
                        //X軸
                        pos.x + hi * (float)0.01 ,
                        //Y軸
                        pos.y,
                        //Z軸
                        pos.z + vi * (float)0.01
                    //Quaternion.identityは無回転を指定する
                    ), Quaternion.identity);

                //生成したオブジェクトに番号の名前をつける
                copy.name = vi + "-" + hi.ToString();
            }
        }

        //ランダムな数字を縦横分の2つ出す
        //0からだが、並ぶオブジェクトの内側から選びたいので1からにした
        int ver1 = 2 * Random.Range(0, (vertical - 1)/2) + 1;
        int hor1 = 2 * Random.Range(0, (horizontal - 1)/2) + 1;

        //ランダムな数字からオブジェクトを検索してDestroyで消す
        GameObject start = GameObject.Find(ver1 + "-" + hor1);
        Destroy(start);
        //その位置をコンソールに表示
        Debug.Log(start);

        //Minerを生成
        GameObject minerObj = Instantiate(miner, Vector3.zero, Quaternion.identity); //Vector3.zero (0,0,0)と同じ意味
        //MinerオブジェクトのMinerスクリプトを取得
        Miner minerScr = minerObj.GetComponent<Miner>();
        //MinerスクリプトのMining関数に引数を送って実行させる
        minerScr.DoMining(ver1, hor1);
    }
}