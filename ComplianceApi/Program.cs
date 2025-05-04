using ComplianceApi;
using Microsoft.AspNetCore.Cors;

var builder = WebApplication.CreateBuilder(args);

// ? Register CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
        policy.WithOrigins("http://localhost:3000")
              .AllowAnyMethod()
              .AllowAnyHeader());
});

var app = builder.Build();

// ? Apply CORS policy
app.UseCors("AllowFrontend");

// === API ROUTES ===
var trades = new List<FlaggedTrade>
{
    new() { Id = 1, Symbol = "AAPL", Reason = "Unusual volume", Reviewed = false },
    new() { Id = 2, Symbol = "TSLA", Reason = "Outside trading window", Reviewed = false },
    new() { Id = 3, Symbol = "NVDA", Reason = "Exceeded daily threshold", Reviewed = false }
};

app.MapGet("/api/trades", () => trades);

app.MapPut("/api/trades/{id}/review", (int id) =>
{
    var trade = trades.FirstOrDefault(t => t.Id == id);
    if (trade == null) return Results.NotFound();
    trade.Reviewed = true;
    return Results.Ok(trade);
});

app.Run();
