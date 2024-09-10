using BlackHole._360.BusinessLogic.DTO.User;
using BlackHole._360.BusinessLogic.Services;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

namespace BlackHole._360.Api.Controllers;

public class NewsController(IHttpClientFactory httpClientFactory) : BaseController()
{
    private class News
    {
        public string? ImageLink { get; set; }
        public string? Title { get; set; }
        public string? Link { get; set;}
        public string? Content { get; set; }
        public string? Date { get; set; }

    }

    [HttpGet]
    public async Task<IActionResult> IndexAsync(int offset, int count, CancellationToken cancellationToken = default)
    {
        return Ok(await GetNewsAsync(offset, count));
    }

    [HttpGet("{link}")]
    public async Task<IActionResult> GetAsync(string link, CancellationToken cancellationToken = default)
    {
        var httpClient = httpClientFactory.CreateClient();

        var html = await httpClient.GetAsync(Uri.UnescapeDataString(link), cancellationToken);

        return Ok(await html.Content.ReadAsStringAsync(cancellationToken));
    }

    private async Task<IEnumerable<News>> GetNewsAsync(int offset, int count)
    {
        const string aceLink = "https://ace.ucv.ro/media/index.php?pag=";
        var newsList = new List<News>();

        var pagesToFetch = (offset + count) / 10;

        var httpClient = httpClientFactory.CreateClient();

        for (var index = 1; index <= pagesToFetch; index++)
        {
            var html = await httpClient.GetAsync(aceLink + index);

            var document = new HtmlDocument();
            document.LoadHtml(await html.Content.ReadAsStringAsync());

            var mediaElements = document.DocumentNode.SelectNodes("//div[@class='media_element']");

            foreach (var element in mediaElements)
            {
                var imageNode = element.SelectSingleNode(".//img[@class='media_imagine']");
                var imageUrl = "https://ace.ucv.ro/" + imageNode?.Attributes["src"]?.Value;

                var titleNode = element.SelectSingleNode(".//h2/a");
                var title = titleNode?.InnerText;

                var link = titleNode?.Attributes["href"]?.Value;

                var dateNode = element.SelectSingleNode(".//h3");
                var date = dateNode?.InnerText.Split('@').Last().Trim().Split(' ').Last();

                var contentNode = element.SelectSingleNode(".//h2/following-sibling::text()[normalize-space()]");
                var content = HtmlEntity.DeEntitize(contentNode?.InnerText?.Trim() ?? "");

                newsList.Add(new News
                {
                    ImageLink = imageUrl,
                    Title = title,
                    Link = "https://ace.ucv.ro/media/" + link,
                    Date = date,
                    Content = content
                });
            }
        }


        return newsList;
    }
}
