namespace Lessons.Architecture.PM
{
    public interface IPopupView
    {
        public void Show(IViewModel viewModel);
        public void Hide();
    }
}