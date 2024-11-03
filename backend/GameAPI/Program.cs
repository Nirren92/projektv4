using System;
using System.Text.Json;
using GameAPI;
using Microsoft.AspNetCore.Http.HttpResults;


namespace GameAPI
{

    public class Program
    {
        
        public static async Task Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);         
            builder.Services.AddEndpointsApiExplorer();

            //lägger till cors så att anslutningar blir tillåtna oavsett. 
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyOrigin()  
                        .AllowAnyHeader()  
                        .AllowAnyMethod(); 
                });
            });

            var app = builder.Build();
            app.UseCors("AllowAll");
            // API för att beräkna vilket steg som AI ska ta. 
            app.MapPost("/AI_Move", async (HttpRequest request) =>
            {
                try
                {
                    //skapar mitt AI objekt där jag kontrollerar vilket som är det bästa draget. 
                    AI_Unbeatable ai = new AI_Unbeatable();
                    //skapar ett objekt med spelplanen. validerar i skapandet. 
                    var game_IN = await request.ReadFromJsonAsync<Game>(); 
                    //beräknar drag. data in är ok och reda att användas
                    var (bestMove, score) = ai.CalcMove(game_IN, true, 0);
                    //retunerar bästa draget. Retuneras -1 betyder det att det inte finns några tillgänliga drag. 
                    return Results.Json(new { BestMove = bestMove, Score = score });
                }
                catch (Exception ex)
                {
                    Console.WriteLine("det gick fel med indatan"+ ex.Message);
                    return Results.BadRequest(ex.Message);
                }
            })
            .WithName("GetAI_Move")
            .WithOpenApi();
            app.Run();

        }
    }

}