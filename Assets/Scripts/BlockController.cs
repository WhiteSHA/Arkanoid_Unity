using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    public GameObject[] prefabs;
    // Start is called before the first frame update
    void Start()
    {
        nextLevel();
        GlobalVariables.createBlocks = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GlobalVariables.createBlocks)
        {
            GlobalVariables.createBlocks = false;
            nextLevel();
        }
    }

    void nextLevel()
    {
        if (!MainMenu.gameIsStarted)
            return;

        GlobalVariables.level++;

        List<GameObject> blocks = new List<GameObject>();

        int prefabCount = prefabs.Length;
        for (int i = 0; i < prefabs.Length; i++)
        {
            int x = Random.Range(-12, 12);
            while (x % 3 != 0)
                x = Random.Range(-13, 13);
            int y = Random.Range(8, 15);

            GameObject newBlock = Instantiate(prefabs[i], new Vector3(x, y, 0), new Quaternion());

            newBlock.GetComponent<Renderer>().sharedMaterial.color = new Color(Random.Range(0, 3), Random.Range(0, 3), Random.Range(0, 2)); ;

            blocks.Add(newBlock);
        }

        // check dublicates
        for (int i = 0; i < blocks.Count; i++)
        {
            for (int j = i + 1; j < blocks.Count; j++)
            {
                if (blocks[i].transform.position.x == blocks[j].transform.position.x && blocks[i].transform.position.y == blocks[j].transform.position.y && i != j)
                {
                    Debug.Log("Dublicate is identified x = " + blocks[i].transform.position.x + " y = " + blocks[i].transform.position.y + 
                        " j.x = " + blocks[j].transform.position.x + " j.y = " + blocks[j].transform.position.y + " i = " + i + " j = " + j);

                    Destroy(blocks[i]);
                    blocks.RemoveAt(i);

                    i--;
                    j--;
                    if (i < 0)
                        i = 0;
                    if (j < 0)
                        j = 0;
                }
            }
        }

        GlobalVariables.countOfVblocks = (short)blocks.Count;
        Debug.Log("Count of blocks = " + GlobalVariables.countOfVblocks);

        return;
        int countOfBlocks = GameObject.FindGameObjectsWithTag("block").Length;
        if (GlobalVariables.countOfVblocks != countOfBlocks)
        {
            GlobalVariables.countOfVblocks = 0;
            GameObject[] bl = GameObject.FindGameObjectsWithTag("block");
            for (int i = 0; i < countOfBlocks; i++)
            {
                
            }
        }
    }
}
