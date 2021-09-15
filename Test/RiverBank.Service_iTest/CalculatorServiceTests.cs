using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;
using RiverBank.Service;
using RiverBank.Service_iTest.Helpers;
using System.Threading.Tasks;

namespace RiverBank.Service_iTest
{
    [TestFixture]
    [Timeout(10000)]
    public class CalculatorServiceTests
    {
        private WebApplicationFactory<Startup> _webAppFactory;

        [SetUp]
        public void SetUp()
        {
            _webAppFactory = new WebApplicationFactory<Startup>();
        }

        [TearDown]
        public void TearDown()
        {
            _webAppFactory.Dispose();
        }

        [Test]
        public async Task Get_ValidInput_OutputMessageContainsCorrectValues()
        {
            int amount = 1000;
            int termYears = 5;
            var client = _webAppFactory.CreateClient();

            var response = await client.GetAsync(GetLoanCalculatorUrl(amount, termYears));
            var content = await HtmlHelpers.GetDocumentAsync(response);
            var output = GetOutputMessage(content);

            Assert.That(() => output.Contains($"{amount:N0}"));
            Assert.That(() => output.Contains(termYears.ToString()));
            Assert.That(() => output.Contains("21"));
        }

        [Test]
        public async Task Get_InvalidInput_ErrorMessageForFieldAndNoOutputMessage()
        {
            int amount = -1000;
            int termYears = 5;
            var client = _webAppFactory.CreateClient();

            var response = await client.GetAsync(GetLoanCalculatorUrl(amount, termYears));
            var content = await HtmlHelpers.GetDocumentAsync(response);
            var output = GetOutputMessage(content);
            var errorMessage = GetElement(content, "span", "amount-error-message").TextContent;

            Assert.That(() => errorMessage, Is.Not.Empty);
            Assert.That(() => output, Is.Empty);
        }

        private string GetOutputMessage(IHtmlDocument document)
        {
            return GetElement(document, "h4", "output-message").TextContent;
        }

        private IElement GetElement(IHtmlDocument document, string elementType, string elementName)
        {
            return document.QuerySelector($"{elementType}[id='{elementName}']");
        }

        private string GetLoanCalculatorUrl(int amount, int termYears) => $"/Calculators/LoanCalculator?Amount={amount}&TermYears={termYears}";
    }
}