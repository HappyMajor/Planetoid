using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Planetoid.Livestock
{
    [Serializable]
    public class Livestock 
    {
        public string Description { get => description; set => description = value; }
        public string Name { get => name; set => name = value; }

        private string name = "";
        private string description = "";
        private List<Attribute> attributes = new List<Attribute>();

        public Livestock()
        {
        }

        public void AddAttribute(Attribute attribute)
        {
            this.attributes.Add(attribute);
        }

        public void RemoveAttribute(Attribute attribute)
        {
            this.attributes.Remove(attribute);
        }

        public Attribute GetAttribute(AttributeType type)
        {
            foreach (Attribute attribute in this.attributes)
            {
                if (attribute.Type == type) return attribute;
            }
            return null;
        }

        public void Update(float deltaTimeMillis)
        {
            
        }
    }
}
