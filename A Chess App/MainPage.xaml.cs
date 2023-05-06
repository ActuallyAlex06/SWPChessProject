

namespace A_Chess_App;

public partial class MainPage : ContentPage
{

	public MainPage()
	{
		InitializeComponent();
	}

	private void OnLoginClicked(object sender, EventArgs e)
	{
		string username = Username.Text;
		string password = Password.Text;

		if (username.Equals("") || password.Equals(""))
		{
			lbladvice.Text = "Fill out every textbox!";
            Username.Background = Colors.Red;
            Password.Background = Colors.Red;
        } else if (SQLCommunication.LoginUser(username, password, false) != 1)
		{
			lbladvice.Text = "Your Login information is incorrect!";
            Username.Background = Colors.Red;
			Password.Background = Colors.Red;
        } else
		{
			//DoLogin();
		}
	}

    private void OnRegisterClicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new Register());
    }

	private void OnPasswordChanged(object sender, EventArgs e)
	{
		Password.Background = Colors.White;
	}

	private void OnUsernameChanged(object sender, EventArgs e)
	{
		Username.Background = Colors.White;
	}
}

