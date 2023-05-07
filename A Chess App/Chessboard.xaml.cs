//using Android.Bluetooth;

namespace A_Chess_App;

public partial class Chessboard : ContentPage
{
	public Chessboard()
	{
		InitializeComponent();
	}

	public void FieldClicked(object sender, EventArgs e)
	{
        ImageButton ibtn = (ImageButton)sender;
        ImageButton field = FindByName(ibtn.StyleId) as ImageButton;
        field.Source = "C:\\Users\\User\\source\\repos\\SWChessProject\\A Chess App\\Resources\\Images\\Images\\kingb";
        foreach (var x in board)
        {
            if (x is ImageButton)
            {
                ImageButton y = x as ImageButton;
                if(y.StyleId == ibtn.StyleId)
                {
                    ImageButton z = (ImageButton)FindByName(y.StyleId);
                    z.Source = "C:\\Users\\User\\source\\repos\\SWChessProject\\A Chess App\\Resources\\Images\\Images\\kingb";
                    //throw new NotFiniteNumberException();
                }
            }
        }

    }

    public List<ImageButton> GetAllFields()
    {
        List<ImageButton> li = new List<ImageButton>();
        foreach (var x in board)
        {
            if (x is ImageButton)
            {
                li.Add((ImageButton)x);
            }
        }
        return li;
    }
}