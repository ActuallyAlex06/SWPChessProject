namespace A_Chess_App;

public partial class Register : ContentPage
{
	public Register()
	{
		InitializeComponent();
	}

    private void OnRegisterClicked(object sender, EventArgs e)
    {
        string username = Username.Text;
        string password = Password.Text;
        string passagain = PasswordAgain.Text;

        if (username.Equals("") || password.Equals("") || passagain.Equals(""))
        {
            Password.Background = Colors.Red;
            PasswordAgain.Background = Colors.Red;
            Username.Background = Colors.Red;
            lbladvice.Text = "Fill out everytextbox!";
        }
        if (!password.Equals(passagain) || password.Length < 8)
        {
            Password.Background = Colors.Red;
            PasswordAgain.Background = Colors.Red;
            lbladvice.Text = "Check your given passwords!";
        } 
        if (SQLCommunication.LoginUser(username, password, true) == 0) {
            Username.Background = Colors.Red;
            lbladvice.Text = "Username is already taken!";
        }
        else
        {
            SQLCommunication.CreateUser(username, password);
            App.Current.MainPage = new NavigationPage(new Chessboard());
        }
    }

    private void OnUsernameChanged(object sender, EventArgs e)
    {
        Username.Background = Colors.White;
    }

    private void OnPasswordChanged(object sender, EventArgs e)
    {
        Password.Background = Colors.White;
    }

    private void OnPasswordAgainChanged(object sender, EventArgs e)
    {
        PasswordAgain.Background = Colors.White;
    }
}