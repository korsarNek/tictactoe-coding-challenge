using Tictactoe;

var game = new Game(new Screen(new ConsoleWrapper()), new GameState(), new Statistics(), new InputParser());
game.RunFrame("");
while (game.RunFrame(Console.ReadLine() ?? "")) {}
