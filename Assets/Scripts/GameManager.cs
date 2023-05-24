using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("UI�]�w")]
    public Image lifeBarImage;


    // Start is called before the first frame update
    void Start()
    {
        PlayerController.PlayerDamage += UpdateLifeBar;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        // �����q�\�ƥ�  
        PlayerController.PlayerDamage -= UpdateLifeBar;
    }

    private void UpdateLifeBar(float _value)
    {
        lifeBarImage.fillAmount = _value;
    }
}
