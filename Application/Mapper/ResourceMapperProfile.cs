using Application.UseCases.StockContext.Outputs;
using Application.UseCases.StockContext;
using AutoMapper;
using Domain.Entities;
using Domain.Models;
using Application.UseCases.CustomerContext.Outputs;
using Application.UseCases.AccountContext.Outputs;

namespace Application.Mapper
{
    public class ResourceMapperProfile : Profile
    {
        public ResourceMapperProfile()
        {
            CreateMap<Stock, StockOutput>();
            CreateMap<StockHistoryPrice, StockHistoryPriceOutput>();
            CreateMap<Customer, CustomerOutput>();
            CreateMap<Account, AccountOutput>();
            CreateMap<TransactionHistory, TransactionHistoryOutput>();
            CreateMap<Wallet, WalletOutput>();
            CreateMap<StocksWallet, StocksWalletOutput>();
        }
    }
}
