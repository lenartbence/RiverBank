using System;
using NUnit.Framework;
using RiverBank.Core.Calculators;
using RiverBank.Core.Calculators.LoanCalculator;

namespace RiverBank.Core_uTest
{
    [TestFixture]
    public class LoanCalculatorTests
    {
        private LoanCalculator _calculator;

        [SetUp]
        public void SetUp()
        {
            _calculator = new LoanCalculator();
        }

        [Test]
        public void Calculate_HappyPath_ResultContainsCorrectValue()
        {
            var input = new LoanCalculatorInput(100m, 12, 0.1m);

            CalculationResult<decimal> result = _calculator.Calculate(input);

            Assert.That(() => result.Successful, Is.True);
            Assert.That(() => result.Value, Is.EqualTo(8.79m));
        }

        [Test]
        public void Calculate_ZeroAmount_ResultContainsZero()
        {
            var input = new LoanCalculatorInput(0, 12, 0.1m);

            CalculationResult<decimal> result = _calculator.Calculate(input);

            Assert.That(() => result.Successful, Is.True);
            Assert.That(() => result.Value, Is.EqualTo(0));
        }

        [Test]
        public void Calculate_NegativeAmount_FailsWithInvalidInputReason()
        {
            var input = new LoanCalculatorInput(-100m, 12, 0.1m);

            CalculationResult<decimal> result = _calculator.Calculate(input);

            Assert.That(() => result.Successful, Is.False);
            Assert.That(() => result.FailureReason, Is.EqualTo(CalculationFailureReason.InvalidInput));
        }

        [Test]
        public void Calculate_ZeroTerm_FailsWithInvalidInputReason()
        {
            var input = new LoanCalculatorInput(100m, 0, 0.1m);

            CalculationResult<decimal> result = _calculator.Calculate(input);

            Assert.That(() => result.Successful, Is.False);
            Assert.That(() => result.FailureReason, Is.EqualTo(CalculationFailureReason.InvalidInput));
        }

        [Test]
        public void Calculate_NegativeTerm_FailsWithInvalidInputReason()
        {
            var input = new LoanCalculatorInput(100m, -12, 0.1m);

            CalculationResult<decimal> result = _calculator.Calculate(input);

            Assert.That(() => result.Successful, Is.False);
            Assert.That(() => result.FailureReason, Is.EqualTo(CalculationFailureReason.InvalidInput));
        }

        [Test]
        public void Calculate_ZeroInterestRate_ResultContainsCorrectValue()
        {
            var input = new LoanCalculatorInput(100m, 12, 0m);

            CalculationResult<decimal> result = _calculator.Calculate(input);

            Assert.That(() => result.Successful, Is.True);
            Assert.That(() => result.Value, Is.EqualTo(8.33m));
        }

        [Test]
        public void Calculate_NegativeInterestRate_FailsWithInvalidInputReason()
        {
            var input = new LoanCalculatorInput(100m, 12, -0.1m);

            CalculationResult<decimal> result = _calculator.Calculate(input);

            Assert.That(() => result.Successful, Is.False);
            Assert.That(() => result.FailureReason, Is.EqualTo(CalculationFailureReason.InvalidInput));
        }

        [Test]
        public void Calculate_NullInput_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _calculator.Calculate(null));
        }
    }
}
