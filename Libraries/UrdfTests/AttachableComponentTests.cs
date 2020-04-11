using RosSharp.Urdf;
using RosSharp.Urdf.Attachables;
using System;
using Xunit;

namespace UrdfTests
{
    public class AttachableComponentTests
    {
        [Fact]
        public void ShouldCreateInstance()
        {
            string xml = UrdfTests.Properties.Resources.xmlResSingleNode;

            AttachableComponentFactory<IAttachableComponent> factory =
                new AttachableComponentFactory<IAttachableComponent>("tooltip")
                {
                    Constructor = () => new AttachedDataValue(),
                };

            Robot.attachableComponentFactories.Add(factory);

            Robot robot = Robot.FromContent(xml);

            var dataValues = factory.ExtractAttachableComponents<AttachedDataValue>(robot);

            Assert.True(robot.attachedComponents.Count > 0);
            Assert.True(robot.attachedComponents[0] != null);
            Assert.True((robot.attachedComponents[0].component as AttachedDataValue).topic == "/test/topic");
            //Assert.True(robot.attachedComponents[0].parentLink != null);
            //Assert.True(robot.attachedComponents[0].component != null);
        }

        [Fact]
        public void ShouldCreateFullRobot()
        {
            string xml = UrdfTests.Properties.Resources.xmlResFullRobot;
            Robot r = Robot.FromContent(xml);

        }
    }
}
