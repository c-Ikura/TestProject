using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallManager : MonoBehaviour
{
    public GameObject[] walls;
    private Queue<GameObject> disableWalls;

    private void Awake()
    {
        disableWalls = new Queue<GameObject>();
    }
    private void OnEnable()
    {
        EventCenter.onWallDisable += FindDisableWall;
    }
    private void OnDisable()
    {
        EventCenter.onWallDisable -= FindDisableWall;
        walls = null;
        disableWalls.Clear();
    }

    public void FindDisableWall()
    {
        for (var i = 0; i < walls.Length; i++)
        {
            if (walls[i].activeSelf)
            {

            }
            else
            {
                disableWalls.Enqueue(walls[i]);

            }
        }

        StartCoroutine(SetWallEnable());
    }

    public IEnumerator SetWallEnable()
    {
        while (disableWalls.Count > 0)
        {
            float radomTime = Random.Range(0.5f, 1.5f);
            yield return new WaitForSeconds(radomTime);
            if (disableWalls.TryDequeue(out GameObject wall))
            {
                wall.SetActive(true);
            }

        }
    }



}
