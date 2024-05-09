using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class DateTimeTest : MonoBehaviour
{
    void Start()
    {
        DateTime birth = new DateTime(1999, 8, 13, 09, 00, 09);
        Debug.Log(birth);
        DateTime today = DateTime.Today;
        Debug.Log(today);
        DateTime now = DateTime.Now;
        Debug.Log(now);

        DateTime tomorrow = DateTime.Today.AddDays(1);
        Debug.Log(tomorrow);

        DateTime lastWeek = DateTime.Today.AddDays(-7);
        Debug.Log(lastWeek);

    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
