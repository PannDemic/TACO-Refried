namespace Taco.Classes
{
    [ProtoBuf.ProtoContract]
    class SolarSystemData
    {
        [ProtoBuf.ProtoMember(1)]
        public int Id { get; set; }

        [ProtoBuf.ProtoMember(2)]
        public int NativeId { get; set; }

        [ProtoBuf.ProtoMember(3)]
        public string Name { get; set; }

        [ProtoBuf.ProtoMember(4)]
        public double X { get; set; }

        [ProtoBuf.ProtoMember(5)]
        public double Y { get; set; }

        [ProtoBuf.ProtoMember(6)]
        public double Z { get; set; }

        [ProtoBuf.ProtoMember(7)]
        public SolarSystemConnectionData[] ConnectedTo { get; set; }
    }
}
