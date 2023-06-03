using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;  // 要有這個才能控制文字框

public class ShowDistance : MonoBehaviour
{
    public TextMeshProUGUI readingDisplay;  // 顯示是不是正在換彈夾？
    public GameObject Player;
    public GameObject Enemy;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Enemy = GameObject.FindGameObjectWithTag("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        if (readingDisplay != null && Player != null && Enemy != null)
        {
            float distance = Vector3.Distance(Player.transform.position, Enemy.transform.position);

            readingDisplay.SetText(distance.ToString());
        }
           
    }
}
