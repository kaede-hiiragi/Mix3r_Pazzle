using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//UIを使うとき追加

public class ClearPoint : MonoBehaviour {

    [SerializeField] private CanvasGroup a;//CanvasGroup型の変数aを宣言　あとでCanvasGroupをアタッチする


    bool flag=false;
    float t = 0.0f;
    void Start () {
            a.alpha = 0.0f;
    }

    private void OnTriggerEnter(Collider other)
        {
            flag=true;
        }
    void Update () {
        if(flag==true){
            t+=0.005f;
            a.alpha=-(t-1)*(t-1)+1;
            if(a.alpha==0.0f){
                flag=false;
                t = 0.0f;
            }
        }
            }
        }
