using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using B7_MVVM.Models;
using B7_MVVM.Services;

namespace B7_MVVM.ViewModels;
public class LapHoaDonViewModel : BaseViewModel
{
    private readonly QuanLyHoaDonService _quanLyHoaDonService;

    private string _tenKhachHang = string.Empty;
    private string _soDienThoai = string.Empty;
    private bool _laSinhVien;
    private bool _ban01;
    private bool _ban02;
    private bool _ban03;
    private bool _ban04;
    private string _viTriBan = string.Empty;
    private HoaDon? _hoaDonTam;

    public LapHoaDonViewModel(QuanLyHoaDonService quanLyHoaDonService)
    {
        _quanLyHoaDonService = quanLyHoaDonService;

        DanhSachNuocUong = new ObservableCollection<LuaChonMonViewModel>
        {
            new() { TenMon = "Cafe đen", DonGia = 20000 },
            new() { TenMon = "Cafe sữa", DonGia = 25000 },
            new() { TenMon = "Cafe đá", DonGia = 25000 },
            new() { TenMon = "Cafe kem", DonGia = 35000 },
            new() { TenMon = "Cafe sữa đá", DonGia = 30000 }
        };

        DanhSachThucAn = new ObservableCollection<LuaChonMonViewModel>
        {
            new() { TenMon = "Bánh mì trứng", DonGia = 15000 },
            new() { TenMon = "Bánh mì cá", DonGia = 15000 },
            new() { TenMon = "Mì tôm trứng", DonGia = 20000 },
            new() { TenMon = "Mì xào bò", DonGia = 30000 },
            new() { TenMon = "Mì cay", DonGia = 50000 }
        };

        DanhSachHoaDonTam = new ObservableCollection<HoaDon>();

        ChonCommand = new RelayCommand(_ => ChonMon());
        NhapLaiCommand = new RelayCommand(_ => NhapLai());
        ThanhToanCommand = new RelayCommand(_ => ThanhToan());
        ThoatCommand = new RelayCommand(_ => Application.Current.Shutdown());
    }

    public string TenKhachHang
    {
        get => _tenKhachHang;
        set
        {
            _tenKhachHang = value;
            OnPropertyChanged();
        }
    }

    public string SoDienThoai
    {
        get => _soDienThoai;
        set
        {
            _soDienThoai = value;
            OnPropertyChanged();
        }
    }

    public bool LaSinhVien
    {
        get => _laSinhVien;
        set
        {
            _laSinhVien = value;
            OnPropertyChanged();
        }
    }

    public bool Ban01
    {
        get => _ban01;
        set
        {
            _ban01 = value;
            if (value) ViTriBan = "Bàn 01";
            OnPropertyChanged();
        }
    }

    public bool Ban02
    {
        get => _ban02;
        set
        {
            _ban02 = value;
            if (value) ViTriBan = "Bàn 02";
            OnPropertyChanged();
        }
    }

    public bool Ban03
    {
        get => _ban03;
        set
        {
            _ban03 = value;
            if (value) ViTriBan = "Bàn 03";
            OnPropertyChanged();
        }
    }

    public bool Ban04
    {
        get => _ban04;
        set
        {
            _ban04 = value;
            if (value) ViTriBan = "Bàn 04";
            OnPropertyChanged();
        }
    }

