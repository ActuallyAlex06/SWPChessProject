
using Microsoft.Maui.Controls.Internals;

namespace A_Chess_App;

public partial class Chessboard : ContentPage
{
	public Chessboard()
	{
		InitializeComponent();
	}
    string MoveFigure = " ";
    string Coordinates = " ";
    static string whoseturn = "b";
    List<string> possiblemoves = new List<string> { };

	public void FieldClicked(object sender, EventArgs e)
	{
        Button btnclicked = sender as Button;
        possiblemoves.Clear();
        if (MoveFigure.Equals(" "))
        {
            if (btnclicked.ImageSource != null)
            {
                if (CheckTurn(GetOnlyPath(btnclicked.ImageSource.ToString())))
                {              
                    MoveFigure = GetOnlyPath(btnclicked.ImageSource.ToString());
                    Coordinates = btnclicked.Text;
                    btnclicked.ImageSource = " ";
                }
            }
        }
	    else 
        {
            FigureAllPossibleMoves(MoveFigure);
            if (possiblemoves.Contains(btnclicked.Text))
            {
                btnclicked.ImageSource = MoveFigure;
                MoveFigure = " ";

                if (btnclicked.Text != Coordinates)
                {
                    UpdateTurn();
                    Coordinates = " ";
                }
            }
        }
	}

    private void FigureAllPossibleMoves(string piece)
    { 
        string[] specificlocation = Coordinates.Split(' ');

        if (piece.Equals("pawnb.png"))
        {
            if (specificlocation[1].Equals("7")) { possiblemoves.Add(specificlocation[0] + " " + 5); }
            possiblemoves.Add(specificlocation[0] + " " + BlackPawnMath(specificlocation[1]));
            possiblemoves.Add(specificlocation[0] + " " + specificlocation[1]);
        }
        else if (piece.Equals("pawnw.png"))
        {
            if (specificlocation[1].Equals("2")) { possiblemoves.Add(specificlocation[0] + " " + 4); }
            possiblemoves.Add(specificlocation[0] + " " + WhitePawnMath(specificlocation[1]));
            possiblemoves.Add(specificlocation[0] + " " + specificlocation[1]);
        }
        else if (piece.Equals("kingw.png") || piece.Equals("kingb"))
        {
            possiblemoves.Add(specificlocation[0] + " " + WhitePawnMath(specificlocation[1]));
            possiblemoves.Add(specificlocation[0] + " " + BlackPawnMath(specificlocation[1]));
            possiblemoves.Add(BlackPawnMath(specificlocation[0]) + " " + specificlocation[1]);
            possiblemoves.Add(WhitePawnMath(specificlocation[0]) + " " + specificlocation[1]);
            possiblemoves.Add(BlackPawnMath(specificlocation[0]) + " " + WhitePawnMath(specificlocation[1]));
            possiblemoves.Add(BlackPawnMath(specificlocation[0]) + " " + BlackPawnMath(specificlocation[1]));
            possiblemoves.Add(WhitePawnMath(specificlocation[0]) + " " + WhitePawnMath(specificlocation[1]));
            possiblemoves.Add(WhitePawnMath(specificlocation[0]) + " " + BlackPawnMath(specificlocation[1]));
            possiblemoves.Add(specificlocation[0] + " " + specificlocation[1]);
        }
        else if (piece.Equals("rookw.png") || piece.Equals("rookb.png"))
        {
            for (int i = 1; i < 9; i++)
            {
                possiblemoves.Add(specificlocation[0] + " " + i);
            }

            for (int i = 1; i < 9; i++)
            {
                possiblemoves.Add(i + " " + specificlocation[1]);
            }
        }
        else if (piece.Equals("bishopw.png") || piece.Equals("bishopb.png")) {

            for (int i = 1; i < 9; i++)
            {
                int num = int.Parse(specificlocation[1]) - i;
                int num2 = int.Parse(specificlocation[0]) - i;
                possiblemoves.Add(num2 + " " + num.ToString());
            }

            for (int i = 1; i < 9; i++)
            {
                int num = int.Parse(specificlocation[1]) + i;
                int num2 = int.Parse(specificlocation[0]) + i;
                possiblemoves.Add(num2 + " " + num.ToString());
            }

            for (int i = 1; i < 9; i++)
            {
                int num = int.Parse(specificlocation[1]) - i;
                int num2 = int.Parse(specificlocation[0]) + i;
                possiblemoves.Add(num2 + " " + num.ToString());
            }

            for (int i = 1; i < 9; i++)
            {
                int num = int.Parse(specificlocation[1]) + i;
                int num2 = int.Parse(specificlocation[0]) - i;
                possiblemoves.Add(num2 + " " + num.ToString());
            }
        }
        else if (piece.Equals("knightw.png") || piece.Equals("knightb.png"))
        {
            possiblemoves.Add(WhitePawnMath(specificlocation[0]) + " " + KnightPlusMath(specificlocation[1]));
            possiblemoves.Add(WhitePawnMath(specificlocation[0]) + " " + KnightMinusMath(specificlocation[1]));
            possiblemoves.Add(BlackPawnMath(specificlocation[0]) + " " + KnightPlusMath(specificlocation[1]));
            possiblemoves.Add(BlackPawnMath(specificlocation[0]) + " " + KnightMinusMath(specificlocation[1]));
            possiblemoves.Add(KnightMinusMath(specificlocation[0]) + " " + BlackPawnMath(specificlocation[1]));
            possiblemoves.Add(KnightPlusMath(specificlocation[0]) + " " + BlackPawnMath(specificlocation[1]));
            possiblemoves.Add(KnightPlusMath(specificlocation[0]) + " " + WhitePawnMath(specificlocation[1]));
            possiblemoves.Add(KnightMinusMath(specificlocation[0]) + " " + WhitePawnMath(specificlocation[1]));
        }
        else if (piece.Equals("queenw.png") || piece.Equals("queenb.png"))
        {
            for (int i = 1; i < 9; i++)
            {
                int num = int.Parse(specificlocation[1]) - i;
                int num2 = int.Parse(specificlocation[0]) - i;
                possiblemoves.Add(num2 + " " + num.ToString());
            }

            for (int i = 1; i < 9; i++)
            {
                int num = int.Parse(specificlocation[1]) + i;
                int num2 = int.Parse(specificlocation[0]) + i;
                possiblemoves.Add(num2 + " " + num.ToString());
            }

            for (int i = 1; i < 9; i++)
            {
                int num = int.Parse(specificlocation[1]) - i;
                int num2 = int.Parse(specificlocation[0]) + i;
                possiblemoves.Add(num2 + " " + num.ToString());
            }

            for (int i = 1; i < 9; i++)
            {
                int num = int.Parse(specificlocation[1]) + i;
                int num2 = int.Parse(specificlocation[0]) - i;
                possiblemoves.Add(num2 + " " + num.ToString());
            }

            for (int i = 1; i < 9; i++)
            {
                possiblemoves.Add(specificlocation[0] + " " + i);
            }

            for (int i = 1; i < 9; i++)
            {
                possiblemoves.Add(i + " " + specificlocation[1]);
            }
        }

        foreach(string move in possiblemoves.ToList())
        {
            string[] checker = move.Split(" ");
            if (int.Parse(checker[0]) > 8 || int.Parse(checker[1]) > 8 || int.Parse(checker[0]) < 1 || int.Parse(checker[1]) < 1)
            {
                possiblemoves.Remove(move);
            }
        }
    }

