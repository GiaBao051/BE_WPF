using System.Linq;

namespace B7_MVVM.Models;

public class HoaDon
{
    public int STT { get; set; }

    public KhachHang KhachHang { get; set; } = new();

    public string ViTriBan { get; set; } = string.Empty;

    public List<MonAnNuocUong> DanhSachNuocUong { get; set; } = new();

    public List<MonAnNuocUong> DanhSachThucAn { get; set; } = new();

    public decimal TongTienTamTinh { get; set; }

    public decimal TongTienThanhToan { get; set; }

    public string SinhVienHienThi => KhachHang.LaSinhVien ? "Có" : "Không";

    public string NuocUongHienThi => string.Join(", ", DanhSachNuocUong.Select(x => x.TenMon));

    public string ThucAnHienThi => string.Join(", ", DanhSachThucAn.Select(x => x.TenMon));
}
