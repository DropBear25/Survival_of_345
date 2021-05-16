using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadarBlips : MonoBehaviour
{
    //image position 
    public Image image;



    void Start()
    {
        Radar.RegisterRadarObject(this.gameObject, image);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     void OnDestroy()
    {
        Radar.RemoveRadarObject(this.gameObject);
    }


}
