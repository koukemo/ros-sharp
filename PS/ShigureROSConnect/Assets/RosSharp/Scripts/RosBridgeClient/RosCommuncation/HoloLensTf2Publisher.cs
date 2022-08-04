using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RosSharp.RosBridgeClient
{
    public class HoloLensTf2Publisher : UnityPublisher<MessageTypes.Tf2.TFMessage>
    {
        
        //public Transform ParentAnchor;
        public string FrameId = "World";
        public string Child_FrameId = "Hololens2_1";
        Vector3 pos;
        Vector3 dirforward;
        Vector3 dirup;

        GameObject targetObject;
        
        private MessageTypes.Geometry.TransformStamped message;
        private MessageTypes.Tf2.TFMessage message2;

        protected override void Start()
        {
            targetObject = Camera.main.gameObject;

            base.Start();
            InitializeMessage();
        }

        public void OnClickStart()
        {
            base.Start();
            InitializeMessage();
        }

        private void FixedUpdate()
        {
            pos =targetObject.transform.position;
            dirforward = targetObject.transform.forward;
            dirup =targetObject.transform.up;
            UpdateMessage(Child_FrameId ,pos, dirforward, dirup);
        }

        private void InitializeMessage()
        {
            message2 = new MessageTypes.Tf2.TFMessage { };

            message = new MessageTypes.Geometry.TransformStamped
            {
                header = new MessageTypes.Std.Header()
                {
                    frame_id = FrameId
                }


            };   
        }

        private void UpdateMessage(string child, Vector3 pos, Vector3 dirforward, Vector3 dirup)
        {
            this.transform.forward = dirforward;
            dirforward = this.transform.forward;
            this.transform.up = dirup;
            dirup = this.transform.up;
            this.transform.rotation = Quaternion.LookRotation(dirforward, dirup);
            message.header.Update();
            message.child_frame_id = child;
            GetGeometryPoint(pos.Unity2Ros(), message.transform.translation);
            GetGeometryQuaternion(this.transform.rotation.Unity2Ros(), message.transform.rotation);
            MessageTypes.Geometry.TransformStamped [] TFmessage = new MessageTypes.Geometry.TransformStamped[] {message};


            message2.transforms = TFmessage;
            Publish(message2);
        }

        private static void GetGeometryPoint(Vector3 position, MessageTypes.Geometry.Vector3 geometryPoint)
        {
            geometryPoint.x = position.x;
            geometryPoint.y = position.y;
            geometryPoint.z = position.z;
        }

        private static void GetGeometryQuaternion(Quaternion quaternion, MessageTypes.Geometry.Quaternion geometryQuaternion)
        {
            geometryQuaternion.x = quaternion.x;
            geometryQuaternion.y = quaternion.y;
            geometryQuaternion.z = quaternion.z;
            geometryQuaternion.w = quaternion.w;
        }

    }
}

