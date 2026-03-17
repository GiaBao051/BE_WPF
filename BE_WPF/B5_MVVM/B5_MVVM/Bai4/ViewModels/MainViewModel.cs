using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using Bai4.Models;
using Newtonsoft.Json;

namespace Bai4.ViewModels;

public class MainViewModel : BaseViewModel
{
    private readonly Stack<List<SV_Model>> _undoStack = new();
    private readonly Stack<List<SV_Model>> _redoStack = new();
    private readonly RelayCommand _updateCommand;
    private readonly RelayCommand _deleteCommand;
    private readonly RelayCommand _undoCommand;
    private readonly RelayCommand _redoCommand;
    private SV_Model? _selectedStudent;
    private string _inputHoTen = string.Empty;
    private string _inputTuoi = "0";
    private string _inputThanhPho = string.Empty;
    private string _inputGioiTinh = "Nam";
    private string _filterText = string.Empty;
    private int _tongSo;
    private int _soNam;
    private int _soNu;

    public MainViewModel()
    {
        Students = new ObservableCollection<SV_Model>
        {
            new SV_Model { HoTen = "An", Tuoi = 20, GioiTinh = "Nam", ThanhPho = "Ha Noi" },
            new SV_Model { HoTen = "Binh", Tuoi = 19, GioiTinh = "Nu", ThanhPho = "Da Nang" }
        };

        ThanhPhoOptions = new ObservableCollection<string>
        {
            "Ha Noi",
            "Da Nang",
            "TP HCM",
            "Can Tho",
            "Hue"
        };

        InputThanhPho = ThanhPhoOptions[0];

        StudentsView = CollectionViewSource.GetDefaultView(Students);
        StudentsView.Filter = FilterStudentByName;

        Students.CollectionChanged += (_, _) => UpdateStatistics();

        AddCommand = new RelayCommand(_ => AddStudent());
        _updateCommand = new RelayCommand(_ => UpdateStudent(), _ => SelectedStudent is not null);
        _deleteCommand = new RelayCommand(_ => DeleteStudent(), _ => SelectedStudent is not null);
        _undoCommand = new RelayCommand(_ => Undo(), _ => _undoStack.Count > 0);
        _redoCommand = new RelayCommand(_ => Redo(), _ => _redoStack.Count > 0);

        UpdateCommand = _updateCommand;
        DeleteCommand = _deleteCommand;
        UndoCommand = _undoCommand;
        RedoCommand = _redoCommand;

        UpdateStatistics();
        RaiseCommandCanExecute();
    }

    public ObservableCollection<SV_Model> Students { get; }

    public ICollectionView StudentsView { get; }

    public ObservableCollection<string> ThanhPhoOptions { get; }

    public ICommand AddCommand { get; }

    public ICommand UpdateCommand { get; }

    public ICommand DeleteCommand { get; }

    public ICommand UndoCommand { get; }

    public ICommand RedoCommand { get; }

    public SV_Model? SelectedStudent
    {
        get => _selectedStudent;
        set
        {
            if (_selectedStudent == value)
            {
                return;
            }

            _selectedStudent = value;
            OnPropertyChanged();

            if (_selectedStudent is not null)
            {
                InputHoTen = _selectedStudent.HoTen;
                InputTuoi = _selectedStudent.Tuoi.ToString();
                InputThanhPho = _selectedStudent.ThanhPho;
                SetInputGioiTinh(_selectedStudent.GioiTinh);
            }

            RaiseCommandCanExecute();
        }
    }

    public string InputHoTen
    {
        get => _inputHoTen;
        set
        {
            if (_inputHoTen == value)
            {
                return;
            }

            _inputHoTen = value;
            OnPropertyChanged();
        }
    }

    public string InputTuoi
    {
        get => _inputTuoi;
        set
        {
            if (_inputTuoi == value)
            {
                return;
            }

            _inputTuoi = value;
            OnPropertyChanged();
        }
    }

    public string InputThanhPho
    {
        get => _inputThanhPho;
        set
        {
            if (_inputThanhPho == value)
            {
                return;
            }

            _inputThanhPho = value;
            OnPropertyChanged();
        }
    }

    public bool IsNam
    {
        get => _inputGioiTinh == "Nam";
        set
        {
            if (value)
            {
                SetInputGioiTinh("Nam");
            }
        }
    }

    public bool IsNu
    {
        get => _inputGioiTinh == "Nu";
        set
        {
            if (value)
            {
                SetInputGioiTinh("Nu");
            }
        }
    }

    public string FilterText
    {
        get => _filterText;
        set
        {
            if (_filterText == value)
            {
                return;
            }

            _filterText = value;
            OnPropertyChanged();
            StudentsView.Refresh();
        }
    }

    public int TongSo
    {
        get => _tongSo;
        private set
        {
            if (_tongSo == value)
            {
                return;
            }

            _tongSo = value;
            OnPropertyChanged();
        }
    }

    public int SoNam
    {
        get => _soNam;
        private set
        {
            if (_soNam == value)
            {
                return;
            }

            _soNam = value;
            OnPropertyChanged();
        }
    }

    public int SoNu
    {
        get => _soNu;
        private set
        {
            if (_soNu == value)
            {
                return;
            }

            _soNu = value;
            OnPropertyChanged();
        }
    }

    public void SaveToFile(string path)
    {
        try
        {
            string json = JsonConvert.SerializeObject(Students, Formatting.Indented);
            File.WriteAllText(path, json);
            MessageBox.Show($"Da luu du lieu vao file: {Path.GetFullPath(path)}");
        }
        catch (Exception ex)
        {
            MessageBox.Show("Loi khi luu file: " + ex.Message);
        }
    }

