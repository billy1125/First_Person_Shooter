using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;  // �n���o�Ӥ~�౱���r��

public class ShowDistance : MonoBehaviour
{
    public TextMeshProUGUI readingDisplay;  // ��ܬO���O���b���u���H
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
