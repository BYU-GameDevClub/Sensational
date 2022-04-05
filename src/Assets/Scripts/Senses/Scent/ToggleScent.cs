using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleScent : MonoBehaviour
{
    public bool scentActive;
    public Camera scentCamera;
    public Camera sightCamera;
    public Transform player;

    [Range(0, 20)]
    public int scentLevel = 5;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnValidate()
    {
        sightCamera.gameObject.SetActive(!scentActive);
        scentCamera.gameObject.SetActive(scentActive);
        foreach(ScentAssign sa in FindObjectsOfType<ScentAssign>())
        {
            sa.setActive(scentActive, player, scentLevel);
        }
        scentCamera.orthographicSize = scentLevel;
    }
}

//TODO for scent: Make size of effect change based on strength, scentLevel, and distance from player