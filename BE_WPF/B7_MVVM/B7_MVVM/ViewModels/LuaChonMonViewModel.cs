namespace B7_MVVM.ViewModels;
public class LuaChonMonViewModel : BaseViewModel
{
    private bool _isSelected;

    public string TenMon { get; set; } = string.Empty;

    public decimal DonGia { get; set; }

    public bool IsSelected
    {
        get => _isSelected;
        set
        {
            _isSelected = value;
            OnPropertyChanged();
        }
    }
}
