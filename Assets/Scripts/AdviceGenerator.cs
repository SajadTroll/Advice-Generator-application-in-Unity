using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using System;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine.UI;

public class AdviceGenerator : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI adviceText;
    [SerializeField] Button btn;

    void Start()
    {
        GetAdvice();
    }

    public void GetAdvice()
    {
        adviceText.text = "Loading...";
        StartCoroutine(SendAdviceRequest());
    }

    public IEnumerator SendAdviceRequest()
    {
        string url = "https://api.adviceslip.com/advice";
        using (var request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.Success)
            {
                MyClass myClass = new MyClass();
                myClass = JsonUtility.FromJson<MyClass>(request.downloadHandler.text);
                adviceText.text = "“" + myClass.slip.advice + "”";
            }
            else
            {
                adviceText.text = "“check your internet connection and try again :)”";
            }

            btn.interactable = true;
        }
    }

    [Serializable]
    public class MyClass
    {
        [Serializable]
        public class Slip
        {
            public string id;
            public string advice;
        }

        public Slip slip; 
    }
}