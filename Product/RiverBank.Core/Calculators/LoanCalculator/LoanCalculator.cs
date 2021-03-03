using System;

namespace RiverBank.Core.Calculators.LoanCalculator
{
    /// <summary>
    /// Class for calculating loan payment.
    /// </summary>
    internal class LoanCalculator : ILoanCalculator
    {
        /// <summary>
        /// Calculates loan payment based on information in <see cref="LoanCalculatorInput"/>.
        /// </summary>
        /// <param name="input">Serves as an input for payment calculation.</param>
        /// <returns>The payment wrapped in a <see cref="CalculationResult{TResult}"/> instance.</returns>
        public CalculationResult<decimal> Calculate(LoanCalculatorInput input)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            if (input.TermMonths <= 0)
            {
                return CalculationResult<decimal>.Fail(CalculationFailureReason.InvalidInput);
            }
            if (input.PrincipalAmount < 0)
            {
                return CalculationResult<decimal>.Fail(CalculationFailureReason.InvalidInput);
            }
            if (input.InterestRate < 0)
            {
                return CalculationResult<decimal>.Fail(CalculationFailureReason.InvalidInput);
            }

            decimal calculationOutput;
            if (input.InterestRate == 0)
            {
                calculationOutput = input.PrincipalAmount / input.TermMonths;
            }
            else
            {
                double presentValue = (double)input.PrincipalAmount;
                double rateOverPeriods = (double)input.InterestRate / 12;
                int numberOfPeriods = input.TermMonths;

                double payment = presentValue * rateOverPeriods / (1 - Math.Pow(1 + rateOverPeriods, -numberOfPeriods));

                calculationOutput = Convert.ToDecimal(payment);
            }

            return CalculationResult<decimal>.Success(Math.Round(calculationOutput, 2));
        }
    }
}