using ProtoBuf;

namespace Taco.Classes
{
    [ProtoContract]
    class ConfigVerion
    {
        [ProtoMember(1)]
        public int Version { get; set; }
    }
}
