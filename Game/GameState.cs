using System.Security.Cryptography.X509Certificates;

namespace Tictactoe
{
    class GameState
    {
        public View ActiveView { get; set; } = View.Game;

        public Player ActivePlayer { get; set; } = Player.X;

        public Player?[,] Grid { get; } = new Player?[3, 3];

        public void Reset()
        {
            for (int x = 0; x < Grid.GetLength(0); x++)
                for (int y = 0; y < Grid.GetLength(1); y++)
                    Grid[x, y] = null;
        }

        public void SwitchToOppositePlayer(Player currentPlayer)
        {
            if (currentPlayer == Player.X)
                ActivePlayer = Player.O;
            else
                ActivePlayer = Player.X;
        }

        public void SwitchToOppositePlayer()
        {
            SwitchToOppositePlayer(ActivePlayer);
        }

        public bool IsGridFull()
        {
            for (int x = 0; x < Grid.GetLength(0); x++)
                for (int y = 0; y < Grid.GetLength(1); y++)
                    if (Grid[x, y] == null)
                        return false;

            return true;
        }

        public Player? HasSomeoneWon()
        {
            var width = Grid.GetLength(0);
            var height = Grid.GetLength(1);

            int continousFields = 0;
            Player? winningPlayer = null;
            var check = (int x, int y) =>
            {
                if (winningPlayer != Grid[x, y])
                {
                    winningPlayer = Grid[x, y];
                    continousFields = 1;
                }
                else
                    continousFields++;
            };
            var reset = () =>
            {
                continousFields = 0;
                winningPlayer = null;
            };

            //vertical check
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                    check(x, y);

                if (winningPlayer != null && continousFields == 3)
                    return winningPlayer;
            }

            //horizontal check
            reset();
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                    check(x, y);

                if (winningPlayer != null && continousFields == 3)
                    return winningPlayer;
            }

            //diagonal top left check
            reset();
            for (int i = 0; i < width && i < height; i++)
                check(i, i);

            if (winningPlayer != null && continousFields == 3)
                return winningPlayer;

            //diagonal top right check
            reset();
            for (int i = 0; i < width && i < height; i++)
                check(width - i - 1, i);

            if (winningPlayer != null && continousFields == 3)
                return winningPlayer;

            return null;
        }
    }

    class Statistics
    {
        public int XWins { get; set; }

        public int YWins { get; set; }

        public void AddWin(Player player)
        {
            if (player == Player.X)
                XWins++;
            else
                YWins++;
        }
    }

    enum Player
    {
        X,
        O
    }

    enum View
    {
        Game,
        Stats,
        EndGame,
    }
}