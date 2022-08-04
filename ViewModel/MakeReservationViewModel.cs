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
        set { _startDate = value; OnPropertyChanged(nameof(StartDate)); }
    }

    private DateTime _startDate = DateTime.Now.AddMonths(1);
    public DateTime EndDate
    {
        get => _endDate;
        set
        {
            _endDate = value;
            OnPropertyChanged(nameof(EndDate));
            _propertyNameToErrorsDictionary.Remove(nameof(EndDate));
            if (EndDate < StartDate)
            {
                List<string> endDateErros = new List<string>()
                {
                "The end date cannot be before the start date."
                };
                _propertyNameToErrorsDictionary.Add(nameof(EndDate), endDateErros);
                ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(nameof(EndDate)));
            }
        }
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
    }

    public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;
    public bool HasErrors => _propertyNameToErrorsDictionary is not null && _propertyNameToErrorsDictionary.Any();
    public IEnumerable GetErrors(string? propertyName) => _propertyNameToErrorsDictionary.GetValueOrDefault(propertyName, new List<string>());
}
