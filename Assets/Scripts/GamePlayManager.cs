using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class GamePlayManager : MonoBehaviour
{
    [System.Serializable]
    public class MathStandard
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

        public MathStandard(string id, string subject, string grade, int mastery, string domainid, string domain, string cluster, string standardid, string standarddescription)
        {
            this.id = id;
            this.subject = subject;
            this.grade = grade;
            this.mastery = mastery;
            this.domainid = domainid;
            this.domain = domain;
            this.cluster = cluster;
            this.standardid = standardid;
            this.standarddescription = standarddescription;
        }
    }

    [SerializeField]
    private string apiUrl; //the url for the API

    private List<MathStandard> mathStandards = new List<MathStandard>();
    public Stack sixthGradeStack;
    public Stack seventhGradeStack;
    public Stack eighthGradeStack;

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
            List<MathStandard> sixthGradeData = mathStandards.Where(x => x.grade.Equals("6th Grade")).ToList();
            sixthGradeStack.SetupStack(sixthGradeData, "6th Grade");
            List<MathStandard> seventhGradeData = mathStandards.Where(x => x.grade.Equals("7th Grade")).ToList();
            seventhGradeStack.SetupStack(seventhGradeData, "7th Grade");
            List<MathStandard> eighthGradeData = mathStandards.Where(x => x.grade.Equals("8th Grade")).ToList();
            eighthGradeStack.SetupStack(eighthGradeData, "8th Grade");
        }
        ));
        //move camera in position at the beginning
        Camera.main.GetComponent<CameraController>().MoveToStack(sixthGradeStack.transform);
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


    #region gameplay

    /// <summary>
    /// called when pressing the "Test my stack button"
    /// </summary>
    public void TestMyStack()
    {
        sixthGradeStack.TestMyStack();
        seventhGradeStack.TestMyStack();
        eighthGradeStack.TestMyStack();
    }

    #endregion

}
