namespace RiverBank.Core.Calculators.LoanCalculator
{
    /// <summary>
    /// Contains information for loan payment calculation.
    /// </summary>
    public class LoanCalculatorInput
    {
        /// <summary>
        /// Initializes the <see cref="LoanCalculatorInput"/>
        /// </summary>
        /// <param name="principalAmount">The amount of the loan.</param>
        /// <param name="termMonths">The term of the loan represented in months.</param>
        /// <param name="interestRate">Annual interest rate of the loan.</param>
        public LoanCalculatorInput(decimal principalAmount, int termMonths, decimal interestRate)
        {
            PrincipalAmount = principalAmount;
            TermMonths = termMonths;
            InterestRate = interestRate;
        }

        /// <summary>
        /// The amount of the loan.
        /// </summary>
        public decimal PrincipalAmount { get; }

        /// <summary>
        /// The term of the loan represented in months.
        /// </summary>
        public int TermMonths { get; }

        /// <summary>
        /// Annual interest rate of the loan.
        /// </summary>
        public decimal InterestRate { get; }
    }
}
