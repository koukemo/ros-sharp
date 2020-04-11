using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Reflection;
using static RosSharp.Urdf.Joint;

namespace RosSharp.Urdf
{
    public class AttachableComponent<T> where T : IAttachableComponent
    {
        /// <summary>
        /// The classname in the XML tree structure
        /// </summary>
        public string className;
        public T component;

        public AttachableComponent(string className, T instance)
        {
            this.component = instance;
            this.className = className;
        }


        /// <summary>
        /// Initializes all properties of <typeparamref name="T"/> via reflection
        /// </summary>
        /// <param name="node"></param>
        public void Initialize(XElement node)
        {
            component.name = (string)node.Attribute("name");

            //Read and assign other parameters via reflection
            foreach (var attribute in node.Attributes())
            {
                string value = attribute.Value;
                var property = GetPropertyInfo(attribute.Name.ToString());
                if (property != null)
                {
                    property.SetValue(component, value);
                }
            }

            component.parent = (string)node.Element("parent").Attribute("link");

            //Set the "default" existing objects if they exist in both the class and xml node
            SetIfExists<Origin>(node, "origin", (o) => new Origin(o));
            SetIfExists<Axis>(node, "axis", (o) => new Axis(o));
            SetIfExists<Calibration>(node, "calibration", (o) => new Calibration(o));
            SetIfExists<Dynamics>(node, "dynamics", (o) => new Dynamics(o));
            SetIfExists<Limit>(node, "limit", (o) => new Limit(o));
            SetIfExists<Mimic>(node, "mimic", (o) => new Mimic(o));
        }

        private PropertyInfo GetPropertyInfo(string propName)
        {
            PropertyInfo prop = component.GetType().GetProperty(propName, BindingFlags.Public | BindingFlags.Instance);
            if (prop != null && prop.CanWrite)
            {
                return prop;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Sets the field of type <typeparamref name="K"/> if the property with the same name exists and the node is present in <paramref name="node"/> child-tree
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <param name="node"></param>
        /// <param name="name"></param>
        /// <param name="constructor"></param>
        private void SetIfExists<K>(XElement node, string name, Func<XElement, K> constructor)
        {
            var property = GetPropertyInfo(name);
            if (property == null)
                //property does not even exist on object. We can skip this one
                return;

            if(node.Element(name) != null)
            {
                property.SetValue(component, constructor(node.Element(name)));
            }
        }
        
    }
}
