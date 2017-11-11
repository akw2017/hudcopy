using System;

namespace AIC.Domain
{
    public partial class GearComponentWrapper : ModelWrapper<GearComponent>
    {
        public GearComponentWrapper(GearComponent model) : base(model)
        {
        }


        public System.Guid ID
        {
            get { return GetValue<System.Guid>(); }
            set { SetValue(value); }
        }

        public System.Guid IDOriginalValue => GetOriginalValue<System.Guid>(nameof(ID));

        public bool IDIsChanged => GetIsChanged(nameof(ID));

        public System.String Name
        {
            get { return GetValue<System.String>(); }
            set { SetValue(value); }
        }

        public System.String NameOriginalValue => GetOriginalValue<System.String>(nameof(Name));

        public bool NameIsChanged => GetIsChanged(nameof(Name));


        public GearWrapper Component { get; private set; }

        protected override void InitializeComplexProperties(GearComponent model)
        {
            if (model == null)
            {
                throw new ArgumentException("model cannot be null");
            }
            if (model.Component == null)
            {
                throw new ArgumentException("Component cannot be null");
            }
            Component = new GearWrapper(model.Component);
            RegisterComplex(Component);
        }
    }
}