    Func<string, int> BlackPawnMath = pos => int.Parse(pos) - 1;

    Func<string, int> WhitePawnMath = pos => int.Parse(pos) + 1;

    Func<string, int> KnightPlusMath = pos => int.Parse(pos) + 2;

    Func<string, int> KnightMinusMath = pos => int.Parse(pos) - 2;

    private void UpdateTurn()
    {
        if (whoseturn.Equals("b")) { whoseturn = "w"; }
        else  { whoseturn = "b"; }
    }

    private bool CheckTurn(string source)
    {
        string[] ender = source.Split(".");
        if (ender[0].EndsWith(whoseturn)) { return true; }
        else { return false; }
    }

    private string GetOnlyPath(string path)
    {

        string[] onlypath = path.Split(":");      
        return onlypath[1].Trim();
    }

	public void StartGame(object sender, EventArgs e)
	{
        A1.ImageSource = "rookw.png";
		B1.ImageSource = "knightw.png";
		C1.ImageSource = "bishopw.png";
        D1.ImageSource = "queenw.png";
        E1.ImageSource = "kingw.png";
        F1.ImageSource = "bishopw.png";
        G1.ImageSource = "knightw.png";
        H1.ImageSource = "rookw.png";
        A8.ImageSource = "rookb.png";
        B8.ImageSource = "knightb.png";
        C8.ImageSource = "bishopb.png";
        D8.ImageSource = "queenb.png";
        E8.ImageSource = "kingb.png";
        F8.ImageSource = "bishopb.png";
        G8.ImageSource = "knightb.png";
        H8.ImageSource = "rookb.png";

        A2.ImageSource = "pawnw.png";
        B2.ImageSource = "pawnw.png";
        C2.ImageSource = "pawnw.png";
        D2.ImageSource = "pawnw.png";
        E2.ImageSource = "pawnw.png";
        F2.ImageSource = "pawnw.png"; 
        G2.ImageSource = "pawnw.png";
        H2.ImageSource = "pawnw.png";

        A7.ImageSource = "pawnb.png";
        B7.ImageSource = "pawnb.png";
        C7.ImageSource = "pawnb.png";
        D7.ImageSource = "pawnb.png";
        E7.ImageSource = "pawnb.png";
        F7.ImageSource = "pawnb.png";
        G7.ImageSource = "pawnb.png";
        H7.ImageSource = "pawnb.png";

        btnstart.IsVisible = false;
    }
}