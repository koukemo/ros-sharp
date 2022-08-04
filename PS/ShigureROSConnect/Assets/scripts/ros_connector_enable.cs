using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RosSharp.RosBridgeClient;

public class ros_connector_enable : MonoBehaviour
{
    [SerializeField] private GameObject ros_connector;
    [SerializeField] private GameObject console_controller;
    [SerializeField] private GameObject ip_controller;

    private int click_count = 1;

    private RosConnector ros_connector_script;
    private ShigurePeopleSubscriber ros_shigure_people_script;

    // Start is called before the first frame update
    void Start() 
    {
       ros_connector_script = ros_connector.GetComponent<RosConnector>();
       ros_shigure_people_script = ros_connector.GetComponent<ShigurePeopleSubscriber>();
    }

    // Update is called once per frame
    void Update() {}

    public void onClick()
    {
        if (click_count == 1)
        {
            ros_connector.SetActive(true);
            console_controller.SetActive(true);
            ip_controller.SetActive(false);
        }
        else if (click_count % 2 == 1)
        {
            ros_connector_script.Awake();
            ros_shigure_people_script.OnClickStart();
            ros_connector.SetActive(true);
            console_controller.SetActive(true);
            ip_controller.SetActive(false);
        } else
        {
            ros_connector_script.OnClickQuit();
            ros_connector.SetActive(false);
            console_controller.SetActive(false);
            ip_controller.SetActive(true);
        }

        click_count++;
        Debug.Log(click_count);
    }
}
