namespace Xample.Simple;

using Microsoft.Extensions.Logging;

internal partial class Form1 : Form
{
    private readonly ILogger<Form1> _logger;

    public Form1(ILogger<Form1> logger)
    {
        _logger = logger;
        InitializeComponent();

        Form1Created();
    }

    [LoggerMessage(EventId = 1, Level = LogLevel.Information, Message = "From1 created.")]
    public partial void Form1Created();
}
