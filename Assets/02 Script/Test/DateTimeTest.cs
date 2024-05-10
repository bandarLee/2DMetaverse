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

        Debug.Log(tomorrow.Year);
        Debug.Log(tomorrow.Month);
        Debug.Log(tomorrow.Day);
        Debug.Log(tomorrow.Hour);
            
        Debug.Log(tomorrow.Minute);
        Debug.Log(tomorrow.Second);
        Debug.Log(tomorrow.DayOfWeek);

        DateTime person1Birth = new DateTime(2000, 1, 1);
        DateTime person2Birth = new DateTime(2002, 5, 31);
        Debug.Log(person1Birth > person2Birth);
        Debug.Log($"{today.Year}년{today.Month}월{today.Day}일은 {tomorrow.DayOfWeek}임.");

        Debug.Log(today.ToString("yyyy년 MM월dd일 임. ddd이다"));
        Debug.Log(today.ToString("yyyy/MM/dd/ddd"));

        DateTime now2 = DateTime.Now;
        Debug.Log(now2);
        Debug.Log(now2.Kind);

        DateTime utcNow = DateTime.UtcNow;
        Debug.Log(utcNow);
        Debug.Log(utcNow.Kind);

    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
