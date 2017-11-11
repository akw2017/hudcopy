using AIC.Core;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System;

namespace AIC.Domain
{
    public class ChannelIdentity  :ValueObject<ChannelIdentity>
    {
        private ChannelIdentity(string ip, string cardNum, string channelNum)
        {
            IP = ip;
            CardNum = cardNum;
            ChannelNum = channelNum;
        }

        public static Result<ChannelIdentity> Create(string ip, string cardNum, string channelNum)
        {
            if (!Regex.IsMatch(ip, @"\b(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\b"))
                return Result.Fail<ChannelIdentity>($"无效的ip地址{ip}");
            if (string.IsNullOrEmpty(cardNum))
                return Result.Fail<ChannelIdentity>("插槽号不能为空");
            if (string.IsNullOrEmpty(channelNum))
                return Result.Fail<ChannelIdentity>("通道号不能为空");

            return Result.Ok(new ChannelIdentity(ip, cardNum, channelNum));
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
        public string CardNum { get; }
        public string ChannelNum { get; }
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
