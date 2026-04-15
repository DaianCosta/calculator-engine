using CalculatorEngine.Engine;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Steps
builder.Services.AddScoped<ICalculationStep, InterestStep>();
builder.Services.AddScoped<ICalculationStep, FineStep>();
builder.Services.AddScoped<ICalculationStep, ChargesStep>();
builder.Services.AddScoped<ICalculationStep, MonetaryCorrectionStep>();

// Engine
builder.Services.AddScoped<StepRegistry>();
builder.Services.AddScoped<CalculationEngine>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();