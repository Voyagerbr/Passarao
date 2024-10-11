namespace passarin;

public partial class GamePage : ContentPage
{

	double HeightWindow = 0;
	double WidthtWindow = 0;
	const int Velocity = 7;
	const int GravityX = 3;
	const int GravityY = 3;

	const int TimeBeteweenFrames = 25;

	bool IsDied = true;
	//fundo.TranslationX = 0;
	//fundo.TranslationY = 0;


	// Ai


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
			ManageTower();
			await Task.Delay(TimeBeteweenFrames);
		}
	}
    protected override void OnSizeAllocated(double width, double height)
    {
        base.OnSizeAllocated(width, height);
		WidthtWindow = width;
		HeightWindow = height;
    }

	void ManageTower ()
	{
		PredioBaixo.TranslationX -= Velocity;
		PredioCima.TranslationX -= Velocity;
		
		if (PredioBaixo.TranslationX <- WidthtWindow)
		{
			PredioBaixo.TranslationX = 0;
			PredioCima.TranslationX = 0;
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
