
using Android.Drm;
using Microsoft.Maui.Controls.Internals;
//using Network;
using System.Transactions;

namespace A_Chess_App;

public partial class Chessboard : ContentPage
{
	public Chessboard()
	{
		InitializeComponent();
        ImageSourceNotNull(); //Sets up all the Image Source to not be null, fixes an object null reference bug
	}

    string movefigure = " "; 
    string coordinates = " ";
    string whoisturn = "b";
    List<string> lstpossiblemoves = new List<string> { }; 
    List<Button> lstbuttons;
    List<string> lstatoh = new List<string>() { "A", "B", "C", "D", "E", "F", "G", "H"};

    public void FieldClicked(object sender, EventArgs e) //When a Figure / ChessboardField is clicked is clicked
	{
        Button btnclicked = sender as Button;
        lstpossiblemoves.Clear(); 

        if (movefigure.Equals(" ")) //When no figure is selected
        {
            if (btnclicked.ImageSource != null) //And the image source of the clicked button is not null
            {
                if (CheckTurn(GetOnlyPath(btnclicked.ImageSource.ToString()))) //B -> Black moves, W > White moves (Whose turn)
                {              
                    movefigure = GetOnlyPath(btnclicked.ImageSource.ToString()); //Select the figure to move
                    coordinates = btnclicked.Text;
                    btnclicked.ImageSource = " ";
                }
            }
        }
	    else //When a figure is already selcted to move
        {
            lstbuttons = GetAllFields(); //Get All Buttons
            FigureAllPossibleMoves(movefigure); //Figure out all possiblemoves
 
            if (lstpossiblemoves.Contains(btnclicked.Text)) //When the move the user wants to make is possible
            {
                btnclicked.ImageSource = movefigure;
                movefigure = " ";

                if (btnclicked.Text != coordinates) //Updates the turn based on if the move was made
                {
                    UpdateTurn();
                    coordinates = " ";
                }
            }
        }
	}

    #region FigureOutMoves

