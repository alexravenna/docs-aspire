using System.Net.Mail;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add services to the container.
// <smtp>
builder.Services.AddSingleton<SmtpClient>(sp =>
{
    var smtpUri = new Uri(builder.Configuration.GetConnectionString("maildev")!);

    var smtpClient = new SmtpClient(smtpUri.Host, smtpUri.Port);

    return smtpClient;
});
// </smtp>

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

// <subs>
app.MapPost("/subscribe", async (SmtpClient smtpClient, string email) =>
{
    using var message = new MailMessage("newsletter@yourcompany.com", email)
    {
        Subject = "Welcome to our newsletter!",
        Body = "Thank you for subscribing to our newsletter!"
    };

    await smtpClient.SendMailAsync(message);
});

app.MapPost("/unsubscribe", async (SmtpClient smtpClient, string email) =>
{
    using var message = new MailMessage("newsletter@yourcompany.com", email)
    {
        Subject = "You are unsubscribed from our newsletter!",
        Body = "Sorry to see you go. We hope you will come back soon!"
    };

    await smtpClient.SendMailAsync(message);
});
// </subs>

app.Run();
