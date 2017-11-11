//using NullGuard;

namespace AIC.Domain
{
    public class Gear : IMach
    {
        //[AllowNull]
        public string Name { get; set; }
        public int TeethNumber { get; set; }

        //public Gear Clone()
        //{
        //    return new Gear()
        //    {
        //        Name = this.Name,
        //        TeethNumber = this.TeethNumber,
        //    };
        //}

        //IMach IMach.Clone()
        //{
        //    return Clone();
        //}
    }
}