    private void FigureAllPossibleMoves(string piece)
    {
        string[] specificlocation = coordinates.Split(' '); //Splits the coordinates into an x and y part

        if (piece.Equals("pawnb.png"))
        {
            if (specificlocation[1].Equals("7")) //When the pawn is on the first rank add a special move
            {
                lstpossiblemoves.Add(specificlocation[0] + " " + 5);
            }

            lstpossiblemoves.Add(specificlocation[0] + " " + BlackPawnMath(specificlocation[1])); //Pawn can move forward once
            lstpossiblemoves.Add(specificlocation[0] + " " + specificlocation[1]); //If you decide otherwise and don't want to move it
        }

        else if (piece.Equals("pawnw.png"))
        {
            if (specificlocation[1].Equals("2")) //When the pawn is on the first rank add a special move
            {
                lstpossiblemoves.Add(specificlocation[0] + " " + 4); 
            }

            lstpossiblemoves.Add(specificlocation[0] + " " + WhitePawnMath(specificlocation[1])); //Pawn can move forward once
            lstpossiblemoves.Add(specificlocation[0] + " " + specificlocation[1]); //If you decide otherwise and don't want to move it
        }

        else if (piece.Equals("kingw.png") || piece.Equals("kingb")) //The King can move one square around it
        {
            lstpossiblemoves.Add(specificlocation[0] + " " + WhitePawnMath(specificlocation[1]));
            lstpossiblemoves.Add(specificlocation[0] + " " + BlackPawnMath(specificlocation[1]));
            lstpossiblemoves.Add(BlackPawnMath(specificlocation[0]) + " " + specificlocation[1]);
            lstpossiblemoves.Add(WhitePawnMath(specificlocation[0]) + " " + specificlocation[1]);
            lstpossiblemoves.Add(BlackPawnMath(specificlocation[0]) + " " + WhitePawnMath(specificlocation[1]));
            lstpossiblemoves.Add(BlackPawnMath(specificlocation[0]) + " " + BlackPawnMath(specificlocation[1]));
            lstpossiblemoves.Add(WhitePawnMath(specificlocation[0]) + " " + WhitePawnMath(specificlocation[1]));
            lstpossiblemoves.Add(WhitePawnMath(specificlocation[0]) + " " + BlackPawnMath(specificlocation[1]));
            lstpossiblemoves.Add(specificlocation[0] + " " + specificlocation[1]); //If you decide otherwise and don't want to move it
        }

        else if (piece.Equals("rookw.png") || piece.Equals("rookb.png")) //The Rook moves in straight lines
        {
            for (int i = 1; i < 9; i++) //horizontal
            {
                lstpossiblemoves.Add(specificlocation[0] + " " + i);
            }

            for (int i = 1; i < 9; i++) //vertical
            {
                lstpossiblemoves.Add(i + " " + specificlocation[1]);
            }
        }

        else if (piece.Equals("bishopw.png") || piece.Equals("bishopb.png")) //The bishop moves in diagonals
        {

            for (int i = 1; i < 9; i++) //Left upwards
            {
                int num = int.Parse(specificlocation[1]) - i;
                int num2 = int.Parse(specificlocation[0]) - i;
                lstpossiblemoves.Add(num2 + " " + num.ToString());
            }

            for (int i = 1; i < 9; i++) //Right downwards
            {
                int num = int.Parse(specificlocation[1]) + i;
                int num2 = int.Parse(specificlocation[0]) + i;
                lstpossiblemoves.Add(num2 + " " + num.ToString());
            }

            for (int i = 1; i < 9; i++) //Left downwards
            {
                int num = int.Parse(specificlocation[1]) - i;
                int num2 = int.Parse(specificlocation[0]) + i;
                lstpossiblemoves.Add(num2 + " " + num.ToString());
            }

            for (int i = 1; i < 9; i++) //Right upwards
            {
                int num = int.Parse(specificlocation[1]) + i;
                int num2 = int.Parse(specificlocation[0]) - i;
                lstpossiblemoves.Add(num2 + " " + num.ToString());
            }
        }

        else if (piece.Equals("knightw.png") || piece.Equals("knightb.png")) //Knights move in an "L" Shape around themselves
        {
            lstpossiblemoves.Add(WhitePawnMath(specificlocation[0]) + " " + KnightPlusMath(specificlocation[1]));
            lstpossiblemoves.Add(WhitePawnMath(specificlocation[0]) + " " + KnightMinusMath(specificlocation[1]));
            lstpossiblemoves.Add(BlackPawnMath(specificlocation[0]) + " " + KnightPlusMath(specificlocation[1]));
            lstpossiblemoves.Add(BlackPawnMath(specificlocation[0]) + " " + KnightMinusMath(specificlocation[1]));
            lstpossiblemoves.Add(KnightMinusMath(specificlocation[0]) + " " + BlackPawnMath(specificlocation[1]));
            lstpossiblemoves.Add(KnightPlusMath(specificlocation[0]) + " " + BlackPawnMath(specificlocation[1]));
            lstpossiblemoves.Add(KnightPlusMath(specificlocation[0]) + " " + WhitePawnMath(specificlocation[1]));
            lstpossiblemoves.Add(KnightMinusMath(specificlocation[0]) + " " + WhitePawnMath(specificlocation[1]));
        }

        else if (piece.Equals("queenw.png") || piece.Equals("queenb.png")) //Queen is a combination of rook and bishop
        {
            for (int i = 1; i < 9; i++)
            {
                int num = int.Parse(specificlocation[1]) - i;
                int num2 = int.Parse(specificlocation[0]) - i;
                lstpossiblemoves.Add(num2 + " " + num.ToString());
            }

            for (int i = 1; i < 9; i++)
            {
                int num = int.Parse(specificlocation[1]) + i;
                int num2 = int.Parse(specificlocation[0]) + i;
                lstpossiblemoves.Add(num2 + " " + num.ToString());
            }

            for (int i = 1; i < 9; i++)
            {
                int num = int.Parse(specificlocation[1]) - i;
                int num2 = int.Parse(specificlocation[0]) + i;
                lstpossiblemoves.Add(num2 + " " + num.ToString());
            }

            for (int i = 1; i < 9; i++)
            {
                int num = int.Parse(specificlocation[1]) + i;
                int num2 = int.Parse(specificlocation[0]) - i;
                lstpossiblemoves.Add(num2 + " " + num.ToString());
            }

            for (int i = 1; i < 9; i++)
            {
                lstpossiblemoves.Add(specificlocation[0] + " " + i);
            }

            for (int i = 1; i < 9; i++)
            {
                lstpossiblemoves.Add(i + " " + specificlocation[1]);
            }
        }

        foreach (string move in lstpossiblemoves.ToList()) //Any moves, that exceed the chessboard, are deleted
        {
            string[] checker = move.Split(" ");
            if (int.Parse(checker[0]) > 8 || int.Parse(checker[1]) > 8 || int.Parse(checker[0]) < 1 || int.Parse(checker[1]) < 1)
            {
                lstpossiblemoves.Remove(move);
            }
        }
    }

