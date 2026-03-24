using System.Collections.ObjectModel;
using B7_MVVM.Models;

namespace B7_MVVM.Services;

public class QuanLyHoaDonService
{
    public ObservableCollection<HoaDon> DanhSachHoaDon { get; } = new();

    public void ThemHoaDon(HoaDon hoaDon)
    {
        hoaDon.STT = DanhSachHoaDon.Count + 1;
        DanhSachHoaDon.Add(hoaDon);
    }
}
