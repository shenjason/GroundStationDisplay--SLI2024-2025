using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotifyManeger : MonoBehaviour
{
    public GameObject notifyObject;
    // Start is called before the first frame update

    public void SpawnNotification(string text)
    {
        NotifyText nf = Instantiate(notifyObject).GetComponent<NotifyText>();
        nf.transform.position = transform.position;
        nf.transform.SetParent(transform);
        
        nf.setText(text);
    }
}
