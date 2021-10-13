using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageText : MonoBehaviour
{
    float speed = .5f;
    public int dam;
    Vector3 temp;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<TextMeshPro>().text = dam.ToString();
        Destroy(gameObject, 0.3f);
    }

    // Update is called once per frame
    void Update()
    {

        temp = transform.position;
        temp.y += speed * Time.deltaTime;
        transform.position = temp;
    }
}
