using Application.Shared;
using Application.UseCases.AccountContext;
using Application.UseCases.AccountContext.Inputs;
using Application.UseCases.AccountContext.Outputs;
using Application.UseCases.CustomerContext;
using Application.UseCases.CustomerContext.Inputs;
using Application.UseCases.CustomerContext.Outputs;
using Application.UseCases.LoginContext;
using Application.UseCases.StockContext;
using Application.UseCases.StockContext.Inputs;
using Application.UseCases.StockContext.Outputs;
using Microsoft.Extensions.DependencyInjection;
using StockBrokarageChallenge.Application.UseCases.LoginContext.Inputs;
using StockBrokarageChallenge.Application.UseCases.LoginContext.Outputs;

namespace Infrastructure.IoC
{
    public static class RequestHandlersExtension {
        public static IServiceCollection AddRequestHandlers(this IServiceCollection services)
        {
            services.AddScoped<IRequestHandler<StockCreateInput, Output<StockOutput>>, StockCreateUseCase>();
            services.AddScoped<IRequestHandler<object, Output<List<StockOutput>>>, StockGetAllUseCase>();
            services.AddScoped<IRequestHandler<string, Output<StockOutput>>, StockGetByCodeOrNameUseCase>();

            services.AddScoped<IRequestHandler<CustomerCreateInput, Output<object>>, CustomerCreateUseCase>();

            services.AddScoped<IRequestHandler<LoginInput, Output<LoginOutput>>, AuthenticateUseCase>();

            services.AddScoped<IRequestHandler<AccountBuyStocksInput, Output<object>>, AccountBuyStockUseCase>();
            services.AddScoped<IRequestHandler<AccountDepositValueInput, Output<object>>, AccountDepositValueUseCase>();
            services.AddScoped<IRequestHandler<int, Output<WalletOutput>>, AccountListAccountUseCase>();
            services.AddScoped<IRequestHandler<int, Output<AccountOutput>>, AccountListTransactionHistoryUseCase>();
            services.AddScoped<IRequestHandler<AccountSellStocksInput, Output<object>>, AccountSellStockUseCase>();
            services.AddScoped<IRequestHandler<int, Output<List<TransactionHistoryOutput>>>, AccountStockTransactionsUseCase>();
            services.AddScoped<IRequestHandler<AccountWithdrawInput, Output<object>>, AccountWithdrawValueUseCase>();

            return services;
        }
    }
}