namespace ReferralRockIntegration.Service.Interfaces
{
    public interface IReferralService
    {
        Task AddAsync();
        Task EditAsync();
        Task RemoveAsync(string id);
    }
}
