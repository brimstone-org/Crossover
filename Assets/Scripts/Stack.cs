using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using static GamePlayManager;

public class Stack : MonoBehaviour
{
    [SerializeField]
    private List<Transform> spawnPoints;
    [SerializeField]
    private TMP_Text stackName;
    [SerializeField]
    private Block blockPrefab;
    [SerializeField]
    private Material glassMat;
    [SerializeField]
    private Material woodMat;
    [SerializeField]
    private Material stoneMat;
    List<Block> myBlocks =  new List<Block>();

    /// <summary>
    /// sets up the stack at the beginning of the game
    /// </summary>
    /// <param name="mathStandards"></param>
    public void SetupStack(List<MathStandard> mathStandards, string stackNameText)
    {
        //name the stacks
        stackName.text = stackNameText;
        //order the blocks as requested
        mathStandards = mathStandards.OrderBy(x => x.domain).ThenBy(y => y.cluster).ThenBy(z => z.standardid).ToList();

        int step = -1; //keeps track of when to loop through the six spawn points
        int heightStep = -1; //keeps track of when to raise the height of the blocks
        for (int i = 0; i < mathStandards.Count; i++)
        {
            if (i%6 == 0) //increase the step every 6 increments because each stack has six spawn points
            {
                step++;
            }
            Block newBlock = Instantiate(blockPrefab, spawnPoints[i - step * 6]);
            if (i%3 == 0)
            {
                heightStep++;
                
            }
            newBlock.transform.localPosition = new Vector3(0, heightStep + 0.5f, 0f);
            Material materialToAssign = null;
            switch (mathStandards[i].mastery)
            {
                case 0:
                    materialToAssign = glassMat;
                    break;
                case 1:
                    materialToAssign = woodMat;
                    break;
                case 2:
                    materialToAssign = stoneMat;
                    break;
            }
            newBlock.SetUpBlock(materialToAssign, mathStandards[i]);
            myBlocks.Add(newBlock);

        }
    }

    /// <summary>
    /// enables rigidbodies
    /// </summary>
    public void TestMyStack()
    {
        for (int i = 0; i < myBlocks.Count; i++)
        {
            myBlocks[i].TestStack();
        }
    }
}
