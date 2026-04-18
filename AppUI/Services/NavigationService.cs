namespace Laboratory_ProductManager.AppUI.Services;

public interface INavigationService
{
    object CurrentView { get; }
    void NavigateTo<T>(Action<T> initialize = null) where T : class;
    event Action CurrentViewChanged;
}

public class NavigationService : INavigationService
{
    private readonly IServiceProvider _serviceProvider;
    private object _currentView;

    public NavigationService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public event Action CurrentViewChanged;

    public object CurrentView
    {
        get => _currentView;
        private set
        {
            _currentView = value;
            CurrentViewChanged?.Invoke();
        }
    }

    public void NavigateTo<T>(Action<T> initialize = null) where T : class
    {
        var viewModel = _serviceProvider.GetRequiredService<T>();

        initialize?.Invoke(viewModel);

        CurrentView = viewModel;
    }
}