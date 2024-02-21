using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlatforms : MonoBehaviour
{
    public List<GameObject> platforms = new List<GameObject>(); // lista do objs
    public List<Transform> currentPlatforms = new List<Transform>(); //lista dos obj instanciáveis

    public float offSet;
    public float indexTransitionPlatform = 5;

    private Transform player;
    private Transform currentPlatformPoint;
    private int platformIndex;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        for (int i = 0; i < platforms.Count; i++)
        {
           Transform p = Instantiate(platforms[i], new Vector3(0,0,i * 269.5f), transform.rotation).transform;
            currentPlatforms.Add(p);
            offSet += 269.5f;
        }

        currentPlatformPoint = currentPlatforms[platformIndex].GetComponent<Platforms>().point;
    }


    public GameObject myPlatform;
    
    // Update is called once per frame
    void Update()
    {
        float distance = player.position.z - currentPlatformPoint.position.z;


        if(distance >= indexTransitionPlatform)
        {
            Recycle(currentPlatforms[platformIndex].gameObject);
            platformIndex++;

            if (platformIndex > currentPlatforms.Count - 1)
            {
                platformIndex = 0;

            }

            currentPlatformPoint = currentPlatforms[platformIndex].GetComponent<Platforms>().point;
           
        }

        //test
        //if (Input.GetKeyDown(KeyCode.S))
        //{
        //    Recycle(myPlatform);
        //}   
    }

    public void Recycle(GameObject platform)
    {
        platform.transform.position = new Vector3(0, 0, offSet);
        offSet += 269.5f;
    }


   
}
