namespace A_Chess_App;

public partial class Chessboard : ContentPage
{
	public Chessboard()
	{
		InitializeComponent();
	}

	

	public void FieldClicked(object sender, EventArgs e)
	{
		Button btnonboard = sender as Button;

	
	}

	public void StartGame(object sender, EventArgs e)
	{

		aaa.Text = FileSystem.AppDataDirectory;
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
    }
}