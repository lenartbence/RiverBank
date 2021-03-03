namespace RiverBank.Core.Calculators
{
    /// <summary>
    /// Contains information about the result of a calculation.
    /// </summary>
    /// <typeparam name="TResult">The type of the calculation's output.</typeparam>
    public class CalculationResult<TResult>
    {
        private CalculationResult()
        {
        }

        /// <summary>
        /// Indicates if the calculation was successful.
        /// </summary>
        public bool Successful { get; private set; }

        /// <summary>
        /// Stores the result of the calculation if it was successful.
        /// </summary>
        public TResult Value { get; private set; }

        /// <summary>
        /// Gets the failure reason if the calculation was unsuccessful.
        /// </summary>
        public CalculationFailureReason FailureReason { get; private set; }

        /// <summary>
        /// Initializes a successful <see cref="CalculationResult{TResult}"/> instance containing the result.
        /// </summary>
        /// <param name="value">The result of the calculation.</param>
        /// <returns>The result of the calculation wrapped in a <see cref="CalculationResult{TResult}"/>.</returns>
        public static CalculationResult<TResult> Success(TResult value)
        {
            return new CalculationResult<TResult>()
            {
                Successful = true,
                Value = value
            };
        }

        /// <summary>
        /// Initializes an unsuccessful <see cref="CalculationResult{TResult}"/> instance containing the reason of the failure.
        /// </summary>
        /// <param name="reason">The reason of the failure.</param>
        /// <returns>A <see cref="CalculationResult{TResult}"/> instance containing information about the failure.</returns>
        public static CalculationResult<TResult> Fail(CalculationFailureReason reason)
        {
            return new CalculationResult<TResult>()
            {
                Successful = false,
                FailureReason = reason
            };
        }
    }
}