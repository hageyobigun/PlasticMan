using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class Test : MonoBehaviour
{

    [SerializeField] private GameObject test = default;

    [SerializeField]
    private float distanceMove = 1;

    [SerializeField]
    private float animationSpeed = 1;

    private TextMeshProUGUI textMeshPro;


    private void Awake()
    {
        textMeshPro = test.GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        // メッシュ更新
        textMeshPro.ForceMeshUpdate(true);

        //テキストメッシュプロの情報
        var textInfo = textMeshPro.textInfo;

        //テキスト数がゼロであれば表示しない
        if (textInfo.characterCount == 0)
        {
            return;
        }


        //1文字毎にloop
        for (int index = 0; index < textInfo.characterCount; index++)
        {
            //1文字単位の情報
            var charaInfo = textInfo.characterInfo[index];

            //ジオメトリない文字はスキップ
            if (!charaInfo.isVisible)
            {
                continue;
            }

            //Material参照しているindex取得
            int materialIndex = charaInfo.materialReferenceIndex;

            //頂点参照しているindex取得
            int vertexIndex = charaInfo.vertexIndex;

            //テキスト全体の頂点を格納(変数のdestは、destinationの略)
            Vector3[] destVertices = textInfo.meshInfo[materialIndex].vertices;

            //移動する分
            float sinValue = Mathf.Sin(Time.time * animationSpeed + 10 * index);

            // メッシュ情報にアニメーション後の頂点情報を入れる
            destVertices[vertexIndex + 0] += distanceMove * (Vector3.down * sinValue);
            destVertices[vertexIndex + 1] += distanceMove * (Vector3.down * sinValue);
            destVertices[vertexIndex + 2] += distanceMove * (Vector3.down * sinValue);
            destVertices[vertexIndex + 3] += distanceMove * (Vector3.down * sinValue);
        }

        //ジオメトリ更新
        for (int i = 0; i < textInfo.meshInfo.Length; i++)
        {
            //メッシュ情報を、実際のメッシュ頂点へ反映
            textInfo.meshInfo[i].mesh.vertices = textInfo.meshInfo[i].vertices;
            textMeshPro.UpdateGeometry(textInfo.meshInfo[i].mesh, i);
        }
    }


    private void AaTest()
    {
        //var punch = new Vector3(0, 50f, 0); // 力
        //var duration = 10f;          // 時間
        //var vibrato = 1;          // 揺れ度合い
        //var elasticity = 10f;          // 弾力
        //var snapping = false;       // 値を整数に変換するか
        //test.transform.DOPunchPosition(punch, duration, vibrato, elasticity, snapping).SetLoops(-1);
    }

}

