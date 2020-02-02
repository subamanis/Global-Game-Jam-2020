using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCameraFocus : MonoBehaviour
{
    private const int time = 1;
    List<Transform> bodyParts = new List<Transform>();
    public Camera mainCamera;
    public CinemachineVirtualCamera virtualCamera;
    int listCounter = 0;
    // Start is called before the first frame update
    void Start()
    {
        Transform partsParent = FindObjectOfType<Space>().transform.Find("BodyParts");
        foreach(Transform child in partsParent.transform)
        {
            bodyParts.Add(child);
        }      
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            print("yes");
            Transform nextPart = GetNextPart();
            //virtualCamera.LookAt = nextPart;
            virtualCamera.Follow = nextPart;
            //virtualCamera.GetComponent<CinemachineBrain>().m_DefaultBlend.m_Style = 
        }
    }

    private Transform GetNextPart()
    {
        if(listCounter == bodyParts.Count - 1)
        {
            listCounter = 0;
        }
        else
        {
            ++listCounter;
        }
        return bodyParts[listCounter];
    }

    public void InitiateCameraChange(GameObject obj)
    {
    }
}
