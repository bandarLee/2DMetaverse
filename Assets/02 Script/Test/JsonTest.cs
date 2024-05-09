using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;


[System.Serializable]
public class MyTextData
{
        public string Name;

    public int Age;
    public string[] Hobby;
}
[System.Serializable]
public class MyTextDataArray
{
    public MyTextData[] data;
}

public class JsonTest : MonoBehaviour
{
    TextAsset textData;

    MyTextDataArray myText;
    private void Start()
    {
        textData = Resources.Load("person") as TextAsset;
        myText = JsonUtility.FromJson<MyTextDataArray>(textData.ToString());
        foreach (var person in myText.data)
        {
            Debug.Log($"이름: {person.Name}");
            Debug.Log($"나이: {person.Age}");
            string hobbies = string.Join(", ", person.Hobby);
            Debug.Log($"취미: {hobbies}");
            

        }


    }
}
