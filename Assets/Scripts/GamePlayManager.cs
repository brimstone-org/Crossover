using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class GamePlayManager : MonoBehaviour
{

    [SerializeField]
    private string apiUrl; //the url for the API

    [System.Serializable]
    class MathStandard
    {
        public string id = string.Empty;
        public string subject = string.Empty;
        public string grade = string.Empty;
        public int mastery = 0;
        public string domainid = string.Empty;
        public string domain = string.Empty;
        public string cluster = string.Empty;
        public string standardid = string.Empty;
        public string standarddescription = string.Empty;
    }

    private List<MathStandard> mathStandards = new List<MathStandard>();

    private void Start()
    {
        GetData();    
    }


    #region API

    /// <summary>
    /// calls the API to retrieve the data
    /// </summary>
    private void GetData()
    {
        UnityWebRequest page = UnityWebRequest.Get(apiUrl);
        StartCoroutine(CallDatabase(page, (code, response)=> 
        {
            string serviceData = "{\"Items\":" + response + "}";
            mathStandards = Utilities.JsonHelper.FromJson<MathStandard>(serviceData).ToList();
            
        }
        )); 
    }

    private IEnumerator CallDatabase(UnityWebRequest www, Action<long, string> onComplete = null)
    {
        yield return www.SendWebRequest();
        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Failed to reach server");
        }
        else
        {
            onComplete?.Invoke(www.responseCode, www.downloadHandler.text);
        }
        www.Dispose();
    }
    #endregion


}
