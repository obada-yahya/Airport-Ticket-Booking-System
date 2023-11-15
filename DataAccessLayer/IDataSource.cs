namespace AirportTicketBooking.DataAccessLayer;

public interface IDataSource
{
    public IEnumerable<string[]> GetRecordsFromDataSource();
    public void WriteToDataSource(IEnumerable<string[]> records);
}