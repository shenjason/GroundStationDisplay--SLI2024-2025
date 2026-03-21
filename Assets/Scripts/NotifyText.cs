using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NotifyText : MonoBehaviour
{
    public float speed = 5;

    public float timePassed = 0;

    public TMP_Text textComp;

    void Update()
    {
        transform.position += Vector3.up * speed * Time.deltaTime;
        timePassed += Time.deltaTime;


        if (timePassed > 1.5)
        {
            Destroy(gameObject);
        }

    }


    public void setText(string text)
    {
        textComp.text = text;
    }
}
