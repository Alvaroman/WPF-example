using ReserRoom.Commands;
using ReserRoom.Model;
using ReserRoom.Stores;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace ReserRoom.ViewModel;
public class MakeReservationViewModel : ViewModelBase, INotifyDataErrorInfo
{
    private string _userName;
    public string UserName
    {
        get { return _userName; }
        set
        {
            _userName = value;
            OnPropertyChanged(nameof(UserName));

        }
    }
    private int _floorNumber;
    public int FloorNumber
    {
        get => _floorNumber; set
        {
            _floorNumber = value;
            OnPropertyChanged(nameof(FloorNumber));
        }
    }
    private int _roomNumber;
    public int RoomNumber
    {
        get => _roomNumber; set
        {
            _roomNumber = value;
            OnPropertyChanged(nameof(RoomNumber));
        }
    }

    public DateTime StartDate
    {
        get => _startDate;
        set
        {
            _startDate = value;
            OnPropertyChanged(nameof(StartDate));
            ClearErros(nameof(StartDate));
            ClearErros(nameof(EndDate));
            if (EndDate < StartDate)
            {
                AddError("The start date cannot be before the end date.", nameof(StartDate));
            }
        }
    }

    private DateTime _startDate = DateTime.Now.AddMonths(1);
    public DateTime EndDate
    {
        get => _endDate;
        set
        {
            _endDate = value;
            OnPropertyChanged(nameof(EndDate));
            ClearErros(nameof(StartDate));
            ClearErros(nameof(EndDate));
            if (EndDate < StartDate)
            {
                AddError("The end date cannot be before the start date.", nameof(EndDate));
            }
        }
    }

    private void AddError(string errorMessage, string propertyName)
    {
        if (!_propertyNameToErrorsDictionary.ContainsKey(propertyName))
        {
            _propertyNameToErrorsDictionary.Add(propertyName, new List<string>());
        }
        _propertyNameToErrorsDictionary[propertyName].Add(errorMessage);
        OnErrorChanged(propertyName);
    }

    private void OnErrorChanged(string propertyName)
    {
        ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
    }

    private void ClearErros(string propertyName)
    {
        _propertyNameToErrorsDictionary.Remove(nameof(propertyName));
        OnErrorChanged(propertyName);
    }

    private DateTime _endDate = DateTime.Now.AddDays(45);

    public ICommand SubmitCommand { get; }
    public ICommand CancelCommand { get; }

    public readonly Dictionary<string, List<string>> _propertyNameToErrorsDictionary;
    public MakeReservationViewModel(HotelStore hotelStore,
                                    Services.NavigationService reservationViewNavigationService)
    {
        SubmitCommand = new MakeReservationCommand(this, hotelStore, reservationViewNavigationService);
        CancelCommand = new NavigateCommand(reservationViewNavigationService);
        _propertyNameToErrorsDictionary = new Dictionary<string, List<string>>();
    }

    public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;
    public bool HasErrors => _propertyNameToErrorsDictionary is not null && _propertyNameToErrorsDictionary.Any();
    public IEnumerable GetErrors(string? propertyName) => _propertyNameToErrorsDictionary.GetValueOrDefault(propertyName, new List<string>());
}
