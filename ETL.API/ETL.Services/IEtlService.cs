using ETL.Services.DTOs;

namespace ETL.Services
{
    public interface IEtlService
    {
        void ClearData();
        IEnumerable<TransactionDto> Start();
    }
}
