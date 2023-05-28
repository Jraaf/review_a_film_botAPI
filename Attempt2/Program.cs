using Attempt2.Models;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bots.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Exceptions;
using Telegram.Bots.Types;
using UpdateType = Telegram.Bot.Types.Enums.UpdateType;
using Update = Telegram.Bot.Types.Update;
using Telegram.Bots.Http;
using BotCommand = Telegram.Bot.Types.BotCommand;
using Attempt2.Controllers;
using MongoDB.Bson.IO;
using IMDbApiLib;
using System.Text.RegularExpressions;
using Attempt2;
using MongoDB.Driver;
using MongoDB.Bson;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IMongoClient, MongoClient>(s =>
{
    var uri = s.GetRequiredService<IConfiguration>()["MongoUri"];
    return new MongoClient(uri);
});
builder.Services.AddControllers();
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

app.UseAuthorization();

app.MapControllers();

app.Run();