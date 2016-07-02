using ProtoBuf;

namespace Taco.Classes
{
    [ProtoContract]
    class CharactersVersion
    {
        [ProtoMember(1)]
        public int Version { get; set; }
    }
}
