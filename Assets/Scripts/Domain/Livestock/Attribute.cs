namespace Planetoid.Livestock {
    public class Attribute
    {
        private AttributeType type;
        private float value;
        public AttributeType Type { get => type; set => type = value; }
        public float Value { get => value; set => this.value = value; }
        public Attribute() { }

        public Attribute(AttributeType type, float value)
        {
            this.type = type;
            this.value = value;
        }
    }
}