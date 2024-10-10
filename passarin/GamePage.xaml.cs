namespace passarin;

public partial class GamePage : ContentPage
{

	const int GravityX = 3;
	const int GravityY = 3;

	const int TimeBeteweenFrames = 25;

	bool IsDied = true;
	//fundo.TranslationX = 0;
	//fundo.TranslationY = 0;

	public GamePage()
	{
		InitializeComponent();
	}

	void ApplyGravity()
	{
		Passaro.TranslationY += GravityY;
		//Passaro.TranslationX += GravityX;
	}

	public async void Desenha()
	{
		while (!IsDied)
		{
			ApplyGravity();
			await Task.Delay(TimeBeteweenFrames);
		}
	}

	

	void Ai(Object s, TappedEventArgs e)
	{
		GameOverFrame.IsVisible = false;
		IsDied = false;
		Inicializar();
		Desenha();
	}

	void Inicializar()
	{
		Passaro.TranslationY = 0;
	}

}
