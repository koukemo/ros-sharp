/* 
 * This message is auto generated by ROS#. Please DO NOT modify.
 * Note:
 * - Comments from the original code will be written in their own line 
 * - Variable sized arrays will be initialized to array of size 0 
 * Please report any issues at 
 * <https://github.com/siemens/ros-sharp> 
 */



using RosSharp.RosBridgeClient.MessageTypes.Std;

namespace RosSharp.RosBridgeClient.MessageTypes.ShigureCoreRos1
{
    public class ContactedList : Message
    {
        public override string RosMessageName => "shigure_core_ros1_msgs/ContactedList";

        public Header header { get; set; }
        public Contacted[] contacted_list { get; set; }

        public ContactedList()
        {
            this.header = new Header();
            this.contacted_list = new Contacted[0];
        }

        public ContactedList(Header header, Contacted[] contacted_list)
        {
            this.header = header;
            this.contacted_list = contacted_list;
        }
    }
}
