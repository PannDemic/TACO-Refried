namespace Taco.Classes
{
    [ProtoBuf.ProtoContract]
    class SolarSystemConnectionData
    {
        [ProtoBuf.ProtoMember(1)]
        public int ToSystemId { get; set; }

        [ProtoBuf.ProtoMember(2)]
        public int ToSystemNativeId { get; set; }
        
        [ProtoBuf.ProtoMember(3)]
        public bool IsRegional { get; set; }
    }
}
