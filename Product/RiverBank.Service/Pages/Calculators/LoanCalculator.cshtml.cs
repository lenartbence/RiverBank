using System;
using System.Collections.Generic;
using DataAnnotationsExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using RiverBank.Core.Calculators;
using RiverBank.Core.Calculators.LoanCalculator;
using RiverBank.Service.InputValidation;
using RiverBank.Service.Localization;
using RiverBank.Service.Resources;

namespace RiverBank.Service.Pages.Calculators
{
    public class LoanCalculatorModel : PageModel
    {
        private readonly IEnumerable<int> _selectableTermYears = new List<int>() { 5, 10, 15, 20, 25 };
        private readonly ILoanCalculator _loanCalculator;
        private readonly IStringLocalizer<LoanCalculatorModel> _localizer;

        public LoanCalculatorModel(ILoanCalculator loanCalculator,
                                   IStringLocalizer<LoanCalculatorModel> localizer)
        {
            _loanCalculator = loanCalculator;
            _localizer = localizer;

            SelectableTermYears = new SelectList(_selectableTermYears);
        }

        [BindProperty(SupportsGet = true)]
        [Min(0, ErrorMessageResourceType = typeof(ModelBindingMessages), ErrorMessageResourceName = nameof(ModelBindingMessages.ValueMustNotBeNegative))]
        public decimal Amount { get; set; }

        // TODO: Eliminate collection duplication
        [BindProperty(SupportsGet = true)]
        [CollectionContainsValue(new object[] { 5, 10, 15, 20, 25 }, ErrorMessageResourceType = typeof(ModelBindingMessages), ErrorMessageResourceName = nameof(ModelBindingMessages.AttemptedValueIsInvalid))]
        public int TermYears { get; set; }

        [BindProperty]
        public string OutputMessage { get; set; }

        public SelectList SelectableTermYears { get; }

        public decimal InterestRate { get; } = 0.1m;

        public void OnGet()
        {
            if (!ModelState.IsValid)
            {
                return;
            }

            var input = new LoanCalculatorInput(Amount, TermYears * 12, InterestRate);

            CalculationResult<decimal> result = null;
            try
            {
                result = _loanCalculator.Calculate(input);
            }
            catch (ArgumentNullException)
            {
                OutputMessage = _localizer[LocalizationResourceKeys.LoanCalculatorErrorMessage];
            }

            if (result != null && result.Successful)
            {
                OutputMessage = string.Format(_localizer[LocalizationResourceKeys.LoanCalculatorResultMessage], Amount, TermYears, result.Value);
            }
        }
    }
}