    public void LoadFromFile(string path)
    {
        try
        {
            if (!File.Exists(path))
            {
                MessageBox.Show("Khong tim thay file: " + Path.GetFullPath(path));
                return;
            }

            string json = File.ReadAllText(path);
            var list = JsonConvert.DeserializeObject<ObservableCollection<SV_Model>>(json);

            Students.Clear();

            if (list is not null)
            {
                foreach (SV_Model item in list)
                {
                    Students.Add(CloneStudent(item));
                }
            }

            StudentsView.Refresh();
            ClearInput();
            UpdateStatistics();
            _undoStack.Clear();
            _redoStack.Clear();
            RaiseCommandCanExecute();
            MessageBox.Show("Load file JSON thanh cong.");
        }
        catch (Exception ex)
        {
            MessageBox.Show("Loi khi load file: " + ex.Message);
        }
    }

    private void AddStudent()
    {
        if (!ValidateInput(out int tuoi))
        {
            return;
        }

        SaveStateForUndo();

        Students.Add(new SV_Model
        {
            HoTen = InputHoTen.Trim(),
            Tuoi = tuoi,
            GioiTinh = _inputGioiTinh,
            ThanhPho = InputThanhPho
        });

        StudentsView.Refresh();
        UpdateStatistics();
        ClearInput();
    }

    private void UpdateStudent()
    {
        if (SelectedStudent is null)
        {
            MessageBox.Show("Vui long chon sinh vien can sua.");
            return;
        }

        if (!ValidateInput(out int tuoi))
        {
            return;
        }

        SaveStateForUndo();

        SelectedStudent.HoTen = InputHoTen.Trim();
        SelectedStudent.Tuoi = tuoi;
        SelectedStudent.GioiTinh = _inputGioiTinh;
        SelectedStudent.ThanhPho = InputThanhPho;

        StudentsView.Refresh();
        UpdateStatistics();
        ClearInput();
    }

    private void DeleteStudent()
    {
        if (SelectedStudent is null)
        {
            MessageBox.Show("Vui long chon sinh vien can xoa.");
            return;
        }

        SaveStateForUndo();
        Students.Remove(SelectedStudent);
        StudentsView.Refresh();
        UpdateStatistics();
        ClearInput();
    }

    private void Undo()
    {
        if (_undoStack.Count == 0)
        {
            return;
        }

        _redoStack.Push(CreateSnapshot());

        List<SV_Model> previousState = _undoStack.Pop();
        ApplySnapshot(previousState);
        RaiseCommandCanExecute();
    }

    private void Redo()
    {
        if (_redoStack.Count == 0)
        {
            return;
        }

        _undoStack.Push(CreateSnapshot());

        List<SV_Model> nextState = _redoStack.Pop();
        ApplySnapshot(nextState);
        RaiseCommandCanExecute();
    }

    private bool ValidateInput(out int tuoi)
    {
        tuoi = 0;

        if (string.IsNullOrWhiteSpace(InputHoTen))
        {
            MessageBox.Show("Ho ten khong duoc de trong.");
            return false;
        }

        if (!int.TryParse(InputTuoi, out tuoi) || tuoi <= 0)
        {
            MessageBox.Show("Tuoi phai la so nguyen duong.");
            return false;
        }

        if (string.IsNullOrWhiteSpace(InputThanhPho))
        {
            MessageBox.Show("Vui long chon thanh pho.");
            return false;
        }

        return true;
    }

    private bool FilterStudentByName(object item)
    {
        if (item is not SV_Model student)
        {
            return false;
        }

        if (string.IsNullOrWhiteSpace(FilterText))
        {
            return true;
        }

        return student.HoTen.Contains(FilterText, StringComparison.OrdinalIgnoreCase);
    }

    private void UpdateStatistics()
    {
        TongSo = Students.Count;
        SoNam = Students.Count(x => x.GioiTinh == "Nam");
        SoNu = Students.Count(x => x.GioiTinh == "Nu");
    }

    private void ClearInput()
    {
        SelectedStudent = null;
        InputHoTen = string.Empty;
        InputTuoi = "0";
        InputThanhPho = ThanhPhoOptions.FirstOrDefault() ?? string.Empty;
        SetInputGioiTinh("Nam");
    }

    private void SetInputGioiTinh(string gioiTinh)
    {
        if (_inputGioiTinh == gioiTinh)
        {
            return;
        }

        _inputGioiTinh = gioiTinh;
        OnPropertyChanged(nameof(IsNam));
        OnPropertyChanged(nameof(IsNu));
    }

    private void SaveStateForUndo()
    {
        _undoStack.Push(CreateSnapshot());
        _redoStack.Clear();
        RaiseCommandCanExecute();
    }

    private List<SV_Model> CreateSnapshot()
    {
        return Students.Select(CloneStudent).ToList();
    }

    private void ApplySnapshot(IEnumerable<SV_Model> snapshot)
    {
        Students.Clear();

        foreach (SV_Model item in snapshot)
        {
            Students.Add(CloneStudent(item));
        }

        StudentsView.Refresh();
        UpdateStatistics();
        ClearInput();
    }

    private static SV_Model CloneStudent(SV_Model source)
    {
        return new SV_Model
        {
            HoTen = source.HoTen,
            Tuoi = source.Tuoi,
            GioiTinh = source.GioiTinh,
            ThanhPho = source.ThanhPho
        };
    }

    private void RaiseCommandCanExecute()
    {
        _updateCommand.RaiseCanExecuteChanged();
        _deleteCommand.RaiseCanExecuteChanged();
        _undoCommand.RaiseCanExecuteChanged();
        _redoCommand.RaiseCanExecuteChanged();
    }
}
