using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ros_connector_enable : MonoBehaviour
{
    [SerializeField] private GameObject ros_connector;
    [SerializeField] private GameObject console_controller;

    // Start is called before the first frame update
    void Start() {}

    // Update is called once per frame
    void Update() {}

    public void onClick()
    {
        ros_connector.SetActive(true);
        console_controller.SetActive(true);
    }
}
