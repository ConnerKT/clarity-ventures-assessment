using email;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapPost("/email", async (string email, string subject, string htmlMessage) =>
{
    SendEmail sender = new SendEmail(builder.Configuration);
    await sender.SendEmailAsync(email, subject, htmlMessage);
    return "sent";
})

.WithName("SendEmail")
.WithOpenApi();

app.Run();

