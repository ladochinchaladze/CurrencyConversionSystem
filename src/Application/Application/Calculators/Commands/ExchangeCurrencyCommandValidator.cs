﻿using Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Calculators.Commands
{
    public class ExchangeCurrencyCommandValidator : AbstractValidator<ExchangeCurrencyCommand>
    {
        private readonly IApplicationDbContext _context;
        public ExchangeCurrencyCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(x => x.CurrencyFrom)
                .NotEmpty()
                .NotNull()
                .MustAsync((o, currencyFrom, CancellationToken) => { return IsCorrectCurrency(currencyFrom, CancellationToken); })
                .WithMessage("Please select correct currency");


            RuleFor(x => x.CurrencyTo)
                .NotEmpty()
                .NotNull()
                .MustAsync((o, currencyFrom, CancellationToken) => { return IsCorrectCurrency(currencyFrom, CancellationToken); })
                .WithMessage("Please select correct currency");

            RuleFor(x => x.ReceivedAmount)
                .NotEqual(0);


            RuleFor(x => x.PaidAmount)
                .NotEqual(0);


            RuleFor(x => x)
                .MustAsync((o, CancellationToken) => { return MustBeCorrectExchangeRate(o, CancellationToken); })
                .WithMessage("Exchange rate is changed");



            RuleFor(x => x)
                .MustAsync((o, CancellationToken) => { return IsCalculationCorrect(o, CancellationToken); })
                .WithMessage("Calculation is not correct");


            RuleFor(x => x)
                .MustAsync((o, CancellationToken) =>
                {
                    return IsPersonInfo(
                        o.ReceivedAmount,
                        o.CurrencyTo,
                        CancellationToken,
                        o.IdentityNumber,
                        o.FirstName,
                        o.LastName,
                        o.RecomendatorIdentityNumber);
                })
                .WithMessage("Personal info is not filled");



            RuleFor(x => x)
                .MustAsync((o, CancellationToken) => { return RecomendatorExists(o, CancellationToken); })
                .WithMessage("Recomendator doesn't exists");



            RuleFor(x => x)
                .MustAsync((o, CancellationToken) => { return LimitIsNotExceeded(o, CancellationToken); })
                .WithMessage("Limit Is Exceeded");

        }




        public async Task<bool> IsCorrectCurrency(string currencyCode, CancellationToken cancellationToken)
        {
            return await _context.Currencies.AnyAsync(x => x.Code == currencyCode, cancellationToken);
        }




        public async Task<bool> MustBeCorrectExchangeRate(ExchangeCurrencyCommand obj, CancellationToken cancellationToken)
        {
            var currencyFrom = await _context
                   .Currencies
                   .Include(x => x.ExchangeRate)
                   .FirstAsync(x => x.Code == obj.CurrencyFrom, cancellationToken);


            var currencyTo = await _context
                .Currencies
                .Include(x => x.ExchangeRate)
                .FirstAsync(x => x.Code == obj.CurrencyTo, cancellationToken);


            return obj.ExchangeRate == currencyTo.ExchangeRate.Sell / currencyFrom.ExchangeRate.Sell;
        }



        public async Task<bool> IsCalculationCorrect(ExchangeCurrencyCommand obj, CancellationToken cancellationToken, params string[] personInfo)
        {
            var currencyFrom = await _context
                  .Currencies
                  .Include(x => x.ExchangeRate)
                  .FirstAsync(x => x.Code == obj.CurrencyFrom, cancellationToken);


            var currencyTo = await _context
                .Currencies
                .Include(x => x.ExchangeRate)
                .FirstAsync(x => x.Code == obj.CurrencyTo, cancellationToken);


            var exchangeRate = currencyTo.ExchangeRate.Sell / currencyFrom.ExchangeRate.Sell;

            return obj.ReceivedAmount * exchangeRate == obj.PaidAmount;
        }

        public async Task<bool> IsPersonInfo(decimal receivedAmount, string currencyToCode, CancellationToken cancellationToken, params string[] personInfo)
        {
            var currencyTo = await _context
                .Currencies
                .Include(x => x.ExchangeRate)
                .FirstAsync(x => x.Code == currencyToCode, cancellationToken);

            var amount = receivedAmount * currencyTo.ExchangeRate.Sell;


            return amount > 3000 ? personInfo.All(x => !string.IsNullOrEmpty(x)) : true;
        }



        public async Task<bool> RecomendatorExists(ExchangeCurrencyCommand obj, CancellationToken cancellationToken)
        {
            return await _context.Persons.AnyAsync(x => x.IdentityNumber == obj.RecomendatorIdentityNumber, cancellationToken);
        }


        public async Task<bool> LimitIsNotExceeded(ExchangeCurrencyCommand obj, CancellationToken cancellationToken)
        {
            var currencyTo = await _context
               .Currencies
               .Include(x => x.ExchangeRate)
               .FirstAsync(x => x.Code == obj.CurrencyTo, cancellationToken);

            if (obj.ReceivedAmount * currencyTo.ExchangeRate.Sell <= 3000)
                return true;



            var ownOperationAmount = await _context
                 .Conversions
                 .Include(x => x.Person)
                 .Where(x => x.Person.IdentityNumber == obj.IdentityNumber && x.CreateDate.Date == DateTime.Today)
                 .SumAsync(x => x.AmountInLari, cancellationToken);

            var recomendersOperationsAmount = await _context
                 .Conversions
                 .Include(x => x.Person)
                 .Where(x => x.Person.RecomendatorIdentityNumber == obj.IdentityNumber && x.CreateDate.Date == DateTime.Today)
                 .SumAsync(x => x.AmountInLari, cancellationToken);

            return ownOperationAmount + recomendersOperationsAmount <= 100000;
        }

    }
}
