namespace RiverBank.Core.Calculators
{
    /// <summary>
    /// Interface for calculators that can work with any type of input and output.
    /// </summary>
    /// <typeparam name="TInput">Type of the calculation input</typeparam>
    /// <typeparam name="TResult">Type of the calculation result</typeparam>
    public interface ICalculator<TInput, TResult>
    {
        /// <summary>
        /// Performs a calculation based on the information in <see cref="input"/> parameter and returns the result.
        /// </summary>
        /// <param name="input">Serves as the input of the calculation.</param>
        /// <returns>The result of the calculation wrapped in a <see cref="CalculationResult{TResult}"/> instance.</returns>
        CalculationResult<TResult> Calculate(TInput input);
    }
}
