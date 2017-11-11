using AIC.Core;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System;

namespace AIC.ModelWrapperGenerator
{
    public class ChannelIdentity  :ValueObject<ChannelIdentity>
    {
        private ChannelIdentity(string ip, int slotNum, int chaN)
        {
            IP = ip;
            CardNum = slotNum;
            ChannelNum = chaN;
        }

        public static Result<ChannelIdentity> Create(string ip, int slotNum, int chaN)
        {
            if (!Regex.IsMatch(ip, @"\b(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\b"))
                return Result.Fail<ChannelIdentity>($"无效的ip地址{ip}");
            if (slotNum < 0)
                return Result.Fail<ChannelIdentity>($"插槽号不能为负数{slotNum}");
            if (chaN < 0)
                return Result.Fail<ChannelIdentity>($"通道号不能为负数{chaN}");

            return Result.Ok(new ChannelIdentity(ip, slotNum, chaN));
        }

        protected override bool EqualsCore(ChannelIdentity other)
        {
            return IP == other.IP && CardNum == other.CardNum && ChannelNum == other.ChannelNum;
        }

        protected override int GetHashCodeCore()
        {
            unchecked
            {
                int hashCode = IP.GetHashCode();
                hashCode = (hashCode * 397) ^ CardNum.GetHashCode();
                hashCode = (hashCode * 397) ^ ChannelNum.GetHashCode();
                return hashCode;
            }
        }

        public override string ToString()
        {
            return $"IP:{IP},CardNum:{CardNum},ChannelNum:{ChannelNum}";
        }

        public string IP { get; }
        public int CardNum { get; }
        public int ChannelNum { get; }
    }

    public class ChannelIdentityComparer : IEqualityComparer<ChannelIdentity>
    {
        public bool Equals(ChannelIdentity x, ChannelIdentity y)
        {
            x.Equals(y);
            return x.IP == y.IP && x.CardNum == y.CardNum && x.ChannelNum == y.ChannelNum;
        }

        public int GetHashCode(ChannelIdentity obj)
        {
            return obj.GetHashCode();
        }
    }
}
