using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class Wall : MonoBehaviour
{
    public List<GameObject> wallPrefabList;
    private List<GameObject> walls;
    public bool startDisable;
    public float curDisableTime;
    public float disableTime;

    public Text text;


    private void Awake()
    {
        walls = new List<GameObject>();

        var timeObj = GameObject.Find("Timer");
        var time = Instantiate(timeObj, transform);
        time.transform.position = transform.position;

        text = time.GetComponentInChildren<Text>();


    }
    private void OnEnable()
    {
        curDisableTime = disableTime;
        text.text = curDisableTime + "";

        startDisable = false;
        CreatWall();
    }
    private void OnDisable()
    {
        ClearWall();
    }
    public void CreatWall()
    {
        for (var i = -4; i < 5; i += 2)
        {
            Vector2 wallPos = new Vector2(i, 0);
            int randomIndex = Random.Range(0, wallPrefabList.Count);

            int normal = 0;//保证至少要出一个normal墙壁
            if (randomIndex != 0)
            {
                normal++;
                if (normal == 5)
                    randomIndex = 0;
            }

            var wallObj = Instantiate(wallPrefabList[randomIndex], transform);
            wallObj.transform.localPosition = wallPos;
            walls.Add(wallObj);
        }
    }
    public void ClearWall()
    {
        foreach (var wall in walls)
        {
            Destroy(wall);
        }

        walls.Clear();
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            startDisable = true;
        }
    }

    private void Update()
    {
        if (startDisable)
        {
            curDisableTime -= Time.deltaTime;
            text.text = "" + curDisableTime;
            if (curDisableTime <= 0)
            {
                gameObject.SetActive(false);
                EventCenter.OnWallDisable();
            }
        }
    }


}