    Func<string, int> BlackPawnMath = pos => int.Parse(pos) - 1; //Used for FigureOutAllPoosibleMoves -> Takes coordinate and dicreases it

    Func<string, int> WhitePawnMath = pos => int.Parse(pos) + 1; //Used for FigureOutAllPoosibleMoves -> Takes coordinate and increases it

    Func<string, int> KnightPlusMath = pos => int.Parse(pos) + 2; //Used for FigureOutAllPoosibleMoves -> Takes coordinate and increases it

    Func<string, int> KnightMinusMath = pos => int.Parse(pos) - 2; //Used for FigureOutAllPoosibleMoves -> Takes coordinate and dicreases it

    public List<Button> GetAllFields() //Gets All Buttons and adds them to a list
    {
        List<Button> lstbtn = new List<Button>();
        foreach (var x in board)
        {
            if (x is Button)
            {
                lstbtn.Add((Button)x);
            }
        }
        return lstbtn;
    }

    #endregion

    #region ToDo

    private bool IsPossible(Button currentbutton) //ToDo -> Figures out blockades and adjusts lstpossiblemoves accordingly
    {
        if (currentbutton.ImageSource.ToString() == "pawnb.png")
        {
            foreach (Button btn in lstbuttons)
            {
                foreach (string move in lstpossiblemoves)
                {
                    if (btn.ImageSource.ToString() == move) { lstpossiblemoves[lstpossiblemoves.IndexOf(move)] = "0 0"; }
                    DeleteAllFollowingMoves();
                }
            }
        }

        return true;
    }

    public void DeleteAllFollowingMoves() //ToDo -> Used in IsPossible -> Method that deletes the notpossible moves from lstpossiblemoves
    {

        foreach (string move in lstpossiblemoves)
        {
            string[] coords = move.Split(' ');
            List<int> lstIndexes = new List<int> { };

            if (move == "0 0")
            {
                //ToDo -> Figure out Blockades and Delete Following Moves accordingly
            }
        }
    }

    public void PawnHits(string piece) //Adds onto pawn moves it's special hit move
    {
        //TODO not finished yet
        int newX1 = int.Parse(coordinates[0].ToString()) - 1;
        int newX2 = int.Parse(coordinates[0].ToString()) + 1;
        int y;
        if (piece.Equals("pawnw.png"))
            y = int.Parse(coordinates[2].ToString()) + 1;
        else
            y = int.Parse(coordinates[2].ToString()) - 1;
        string pX1 = "";

        pX1 = board.FindByName<Button>(lstatoh[newX1 - 1].ToString() + y.ToString()).ImageSource.ToString();
        if (newX1 > 0 && !pX1.Equals(" "))
        {
            if ((piece.Equals("pawnw.png") && !IsWhite(pX1)) || (piece.Equals("pawnb.png") && IsWhite(pX1)))
                lstpossiblemoves.Add(newX1.ToString() + " " + y.ToString());
        }
        string pX2 = board.FindByName<Button>(lstatoh[newX2].ToString() + y.ToString()).ImageSource.ToString();
        if (newX2 < 9 && pX2.Equals(" "))
        {
            if ((piece.Equals("pawnw.png") && !IsWhite(pX2)) || (piece.Equals("pawnb.png") && IsWhite(pX2)))
                lstpossiblemoves.Add(newX2.ToString() + " " + y.ToString());
        }
    }

    public bool IsWhite(string piece) //Used in pawnHits -> for differenciating beetween white and black
    {
        //TODO
        return false;
    }
    #endregion

    #region Start Game

    public void ImageSourceNotNull() //Sets all Image Sources of everyBUtton to "" instead of null
    {
        List<Button> buttons = GetAllFields();
        foreach (Button btn in buttons)
        {
            btn.ImageSource = "";
        }
    }

    public void StartGame(object sender, EventArgs e) //Adds all Images to the rights buttons
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

        btnStart.IsVisible = false;
    }

    #endregion

    #region PlayLogic

    private void UpdateTurn() //Updates the turn, which colour can move
    {
        if (whoisturn.Equals("b")) { whoisturn = "w"; }
        else { whoisturn = "b"; }
    }

    private bool CheckTurn(string source) //Checks if the figure belongs to the colour that can move
    {
        string[] ender = source.Split(".");
        if (ender[0].EndsWith(whoisturn))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private string GetOnlyPath(string path) //Gets only the of the figure
    {
        string[] onlypath = path.Split(":");
        return onlypath[1].Trim();
    }

    #endregion
}
