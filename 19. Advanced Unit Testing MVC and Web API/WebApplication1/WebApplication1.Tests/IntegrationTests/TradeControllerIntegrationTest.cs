using Fizzler.Systems.HtmlAgilityPack;
using FluentAssertions;
using HtmlAgilityPack;

namespace WebApplication1.Tests.IntegrationTests
{
 public class TradeControllerIntegrationTest : IClassFixture<CustomWebApplicationFactory>
 {
  private readonly HttpClient _client;
  public TradeControllerIntegrationTest(CustomWebApplicationFactory factory)
  {
   _client = factory.CreateClient();
  }

  #region Index

  [Fact]
  public async Task Index_ToReturnView()
  {
   //Arrange
   //Act
   var response = await _client.GetAsync("/Trade/Index/MSFT");

   //Assert
   response.IsSuccessStatusCode.Should().BeTrue();

   var responseBody = await response.Content.ReadAsStringAsync();

   var html = new HtmlDocument();
   html.LoadHtml(responseBody);
   var document = html.DocumentNode;

   document.QuerySelectorAll(".price").Should().NotBeNull();
  }

  #endregion
 }
}
