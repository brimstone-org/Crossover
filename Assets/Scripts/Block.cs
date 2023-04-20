using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using static GamePlayManager;

public class Block : MonoBehaviour
{
    Rigidbody rbd;
    TMP_Text[] descriptions; //the writing on the side of the block
    private MathStandard myStandard;

    /// <summary>
    /// Called after the block is spawned to assign its characteristics
    /// </summary>
    /// <param name="newMaterial"></param>
    public void SetUpBlock(Material newMaterial, MathStandard standard)
    {
        //get references
        rbd = GetComponent<Rigidbody>();
        descriptions = GetComponentsInChildren<TMP_Text>();
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        if (newMaterial != null)
        {
            Material[] tempMaterials = meshRenderer.materials;
            tempMaterials[0] = newMaterial;
            meshRenderer.materials = tempMaterials;
        }
        //copy the data to my local var;
        myStandard = new MathStandard(standard.id, standard.subject, standard.grade, standard.mastery, standard.domainid, standard.domain, standard.cluster, standard.standardid, standard.standarddescription);

        string status = "";
        switch (standard.mastery)
        {
            case 0:
                status = "Not Learned";
                break;
            case 1:
                status = "Learned";
                break;
            case 2:
                status = "Mastered";
                break;
        }
        for (int i = 0; i < descriptions.Length; i++) //display the proper status
        {
            descriptions[i].text = status;
        }

    }

    /// <summary>
    /// called when simulating "Test my stack"
    /// </summary>
    public void TestStack()
    {
        rbd.isKinematic = false;
        if (myStandard.mastery == 0)
        {
            gameObject.SetActive(false);
        }
    }


    void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            UIManager.instance.DisplayInfo(myStandard);
        }
       
    }

}
