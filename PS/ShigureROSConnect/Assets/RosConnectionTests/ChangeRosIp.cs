using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RosSharp.RosBridgeClient;

namespace RosSharp.RosBridgeClient
{
    public class ChangeRosIp : MonoBehaviour
    {
        public GameObject ros_connector;
        

        // Start is called before the first frame update
        void Start() {}

        // Update is called once per frame
        void Update() {}

        public void OnClick_ip_lab()
        {
            var ros_connector_info = ros_connector.GetComponent<RosConnector>();   
            ros_connector_info.RosBridgeServerUrl = "ws://10.40.0.87:9090";
        }

        public void OnClick_ip_home()
        {
            var ros_connector_info = ros_connector.GetComponent<RosConnector>();   
            ros_connector_info.RosBridgeServerUrl = "ws://192.168.11.20:9090";
        }
    }
}
