using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class TimeSpanTest : MonoBehaviour
{
    private void Start()
    {
/*        TimeSpan twoHour = new TimeSpan(2, 0, 0);
        Debug.Log(twoHour);
        Debug.Log(twoHour.TotalMinutes);
        Debug.Log(twoHour.TotalSeconds);

        DateTime xMasDate = new DateTime(DateTime.Today.Year, 12, 25);
        DateTime today = DateTime.Today;

        TimeSpan diff = xMasDate - today;
        Debug.Log(diff.TotalDays);*/
        DateTime Day1 = new DateTime(DateTime.Today.Year, 2, 26);
        DateTime today = DateTime.Today;
        DateTime Day100 = Day1.AddDays(99);
        Debug.Log(Day100.ToString("100일 후는 mm/dd(ddd요일)이다."));

        TimeSpan LeftTIme = (Day100 - today);



        Debug.Log($"남은시간은 {LeftTIme}");
    }
}
