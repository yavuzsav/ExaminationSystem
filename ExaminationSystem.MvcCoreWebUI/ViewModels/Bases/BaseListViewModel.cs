namespace ExaminationSystem.MvcCoreWebUI.ViewModels.Bases
{
    public class BaseListViewModel
    {
        public int PageCount { get; internal set; }
        public int PageSize { get; internal set; }
        public int CurrentPage { get; internal set; }
    }
}