

namespace A_Chess_App;

public partial class MainPage : ContentPage
{

	public MainPage()
	{
		InitializeComponent();
	}

	private void OnLoginClicked(object sender, EventArgs e)
	{
		string username = txtUsername.Text;
		string password = txtPassword.Text;

		if (username.Equals("") || password.Equals("")) //Login fails if textboxes are empty
		{
			lblAdvice.Text = "Fill out every textbox!";
            txtUsername.Background = Colors.Red;
            txtPassword.Background = Colors.Red;
        } else if (SQLCommunication.LoginUser(username, password, false) != 1) //Login faills too if the user doesn't already exist
		{
			lblAdvice.Text = "Your Login information is incorrect!";
            txtUsername.Background = Colors.Red;
			txtPassword.Background = Colors.Red;
        } else //If login success, redirect to main page
		{
            App.Current.MainPage = new NavigationPage(new Chessboard());
        }
	}

    private void OnRegisterClicked(object sender, EventArgs e) //When the user wants to make an account redirect to register
    {
        App.Current.MainPage = new NavigationPage(new Register());
    }

	private void OnPasswordChanged(object sender, EventArgs e) //If the textboxes are changed the color of them gets reset
    {
		txtPassword.Background = Colors.White;
	}

	private void OnUsernameChanged(object sender, EventArgs e) //If the textboxes are changed the color of them gets reset
    {
		txtUsername.Background = Colors.White;
	}

}

