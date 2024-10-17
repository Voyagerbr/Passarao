namespace passarin;

public partial class GamePage : ContentPage
{

	double HeightWindow = 0;
	double WidthtWindow = 0;
	const int Velocity = 15;
	const int Gravity = 5;


	const int TimeBeteweenFrames = 25;

	bool IsDied = true;
	const int JumpForce = 35;
	const int maxJumpTime = 5;
	bool IsJumping = false;
	int JumpTime = 0;
	const int minOpening = 100;
	int Score = 0;




	// Ai


	public GamePage()
	{
		InitializeComponent();
	}

	bool VerifyColision()
	{
		if (!IsDied)
		{
			if (VerifyColisionC() || VerifyColisionT())
			{
				return true;
			}
		}
		return false;
	}
	bool VerifyColisionT()
	{
		var minY = -HeightWindow / 2;
		if (Passaro.TranslationY <= minY)
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	bool VerifyColisionC()
	{
		var maxY = HeightWindow / 2 - Chao.HeightRequest - 50;
		if (Passaro.TranslationY >= maxY)
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	void ApplyGravity()
	{
		Passaro.TranslationY += Gravity;

	}
	void ApplyJump()
	{
		Passaro.TranslationY -= JumpForce;

		JumpTime++;
		if (JumpTime >= maxJumpTime)
		{
			IsJumping = false;
			JumpTime = 0;
		}
	}

	public async void Desenha()
	{
		Passaro.Source = "corvo.png";


		Score = 0;
		while (!IsDied)
		{
			if (IsJumping)
			{
				ApplyJump();
			}
			else
			{
				ApplyGravity();
			}
			ApplyGravity();
			ManageTower();

			if (VerifyColision())
			{
				IsDied = true;
				Passaro.Source = "kabum.png";
				LabelGameOver.Text = $"Você passou por \n{Score} \nprédios";
				ScoreLabel.Text = "Prédios : " + Score.ToString("D3");
				GameOverFrame.IsVisible = true;
				Inicializar();
				break;
			}
			await Task.Delay(TimeBeteweenFrames);
		}
	}
	protected override void OnSizeAllocated(double width, double height)
	{
		base.OnSizeAllocated(width, height);
		WidthtWindow = width;
		HeightWindow = height;
	}

	void ManageTower()
	{
		PredioBaixo.TranslationX -= Velocity;
		PredioCima.TranslationX -= Velocity;


		if (PredioBaixo.TranslationX < -WidthtWindow)
		{
			var aumenta = Velocity;
			PredioBaixo.TranslationX = 0;
			PredioCima.TranslationX = 0;
			var MaxHeight = 0;
			var MinHeight = -PredioBaixo.HeightRequest;
			PredioCima.TranslationY = Random.Shared.Next((int)MinHeight, (int)MaxHeight);
			PredioBaixo.TranslationY = PredioCima.TranslationY + minOpening + PredioBaixo.HeightRequest;
			Score++;
			ScoreLabel.Text = "Prédios : " + Score.ToString("D3");
			aumenta++;

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
		PredioBaixo.TranslationX = 0;
		PredioCima.TranslationX = 0;
	}

	void Ui(Object s, TappedEventArgs e)
	{
		IsJumping = true;
	}
}
