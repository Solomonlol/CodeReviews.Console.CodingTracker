using CodingTracker.Solomonlol.Controllers;
using CodingTracker.Solomonlol.Model;
using Dapper;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;
using static CodingTracker.Solomonlol.Model.MenuValues;


namespace CodingTracker.Solomonlol
{
    internal class Menu
    {
        
        public void MainMenu()
        {
            cod.CreateTableIfNotExists();
            while (true)
            {
                var choice = AnsiConsole.Prompt(new SelectionPrompt<string>()
                    .Title("Select an [green]option[/]:")
                    .AddChoices(menuValues.Keys));

                menuValues[choice]();
            }
        }
    }
}
