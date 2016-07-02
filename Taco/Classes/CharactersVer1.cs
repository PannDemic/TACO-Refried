using ProtoBuf;

namespace Taco.Classes
{
    [ProtoContract]
    class CharactersVer1
    {
        public CharactersVer1() : this(false) { }

        public CharactersVer1(bool useDefaults = false)
        {
            if (useDefaults)
            {
                Names = new string[0]; 
            }
        }

        [ProtoMember(1)]
        public string[] Names { get; set; }
    }
}
