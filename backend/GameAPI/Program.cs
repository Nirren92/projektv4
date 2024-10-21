using System;
using GameAPI;


namespace GameAPI
{

    public class Program
    {
        
        public static async Task Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);

           
            AI_Unbeatable ai = new AI_Unbeatable();


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


            //Spelplanen skapas.
            char[] plane = new char[9] { 'X', '-', 'O', 'X', '-', '-', '-', '-', '-' };

            app.MapGet("/AI_Move", () =>
            {
                var (bestMove, score) = ai.CalcMove(plane, true, 0);
                return "tjena" + bestMove +"score"+score;
            })
            .WithName("GetAI_Move")
            .WithOpenApi();

            app.Run();

        }
    }

}