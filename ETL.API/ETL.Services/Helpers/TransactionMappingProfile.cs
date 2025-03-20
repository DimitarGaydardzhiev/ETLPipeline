using AutoMapper;
using ETL.Data.Models;
using ETL.Services.DTOs;

namespace ETL.Services.Helpers
{
    public class TransactionMappingProfile : Profile
    {
        public TransactionMappingProfile()
        {
            CreateMap<Transaction, TransactionDto>();
        }
    }
}
