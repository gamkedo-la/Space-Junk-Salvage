using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHPBar : MonoBehaviour
{

    public GameObject Fill;

    float PercentHP;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        transform.eulerAngles = new Vector3(0, -45, 0);

    }

    public void UpdateHP(float decimalValueHP)
    {

        PercentHP = decimalValueHP;

        Vector3 scale = new Vector3(PercentHP, 1, 1);

        Fill.transform.localScale = scale;

    }


}
