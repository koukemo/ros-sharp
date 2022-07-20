using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using RosSharp.RosBridgeClient;

namespace RosSharp.RosBridgeClient
{
    public class ShigurePeopleSubscriber : UnitySubscriber<MessageTypes.ShigureCoreRos1.PoseKeyPointsList>
    {
        //GameObject part;

        public TextMeshPro TextElement;
        private string LastMsg;
        //private MessageTypes.ShigureCoreRos1.PoseKeyPointsList LastMsg;
        private bool receivedMsg = false;

        protected override void Start() 
        {
            //RosConnector ros_connector = GetComponent<RosConnector>();
            //ros_connector.IsConnected.WaitOne(ros_connector.SecondsTimeout * 1000);
            base.Start();
        }

        protected override void ReceiveMessage(MessageTypes.ShigureCoreRos1.PoseKeyPointsList message)
        {
            receivedMsg = true;
            LastMsg = message.pose_key_points_list[0].people_id;
            //LastMsg = message;
        }
        public void Update()
        {
            if(TextElement != null && receivedMsg)
            {
                TextElement.text = LastMsg;
                //Debug.Log(LastMsg);
                receivedMsg = false;
            }
        }
    }
}
