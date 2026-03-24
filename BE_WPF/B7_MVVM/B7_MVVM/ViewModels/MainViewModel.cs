using System.Windows;
using System.Windows.Input;
using B7_MVVM.Services;

namespace B7_MVVM.ViewModels;

public class MainViewModel : BaseViewModel
{
    private readonly LapHoaDonViewModel _lapHoaDonViewModel;
    private readonly DanhSachHoaDonViewModel _danhSachHoaDonViewModel;

    private object _currentViewModel;

    public MainViewModel()
    {
        QuanLyHoaDonService quanLyHoaDonService = new();

        _lapHoaDonViewModel = new LapHoaDonViewModel(quanLyHoaDonService);
        _danhSachHoaDonViewModel = new DanhSachHoaDonViewModel(quanLyHoaDonService);

        _currentViewModel = _lapHoaDonViewModel;

        MoLapHoaDonCommand = new RelayCommand(_ => CurrentViewModel = _lapHoaDonViewModel);
        MoDanhSachHoaDonCommand = new RelayCommand(_ =>
        {
            _danhSachHoaDonViewModel.LoadCommand.Execute(null);
            CurrentViewModel = _danhSachHoaDonViewModel;
        });
        ThoatUngDungCommand = new RelayCommand(_ => Application.Current.Shutdown());
    }

    public object CurrentViewModel
    {
        get => _currentViewModel;
        set
        {
            _currentViewModel = value;
            OnPropertyChanged();
        }
    }

    public ICommand MoLapHoaDonCommand { get; }

    public ICommand MoDanhSachHoaDonCommand { get; }

    public ICommand ThoatUngDungCommand { get; }
}
