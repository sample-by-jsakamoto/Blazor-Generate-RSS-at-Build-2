using System.ServiceModel.Syndication;
using System.Xml;
using CommandLineSwitchParser;

var option = CommandLineSwitch.Parse<CommandLineOptions>(ref args);

// Set basic feed information
var feed = new SyndicationFeed("Feed Title", "Feed Description", new Uri("http://example.com"));

// Create feed items
var items = new SyndicationItem[]
{
    new (
        "Item 1 Title",
        "Item 1 Content",
        new Uri("http://example.com/item1"),
        "Item 1 ID",
        DateTimeOffset.Now
    ),
    new (
        "Item 2 Title",
        "Item 2 Content",
        new Uri("http://example.com/item2"),
        "Item 2 ID",
        DateTimeOffset.Now
    )
};

// Add items to the feed
feed.Items = items;

// Prepare the output path
var outputPath = option.OutputPath ?? "feed.xml";
var targetDirectory = Path.GetDirectoryName(Path.GetFullPath(outputPath));
if (targetDirectory is not null) Directory.CreateDirectory(targetDirectory);

// Save the RSS feed to a XML file
using var writer = XmlWriter.Create(outputPath);
var rssFormatter = new Rss20FeedFormatter(feed);
rssFormatter.WriteTo(writer);
