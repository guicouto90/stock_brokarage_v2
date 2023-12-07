using Domain.Entities;
using Domain.Entities.Enums;
using Domain.Validation;
using FluentAssertions;

namespace UnitTests.Entities
{
    public class AccountUnitTest 
    {
        [Fact]
        public void DepositValueMethod_SumBalanceValue_AndAddTransactionHistory()
        {
            // Arrange
            var customer = new Customer("Guilherme", "01234567899", 1, "123456");
            var account = new Account(1, "123456", customer);
            
            // Act
            account.DepositValue(1000.0);

            // Assert
            Assert.Equal(account.Balance, 1000.0);
            Assert.Equal(account.TransactionHistories.Count, 1);
            Assert.Equal(account.TransactionHistories.Last().TypeTransaction, TypeTransaction.DEPOSIT);
        }
        [Fact]
        public void WithdrawValueMethod_SubtractBalanceValue_AndAddTransactionHistory()
        {
            // Arrange
            var customer = new Customer("Guilherme", "01234567899", 1, "123456");
            var account = new Account(1, "123456", customer);

            // Act
            account.DepositValue(1000.0);
            account.WithdrawValue(500.0);

            // Assert
            Assert.Equal(account.Balance, 500.0);
            Assert.Equal(account.TransactionHistories.Count, 2);
            Assert.Equal(account.TransactionHistories.Last().TypeTransaction, TypeTransaction.WITHDRAW);
        }

        [Fact]
        public void VerifyPasswordMethod_ReturnsTrue_With_ValidPassword()
        {
            // Arrange
            var customer = new Customer("Guilherme", "01234567899", 1, "123456");
            var account = new Account(1, "123456", customer);

            var result = account.VerifyPassword("123456");

            Assert.True(result);
        }

        [Fact]
        public void DepositMethod_WithAmount_LessThanZero_DomainException()
        {
            var customer = new Customer("Guilherme", "01234567899", 1, "123456");
            var account = new Account(1, "123456", customer);
            Action action = () => account.DepositValue(-1);
            action.Should()
               .Throw<DomainExceptionValidation>()
                .WithMessage("Amount must be bigger than 0");
        }

        [Fact]
        public void WithdrawMethod_WithAmount_LessThanZero_DomainException()
        {
            var customer = new Customer("Guilherme", "01234567899", 1, "123456");
            var account = new Account(1, "123456", customer);
            Action action = () => account.WithdrawValue(-1);
            action.Should()
               .Throw<DomainExceptionValidation>()
                .WithMessage("Amount must be bigger than 0");
        }

        [Fact]
        public void WithdrawMethod_WithAmountBiggerThanBalance_DomainException()
        {
            var customer = new Customer("Guilherme", "01234567899", 1, "123456");
            var account = new Account(1, "123456", customer);
            Action action = () => account.WithdrawValue(50.0);
            action.Should()
               .Throw<DomainExceptionValidation>()
                .WithMessage("There is no enough balance to withdraw");
        }

    }
}
