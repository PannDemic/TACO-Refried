using System;
using System.Text;
using ProtoBuf;

namespace Taco.Classes
{
    [ProtoContract]
    class AlertTrigger
    {
        [ProtoMember(1)]
        public AlertType Type { get; set; }

        [ProtoMember(2)]
        public RangeAlertOperator UpperLimitOperator { get; set; }

        [ProtoMember(3)]
        public RangeAlertOperator LowerLimitOperator { get; set; }

        [ProtoMember(4)]
        public int UpperRange { get; set; }

        [ProtoMember(5)]
        public int LowerRange { get; set; }

        [ProtoMember(6)]
        public RangeAlertType RangeTo { get; set; }

        [ProtoMember(7)]
        public string CharacterName { get; set; }

        [ProtoMember(8)]
        public int SystemId { get; set; }

        [ProtoMember(9)]
        public int SoundId { get; set; }

        [ProtoMember(10)]
        public string SoundPath { get; set; }

        [ProtoMember(11)]
        public bool Enabled { get; set; }

        [ProtoMember(12)]
        public string Text { get; set; }

        [ProtoMember(13)]
        public int RepeatInterval { get; set; }

        public string SystemName { get; set; }

        public DateTime TriggerTime { get; set; }

        public override string ToString()
        {
            StringBuilder tempBuilder = new StringBuilder();

            if (Type == AlertType.Ranged)
            {
                tempBuilder.Append(UpperLimitOperator == RangeAlertOperator.Equal ? "Range = " : "Range <= ");

                tempBuilder.Append(UpperRange);


                if ((UpperLimitOperator == RangeAlertOperator.Equal) || (LowerRange == 0 && LowerLimitOperator == RangeAlertOperator.GreaterThanOrEqual))
                {
                    tempBuilder.Append(UpperRange == 1 ? " jump from: " : " jumps from: ");
                }
                else
                {
                    tempBuilder.Append(UpperRange == 1 ? " jump and" : " jumps and");

                    tempBuilder.Append(LowerLimitOperator == RangeAlertOperator.GreaterThan ? " > " : " >= ");

                    tempBuilder.Append(LowerRange);

                    tempBuilder.Append(LowerRange == 1 ? " jump from: " : " jumps from: ");
                }

                if (RangeTo == RangeAlertType.Home || RangeTo == RangeAlertType.System)
                {
                    tempBuilder.Append(SystemId == -1 ? "Home" : SystemName);
                }
                else
                {
                    tempBuilder.Append( RangeTo == RangeAlertType.AnyCharacter ? "Any Character" : CharacterName);
                }

                tempBuilder.Append(SoundId == -1 ? " (Custom Sound)" : " (" + SoundPath + ")");
            }
            else if (Type == AlertType.Custom)
            {
                tempBuilder.Append("When \"");

                tempBuilder.Append(Text);

                tempBuilder.Append("\" is seen, play (");

                tempBuilder.Append(SoundId == -1 ? "Custom Sound" : SoundPath);

                tempBuilder.Append("). Trigger ");

                if (RepeatInterval == 0)
                    tempBuilder.Append("every detection.");
                else
                {
                    tempBuilder.Append("every ");
                    tempBuilder.Append(RepeatInterval);

                    tempBuilder.Append(RepeatInterval == 1 ? " sec." : " secs.");
                }
            }

            return tempBuilder.ToString();
        }
    }
}
