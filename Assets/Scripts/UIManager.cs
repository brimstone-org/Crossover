using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static GamePlayManager;

public class UIManager : MonoBehaviour
{
    
    GamePlayManager gameplayManager;
    CameraController mainCamera;

    [SerializeField]
    private TMP_Text infoText;
    [SerializeField]
    private GameObject infoPanel;

    public static UIManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        gameplayManager = FindObjectOfType<GamePlayManager>(); //get reference to the gameplay manager
        mainCamera = Camera.main.GetComponent<CameraController>(); //get reference to the camera controller
    }

    /// <summary>
    /// attached to the buttons on the left
    /// </summary>
    /// <param name="stackIndex"></param>
    public void MoveToStack(int stackIndex)
    {
        Transform stackToMoveTo = null;
        switch (stackIndex)
        {
            case 0:
                stackToMoveTo = gameplayManager.sixthGradeStack.transform;
                break;
            case 1:
                stackToMoveTo = gameplayManager.seventhGradeStack.transform;
                break;
            case 2:
                stackToMoveTo = gameplayManager.eighthGradeStack.transform;
                break;
        }
        if (stackToMoveTo != null)
        {
            mainCamera.MoveToStack(stackToMoveTo);
        }
        
    }

    /// <summary>
    /// attached to the TestMyStack button
    /// </summary>
    public void TestMyStack()
    {
        gameplayManager.TestMyStack();
    }


    public void DisplayInfo(MathStandard data)
    {
        infoText.text = "<b>" + "Grade: " + "</b>" + data.grade + "\n\n" + "<b>" + "Domain: " + "</b>" + data.domain +
            "\n\n" + "<b>" + "Cluster: " + "</b>" + data.cluster + "\n\n" + "<b>" + "Standard description: " + "</b>" + data.standarddescription;
        infoPanel.SetActive(true);
    }
}
