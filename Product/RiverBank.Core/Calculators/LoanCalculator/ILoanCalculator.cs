namespace RiverBank.Core.Calculators.LoanCalculator
{
    /// <summary>
    /// Interface for a calculator that takes <see cref="LoanCalculatorInput"/> as input and returns a <see cref="decimal"/>
    /// </summary>
    public interface ILoanCalculator : ICalculator<LoanCalculatorInput, decimal>
    {
    }
}