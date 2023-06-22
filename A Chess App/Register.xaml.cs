namespace A_Chess_App;

public partial class Register : ContentPage
{
	public Register()
	{
		InitializeComponent();
	}

    private void OnRegisterClicked(object sender, EventArgs e)
    {
        string username = txtUsername.Text;
        string password = txtPassword.Text;
        string passagain = txtPasswordAgain.Text;

        if (username.Equals("") || password.Equals("") || passagain.Equals("")) //Register fails if no username and or password ahs been entered
        {
            txtPassword.Background = Colors.Red;
            txtPasswordAgain.Background = Colors.Red;
            txtUsername.Background = Colors.Red;
            lblAdvice.Text = "Fill out everytextbox!";
        }
        if (!password.Equals(passagain) || password.Length < 8) //Register fails if the password length is below 8
        {
            txtPassword.Background = Colors.Red;
            txtPasswordAgain.Background = Colors.Red;
            lblAdvice.Text = "Check your given passwords!";
        } 
        if (SQLCommunication.LoginUser(username, password, true) == 0) //Register fails if the username is already used
        { 
            txtUsername.Background = Colors.Red;
            lblAdvice.Text = "Username is already taken!";
        }
        else //Creates the user and re3directs them to the chessboard
        {
            SQLCommunication.CreateUser(username, password);
            App.Current.MainPage = new NavigationPage(new Chessboard());
        }
    }

    private void OnUsernameChanged(object sender, EventArgs e) //If the username is changed, reset the textbox color
    {
        txtUsername.Background = Colors.White;
    }

    private void OnPasswordChanged(object sender, EventArgs e) //If the username is changed, reset the textbox color
    {
        txtPassword.Background = Colors.White;
    }

    private void OnPasswordAgainChanged(object sender, EventArgs e) //If the username is changed, reset the textbox color
    {
        txtPasswordAgain.Background = Colors.White;
    }
}