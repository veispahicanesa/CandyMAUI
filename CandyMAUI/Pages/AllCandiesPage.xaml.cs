namespace CandyMAUI.Pages;

public partial class AllCandiesPage : ContentPage
{
	private readonly AllCandiesViewModel _allcandiesViewModel;
	public AllCandiesPage(AllCandiesViewModel allCandiesViewModel)
	{
		_allcandiesViewModel = allCandiesViewModel;
		BindingContext = _allcandiesViewModel;
        InitializeComponent();
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
		if (_allcandiesViewModel.FromSearch)
		{
			await Task.Delay(100);
			searchBar.Focus();
		}
    }

    private void searchBar_TextChanged(object sender, TextChangedEventArgs e)
    {
     if(!string.IsNullOrWhiteSpace(e.OldTextValue)
		&& string.IsNullOrWhiteSpace(e.NewTextValue))
		{
			_allcandiesViewModel.SearchCandiesCommand.Execute(null);
		}
    }
}