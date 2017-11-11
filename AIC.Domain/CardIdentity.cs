using AIC.Core;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System;

namespace AIC.Domain
{
    public class CardIdentity : ValueObject<CardIdentity>
    {
        private CardIdentity(string ip, string cardNum)
        {
            IP = ip;
            CardNum = cardNum;
        }

        public static Result<CardIdentity> Create(string ip, string cardNum)
        {
            if (!Regex.IsMatch(ip, @"\b(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\b"))
                return Result.Fail<CardIdentity>($"无效的ip地址{ip}");
            if (string.IsNullOrEmpty(cardNum))
                return Result.Fail<CardIdentity>("插槽号不能为空");
        

            return Result.Ok(new CardIdentity(ip, cardNum));
        }

        protected override bool EqualsCore(CardIdentity other)
        {
            return IP == other.IP && CardNum == other.CardNum;
        }

        protected override int GetHashCodeCore()
        {
            unchecked
            {
                int hashCode = IP.GetHashCode();
                hashCode = (hashCode * 397) ^ CardNum.GetHashCode();
                return hashCode;
            }
        }

        public override string ToString()
        {
            return $"IP:{IP},CardNum:{CardNum}";
        }

        public string IP { get; }
        public string CardNum { get; }
    }

    public class CardIdentityComparer : IEqualityComparer<CardIdentity>
    {
        public bool Equals(CardIdentity x, CardIdentity y)
        {
            x.Equals(y);
            return x.IP == y.IP && x.CardNum == y.CardNum;
        }

        public int GetHashCode(CardIdentity obj)
        {
            return obj.GetHashCode();
        }
    }
}
