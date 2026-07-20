using CodingTracker.Solomonlol.Controllers;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;


namespace CodingTracker.Solomonlol
{
    internal class Menu
    {

        public void MainMenu()
        {
            CodingController controller = new CodingController();
            controller.CreateTableIfNotExists();
            controller.GetData();
            var choice = AnsiConsole.Prompt(new SelectionPrompt<string>()
                .Title("Select an [green]option[/]:")
                .AddChoices("Show Data", "Add new record", "Update record", "Delete record", "Close app"));

            AnsiConsole.MarkupLine($"Deploying to [blue]{choice}[/]");
            
            
        }
        
    }

}
