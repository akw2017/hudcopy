namespace AIC.ModelWrapperGenerator
{
    public class RelayChannelModel : BaseChannelModel
    {
        private string expression;
        public string Expression
        {
            get { return expression; }
            set { SetProperty(ref expression, value); }
        }
    }
}
