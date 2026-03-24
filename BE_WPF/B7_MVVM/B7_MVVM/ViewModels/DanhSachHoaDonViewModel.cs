using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows.Input;
using B7_MVVM.Models;
using B7_MVVM.Services;

namespace B7_MVVM.ViewModels;

public class DanhSachHoaDonViewModel : BaseViewModel
{
    private readonly QuanLyHoaDonService _quanLyHoaDonService;

    private int _tongKhachHang;
    private decimal _tongTienThanhToan;

    public DanhSachHoaDonViewModel(QuanLyHoaDonService quanLyHoaDonService)
    {
        _quanLyHoaDonService = quanLyHoaDonService;
        _quanLyHoaDonService.DanhSachHoaDon.CollectionChanged += DanhSachHoaDon_CollectionChanged;

        LoadCommand = new RelayCommand(_ => Load());
        ThongKeCommand = new RelayCommand(_ => ThongKe());

        Load();
    }

    public ObservableCollection<HoaDon> DanhSachHoaDon => _quanLyHoaDonService.DanhSachHoaDon;

    public int TongKhachHang
    {
        get => _tongKhachHang;
        set
        {
            _tongKhachHang = value;
            OnPropertyChanged();
        }
    }

    public decimal TongTienThanhToan
    {
        get => _tongTienThanhToan;
        set
        {
            _tongTienThanhToan = value;
            OnPropertyChanged();
        }
    }

    public ICommand LoadCommand { get; }

    public ICommand ThongKeCommand { get; }

    private void DanhSachHoaDon_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        // M?i khi có thay ??i danh sách hóa ??n thì t? ??ng c?p nh?t th?ng kê.
        CapNhatThongKe();
    }

    private void Load()
    {
        OnPropertyChanged(nameof(DanhSachHoaDon));
        CapNhatThongKe();
    }

    private void ThongKe()
    {
        CapNhatThongKe();
    }

    private void CapNhatThongKe()
    {
        TongKhachHang = DanhSachHoaDon.Count;
        TongTienThanhToan = DanhSachHoaDon.Sum(x => x.TongTienThanhToan);
    }
}
