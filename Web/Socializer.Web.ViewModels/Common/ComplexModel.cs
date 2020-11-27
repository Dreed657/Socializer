namespace Socializer.Web.ViewModels.Common
{
    public class ComplexModel<TInput, TView>
    {
        public TInput InputModel { get; set; }

        public TView ViewModel { get; set; }
    }
}