    public string ViTriBan
    {
        get => _viTriBan;
        set
        {
            _viTriBan = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<LuaChonMonViewModel> DanhSachNuocUong { get; }

    public ObservableCollection<LuaChonMonViewModel> DanhSachThucAn { get; }

    public ObservableCollection<HoaDon> DanhSachHoaDonTam { get; }

    public HoaDon? HoaDonTam
    {
        get => _hoaDonTam;
        set
        {
            _hoaDonTam = value;
            OnPropertyChanged();
        }
    }

    public ICommand ChonCommand { get; }

    public ICommand NhapLaiCommand { get; }

    public ICommand ThanhToanCommand { get; }

    public ICommand ThoatCommand { get; }

    private void ChonMon()
    {
        if (!KiemTraDuLieuHopLe())
        {
            return;
        }

        List<MonAnNuocUong> nuocUongDaChon = DanhSachNuocUong
            .Where(x => x.IsSelected)
            .Select(x => new MonAnNuocUong
            {
                TenMon = x.TenMon,
                DonGia = x.DonGia,
                Loai = "Nước uống"
            })
            .ToList();

        List<MonAnNuocUong> thucAnDaChon = DanhSachThucAn
            .Where(x => x.IsSelected)
            .Select(x => new MonAnNuocUong
            {
                TenMon = x.TenMon,
                DonGia = x.DonGia,
                Loai = "Thức ăn"
            })
            .ToList();

        decimal tongTienTamTinh = nuocUongDaChon.Sum(x => x.DonGia) + thucAnDaChon.Sum(x => x.DonGia);
        decimal tongTienThanhToan = LaSinhVien ? tongTienTamTinh * 0.8m : tongTienTamTinh;

        HoaDonTam = new HoaDon
        {
            KhachHang = new KhachHang
            {
                TenKhachHang = TenKhachHang.Trim(),
                SoDienThoai = SoDienThoai.Trim(),
                LaSinhVien = LaSinhVien
            },
            ViTriBan = ViTriBan,
            DanhSachNuocUong = nuocUongDaChon,
            DanhSachThucAn = thucAnDaChon,
            TongTienTamTinh = tongTienTamTinh,
            TongTienThanhToan = tongTienThanhToan
        };

        DanhSachHoaDonTam.Clear();
        DanhSachHoaDonTam.Add(HoaDonTam);

        MessageBox.Show("Đã tạo hóa đơn thành công.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
    }

    private bool KiemTraDuLieuHopLe()
    {
        // Ki?m tra các thông tin b?t bu?c theo ?? bài.
        if (string.IsNullOrWhiteSpace(TenKhachHang))
        {
            MessageBox.Show("Tên khách hàng không được rỗng.", "Lại", MessageBoxButton.OK, MessageBoxImage.Warning);
            return false;
        }

        if (string.IsNullOrWhiteSpace(SoDienThoai))
        {
            MessageBox.Show("Số điện thoại không được rỗng.", "Lại", MessageBoxButton.OK, MessageBoxImage.Warning);
            return false;
        }

        if (!SoDienThoai.All(char.IsDigit))
        {
            MessageBox.Show("Số điện thoại chưa được nhập", "Lại", MessageBoxButton.OK, MessageBoxImage.Warning);
            return false;
        }

        if (string.IsNullOrWhiteSpace(ViTriBan))
        {
            MessageBox.Show("Vui lòng chọn bàn.", "Lại", MessageBoxButton.OK, MessageBoxImage.Warning);
            return false;
        }

        bool coNuoc = DanhSachNuocUong.Any(x => x.IsSelected);
        bool coThucAn = DanhSachThucAn.Any(x => x.IsSelected);

        if (!coNuoc && !coThucAn)
        {
            MessageBox.Show("Vui lòng chọn ít nhất một món ăn hoặc nước uống.", "Lại", MessageBoxButton.OK, MessageBoxImage.Warning);
            return false;
        }

        return true;
    }

    private void ThanhToan()
    {
        if (HoaDonTam == null)
        {
            MessageBox.Show("Bạn cần nhấn nút Chọn ?? Tạo hóa đơn trước khi thanh toán.", "Lại", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        _quanLyHoaDonService.ThemHoaDon(HoaDonTam);
        MessageBox.Show("Thanh toán thành công.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

        NhapLai();
    }

    private void NhapLai()
    {
        TenKhachHang = string.Empty;
        SoDienThoai = string.Empty;
        LaSinhVien = false;

        Ban01 = false;
        Ban02 = false;
        Ban03 = false;
        Ban04 = false;
        ViTriBan = string.Empty;

        foreach (LuaChonMonViewModel nuoc in DanhSachNuocUong)
        {
            nuoc.IsSelected = false;
        }

        foreach (LuaChonMonViewModel mon in DanhSachThucAn)
        {
            mon.IsSelected = false;
        }

        HoaDonTam = null;
        DanhSachHoaDonTam.Clear();
    }
}
