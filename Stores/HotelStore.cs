using ReserRoom.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReserRoom.Stores;
public class HotelStore
{
    private readonly List<Reservation> _reservations;
    private readonly Hotel _hotel;
    private Lazy<Task> _initializeLazy;
    public event Action<Reservation> ReservationMade;
    public IEnumerable<Reservation> Reservations => _reservations;
    public HotelStore(Hotel hotel)
    {
        _initializeLazy = new Lazy<Task>(Inicialize);
        _reservations = new List<Reservation>();
        _hotel = hotel;
    }
    public async Task Load()
    {
        try
        {
            await _initializeLazy.Value;
        }
        catch (Exception)
        {
            _initializeLazy = new Lazy<Task>(Inicialize);
            throw;
        }
    }
    public async Task MakeReservation(Reservation reservation)
    {
        await _hotel.MakeReservation(reservation);
        _reservations.Add(reservation);
        OnReservationMade(reservation);
    }

    private void OnReservationMade(Reservation reservation)
    {
        ReservationMade?.Invoke(reservation);
    }

    private async Task Inicialize()
    {
        IEnumerable<Reservation> reservations = await _hotel.GetAllReservations();
        _reservations.Clear();
        _reservations.AddRange(reservations);
    }
}
