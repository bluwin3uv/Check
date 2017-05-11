using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckerBoard : MonoBehaviour
{
    public GameObject blackPic;
    public GameObject whitePic;
    public int boredX = 8, boredZ = 8;
    public float picRad = 0.5f;
    public Pic[,] pices;
    private int halfBoardX, halfBoardZ;
    private float picDie;
    private Vector3 bottomLeft;

	void Start ()
    {
        halfBoardX = boredX / 2;
        halfBoardZ = boredZ / 2;
        picDie = picRad * 2f;
        bottomLeft = transform.position - Vector3.right * halfBoardX - Vector3.forward * halfBoardZ;
        CreateGrid();
	}

    void CreateGrid()
    {
        pices = new Pic[boredX, boredZ];
        #region generate white pices
        for(int x = 0; x < boredX; x += 2)
        {
            for(int z = 0; z < 3; z++)
            {
                bool evenRow = z % 2 == 0;
                int gridX = evenRow ? x : x + 1;
                int gridZ = z;
                GereratePice(whitePic, gridX, gridZ);
            }
        }
        #endregion

        #region gererate blacks
        for (int x = 0; x < boredX; x += 2)
        {
            for (int z = boredZ - 3; z < boredZ; z++)
            {
                bool evenRow = z % 2 == 0;
                int gridX = evenRow ? x : x + 1;
                int gridZ = z;
                GereratePice(blackPic, gridX, gridZ);
            }
        }
        #endregion
    }

    void GereratePice(GameObject picePrefab, int x, int z)
    {
        GameObject clone = Instantiate(picePrefab);
        clone.transform.SetParent(transform);
        Pic pic = clone.GetComponent<Pic>();
        PlacePice(pic, x, z); 
    }

    void PlacePice(Pic pice, int x, int z)
    {
        float xOffset = x * picDie + picRad;
        float zOffset = z * picDie + picRad;
        pice.gridX = x;
        pice.gridZ = z;
        pice.transform.position = bottomLeft + Vector3.right * xOffset + Vector3.forward * zOffset;
        pices[x, z] = pice;
    }

    void Update ()
    {
		
	}
}
